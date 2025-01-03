import { Result } from "../../core/logic/Result";
import { IMedicalConditionEntryDTO, IMedicalConditionEntryOptionalDTO } from "../../dto/IMedicalConditionEntryDTO";

export default interface IMedicalConditionEntryService {
    
    createMedicalConditionEntry(allergyDTO: IMedicalConditionEntryDTO): Promise<Result<IMedicalConditionEntryDTO>>;

    getMedicalConditionEntryByMedicalRecordNumber(medicalRecordNumber: string): Promise<Result<IMedicalConditionEntryDTO[]>>;

    search(medicalRecordNumber: string, dto: IMedicalConditionEntryOptionalDTO): Promise<Result<IMedicalConditionEntryDTO[]>>;

    editMedicalConditionEntry(entryNumber: string, dto: IMedicalConditionEntryOptionalDTO): Promise<Result<IMedicalConditionEntryDTO>>;

}
