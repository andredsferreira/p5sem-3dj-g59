interface IAllergyEntryDTO {
    entryNumber: string,
    medicalRecordNumber: string,
    allergy: string,
    description: string,
}
interface IAllergyEntryOptionalDTO {
    allergy: string | null,
    description: string|null
}