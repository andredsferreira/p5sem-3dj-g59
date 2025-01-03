# US 7.2.6 (Medical Condition) - As a Doctor, I want to update the Patient Medical Record, namely respecting Medical Conditions.

As a **Doctor**, I want to add an entry about **medical condition** in the medical record of a patient.

## 1. Context

This **US** is part of the new **Express Backend** module. It also has a **Frontend** component.

## 2. Requirements

### 2.1. Acceptance Criteria

1. A doctor must be able to update a **Patient's Medical Record**, adding entries about **medical condition**.

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

Through the requisites and open questions, the team concludes that:
* A doctor must be able to add **medical condition** entries in a patient's **medical record**.
* An entry must contain:
    * Condition that must exist.
    * Year diagnosticated.
* The team will need to create a new type in the backend representing the **Medical Condition Entry** and add a list of these to the **Medical Record** type.

## 4. Design

The team decided that:
* After creation, the new entry will be registered and visible in the frontend.

