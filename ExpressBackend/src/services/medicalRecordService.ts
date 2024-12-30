import { Service, Inject } from 'typedi';
import config from "../../config";
import IMedicalRecordDTO from '../dto/IMedicalRecordDTO';
import { MedicalRecord } from "../domain/medicalRecord";
import IMedicalRecordRepo from './IRepos/IMedicalRecordRepo';
import IMedicalRecordService from './IServices/IMedicalRecordService';
import { Result } from "../core/logic/Result";
import { MedicalRecordMap } from "../mappers/MedicalRecordMap";
import * as jsonfile from 'jsonfile';
import * as fs from 'fs';
import * as path from 'path';
import IFamilyHistoryEntryRepo from './IRepos/IFamilyHistoryEntryRepo';
import { forEach } from 'lodash';
import * as MiniZip from 'minizip-asm.js'

@Service()
export default class MedicalRecordService implements IMedicalRecordService {
  constructor(
    @Inject(config.repos.medicalRecord.name) private MedRecordRepo: IMedicalRecordRepo,
    @Inject(config.repos.familyHistoryEntry.name) private FamHistoryRepo: IFamilyHistoryEntryRepo
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

  public async generateMedicalRecordZip(medicalRecordNumber: string, password: string): Promise<Result<string>> {
    try {
      const familyHistoryEntries = await this.FamHistoryRepo.findByMedicalRecordNumber(medicalRecordNumber);
  
      const jsonData = {
        medicalRecordNumber: medicalRecordNumber,
        familyHistoryEntries: familyHistoryEntries.map(({ relative, history }) => ({
          relative,
          history,
        })),
      };

      const jsonFilePath = path.join(__dirname, '../../temp/medical_record.json');
      await jsonfile.writeFile(jsonFilePath, jsonData, { spaces: 2 });

      const zipFilePath = path.join(__dirname, '../../temp/medical_record.zip');
      const fileData = fs.readFileSync(jsonFilePath);

      const mz = new MiniZip.default();
      mz.append('medical_record.json', fileData, { password: password });
      fs.writeFileSync(zipFilePath, mz.zip());

      fs.unlinkSync(jsonFilePath);

      return Result.ok<string>(zipFilePath);
    } catch (e) {
      console.log(e);
      return Result.fail<string>('Failed to generate ZIP with password');
    }
  }

  public cleanUp(filePath: string) {
    if (fs.existsSync(filePath)) {
      fs.unlinkSync(filePath);
    }
  }
}
