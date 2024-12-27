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

The **C4 Views** for this *US* can be viewed [here](views/readme.md).

## 6. Tests

* Test if items are listed correctly.

```ts
  mocha.it('search get values', async function() {
    // Arrange
    this.timeout(timeout);

    const entry = seedFamilyEntry();

    let medicalRecordRepoInstance:IMedicalRecordRepo = Container.get("MedicalRecordRepo");

    let familyHistoryRepoInstance:IFamilyHistoryEntryRepo = Container.get("FamilyHistoryRepo");
    sinon.stub(familyHistoryRepoInstance, "search").returns(Promise.resolve([entry]));

    // Act
    const serv = new FamilyHistoryEntryService(familyHistoryRepoInstance, medicalRecordRepoInstance);
    const output = (await serv.search(entry.medicalRecordNumber, seedHistoryChange())).getValue();
  
    // Assert
    expect(serv).to.not.be.undefined;
    expect(output).to.deep.include(FamilyHistoryEntryMap.toDTO(entry));
  });
```

* Test if no items are displayed in case of none satisfying the search conditions.

```ts
  mocha.it('search doesnt get values', async function() {
    // Arrange
    this.timeout(timeout);

    const entry = seedFamilyEntry();

    let medicalRecordRepoInstance:IMedicalRecordRepo = Container.get("MedicalRecordRepo");

    let familyHistoryRepoInstance:IFamilyHistoryEntryRepo = Container.get("FamilyHistoryRepo");
    sinon.stub(familyHistoryRepoInstance, "search").returns(Promise.resolve([]));

    // Act
    const serv = new FamilyHistoryEntryService(familyHistoryRepoInstance, medicalRecordRepoInstance);
    const output = (await serv.search(entry.medicalRecordNumber, seedHistoryChange())).errorValue();
  
    // Assert
    expect(serv).to.not.be.undefined;
    expect(output.toString()).to.be.eq("familyHistoryEntry not found");
  });
```

## 7. Implementation

* **familyHistoryEntryService.ts**

```ts
    public async search(medicalRecordNumber: string, dto: IFamilyHistoryEntryOptionalDTO): Promise<Result<IFamilyHistoryEntryDTO[]>> {
        try {
            const familyHistoryEntry = await this.familyHistoryEntryRepo.search(medicalRecordNumber, dto.relative, dto.history);
            if (familyHistoryEntry === null || familyHistoryEntry.length == 0) {
                return Result.fail<IFamilyHistoryEntryDTO[]>("familyHistoryEntry not found")
            }

            var familyHistoryEntryResultDTO = [] as IFamilyHistoryEntryDTO[];
            familyHistoryEntry.forEach(element => {
                familyHistoryEntryResultDTO.push(FamilyHistoryEntryMap.toDTO(element));
            });
            return Result.ok<IFamilyHistoryEntryDTO[]>(familyHistoryEntryResultDTO)
        } catch (err) {
            throw err
        }
    }
```

If there are no entries that match the desired filters, this method returns a Result.fail.

* **familyHistoryEntryRepo.ts**

```ts
    public async search(medicalRecordNumber: string, relative?: string, history?: string): Promise<FamilyHistoryEntry[]> {
        const query: any = { medicalRecordNumber: medicalRecordNumber }
        if (relative) query.relative = relative;
        if (history) query.history = history;

        const familyHistoryEntryRecord = await this.familyHistoryEntrySchema.find(
            query as FilterQuery<IFamilyHistoryEntryPersistence & Document>
        )

        if (familyHistoryEntryRecord != null) {
            var returnValues = [] as FamilyHistoryEntry[];
            familyHistoryEntryRecord.forEach(element => {
                returnValues.push(FamilyHistoryEntryMap.toDomain(element));
            });
            return returnValues
        }
        return null
    }
```

The filtering is done here using a query. If the user chose to filter by a relative, it's added to the query (same to the description). The medical record number is obligatory, since we won't be searching for entries from multiple medical records at once.

## 8. Demonstration

![](demonstration/demonstration.gif)