# US 7.2.19 (Medical Condition) - As a Doctor, I want to edit the Patient Medical Record, namely respecting Medical Conditions.

As a **Doctor**, I want to edit an entry about **medical condition** in the medical record of a patient.

## 1. Context

This **US** is part of the new **Express Backend** module. It also has a **Frontend** component.

## 2. Requirements

### 2.1. Acceptance Criteria

1. A doctor must be able to update a **Patient's Medical Record**, adding entries about **medical condition**.

### 2.2. Dependencies

This **US** depends on:
* [**US 7.2.1**](../7-2-1/readme.md), since this functionality is done in the **Express Backend**.

### 2.3. Pre-Conditions

* A **Patient** must exist.
    * That **Patient's Medical Record** must contain a **Medical Condition Entry**.

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
*hrough the requisites and open questions, the team concludes that:
* A doctor must be able to edit a **medical condition** entry in a patient's **medical record**.
* The doctor should be able to edit the **year** associated with it and its **condition**.

## 4. Design

The team decided that:
* After accessing a patient's medical record, each **medical condition entry** should have an "Edit" button. That allows the user to edit the previously mentioned attributes (USABILITY).

## 5. C4 Views

The **C4 Views** for this *US* can be viewed [here](views/readme.md).

## 6. Tests



## 7. Implementation

### 7.1. Backend section

The only part that's important to point out is the **service**'s method:

```ts
    public async editMedicalConditionEntry(entryNumber: string, dto: IMedicalConditionEntryOptionalDTO): Promise<Result<IMedicalConditionEntryDTO>> {
        try {
            const entry = await this.medicalConditionEntryRepo.findByEntryNumber(entryNumber);
            if (entry === null) return Result.fail<IMedicalConditionEntryDTO>("medicalConditionEntry not found");

            if (dto.condition) {
                const condition = await this.medConditionRepo.findByCondition(dto.condition);
                if (condition === null) return Result.fail<IMedicalConditionEntryDTO>("condition not found");
                entry.condition = dto.condition;
            }
            if (dto.year) entry.year = dto.year;

            await this.medicalConditionEntryRepo.save(entry);

            const medicalConditionEntryResultDTO = MedicalConditionEntryMap.toDTO(entry) as IMedicalConditionEntryDTO
            return Result.ok<IMedicalConditionEntryDTO>(medicalConditionEntryResultDTO)
        } catch (err) {
            throw err
        }
    }
```

The backend request for this functionality is a HTTP Patch.

## 8. Demonstration

![](demonstration/creation.png)