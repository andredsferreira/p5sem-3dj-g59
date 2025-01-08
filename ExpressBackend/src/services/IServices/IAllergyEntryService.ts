import { Result } from "../../core/logic/Result";

export default interface IAllergyEntryService {

    createAllergyEntry(allergyEntryDTO: IAllergyEntryDTO): Promise<Result<IAllergyEntryDTO>>;

    getAllergyEntryByNumber(number: string): Promise<Result<IAllergyEntryDTO>>;

    updateAllergyEntry(number: string, allergyEntryDTO: IAllergyEntryDTO): Promise<Result<IAllergyEntryDTO>>;

}
