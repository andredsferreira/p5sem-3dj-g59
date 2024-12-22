# List Family History entries in Medical Record

As a **Doctor**, I want to list/search entries about **family history** in the medical record of a patient (Must contain filters).

## 1. Context

This **functionality** is part of the new **Express Backend** module. It also has a **Frontend** component.

**Note:** This **functionality** was created by the team in order to facilitate the other functionalities and to comply with **ARQSI**'s requisites.

## 2. Requirements

### 2.1. Acceptance Criteria

1. A doctor must be able to list all **family history entries** in a **Patient's Medical Record**.
2. This functionality must also allow the user to filter the results.

### 2.2. Dependencies

This **US** depends on:
* [**US 7.2.1**](../7-2-1/readme.md), since this functionality is done in the **Express Backend**.

### 2.3. Pre-Conditions

* A **Patient** must exist.
* That **Patient's Medical Record** must contain **Family History Entries**.

### 2.4. Open Questions

No open questions

## 3. Analysis

Through our requisites, the team concludes that:
* A doctor must be able to list all **family history entries** in a patient's **medical record**.
* A doctor must be able to search for specific **family history entries** in a patient's **medical record**.
* A doctor must be able to, after listing/searching, filter the results.

## 4. Design

The team decided that:
* The patient's **medical record** will contain a tab for **family history** entries. That tab will contain the options mentioned in the **Analysis** section.

## 5. C4 Views

-

## 6. Tests

* Test if items are listed correctly.
* Test if no items are displayed in case of none satisfying the search conditions.

## 7. Implementation

-

## 8. Demonstration

-