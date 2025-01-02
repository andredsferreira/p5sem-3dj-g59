package middleware

import (
	"iam/model"
	"iam/service"
	"log"
	"net/http"
	"strings"
	"time"
)

type customResponseWriter struct {
	http.ResponseWriter
	statusCode int
}

func (cw *customResponseWriter) WriteHeader(statusCode int) {
	cw.statusCode = statusCode
	cw.ResponseWriter.WriteHeader(statusCode)
}

func LoggerMiddleware(next http.Handler) http.Handler {
	return http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		cw := &customResponseWriter{
			ResponseWriter: w,
			statusCode:     http.StatusOK,
		}
		s := time.Now()
		next.ServeHTTP(cw, r)
		d := time.Since(s).Seconds()
		log.Printf("HTTP %s %s %d %fs\n", r.Method, r.URL.String(), cw.statusCode, d)
	})
}

func AuthMiddleware(next http.Handler) http.Handler {
	return http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		authHeader := r.Header.Get("Authorization")
		if authHeader == "" {
			http.Error(w, "authorization header missing", http.StatusUnauthorized)
			return
		}
		if !strings.HasPrefix(authHeader, "Bearer ") {
			http.Error(w, "authorization header format must be Bearer {token}", http.StatusUnauthorized)
			return
		}
		token := strings.TrimPrefix(authHeader, "Bearer ")
		err := service.VerifyJWT(token)
		if err != nil {
			http.Error(w, err.Error(), http.StatusUnauthorized)
			return
		}
		next.ServeHTTP(w, r)
	})
}

func AdminOnlyMiddleware(next http.Handler) http.Handler {
	return http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		authHeader := r.Header.Get("Authorization")
		if authHeader == "" {
			http.Error(w, "authorization header missing", http.StatusUnauthorized)
			return
		}
		if !strings.HasPrefix(authHeader, "Bearer ") {
			http.Error(w, "authorization header format must be Bearer {token}", http.StatusUnauthorized)
			return
		}
		token := strings.TrimPrefix(authHeader, "Bearer ")
		err := service.VerifyJWT(token)
		if err != nil {
			http.Error(w, err.Error(), http.StatusUnauthorized)
			return
		}
		role := service.GetRoleFromJWT(token)
		if role != string(model.Admin) {
			http.Error(w, "only admin allowed", http.StatusUnauthorized)
			return
		}
		next.ServeHTTP(w, r)
	})
}
