import { Service, Inject } from 'typedi';

import IMedicalRecordRepo from "../services/IRepos/IMedicalRecordRepo";
import { MedicalRecord } from "../domain/medicalRecord";
import { MedicalRecordId } from "../domain/medicalRecordId";
import { MedicalRecordMap } from "../mappers/MedicalRecordMap";

import { Document, FilterQuery, Model } from 'mongoose';
import { IMedicalRecordPersistence } from '../dataschema/IMedicalRecordPersistence';

@Service()
export default class MedicalRecordRepo implements IMedicalRecordRepo {
  private models: any;

  constructor(
    @Inject('medicalRecordSchema') private MedicalRecordSchema: Model<IMedicalRecordPersistence & Document>,
  ) { }

  private createBaseQuery(): any {
    return {
      where: {},
    }
  }

  public async exists(MedRecord: MedicalRecord): Promise<boolean> {

    const idX = MedRecord.id instanceof MedicalRecordId ? (<MedicalRecordId>MedRecord.id).toValue() : MedRecord.id;

    const query = { domainId: idX };
    const MedRecordDocument = await this.MedicalRecordSchema.findOne(query as FilterQuery<IMedicalRecordPersistence & Document>);

    return !!MedRecordDocument === true;
  }

  public async save(MedRecord: MedicalRecord): Promise<MedicalRecord> {
    const query = { domainId: MedRecord.id.toString() };

    const MedRecordDocument = await this.MedicalRecordSchema.findOne(query);

    try {
      if (MedRecordDocument === null) {
        const rawMedRecord: any = MedicalRecordMap.toPersistence(MedRecord);

        const MedRecordCreated = await this.MedicalRecordSchema.create(rawMedRecord);

        return MedicalRecordMap.toDomain(MedRecordCreated);
      }
    } catch (err) {
      throw err;
    }
  }

  public async findByDomainId(MedRecordId: string): Promise<MedicalRecord> {
    const query = { domainId: MedRecordId };
    const MedRecordRecord = await this.MedicalRecordSchema.findOne(query as FilterQuery<IMedicalRecordPersistence & Document>);

    if (MedRecordRecord != null) {
      return MedicalRecordMap.toDomain(MedRecordRecord);
    }
    else
      return null;
  }
  
  public async findByMedicalRecordNumber(MedRecordNumber: string): Promise<MedicalRecord> {
    const query = { medicalRecordNumber: MedRecordNumber };
    const MedRecordRecord = await this.MedicalRecordSchema.findOne(query as FilterQuery<IMedicalRecordPersistence & Document>);

    if (MedRecordRecord != null) {
      return MedicalRecordMap.toDomain(MedRecordRecord);
    }
    else
      return null;
  }
}