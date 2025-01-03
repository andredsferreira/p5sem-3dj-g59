export interface IMedicalConditionEntryDTO {
    entryNumber: string|undefined,
    medicalRecordNumber: string,
    condition: string
    year: number
  }
  export interface IMedicalConditionEntryOptionalDTO {
    condition: string|undefined
    year: number|undefined
  }