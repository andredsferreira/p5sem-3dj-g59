package model

type Role string

const (
	Admin      Role = "admin"
	Doctor     Role = "doctor"
	Nurse      Role = "nurse"
	Technician Role = "technician"
	Patient    Role = "patient"
)
