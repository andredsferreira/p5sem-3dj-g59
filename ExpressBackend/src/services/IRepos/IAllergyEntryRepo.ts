import { Repo } from "../../core/infra/Repo";
import { AllergyEntry } from "../../domain/allergyEntry";
import { AllergyId } from "../../domain/allergyId";

export default interface IAllergyEntryRepo extends Repo<AllergyEntry> {

    save(allergyEntry: AllergyEntry): Promise<AllergyEntry>

    findByEntryNumber(number: number): Promise<AllergyEntry>

}