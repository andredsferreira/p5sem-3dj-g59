# US 7.2.7 (Medical Condition) - As a Doctor, I want to search for entries in the Patient Medical Record, namely respecting Medical Conditions.

As a **Doctor**, I want to list/search entries about  **medical condition** in the medical record of a patient (Must contain filters).

## 1. Context

This **US** is part of the new **Express Backend** module. It also has a **Frontend** component.

## 2. Requirements

### 2.1. Acceptance Criteria

1. A doctor must be able to list all **medical condition entries** in a **Patient's Medical Record**.
2. This functionality must also allow the user to filter the results.

### 2.2. Dependencies

This **US** depends on:
* [**US 7.2.1**](../7-2-1/readme.md), since this functionality is done in the **Express Backend**.

### 2.3. Pre-Conditions

* A **Patient Medical Record** must exist.
* A **Medical Condition** must exist.

### 2.4. Open Questions

**Question 1:** O medical record pode incluir várias alergies e medical conditions. Estas informações são suficientes ou considera necessário um campo de texto livre?

**Answer 1:** 
sim. pode incluir o registo de várias alergias e conditions.

um exemplo de um registo médico é o seguinte:

(...)

**Condições Médicas**
* Asma (diagnosticada em 2005)
* Hipertensão Arterial (diagnosticada em 2018)
* Rinite Alérgica Sazonal (diagnosticada em 2010)
* Gastrite (diagnosticada em 2021)

(...)

(Only the part that's relevant to this **US**).

## 3. Analysis

Through our requisites, the team concludes that:
* A doctor must be able to list all **medical condition entries** in a patient's **medical record**.
* A doctor must be able to search for specific **medical condition entries** in a patient's **medical record**.
* A doctor must be able to, after listing/searching, filter the results.

