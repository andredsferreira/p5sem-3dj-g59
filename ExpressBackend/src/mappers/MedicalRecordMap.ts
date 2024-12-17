import { Mapper } from "../core/infra/Mapper";

import { Document, Model } from 'mongoose';
import { IMedicalRecordPersistence } from '../dataschema/IMedicalRecordPersistence';

import IMedicalRecordDTO from "../dto/IMedicalRecordDTO";
import { MedicalRecord } from "../domain/medicalRecord";

import { UniqueEntityID } from "../core/domain/UniqueEntityID";

export class MedicalRecordMap extends Mapper<MedicalRecord> {
  
  public static toDTO( medRecord: MedicalRecord): IMedicalRecordDTO {
    return {
      medicalRecordNumber: medRecord.medicalRecordNumber,
    } as IMedicalRecordDTO;
  }

  public static toDomain (medRecord: any | Model<IMedicalRecordPersistence & Document> ): MedicalRecord {
    const medRecordOrError = MedicalRecord.create(
      medRecord,
      new UniqueEntityID(medRecord.domainId)
    );

    medRecordOrError.isFailure ? console.log(medRecordOrError.error) : '';

    return medRecordOrError.isSuccess ? medRecordOrError.getValue() : null;
  }

  public static toPersistence (medRecord: MedicalRecord): any {
    return {
      domainId: medRecord.id.toString(),
      medicalRecordNumber: medRecord.medicalRecordNumber,
    }
  }
}