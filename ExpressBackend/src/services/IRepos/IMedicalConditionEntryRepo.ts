import { Repo } from "../../core/infra/Repo";
import { MedicalConditionEntry } from "../../domain/medicalConditionEntry";

export default interface IMedicalConditionEntryRepo extends Repo<MedicalConditionEntry> {

    save(entry: MedicalConditionEntry): Promise<MedicalConditionEntry>
    findByMedicalRecordNumber(medicalRecordNumber: string): Promise<MedicalConditionEntry[]>
    findByEntryNumber(entryNumber: string): Promise<MedicalConditionEntry>
    search(medicalRecordNumber: string, condition: string|null, year: number|null): Promise<MedicalConditionEntry[]>
}