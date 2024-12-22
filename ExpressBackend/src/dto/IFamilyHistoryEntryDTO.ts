export interface IFamilyHistoryEntryDTO {
  entryNumber: string|undefined,
  medicalRecordNumber: string,
  relative: string
  history: string
}
export interface IFamilyHistoryEntryOptionalDTO {
  relative: string|undefined
  history: string|undefined
}