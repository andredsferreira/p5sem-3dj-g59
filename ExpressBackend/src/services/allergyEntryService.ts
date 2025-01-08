import { Inject, Service } from "typedi";
import config from "../../config";

import { Result } from "../core/logic/Result";

import IAllergyService from "./IServices/IAllergyService";
import IAllergyEntryDTO from "../dto/IAllergyEntryDTO";
import { Allergy } from "../domain/allergy";
import { AllergyMap } from "../mappers/AllergyMap";
import IAllergyEntryService from "./IServices/IAllergyEntryService";
import IAllergyEntryRepo from "./IRepos/IAllergyEntryRepo";
import { AllergyEntry } from "../domain/allergyEntry";

@Service()
export default class AllergyEntryService implements IAllergyEntryService {

    constructor(@Inject(config.repos.allergyEntry.name) private allergyEntryRepo: IAllergyEntryRepo) {

    }

    public async createAllergyEntry(allergyEntryDTO: IAllergyEntryDTO): Promise<Result<IAllergyEntryDTO>> {
        try {
            const allergyOrError = await AllergyEntry.create(allergyEntryDTO)
            if (allergyOrError.isFailure) {
                return Result.fail<IAllergyEntryDTO>(allergyOrError.errorValue())
            }

            const allergyResult = allergyOrError.getValue()
            await this.allergyEntryRepo.save(allergyResult)

            const allergyResultDTO = AllergyMap.toDTO(allergyResult) as IAllergyEntryDTO
            return Result.ok<IAllergyEntryDTO>(allergyResultDTO)
        } catch (err) {
            throw err
        }
    }

    public async getAllergyEntryByNumber(number: number): Promise<Result<IAllergyEntryDTO>> {
        try {
            const allergy = await this.allergyEntryRepo.findByName(name)
            if (allergy === null) {
                return Result.fail<IAllergyEntryDTO>("allergy not found")
            }

            const allergyResultDTO = AllergyMap.toDTO(allergy) as IAllergyEntryDTO
            return Result.ok<IAllergyEntryDTO>(allergyResultDTO)
        } catch (err) {
            throw err
        }
    }

    public async updateAllergyEntry(number: number, allergyEntryDTO: IAllergyEntryDTO): Promise<Result<IAllergyEntryDTO>> {
        try {
            const allergy = await this.allergyEntryRepo.findByName(originalName)
            if (allergy === null) {
                return Result.fail<IAllergyEntryDTO>("allergy not found")
            }

            if (allergyEntryDTO.code && allergyEntryDTO.code != "") {
                allergy.code = allergyEntryDTO.code
            }
            if (allergyEntryDTO.name && allergyEntryDTO.name != "") {
                allergy.name = allergyEntryDTO.name
            }
            if (allergyEntryDTO.description && allergyEntryDTO.description != "") {
                allergy.description = allergyEntryDTO.description
            }

            await this.allergyEntryRepo.save(allergy);

            const allergyResultDTO = AllergyMap.toDTO(allergy) as IAllergyEntryDTO
            return Result.ok<IAllergyEntryDTO>(allergyResultDTO)
        } catch (err) {
            throw err
        }
    }

}
