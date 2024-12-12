package model

import (
	"database/sql"
	"iam/db"
	"regexp"
)

type User struct {
	Username string `json:"username"`
	Password string `json:"password"`
	Email    string `json:"email"`
	Phone    string `json:"phone"`
	Role     string `json:"role"`
}


func GetAllUsers() ([]User, error) {
	var users []User
	query := `
		SELECT username, password, email, phone
		FROM users
	`
	rows, err := db.MySql.Query(query)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	for rows.Next() {
		var u User
		if err := rows.Scan(&u.Username, &u.Password, &u.Email, &u.Phone); err != nil {
			return nil, err
		}
		users = append(users, u)
	}
	return users, nil
}

func GetUserByUsername(username string) (User, error) {
	var u User
	query := `
		SELECT username, password, email, phone, role
		FROM users
		WHERE username = ?
	`
	row := db.MySql.QueryRow(query, username)
	if err := row.Scan(&u.Username, &u.Password, &u.Email, &u.Phone, &u.Role); err != nil {
		if err == sql.ErrNoRows {
			return u, err
		}
		return u, err
	}
	return u, nil
}

func AddUser(username, password, email, phone, role string) error {
	query := `
        INSERT INTO users (username, password, email, phone, role) 
        VALUES (?, ?, ?, ?, ?)
    `
	_, err := db.MySql.Exec(query, username, password, email, phone, role)
	if err != nil {
		return err
	}
	return nil
}

func ValidateUser(username, password, email string) bool {
	emailRegex := `^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$`
	isValidEmail := regexp.MustCompile(emailRegex).MatchString(email)
	if len(username) >= 3 && len(password) >= 3 && isValidEmail {
		return true
	}
	return false
}
