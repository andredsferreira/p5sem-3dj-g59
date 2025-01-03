import { Inject, Service } from "typedi";
import config from "../../config";

import { Result } from "../core/logic/Result";

import IMedicalConditionEntryService from "./IServices/IMedicalConditionEntryService";
import { IMedicalConditionEntryDTO, IMedicalConditionEntryOptionalDTO } from "../dto/IMedicalConditionEntryDTO";
import IMedicalConditionEntryRepo from "./IRepos/IMedicalConditionEntryRepo";
import { MedicalConditionEntry } from "../domain/medicalConditionEntry";
import { MedicalConditionEntryMap } from "../mappers/MedicalConditionEntryMap";
import IMedicalRecordRepo from "./IRepos/IMedicalRecordRepo";
import IMedConditionRepo from "./IRepos/IMedConditionRepo";

@Service()
export default class MedicalConditionEntryService implements IMedicalConditionEntryService {

    constructor(@Inject(config.repos.medicalConditionEntry.name) private medicalConditionEntryRepo: IMedicalConditionEntryRepo,
                @Inject(config.repos.medicalRecord.name) private medicalRecordRepo: IMedicalRecordRepo,
                @Inject(config.repos.medCondition.name) private medConditionRepo: IMedConditionRepo){

    }

    public async createMedicalConditionEntry(medicalConditionEntryDTO: IMedicalConditionEntryDTO): Promise<Result<IMedicalConditionEntryDTO>> {
        try {
            var medicalRecords = await this.medicalRecordRepo.findByMedicalRecordNumber(medicalConditionEntryDTO.medicalRecordNumber)
            if( medicalRecords === null)
                return Result.fail<IMedicalConditionEntryDTO>("Medical Record does not exist")

            var medConditions = await this.medConditionRepo.findByCondition(medicalConditionEntryDTO.condition)
            if( medConditions === null)
                return Result.fail<IMedicalConditionEntryDTO>("Medical Condition does not exist")

            const entryList = await this.medicalConditionEntryRepo.findByMedicalRecordNumber(medicalConditionEntryDTO.medicalRecordNumber);
            medicalConditionEntryDTO.entryNumber = medicalConditionEntryDTO.medicalRecordNumber + String(entryList.length+1).padStart(3,'0');
            
            const medicalConditionEntryOrError = await MedicalConditionEntry.create(medicalConditionEntryDTO)
            if (medicalConditionEntryOrError.isFailure) {
                return Result.fail<IMedicalConditionEntryDTO>(medicalConditionEntryOrError.errorValue())
            }

            const medicalConditionEntryResult = medicalConditionEntryOrError.getValue()
            await this.medicalConditionEntryRepo.save(medicalConditionEntryResult)

            const medicalConditionEntryResultDTO = MedicalConditionEntryMap.toDTO(medicalConditionEntryResult) as IMedicalConditionEntryDTO
            return Result.ok<IMedicalConditionEntryDTO>(medicalConditionEntryResultDTO)
        } catch (err) {
            throw err
        }
    }

    public async getMedicalConditionEntryByMedicalRecordNumber(medicalRecordNumber: string): Promise<Result<IMedicalConditionEntryDTO[]>> {
        try {
            const medicalConditionEntry = await this.medicalConditionEntryRepo.findByMedicalRecordNumber(medicalRecordNumber)
            if (medicalConditionEntry === null) {
                return Result.fail<IMedicalConditionEntryDTO[]>("medicalConditionEntry not found")
            }

            var medicalConditionEntryResultDTO = [] as IMedicalConditionEntryDTO[];
            medicalConditionEntry.forEach(element => {
                medicalConditionEntryResultDTO.push(MedicalConditionEntryMap.toDTO(element));
            });
            //const medicalConditionEntryResultDTO = MedicalConditionEntryMap.toDTO(medicalConditionEntry);
            return Result.ok<IMedicalConditionEntryDTO[]>(medicalConditionEntryResultDTO)
        } catch (err) {
            throw err
        }
    }

    public async search(medicalRecordNumber: string, dto: IMedicalConditionEntryOptionalDTO): Promise<Result<IMedicalConditionEntryDTO[]>> {
        try {
            const medicalConditionEntry = await this.medicalConditionEntryRepo.search(medicalRecordNumber, dto.condition, dto.year);
            if (medicalConditionEntry === null) {
                return Result.fail<IMedicalConditionEntryDTO[]>("medicalConditionEntry not found")
            }

            var medicalConditionEntryResultDTO = [] as IMedicalConditionEntryDTO[];
            medicalConditionEntry.forEach(element => {
                medicalConditionEntryResultDTO.push(MedicalConditionEntryMap.toDTO(element));
            });
            //const medicalConditionEntryResultDTO = MedicalConditionEntryMap.toDTO(medicalConditionEntry);
            return Result.ok<IMedicalConditionEntryDTO[]>(medicalConditionEntryResultDTO)
        } catch (err) {
            throw err
        }
    }
    public async editMedicalConditionEntry(entryNumber: string, dto: IMedicalConditionEntryOptionalDTO): Promise<Result<IMedicalConditionEntryDTO>> {
        try {
            const entry = await this.medicalConditionEntryRepo.findByEntryNumber(entryNumber);
            if (entry === null) return Result.fail<IMedicalConditionEntryDTO>("medicalConditionEntry not found");

            if (dto.condition) {
                const condition = await this.medConditionRepo.findByCondition(dto.condition);
                if (condition === null) return Result.fail<IMedicalConditionEntryDTO>("condition not found");
                entry.condition = dto.condition;
            }
            if (dto.year) entry.year = dto.year;

            await this.medicalConditionEntryRepo.save(entry);

            const medicalConditionEntryResultDTO = MedicalConditionEntryMap.toDTO(entry) as IMedicalConditionEntryDTO
            return Result.ok<IMedicalConditionEntryDTO>(medicalConditionEntryResultDTO)
        } catch (err) {
            throw err
        }
    }
}
