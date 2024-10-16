# US 1 - Create a new Backoffice User

As an **Admin**, I want to register new backoffice users (e.g., doctors, nurses,
technicians, admins) via an out-of-band process, so that they can access the
backoffice system with appropriate permissions.

## 1. Context

This US is part of **Sprint A**, as part of the **USs regarding User Management**.

## 2. Requirements

### 2.1. Acceptance Criteria

1. Backoffice users (e.g., doctors, nurses, technicians) are **registered by an Admin** via an **internal process**, not via self-registration.
2. Admin **assigns roles** (e.g., Doctor, Nurse, Technician) during the registration process.
3. Registered users receive a **one-time setup link via email** to **set their password** and **activate their account**.
4. The system enforces **strong password requirements** for security.
5. A **confirmation email** is sent to verify the **user’s registration**.

### 2.2. Dependencies

This **US** has no dependencies.

### 2.3. Pre-Conditions

This **US** has no pre-conditions.

### 2.4. Open Questions

* **Question 1:** Can you please clarify if backoffice users registration uses the IAM system? And if the IAM system is the out-of-band process?
    * **Answer 1:** what this means is that backoffice users can not self-register in the system like the patients do. the admin must register the backoffice user. If you are using an external IAM (e.g., Google, Azzure, Linkedin, ...) the backoffice user must first create their account in the IAM provider and then pass the credential info to the admin so that the user account in the system is "linked" wit the external identity provider.

* **Question 2:** What does the client define as an out-of-band process?
    * **Answer 2:** this applies mainly to the use of an external IAM module. it means the creation of the account is done at the IAM, thus is done outside of the system we are building.