# US 6.2.8 - Delete a Patient Profile

As an **Admin**, I want to delete a patient profile, so that I can remove patients who are no longer under care.

## 1. Context

This **US** is the *Frontend* version of [**US 5.1.10**](../../sprint-a/us10/readme.md).

## 2. Requirements

### 2.1. Acceptance Criteria

1. Admins can **search for a patient profile** and mark it for deletion.
2. Before deletion, the system prompts the admin to confirm the action.
3. Once deleted, all patient data is permanently removed from the system within a predefined time frame.
4. The system logs the deletion for audit and GDPR compliance purposes.

### 2.2. Dependencies

This **US** depends on:
* [**US 5.1.10**](../../sprint-a/us10/readme.md), since this functionality calls the *Web API* request to delete a *Patient Profile*.
* [**US 6.2.9**](../6-2-9/readme.md), since the user has to list *Patient Profiles* in order to select one to delete.

### 2.3. Pre-Conditions

For this **US** to work, there needs to be a **Patient Profile** inside the system ([**US 5.1.8**](../../sprint-a/us8/readme.md)).

### 2.4. Open Questions

This **US** has no **Open Questions** yet.

## 3. Analysis

This *US* is merely a *Frontend version* of another **US**, which contains the logic. Thus, this section does not apply here.

## 4. Design

The team decided the following aspects:
* The delete button shouldn't appear until the user has clicked on a specific *Patient Profile* from the list.
* After confirmation, the system should create a pop-up telling the user if the *Patient Profile* was successfully deleted or not.

## 5. C4 Views

-

## 6. Tests

* Test if deletion occurs.
* Test if deletion can't work on a *Patient Profile* that doesn't exist.

## 7. Implementation

-

## 8. Demonstration

-