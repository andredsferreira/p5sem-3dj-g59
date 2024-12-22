import { Repo } from "../../core/infra/Repo";
import { FamilyHistoryEntry } from "../../domain/familyHistoryEntry";

export default interface IFamilyHistoryEntryRepo extends Repo<FamilyHistoryEntry> {

    save(entry: FamilyHistoryEntry): Promise<FamilyHistoryEntry>
    findByMedicalRecordNumber(medicalRecordNumber: string): Promise<FamilyHistoryEntry[]>
    findByEntryNumber(entryNumber: string): Promise<FamilyHistoryEntry>
    search(medicalRecordNumber: string, relative: string|null, history: string|null): Promise<FamilyHistoryEntry[]>
}