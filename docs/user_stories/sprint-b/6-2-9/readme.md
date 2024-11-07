# US 6.2.9 - List/Search Patient Profiles

As an **Admin**, I want to list/search patient profiles by different attributes, so that I can view the details, edit, and remove patient profiles.

## 1. Context

This **US** is the *Frontend* version of [**US 5.1.11**](../../sprint-a/us11/readme.md).

## 2. Requirements

### 2.1. Acceptance Criteria

1. Admins can **search patient profiles** by various attributes, including **name**, **email**, **date of birth**, or **medical record number**.
2. The system displays search results in a list view with key patient information (name, email, date of birth).
3. Admins can **select a profile from the list** to view, edit or delete the patient record.
4. The search results are **paginated**, and filters are available to refine the search results.

### 2.2. Dependencies

This **US** depends on:
* [**US 5.1.8**](../../sprint-a/us8/readme.md) (Explained in **2.3. Pre-Conditions**).
* [**US 5.1.11**](../../sprint-a/us11/readme.md), since this functionality calls the *Web API* request to search and filter *Patient Profiles*.

### 2.3. Pre-Conditions

For this **US** to work, there needs to be a **Patient Profile** inside the system, hence the dependency on [**US 5.1.8**](../../sprint-a/us8/readme.md).

### 2.4. Open Questions

This **US** has no **Open Questions** yet.

## 3. Analysis

This *US* is merely a *Frontend version* of another **US**, which contains the logic. Thus, this section does not apply here.

## 4. Design

The team decided the following aspects:
* The user should be able to pick what attributes they want to filter by.
    * If they pick an attribute, its text box will unlock, allowing the user to enter the value they desire.
* If no patients are found with the picked conditions, a message should appear.
* Otherwise, each entry should:
    * Have their **Medical Record Number**, **Full Name**, **Email** and **Date of Birth** shown when listed.
    * After picking one, it should additionally show the other attributes of that **Patient** and show an **Edit button** and a **Delete button**.
* Each page should display **5 Patients**.
* If, when listing *Patient Profiles*, a change (a creation, an edit or a deletion) is made, the list should reload.

## 5. C4 Views

-

## 6. Tests

* Test if filter works as intended.
* Test if an empty list is presented with a message.
* Test if page is reloaded when a change is made.

## 7. Implementation

-

## 8. Demonstration

-