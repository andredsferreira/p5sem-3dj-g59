# US 5 - Delete a Patient's Information

As an **Patient**, I want to delete my account and all associated data, so that I can exercise my right to be forgotten as per GDPR

## 1. Context

This US is part of **Sprint A**, as part of the **Group of User Stories regarding user management**.

## 2. Requirements

### 2.1. Acceptance Criteria

1. Patients can request to delete their account through the profile settings.
2. The system sends a confirmation email to the patient before proceeding with account deletion.
3. Upon confirmation, all personal data is permanently deleted from the system within the legally required time frame (e.g., 30 days).
4. Patients are notified once the deletion is complete, and the system logs the action for GDPR compliance.
5. Some anonymized data may be retained for legal or research purposes, but all identifiable information is erased.

### 2.2. Dependencies

This **US** depends on:
* [**US3**](../us8/readme.md) (Explained in **2.3. Pre-Conditions**).

### 2.3. Pre-Conditions

For this **US** to work, there needs to be a **Patient User** inside the system, hence the dependency on [**US3**](../us3/readme.md).

### 2.4. Open Questions

*No **Open questions***.

## 3. Analysis

When the **Patient** clicks to delete their profile, it's only **marked for deletion**. The profile is only deleted when they press the **confirmation link** sent through email.

As the **Acceptance Criteria** states, every piece of data that mentions the deleted **Patient** must be at least anonymized. Hence, this **US** affects not just the **Patient aggregate**, but also the **Operation Request aggregate** (since an Operation Request is related to a Patient) and any **Logs** that contain personal data about the Patient.

## 4. Design

The **Http** requests shall be received by the existing class **PatientController**, which in turn calls the **PatientService**. It will have two methods:
1. One for marking the initial request of deleting the profile.
2. Other for actually deleting it.

The **PatientService** must also have two methods:
1. One for marking the profile for deletion and sending an **email notification** to the **Patient's Email Address**.
2. Other for actually deleting the profile.

## 5. C4 Views

The **C4 Views** for this *US* can be viewed [here](views/readme.md).

## 6. Tests

-

## 7. Implementation

-

## 8. Demonstration

-