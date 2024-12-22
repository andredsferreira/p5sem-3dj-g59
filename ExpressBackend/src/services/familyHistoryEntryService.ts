import { Inject, Service } from "typedi";
import config from "../../config";

import { Result } from "../core/logic/Result";

import IFamilyHistoryEntryService from "./IServices/IFamilyHistoryEntryService";
import { IFamilyHistoryEntryDTO, IFamilyHistoryEntryOptionalDTO } from "../dto/IFamilyHistoryEntryDTO";
import IFamilyHistoryEntryRepo from "./IRepos/IFamilyHistoryEntryRepo";
import { FamilyHistoryEntry } from "../domain/familyHistoryEntry";
import { FamilyHistoryEntryMap } from "../mappers/FamilyHistoryEntryMap";
import IMedicalRecordRepo from "./IRepos/IMedicalRecordRepo";

@Service()
export default class FamilyHistoryEntryService implements IFamilyHistoryEntryService {

    constructor(@Inject(config.repos.familyHistoryEntry.name) private familyHistoryEntryRepo: IFamilyHistoryEntryRepo,
                @Inject(config.repos.medicalRecord.name) private medicalRecordRepo: IMedicalRecordRepo) {

    }

    public async createFamilyHistoryEntry(familyHistoryEntryDTO: IFamilyHistoryEntryDTO): Promise<Result<IFamilyHistoryEntryDTO>> {
        try {
            var medicalRecords = await this.medicalRecordRepo.findByMedicalRecordNumber(familyHistoryEntryDTO.medicalRecordNumber)
            if( medicalRecords === null)
                return Result.fail<IFamilyHistoryEntryDTO>("Medical Record does not exist")

            const entryList = await this.familyHistoryEntryRepo.findByMedicalRecordNumber(familyHistoryEntryDTO.medicalRecordNumber);
            familyHistoryEntryDTO.entryNumber = familyHistoryEntryDTO.medicalRecordNumber + String(entryList.length+1).padStart(3,'0');
            
            const familyHistoryEntryOrError = await FamilyHistoryEntry.create(familyHistoryEntryDTO)
            if (familyHistoryEntryOrError.isFailure) {
                return Result.fail<IFamilyHistoryEntryDTO>(familyHistoryEntryOrError.errorValue())
            }

            const familyHistoryEntryResult = familyHistoryEntryOrError.getValue()
            await this.familyHistoryEntryRepo.save(familyHistoryEntryResult)

            const familyHistoryEntryResultDTO = FamilyHistoryEntryMap.toDTO(familyHistoryEntryResult) as IFamilyHistoryEntryDTO
            return Result.ok<IFamilyHistoryEntryDTO>(familyHistoryEntryResultDTO)
        } catch (err) {
            throw err
        }
    }

    public async getFamilyHistoryEntryByMedicalRecordNumber(medicalRecordNumber: string): Promise<Result<IFamilyHistoryEntryDTO[]>> {
        try {
            const familyHistoryEntry = await this.familyHistoryEntryRepo.findByMedicalRecordNumber(medicalRecordNumber)
            if (familyHistoryEntry === null) {
                return Result.fail<IFamilyHistoryEntryDTO[]>("familyHistoryEntry not found")
            }

            var familyHistoryEntryResultDTO = [] as IFamilyHistoryEntryDTO[];
            familyHistoryEntry.forEach(element => {
                familyHistoryEntryResultDTO.push(FamilyHistoryEntryMap.toDTO(element));
            });
            //const familyHistoryEntryResultDTO = FamilyHistoryEntryMap.toDTO(familyHistoryEntry);
            return Result.ok<IFamilyHistoryEntryDTO[]>(familyHistoryEntryResultDTO)
        } catch (err) {
            throw err
        }
    }

    public async search(medicalRecordNumber: string, dto: IFamilyHistoryEntryOptionalDTO): Promise<Result<IFamilyHistoryEntryDTO[]>> {
        try {
            const familyHistoryEntry = await this.familyHistoryEntryRepo.search(medicalRecordNumber, dto.relative, dto.history);
            if (familyHistoryEntry === null) {
                return Result.fail<IFamilyHistoryEntryDTO[]>("familyHistoryEntry not found")
            }

            var familyHistoryEntryResultDTO = [] as IFamilyHistoryEntryDTO[];
            familyHistoryEntry.forEach(element => {
                familyHistoryEntryResultDTO.push(FamilyHistoryEntryMap.toDTO(element));
            });
            //const familyHistoryEntryResultDTO = FamilyHistoryEntryMap.toDTO(familyHistoryEntry);
            return Result.ok<IFamilyHistoryEntryDTO[]>(familyHistoryEntryResultDTO)
        } catch (err) {
            throw err
        }
    }
    public async editFamilyHistoryEntry(entryNumber: string, dto: IFamilyHistoryEntryOptionalDTO): Promise<Result<IFamilyHistoryEntryDTO>> {
        try {
            const entry = await this.familyHistoryEntryRepo.findByEntryNumber(entryNumber);
            if (entry === null) return Result.fail<IFamilyHistoryEntryDTO>("familyHistoryEntry not found");

            if (dto.relative) entry.relative = dto.relative;
            if (dto.history) entry.history = dto.history;

            await this.familyHistoryEntryRepo.save(entry);

            const familyHistoryEntryResultDTO = FamilyHistoryEntryMap.toDTO(entry) as IFamilyHistoryEntryDTO
            return Result.ok<IFamilyHistoryEntryDTO>(familyHistoryEntryResultDTO)
        } catch (err) {
            throw err
        }
    }
}
