package main

import (
	"fmt"
	"iam/db"
	"iam/handler"
	"iam/middleware"
	"log"
	"net/http"
)

func init() {
	db.ConnectDatabase()
}

func main() {
	mux := http.NewServeMux()

	mux.HandleFunc("/", func(w http.ResponseWriter, r *http.Request) {
		fmt.Fprint(w, "Hello bro!")
	})

	mux.HandleFunc("POST /auth/login", handler.LoginHandler)
	mux.HandleFunc("POST /auth/register/backoffice", handler.RegisterBackofficeHandler)
	mux.HandleFunc("POST /auth/register/patient", handler.RegisterPatientHandler)

	server := &http.Server{
		Addr:    ":8090",
		Handler: middleware.LoggerMiddleware(mux),
	}
	fmt.Println("listening on: http://localhost:8090")
	log.Fatal(server.ListenAndServe())
}
