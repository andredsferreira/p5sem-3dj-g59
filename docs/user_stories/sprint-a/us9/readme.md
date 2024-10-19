# US 9 - Edit a new Patient Profile

As an **Admin**, I want to edit an existing patient profile, so that I can update their information when needed.

## 1. Context

This US is part of **Sprint A**, as part of the **Group of User Stories regarding patient profiles**.

## 2. Requirements

### 2.1. Acceptance Criteria

1. Admins can **search for** and **select** a patient profile to edit.
2. Editable fields include **name**, **contact information**, **medical history**, and **allergies**.
3. Changes to **sensitive data** (e.g., contact information) trigger an **email notification** to the patient..
4. The system **logs all profile changes** for auditing purposes.

### 2.2. Dependencies

This **US** depends on:
* [**US8**](../us8/readme.md) (Explained in **2.3. Pre-Conditions**).
* [**US11**](../us11/readme.md), since, from a usability point of view, this functionality starts from **Patient Profile Search**.


### 2.3. Pre-Conditions

For this **US** to work, there needs to be a **Patient Profile** inside the system, hence the dependency on [**US8**](../us8/readme.md).

### 2.4. Open Questions

* **Question 1:** Regarding the editing of patient information, is contact information the only sensitive data? Is it the only data that triggers an email notification?
    * **Answer 1:** faz parte das vossas responsabilidades no âmbito do módulo de proteçãod e dados e de acordo com a politica que venham a definir

* **Question 2:** In this US an admin can edit a user profile. Does the system display a list of all users or the admin searchs by ID? Or both?
    * **Answer 2:** this requirement is for the editing of the user profile. from a usability point of view, the user should be able to start this feature either by searching for a specific user or listing all users and selecting one.

        note that we are not doing the user interface of the system in this sprint.

* **Question 3:** When one of the contents that administrator edits is a sensitive content (eg. email), the notification is sent for what patient's email, the email in patient account, the old email of patient or the new email of patient?
    * **Answer 3:** if the email is changed, the notification should be sent to the "old" email