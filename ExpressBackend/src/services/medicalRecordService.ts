import { Service, Inject } from 'typedi';
import config from "../../config";
import IMedicalRecordDTO from '../dto/IMedicalRecordDTO';
import { MedicalRecord } from "../domain/medicalRecord";
import IMedicalRecordRepo from './IRepos/IMedicalRecordRepo';
import IMedicalRecordService from './IServices/IMedicalRecordService';
import { Result } from "../core/logic/Result";
import { MedicalRecordMap } from "../mappers/MedicalRecordMap";

@Service()
export default class MedicalRecordService implements IMedicalRecordService {
  constructor(
    @Inject(config.repos.medicalRecord.name) private MedRecordRepo: IMedicalRecordRepo
  ) { }

  public async getMedRecordByMedicalRecordNumber(MedRecordCode: string): Promise<Result<IMedicalRecordDTO>> {
    try {
      const MedRecord = await this.MedRecordRepo.findByMedicalRecordNumber(MedRecordCode);

      if (MedRecord === null) {
        return Result.fail<IMedicalRecordDTO>("MedRecord not found");
      }
      else {
        const MedRecordDTOResult = MedicalRecordMap.toDTO(MedRecord) as IMedicalRecordDTO;
        return Result.ok<IMedicalRecordDTO>(MedRecordDTOResult)
      }
    } catch (e) {
      throw e;
    }
  }

  public async createMedRecord(MedRecordDTO: IMedicalRecordDTO): Promise<Result<IMedicalRecordDTO>> {
    try {
      const MedRecordOrError = MedicalRecord.create(MedRecordDTO);
      if (MedRecordOrError.isFailure) {
        return Result.fail<IMedicalRecordDTO>(MedRecordOrError.errorValue());
      }

      const MedRecordResult = MedRecordOrError.getValue();

      await this.MedRecordRepo.save(MedRecordResult);

      const MedRecordDTOResult = MedicalRecordMap.toDTO(MedRecordResult) as IMedicalRecordDTO;
      return Result.ok<IMedicalRecordDTO>(MedRecordDTOResult)
    } catch (e) {
      throw e;
    }
  }
}
