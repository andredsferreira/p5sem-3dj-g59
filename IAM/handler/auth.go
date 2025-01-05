package handler

import (
	"encoding/json"
	"fmt"
	"iam/model"
	"iam/service"
	"net/http"
)

func LoginHandler(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Access-Control-Allow-Origin", "http://localhost:4200")
	w.Header().Set("Access-Control-Allow-Methods", "POST, OPTIONS")
	w.Header().Set("Access-Control-Allow-Headers", "Content-Type")

	if r.Method == http.MethodOptions {
		w.WriteHeader(http.StatusOK)
		return
	}

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
	t, err := service.GenerateJWT(u.Username, u.Role, u.Email)
	if err != nil {
		http.Error(w, err.Error(), http.StatusInternalServerError)
		return
	}
	w.WriteHeader(http.StatusOK)
	json.NewEncoder(w).Encode(map[string]string{"token": t})
}

func RegisterBackofficeHandler(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Access-Control-Allow-Origin", "http://localhost:4200")
	w.Header().Set("Access-Control-Allow-Methods", "POST, OPTIONS")
	w.Header().Set("Access-Control-Allow-Headers", "Content-Type")
	if r.Method == http.MethodOptions {
		w.WriteHeader(http.StatusOK)
		return
	}
	var u model.User
	err := json.NewDecoder(r.Body).Decode(&u)
	if !service.IsValidRole(u.Role) {
		http.Error(w, "invalid user role", http.StatusBadRequest)
		return
	}
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
	fmt.Fprintf(w, "successfully registered user: %s", u.Username)
}

func RegisterPatientHandler(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Access-Control-Allow-Origin", "http://localhost:4200")
	w.Header().Set("Access-Control-Allow-Methods", "POST, OPTIONS")
	w.Header().Set("Access-Control-Allow-Headers", "Content-Type")
	if r.Method == http.MethodOptions {
		w.WriteHeader(http.StatusOK)
		return
	}
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
	if patient.Email == "" {
		http.Error(w, "provide an email", http.StatusBadRequest)
		return
	}
	err = service.CheckPatientRegisteredByAdmin(patient.Email)
	if err != nil {
		http.Error(w, err.Error(), http.StatusBadRequest)
		return
	}
	if service.CheckAlreadyExistingUser(patient.Username) {
		http.Error(w, "username in use", http.StatusBadRequest)
		return
	}
	if !model.ValidateUser(patient.Username, patient.Password, patient.Email) {
		http.Error(w, "invalid user fields", http.StatusBadRequest)
		return
	}
	hash, err := service.HashPassword(patient.Password)
	if err != nil {
		http.Error(w, err.Error(), http.StatusInternalServerError)
		return
	}
	err = model.AddUser(patient.Username, hash, patient.Email,
		patient.Phone, string(model.Patient))
	if err != nil {
		http.Error(w, err.Error(), http.StatusInternalServerError)
		return
	}
	w.WriteHeader(http.StatusOK)
	fmt.Fprintf(w, "successfully registered user: %s", patient.Username)
}
