# US 6.2.7 - Create a Patient Profile

As an **Admin**, I want to create a new patient profile, so that I can register their personal details and medical history.

## 1. Context

This **US** is the *Frontend* version of [**US 5.1.8**](../../sprint-a/us8/readme.md).

## 2. Requirements

### 2.1. Acceptance Criteria

1. Admins can input patient details such as **first name**, **last name**, **date of birth**, **contact information** and **medical history**.
2. A unique **patient ID (Medical Record Number)** is generated upon profile creation.
3. The system validates that the patient's email and phone number are unique.
4. The profile is stored securely in the system, and access is governed by role-based permissions.

### 2.2. Dependencies

This **US** depends on:
* [**US 5.1.8**](../../sprint-a/us8/readme.md), since this functionality calls the *Web API* request to create a *Patient Profile*.
* [**US 6.2.9**](../6-2-9/readme.md), since the user has to list *Patient Profiles* in order to select one to edit.

### 2.3. Pre-Conditions

None

### 2.4. Open Questions

This **US** has no **Open Questions** yet.

## 3. Analysis

This *US* is merely a *Frontend version* of another **US**, which contains the logic. Thus, this section does not apply here.

## 4. Design

The team decided that:
* The button to create a **Patient Profile** should be available before and after listing.
* After pressing button to create a **Patient Profile**, a small window should appear with the necessary data fields.
    * That window should enforce valid formats for fields like 'Email' and 'PhoneNumber'.
    * That window should have a "Save" button and a "Cancel" button.
    * After saving, the system should send a message saying:
        * The Patient was created successfully.
        * If it wasn't due to a Backend business rule violation (e.g. Email wasn't unique), the message should explain what business rule was violated.

## 5. C4 Views

-

## 6. Tests

* Test that:
    * The **Patient** was successfully created.
    * The system warns the user about not unique Email.
    * The system enforces valid Email format.
    * The system warns the user about not unique PhoneNumber.
    * The system enforces valid PhoneNumber format.

## 7. Implementation

-

## 8. Demonstration

-