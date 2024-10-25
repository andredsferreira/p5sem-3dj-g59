# US 2 - Reset Password as a Backoffice User

As a Backoffice User (Admin, Doctor, Nurse, Technician), I want to reset my
password if I forget it, so that I can regain access to the system securely.

## 1. Context

This US is part of **Sprint A**, as part of the **Group of User Stories regarding backoffice user**.

## 2. Requirements

### 2.1. Acceptance Criteria

Acceptance Criteria:

- Backoffice users can request a password reset by providing their email.
- The system sends a password reset link via email.
- The reset link expires after a predefined period (e.g., 24 hours) for security.
- Users must provide a new password that meets the system’s password complexity rules

### 2.2. Dependencies

- 5.1.1 "As an Admin, I want to register new backoffice users (e.g., doctors, nurses, technicians, admins) via an out-of-band process, so that they can access the backoffice system with appropriate permissions".**Must be implemented so that users (such as administrators or technicians) can request a password reset.**

### 2.3. Pre-Conditions

This **US** has no pre-conditions.

### 2.4. Open Questions

This **US** has no open questions for now.

### 2.5 Answers and clarifications from the client

* Q: What are the system's password requirements?
* A: At least 10 characters long, at least a digit, a capital letter and a special character
