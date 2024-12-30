import { Result } from "../../core/logic/Result";
import IMedicalRecordDTO from "../../dto/IMedicalRecordDTO";

export default interface IMedRecordService  {
  createMedRecord(medRecordDTO: IMedicalRecordDTO): Promise<Result<IMedicalRecordDTO>>;

  getMedRecordByMedicalRecordNumber (medRecordNumber: string): Promise<Result<IMedicalRecordDTO>>;

  generateMedicalRecordZip(medicalRecordNumber: string, password: string): Promise<Result<string>>;

  cleanUp(filePath: string);
}
