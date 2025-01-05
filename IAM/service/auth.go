package service

import (
	"database/sql"
	"errors"
	"fmt"
	"iam/model"
	"net/http"
	"strings"
	"time"

	"github.com/golang-jwt/jwt"
	"golang.org/x/crypto/bcrypt"
)

var HMACSecretKey []byte = []byte("KHPK6Ucf/zjvU4qW8/vkuuGLHeIo0l9ACJiTaAPLKbka")

func HashPassword(p string) (string, error) {
	b, err := bcrypt.GenerateFromPassword([]byte(p), 12)
	if err != nil {
		return "", fmt.Errorf("generating hash from password: %w", err)
	}
	return string(b), nil
}

func CheckPasswordHash(p, h string) bool {
	err := bcrypt.CompareHashAndPassword([]byte(h), []byte(p))
	return err == nil
}

func GenerateJWT(u, r, e string) (string, error) {
	t := jwt.NewWithClaims(jwt.SigningMethodHS256,
		jwt.MapClaims{
			"username": u,
			"role":     r,
			"email":    e,
			"exp":      time.Now().Add(30 * time.Minute).Unix(),
		})
	ts, err := t.SignedString(HMACSecretKey)
	if err != nil {
		return "", fmt.Errorf("signing token string: %w", err)
	}
	return ts, nil
}

func VerifyJWT(ts string) error {
	token, err := jwt.Parse(ts, func(token *jwt.Token) (interface{}, error) {
		return HMACSecretKey, nil
	})
	if err != nil {
		return fmt.Errorf("error parsing jwt: %w", err)
	}
	if !token.Valid {
		return fmt.Errorf("invalid jwt")
	}
	return nil
}

func GetClaimsFromJWT(ts string) (jwt.MapClaims, error) {
	t, err := jwt.Parse(ts, func(t *jwt.Token) (interface{}, error) {
		if _, ok := t.Method.(*jwt.SigningMethodHMAC); !ok {
			return nil, fmt.Errorf("unexpected signing method: %v", t.Header["alg"])
		}
		return HMACSecretKey, nil
	})
	if err != nil {
		return nil, fmt.Errorf("error parsing jwt: %w", err)
	}
	if claims, ok := t.Claims.(jwt.MapClaims); ok && t.Valid {
		return claims, nil
	}
	return nil, fmt.Errorf("invalid jwt")
}

func GetUsernameFromJWT(t string) string {
	claims, err := GetClaimsFromJWT(t)
	if err != nil {
		return ""
	}
	return claims["username"].(string)
}

func GetRoleFromJWT(t string) string {
	claims, err := GetClaimsFromJWT(t)
	if err != nil {
		return ""
	}
	return claims["role"].(string)
}

func CheckAlreadyExistingUser(username string) bool {
	_, err := model.GetUserByUsername(username)
	return err != sql.ErrNoRows
}

func IsValidRole(role string) bool {
	switch model.Role(role) {
	case model.Admin, model.Doctor, model.Nurse, model.Technician, model.Patient:
		return true
	default:
		return false
	}
}

func CheckPatientRegisteredByAdmin(email string) error {
	apiUrl := fmt.Sprintf("http://localhost:5000/api/patient/%s",
		strings.TrimSpace(email))
	resp, err := http.Get(apiUrl)
	if err != nil {
		return errors.New("error requesting backend API")
	}
	defer resp.Body.Close()
	if resp.StatusCode != http.StatusOK {
		return errors.New("patient not registered by admin")
	}
	return nil
}
