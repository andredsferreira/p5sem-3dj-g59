import { Inject, Service } from "typedi";
import config from "../../config";

import { Result } from "../core/logic/Result";

import IAllergyService from "./IServices/IAllergyService";
import IAllergyDTO from "../dto/IAllergyDTO";
import IAllergyRepo from "./IRepos/IAllergyRepo";
import { Allergy } from "../domain/allergy";
import { AllergyMap } from "../mappers/AllergyMap";

@Service()
export default class AllergyService implements IAllergyService {

    constructor(@Inject(config.repos.allergy.name) private allergyRepo: IAllergyRepo) {

    }

    public async createAllergy(allergyDTO: IAllergyDTO): Promise<Result<IAllergyDTO>> {
        try {
            const allergyOrError = await Allergy.create(allergyDTO)
            if (allergyOrError.isFailure) {
                return Result.fail<IAllergyDTO>(allergyOrError.errorValue())
            }

            const allergyResult = allergyOrError.getValue()
            await this.allergyRepo.save(allergyResult)

            const allergyResultDTO = AllergyMap.toDTO(allergyResult) as IAllergyDTO
            return Result.ok<IAllergyDTO>(allergyResultDTO)
        } catch (err) {
            throw err
        }
    }

    public async getAllergyByName(name: string): Promise<Result<IAllergyDTO>> {
        try {
            const allergy = await this.allergyRepo.findByName(name)
            if (allergy === null) {
                return Result.fail<IAllergyDTO>("allergy not found")
            }

            const allergyResultDTO = AllergyMap.toDTO(allergy) as IAllergyDTO
            return Result.ok<IAllergyDTO>(allergyResultDTO)
        } catch (err) {
            throw err
        }
    }

}
