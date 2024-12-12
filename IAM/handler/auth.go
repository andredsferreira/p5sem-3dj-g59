package handler

import (
	"encoding/json"
	"iam/model"
	"iam/service"
	"net/http"
	"time"
)

func LoginHandler(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")
	var login struct {
		Username string `json:"username"`
		Password string `json:"password"`
	}
	err := json.NewDecoder(r.Body).Decode(&login)
	if err != nil {
		http.Error(w, err.Error(), http.StatusBadRequest)
		return
	}
	u, err := model.GetUserByUsername(login.Username)
	if err != nil {
		http.Error(w, err.Error(), http.StatusNotFound)
		return
	}
	if !service.CheckPasswordHash(login.Password, u.Password) {
		http.Error(w, "wrong password", http.StatusNotAcceptable)
		return
	}
	t, err := service.GenerateJWT(u.Username, u.Role)
	if err != nil {
		http.Error(w, err.Error(), http.StatusInternalServerError)
		return
	}
	http.SetCookie(w, &http.Cookie{
		Name:     "jwt",
		Value:    t,
		Expires:  time.Now().Add(30 * time.Minute),
		HttpOnly: true,
		SameSite: http.SameSiteStrictMode,
	})
	w.WriteHeader(http.StatusOK)
	w.Write([]byte(t))
}

func LogoutHandler(w http.ResponseWriter, r *http.Request) {
	http.SetCookie(w, &http.Cookie{
		Name:     "jwt",
		Value:    "",
		MaxAge:   -1,
		HttpOnly: true,
		SameSite: http.SameSiteStrictMode,
	})
	w.WriteHeader(http.StatusOK)
	w.Write([]byte("successfull logout"))
}

func RegisterBackofficeHandler(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")
	c, err := r.Cookie("jwt")
	if err != nil {
		http.Error(w, err.Error(), http.StatusUnauthorized)
		return
	}
	role := service.GetRoleFromCookie(c)
	if role != string(model.Admin) {
		http.Error(w, "role is not admin", http.StatusUnauthorized)
	}
	var u model.User
	err = json.NewDecoder(r.Body).Decode(&u)
	if err != nil {
		http.Error(w, err.Error(), http.StatusBadRequest)
		return
	}
	hash, err := service.HashPassword(u.Password)
	if err != nil {
		http.Error(w, err.Error(), http.StatusInternalServerError)
		return
	}
	err = model.AddUser(u.Username, hash, u.Email, u.Phone, u.Role)
	if err != nil {
		http.Error(w, err.Error(), http.StatusBadRequest)
		return
	}
	w.WriteHeader(http.StatusOK)
	w.Write([]byte("user successfully registered"))
}

func RegisterPatient(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")
	var patient struct {
		Username string `json:"username"`
		Password string `json:"password"`
		Email    string `json:"email"`
		Phone    string `json:"phone"`
	}
	err := json.NewDecoder(r.Body).Decode(&patient)
	if err != nil {
		http.Error(w, err.Error(), http.StatusBadRequest)
		return
	}
	if service.CheckAlreadyExistingUser(patient.Username) {
		http.Error(w, "username in use", http.StatusBadRequest)
		return
	}
	err = model.AddUser(patient.Username, patient.Password, patient.Email, patient.Phone, string(model.Patient))
	if err != nil {
		http.Error(w, err.Error(), http.StatusInternalServerError)
		return
	}
	w.WriteHeader(http.StatusOK)
	w.Write([]byte("patient successfully registered"))
}
