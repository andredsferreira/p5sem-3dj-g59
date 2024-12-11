package model

type Role string

const (
	Admin      Role = "Admin"
	Doctor     Role = "Doctor"
	Nurse      Role = "Nurse"
	Technician Role = "Technician"
	Patient    Role = "Patient"
)
