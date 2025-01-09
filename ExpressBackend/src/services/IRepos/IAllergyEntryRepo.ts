import { Repo } from "../../core/infra/Repo";
import { AllergyEntry } from "../../domain/allergyEntry";
import { AllergyId } from "../../domain/allergyId";

export default interface IAllergyEntryRepo extends Repo<AllergyEntry> {
    findByMedicalRecordNumber(medicalRecordNumber: any): Promise<AllergyEntry[]>;
    search(medicalRecordNumber: string, allergy?: string, description?: string): Promise<AllergyEntry[]>
    save(allergyEntry: AllergyEntry): Promise<AllergyEntry>

    findByEntryNumber(entryNumber: string): Promise<AllergyEntry>

}