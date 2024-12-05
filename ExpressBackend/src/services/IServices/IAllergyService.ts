import { Result } from "../../core/logic/Result";
import IAllergyDTO from "../../dto/IAllergyDTO";

export default interface IAllergyService {
    
    createAllergy(allergyDTO: IAllergyDTO): Promise<Result<IAllergyDTO>>;

    getAllergyByName(name: string): Promise<Result<IAllergyDTO>>;

}
