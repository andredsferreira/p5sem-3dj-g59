import { Result } from "../../core/logic/Result";
import { IFamilyHistoryEntryDTO, IFamilyHistoryEntryOptionalDTO } from "../../dto/IFamilyHistoryEntryDTO";

export default interface IFamilyHistoryEntryService {
    
    createFamilyHistoryEntry(allergyDTO: IFamilyHistoryEntryDTO): Promise<Result<IFamilyHistoryEntryDTO>>;

    getFamilyHistoryEntryByMedicalRecordNumber(medicalRecordNumber: string): Promise<Result<IFamilyHistoryEntryDTO[]>>;

    search(medicalRecordNumber: string, dto: IFamilyHistoryEntryOptionalDTO): Promise<Result<IFamilyHistoryEntryDTO[]>>;

    editFamilyHistoryEntry(entryNumber: string, dto: IFamilyHistoryEntryOptionalDTO): Promise<Result<IFamilyHistoryEntryDTO>>;

}
