# 6.2.21 - List/Search Operation Types

As an Admin, I want to list/search operation types, so that I can see the details, edit, and remove operation types.

## 1. Context

This US is the *Frontend* version of [**US 5.1.23**]

## 2. Requirements

### 2.1. Acceptance Criteria

1. Admins can **search for and list all operation types**.
2. The system displays operation types in a searchable list with attributes such as name, required staff, and estimated duration.
3. Admins can **edit and remove operation types** from the list.

### 2.2. Dependencies

This **US** depends on:
* [**US 5.1.23**] since this functionality calls the *Web API* request to search and edit **Operation Types**.
* [**US 5.1.21**] since this functionality calls the *Web API* request to edit **Operation Types**.
* [**US 5.1.20**] since this functionality calls there needs to be  **Operation Types**.

### 2.3. Pre-Conditions

For this **US** to work, there needs to be **Operation Types** inside the system, hence the dependency on [**US 5.1.20**].

### 2.4. Open Questions

This **US** has no **Open Questions** yet.

## 3. Analysis

This *US* is the *Frontend version* of another **US**, which contains the logic. Thus, this section does not apply here.

## 4. Design

The team decided the following aspects:


The team decided the following aspects:
* The user should be able to pick what attributes they want to filter by.
    * If they pick an attribute, its text box will unlock, allowing the user to enter the value they desire.
* If no Operation Types are found with the picked conditions, a message should appear.
* Otherwise, each entry should:
    * Have their main details (name, id, status and Specialization) listed.
    * After picking one, it should additionally show the other attributes of that **OperationType** and show an **Edit button** and a **Delete button**.
* If, when listing *Operation Types*, a change (a creation, an edit or a deletion) is made, the list should reload.

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
