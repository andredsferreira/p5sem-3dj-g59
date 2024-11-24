# US 6.2.6 - Delete account as Patient

As an **Patient**, I want to delete my account and all associated data, so that I can exercise my right to be forgotten as per GDPR.

## 1. Context

This **US** is the *Frontend* version of [**US 5.1.5**](../../sprint-a/us5/readme.md).

## 2. Requirements

### 2.1. Acceptance Criteria

1. Patients can request to delete their account through the profile settings.
2. The system sends a confirmation email to the patient before proceeding with account deletion.
3. Upon confirmation, all personal data is permanently deleted from the system within the legally required time frame (e.g., 30 days).
4. Patients are notified once the deletion is complete, and the system logs the action for GDPR compliance.
5. Some anonymized data may be retained for legal or research purposes, but all identifiable information is erased.

### 2.2. Dependencies

This **US** depends on:
* [**US 5.1.5**](../../sprint-a/us5/readme.md), since this functionality calls the *Web API* request to delete the profile.

### 2.3. Pre-Conditions

The user must have an account.

### 2.4. Open Questions

This **US** has no **Open Questions** yet.

## 3. Analysis

This *US* is merely a *Frontend version* of another **US**, which contains the logic. Thus, this section does not apply here.

## 4. Design

-

## 5. Implementation

-

## 6. Demonstration

-