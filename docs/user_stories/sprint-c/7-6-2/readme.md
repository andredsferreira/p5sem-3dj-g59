# US 7.6.2 - Request Data Deletion

As a Patient, I want to request the deletion of my personal data, so that I can exercise my right to be forgotten under GDPR.

## 1. Context

This **US** is part of the **GDPR module**.

## 2. Requirements

### 2.1. Acceptance Criteria

1. Patients can request data deletion through their profile settings.

### 2.2. Dependencies

None

### 2.3. Pre-Conditions

A **Patient** (the actor) must exist.

### 2.4. Open Questions

**Question 1:** Since its a request, does it need to be approved by, for example the admin, or it doesnt need to be approved?

**Answer 1:** this feature is about requesting the data deletion, not about the actual the actual deletion. The company must define their data deletion process. For instance, the DPO will receive these requests by email and will then manually, outside of the application, do the due diligence to check if the data can be deleted or not. if the data can be deleted, a specific order will be sent to the system admin to execute the proper data deletion or anonymization from the database.

## 3. Analysis

Through the requisites, the team concluded:
* A **Patient** must have a way to request their personal data's deletion through their profile settings.

Through the open questions, the team concluded:
* This functionality does not include the actual deletion, just the request.

## 4. Design

The team decided that, to comply with what was asked, a new button would be added to the Patient Profile's menu.

## 5. Demonstration

-