# US 6 - Log in the system as a Backoffice user

As a (non-authenticated) Backoffice User, I want to log in to the system using my
credentials, so that I can access the backoffice features according to my assigned
role.

## 1. Context

This US is part of **Sprint A**, as part of the **Group of User Stories regarding backoffice users**.

## 2. Requirements

### 2.1. Acceptance Criteria

Acceptance Criteria:

* Backoffice users log in using their username and password.

- Role-based access control ensures that users only have access to features appropriate to their
  role (e.g., doctors can manage appointments, admins can manage users and settings).
- After five failed login attempts, the user account is temporarily locked, and a notification is
  sent to the admin.
- Login sessions expire after a period of inactivity to ensure security.
- *The password and username rules should be applied*

### 2.2. Dependencies

- 5.1.1 "As an Admin, I want to register new backoffice users (e.g., doctors, nurses, technicians, admins) via an out-of-band process, so that they can access the backoffice system with appropriate permissions".**Must be implemented so that users (such as administrators or technicians) can log in.**

### 2.3. Pre-Conditions

This **US** has no pre-conditions.

### 2.4. Open Questions

This **US** has no open questions for now.

### 2.5 Answers and clarifications from the Client

* Q:
* A:
