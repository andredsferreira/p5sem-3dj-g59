import { Result } from "../../core/logic/Result";

export default interface IAllergyEntryService {

    createAllergyEntry(allergyEntryDTO: IAllergyEntryDTO): Promise<Result<IAllergyEntryDTO>>;

    getAllergyEntryByMedicalRecordNumber(medicalRecordNumber: string): Promise<Result<IAllergyEntryDTO[]>>

    search(medicalRecordNumber: string, dto: IAllergyEntryOptionalDTO): Promise<Result<IAllergyEntryDTO[]>> 

    editAllergyEntry(entryNumber: string, dto: IAllergyEntryOptionalDTO): Promise<Result<IAllergyEntryDTO>>

}
