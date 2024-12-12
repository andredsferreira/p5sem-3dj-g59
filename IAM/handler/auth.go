package handler

import (
	"encoding/json"
	"fmt"
	"iam/model"
	"iam/service"
	"net/http"
	"strings"
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
	w.WriteHeader(http.StatusOK)
	json.NewEncoder(w).Encode(map[string]string{"token": t})
}

func RegisterBackofficeHandler(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")
	authHeader := r.Header.Get("Authorization")
	if authHeader == "" {
		http.Error(w, "authorization header is missing", http.StatusUnauthorized)
		return
	}
	var token string
	_, err := fmt.Sscanf(authHeader, "Bearer %s", &token)
	if err != nil || token == "" {
		http.Error(w, "invalid authorization header format", http.StatusUnauthorized)
		return
	}
	role := service.GetRoleFromJWT(token)
	if role != string(model.Admin) {
		http.Error(w, "role is not admin", http.StatusUnauthorized)
		return
	}
	var u model.User
	err = json.NewDecoder(r.Body).Decode(&u)
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
	if patient.Email == "" {
		http.Error(w, "provide an email", http.StatusBadRequest)
		return
	}
	patientEmail := patient.Email
	apiUrl := fmt.Sprintf("http://localhost:5000/api/patient/%s",
		strings.TrimSpace(patientEmail))
	resp, err := http.Get(apiUrl)
	if err != nil {
		http.Error(w, err.Error(), http.StatusInternalServerError)
		return
	}
	defer resp.Body.Close()
	if resp.StatusCode != http.StatusOK {
		http.Error(w, "patient not registered by admin", http.StatusBadRequest)
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
