import { Inject, Service } from "typedi";
import config from "../../config";

import { Result } from "../core/logic/Result";

import IAllergyService from "./IServices/IAllergyService";
import IAllergyDTO from "../dto/IAllergyDTO";
import IAllergyRepo from "./IRepos/IAllergyRepo";

@Service()
export default class AllergyService implements IAllergyService {

    constructor(@Inject(config.repos.allergy.name) private AllergyRepo: IAllergyRepo) {

    }

    createAllergy(allergyDTO: IAllergyDTO): Promise<Result<IAllergyDTO>> {
        throw new Error("Method not implemented.");
    }

    getAllergyByName(name: string): Promise<Result<IAllergyDTO>> {
        throw new Error("Method not implemented.");
    }

}
