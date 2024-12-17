import { Repo } from "../../core/infra/Repo";
import { MedicalRecord } from "../../domain/medicalRecord";
import { MedicalRecordId } from "../../domain/medicalRecordId";

export default interface IMedicalRecordRepo extends Repo<MedicalRecord> {
  save(MedRecord: MedicalRecord): Promise<MedicalRecord>;
  findByDomainId (MedRecordId: MedicalRecordId | string): Promise<MedicalRecord>;
  findByMedicalRecordNumber (MedicalRecordNumber: string): Promise<MedicalRecord>;
    
  //findByIds (MedRecordsIds: MedRecordId[]): Promise<MedRecord[]>;
  //saveCollection (MedRecords: MedRecord[]): Promise<MedRecord[]>;
  //removeByMedRecordIds (MedRecords: MedRecordId[]): Promise<any>
}