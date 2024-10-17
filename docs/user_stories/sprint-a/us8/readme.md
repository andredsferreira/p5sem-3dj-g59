# US 8 - Create a new Patient Profile

As an **Admin**, I want to create a new patient profile, so that I can register their personal details and medical history.

## 1. Context

This US is part of **Sprint A**, as part of the **Group of User Stories regarding patient profiles**.

## 2. Requirements

### 2.1. Acceptance Criteria

1. Admins can input patient details such as **first name**, **last name**, **date of birth**, **contact information**, and **medical history**.
2. A **unique patient ID (Medical Record Number)** is generated upon profile creation.
3. The system validates that **the patient's email and phone number are unique**.
4. The profile is stored securely in the system, and **access is governed by role-based permissions**.

### 2.2. Dependencies

This **US** has no dependencies.

### 2.3. Pre-Conditions

This **US** has no pre-conditions.

### 2.4. Open Questions

* **Question 1:** When an Admin creates a patient profile, should he already register them in the system, as users that can login, or should the registration always be responsibility of the patient?
If the latter is intended, should the patient's user credentials be linked to the existing profile?
    * **Answer 1:** registering a patient record is a separate action from the patient self-registering as a user.
    * **Question 1.5:** How does the activation happen? If that pacient eventualy wants to register himself, should there be an option to activate an existing profile? For example, associate the e-mail from registration input with the existing profile's e-mail?
    * **Answer 1.5:** the admin register the patient (this does not create a user for that patient)
optionally, the patient self-registers in the system by providing the same email that is currently recorded in their patient record and the system associates the user and the patient
there is no option for someone who is not a patient of the system to register as a user

* **Question 2:** It is specified that the admin can input some of the patient's information (name, date of birth, contact information, and medical history).

    Do they also input the omitted information (gender, emergency contact and allergies/medical condition)?
Additionally, does the medical history that the admin inputs refer to the patient's medical record, or is it referring to the appointment history?
    * **Answer 2:** the admin can not input medical history nor allergies. they can however input gender and emergency contact.