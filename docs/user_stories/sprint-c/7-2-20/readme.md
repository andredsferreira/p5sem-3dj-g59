# US 7.2.20 - Add Family History entry in Medical Record

As a **Doctor**, I want to add an entry about **family history** in the medical record of a patient.

## 1. Context

This **US** is part of the new **Express Backend** module. It also has a **Frontend** component.

## 2. Requirements

### 2.1. Acceptance Criteria

1. A doctor must be able to update a **Patient's Medical Record**, adding entries about **family history**.

### 2.2. Dependencies

This **US** depends on:
* [**US 7.2.1**](../7-2-1/readme.md), since this functionality is done in the **Express Backend**.

### 2.3. Pre-Conditions

* A **Patient** must exist.

### 2.4. Open Questions

**Question 1:** O medical record pode incluir várias alergies e medical conditions. Estas informações são suficientes ou considera necessário um campo de texto livre?

**Answer 1:** 
sim. pode incluir o registo de várias alergias e conditions.

um exemplo de um registo médico é o seguinte:

(...)

**Histórico Médico Familiar**
* Pai: Hipertensão arterial, Diabetes Tipo 2.
* Mãe: Asma, Osteoporose.
* Irmãos: Sem histórico médico significativo.

(...)

(Only the part that's relevant to this **US**).

## 3. Analysis

Through the requisites and open questions, the team concludes that:
* A doctor must be able to add **family history** entries in a patient's **medical record**.
* An entry must contain:
    * Relative
    * Free text, in order to represent the various possibilities in this section.
* The team will need to create a new type in the backend representing the **Family history Entry** and add a list of these to the **Medical Record** type.

## 4. Design

-

## 5. C4 Views

-

## 6. Tests

-

## 7. Implementation

-

## 8. Demonstration

-