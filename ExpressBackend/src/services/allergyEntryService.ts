import { Inject, Service } from "typedi";
import config from "../../config";

import { Result } from "../core/logic/Result";

import IAllergyService from "./IServices/IAllergyService";
import { Allergy } from "../domain/allergy";
import { AllergyMap } from "../mappers/AllergyMap";
import IAllergyEntryService from "./IServices/IAllergyEntryService";
import IAllergyEntryRepo from "./IRepos/IAllergyEntryRepo";
import { AllergyEntry } from "../domain/allergyEntry";
import IMedicalRecordRepo from "./IRepos/IMedicalRecordRepo";
import IAllergyRepo from "./IRepos/IAllergyRepo";
import { AllergyEntryMap } from "../mappers/AllergyEntryMap";

@Service()
export default class AllergyEntryService implements IAllergyEntryService {

    constructor(@Inject(config.repos.allergyEntry.name) private allergyEntryRepo: IAllergyEntryRepo,
                @Inject(config.repos.medicalRecord.name) private medicalRecordRepo: IMedicalRecordRepo,
                @Inject(config.repos.allergy.name) private allergyRepo: IAllergyRepo) {

    }

    public async createAllergyEntry(allergyEntryDTO: IAllergyEntryDTO): Promise<Result<IAllergyEntryDTO>> {
        try {
            var medicalRecords = await this.medicalRecordRepo.findByMedicalRecordNumber(allergyEntryDTO.medicalRecordNumber);
            if (medicalRecords === null)
                return Result.fail<IAllergyEntryDTO>("Medical Record does not exist");

            var allergies = await this.allergyRepo.findByName(allergyEntryDTO.allergy)
            if (allergies === null)
                return Result.fail<IAllergyEntryDTO>("Allergy does not exist");

            const entryList = await this.allergyEntryRepo.findByMedicalRecordNumber(allergyEntryDTO.medicalRecordNumber);
            allergyEntryDTO.entryNumber = allergyEntryDTO.medicalRecordNumber + String(entryList.length+1).padStart(3,'0');

            const allergyOrError = await AllergyEntry.create(allergyEntryDTO)
            if (allergyOrError.isFailure) {
                return Result.fail<IAllergyEntryDTO>(allergyOrError.errorValue())
            }

            const allergyResult = allergyOrError.getValue()
            await this.allergyEntryRepo.save(allergyResult)

            const allergyResultDTO = AllergyEntryMap.toDTO(allergyResult) as IAllergyEntryDTO
            return Result.ok<IAllergyEntryDTO>(allergyResultDTO)
        } catch (err) {
            throw err
        }
    }

    public async getAllergyEntryByMedicalRecordNumber(medicalRecordNumber: string): Promise<Result<IAllergyEntryDTO[]>> {
            try {
                const allergyEntry = await this.allergyEntryRepo.findByMedicalRecordNumber(medicalRecordNumber)
                if (allergyEntry === null) {
                    return Result.fail<IAllergyEntryDTO[]>("allergyEntry not found")
                }
    
                var allergyEntryResultDTO = [] as IAllergyEntryDTO[];
                allergyEntry.forEach(element => {
                    allergyEntryResultDTO.push(AllergyEntryMap.toDTO(element));
                });
                //const allergyEntryResultDTO = AllergyEntryMap.toDTO(allergyEntry);
                return Result.ok<IAllergyEntryDTO[]>(allergyEntryResultDTO)
            } catch (err) {
                throw err
            }
        }
    
        public async search(medicalRecordNumber: string, dto: IAllergyEntryOptionalDTO): Promise<Result<IAllergyEntryDTO[]>> {
            try {
                const allergyEntry = await this.allergyEntryRepo.search(medicalRecordNumber, dto.allergy, dto.description);
                if (allergyEntry === null) {
                    return Result.fail<IAllergyEntryDTO[]>("allergyEntry not found")
                }
    
                var allergyEntryResultDTO = [] as IAllergyEntryDTO[];
                allergyEntry.forEach(element => {
                    allergyEntryResultDTO.push(AllergyEntryMap.toDTO(element));
                });
                //const allergyEntryResultDTO = AllergyEntryMap.toDTO(allergyEntry);
                return Result.ok<IAllergyEntryDTO[]>(allergyEntryResultDTO)
            } catch (err) {
                throw err
            }
        }
        public async editAllergyEntry(entryNumber: string, dto: IAllergyEntryOptionalDTO): Promise<Result<IAllergyEntryDTO>> {
            try {
                const entry = await this.allergyEntryRepo.findByEntryNumber(entryNumber);
                if (entry === null) return Result.fail<IAllergyEntryDTO>("allergyEntry not found");
    
                if (dto.allergy) {
                    const allergy = await this.allergyRepo.findByName(dto.allergy);
                    if (allergy === null) return Result.fail<IAllergyEntryDTO>("allergy not found");
                    entry.allergy = dto.allergy;
                }
                if (dto.description) entry.description = dto.description;
    
                await this.allergyEntryRepo.save(entry);
    
                const allergyEntryResultDTO = AllergyEntryMap.toDTO(entry) as IAllergyEntryDTO
                return Result.ok<IAllergyEntryDTO>(allergyEntryResultDTO)
            } catch (err) {
                throw err
            }
        }

}
