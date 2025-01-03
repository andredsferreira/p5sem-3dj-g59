import { Service, Inject } from 'typedi';

import IMedicalConditionEntryRepo from '../services/IRepos/IMedicalConditionEntryRepo';
import { MedicalConditionEntry } from '../domain/medicalConditionEntry';
import { MedicalConditionEntryMap } from '../mappers/MedicalConditionEntryMap';

import { Document, FilterQuery, Model } from 'mongoose';
import { IMedicalConditionEntryPersistence } from '../dataschema/IMedicalConditionEntryPersistence';

@Service()
export default class MedicalConditionEntryRepo implements IMedicalConditionEntryRepo {

    constructor(@Inject("medicalConditionEntrySchema") private medicalConditionEntrySchema: Model<IMedicalConditionEntryPersistence & Document>) {

    }

    public async save(medicalConditionEntry: MedicalConditionEntry): Promise<MedicalConditionEntry> {
        const query = { domainId: medicalConditionEntry.id.toString() }
        const medicalConditionEntryDoc = await this.medicalConditionEntrySchema.findOne(query)

        try {

            if (medicalConditionEntryDoc === null) {
                const rawMedicalConditionEntry: any = MedicalConditionEntryMap.toPersistence(medicalConditionEntry)
                const medicalConditionEntryCreated = await this.medicalConditionEntrySchema.create(rawMedicalConditionEntry)
                return MedicalConditionEntryMap.toDomain(medicalConditionEntryCreated)
            }

            medicalConditionEntryDoc.medicalRecordNumber = medicalConditionEntry.medicalRecordNumber
            medicalConditionEntryDoc.condition = medicalConditionEntry.condition
            medicalConditionEntryDoc.year = medicalConditionEntry.year
            medicalConditionEntryDoc.entryNumber = medicalConditionEntry.entryNumber
            await medicalConditionEntryDoc.save()

            return medicalConditionEntry
        } catch (error) {
            throw error
        }
    }

    public async findByMedicalRecordNumber(medicalRecordNumber: string): Promise<MedicalConditionEntry[]> {
        const query = { medicalRecordNumber: medicalRecordNumber }
        const medicalConditionEntryRecord = await this.medicalConditionEntrySchema.find(
            query as FilterQuery<IMedicalConditionEntryPersistence & Document>
        )

        if (medicalConditionEntryRecord != null) {
            var returnValues = [] as MedicalConditionEntry[];
            medicalConditionEntryRecord.forEach(element => {
                returnValues.push(MedicalConditionEntryMap.toDomain(element));
            });
            return returnValues
            //return MedicalConditionEntryMap.toDomain(medicalConditionEntryRecord)
        }
        return null
    }

    public async findByEntryNumber(entryNumber: string): Promise<MedicalConditionEntry> {
        const query = { entryNumber: entryNumber }
        const medicalConditionEntryRecord = await this.medicalConditionEntrySchema.findOne(
            query as FilterQuery<IMedicalConditionEntryPersistence & Document>
        )

        if (medicalConditionEntryRecord != null) {
            return MedicalConditionEntryMap.toDomain(medicalConditionEntryRecord)
        }
        return null
    }

    public async search(medicalRecordNumber: string, condition?: string, year?: number): Promise<MedicalConditionEntry[]> {
        const query: any = { medicalRecordNumber: medicalRecordNumber }
        if (condition) query.condition = condition;
        if (year) query.year = year;

        const medicalConditionEntryRecord = await this.medicalConditionEntrySchema.find(
            query as FilterQuery<IMedicalConditionEntryPersistence & Document>
        )

        if (medicalConditionEntryRecord != null) {
            var returnValues = [] as MedicalConditionEntry[];
            medicalConditionEntryRecord.forEach(element => {
                returnValues.push(MedicalConditionEntryMap.toDomain(element));
            });
            return returnValues
            //return MedicalConditionEntryMap.toDomain(medicalConditionEntryRecord)
        }
        return null
    }

    exists(t: MedicalConditionEntry): Promise<boolean> {
        throw new Error('Method not implemented.');
    }

}
