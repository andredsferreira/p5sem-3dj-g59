# US 6.2.7 - Edit a Patient Profile

As an **Admin**, I want to edit a patient profile, so that I can update their information when needed.

## 1. Context

This **US** is the *Frontend* version of [**US 5.1.9**](../../sprint-a/us9/readme.md).

## 2. Requirements

### 2.1. Acceptance Criteria

1. Admins can **search for a patient profile** to edit.
2. Editable fields include name, contact information, medical history, and allergies.
3. Changes to sensitive data (e.g., contact information) trigger an email notification to the patient.
4. The system logs all profile changes for auditing purposes.

### 2.2. Dependencies

This **US** depends on:
* [**US 5.1.9**](../../sprint-a/us9/readme.md), since this functionality calls the *Web API* request to edit a *Patient Profile*.
* [**US 6.2.9**](../6-2-9/readme.md), since the user has to list *Patient Profiles* in order to select one to edit.

### 2.3. Pre-Conditions

For this **US** to work, there needs to be a **Patient Profile** inside the system ([**US 5.1.8**](../../sprint-a/us8/readme.md)).

### 2.4. Open Questions

This **US** has no **Open Questions** yet.

## 3. Analysis

This *US* is merely a *Frontend version* of another **US**, which contains the logic. Thus, this section does not apply here.

## 4. Design

The team decided the following aspects:
* The edit button shouldn't appear until the user has clicked on a specific *Patient Profile* from the list.
* The user should be able to pick what attributes they want to edit.
    * If they pick an attribute, its text box will unlock, allowing the user to enter the value they desire.
    * The 'Allergies' attribute should be a list of strings. Thus, it should have a small button to add an allergy and a button to remove an allergy.
* After confirmation, the system should create a pop-up telling the user if the *Patient Profile* was successfully edited or not.

## 5. C4 Views

-

## 6. Tests

-

## 7. Implementation

-

## 8. Demonstration

-