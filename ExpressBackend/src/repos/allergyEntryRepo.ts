import { Service, Inject } from 'typedi';

import IAllergyEntryRepo from '../services/IRepos/IAllergyEntryRepo';
import { AllergyEntry } from '../domain/allergyEntry';
import { AllergyEntryMap } from '../mappers/AllergyEntryMap';

import { Document, FilterQuery, Model } from 'mongoose';
import { IAllergyEntryPersistence } from '../dataschema/IAllergyEntryPersistence';

@Service()
export default class AllergyEntryRepo implements IAllergyEntryRepo {

    constructor(@Inject("allergyEntrySchema") private allergyEntrySchema: Model<IAllergyEntryPersistence & Document>) {

    }

    public async save(allergyEntry: AllergyEntry): Promise<AllergyEntry> {
        const query = { domainId: allergyEntry.id.toString() }
        const allergyEntryDoc = await this.allergyEntrySchema.findOne(query)

        try {

            if (allergyEntryDoc === null) {
                const rawAllergyEntry: any = AllergyEntryMap.toPersistence(allergyEntry)
                const allergyEntryCreated = await this.allergyEntrySchema.create(rawAllergyEntry)
                return AllergyEntryMap.toDomain(allergyEntryCreated)
            }

            allergyEntryDoc.medicalRecordNumber = allergyEntry.medicalRecordNumber
            allergyEntryDoc.allergy = allergyEntry.allergy
            allergyEntryDoc.description = allergyEntry.description
            allergyEntryDoc.entryNumber = allergyEntry.entryNumber
            await allergyEntryDoc.save()

            return allergyEntry
        } catch (error) {
            throw error
        }
    }

    public async findByMedicalRecordNumber(medicalRecordNumber: string): Promise<AllergyEntry[]> {
        const query = { medicalRecordNumber: medicalRecordNumber }
        const allergyEntryRecord = await this.allergyEntrySchema.find(
            query as FilterQuery<IAllergyEntryPersistence & Document>
        )

        if (allergyEntryRecord != null) {
            var returnValues = [] as AllergyEntry[];
            allergyEntryRecord.forEach(element => {
                returnValues.push(AllergyEntryMap.toDomain(element));
            });
            return returnValues
            //return AllergyEntryMap.toDomain(allergyEntryRecord)
        }
        return null
    }

    public async findByEntryNumber(entryNumber: string): Promise<AllergyEntry> {
        const query = { entryNumber: entryNumber }
        const allergyEntryRecord = await this.allergyEntrySchema.findOne(
            query as FilterQuery<IAllergyEntryPersistence & Document>
        )

        if (allergyEntryRecord != null) {
            return AllergyEntryMap.toDomain(allergyEntryRecord)
        }
        return null
    }

    public async search(medicalRecordNumber: string, allergy?: string, description?: string): Promise<AllergyEntry[]> {
        const query: any = { medicalRecordNumber: medicalRecordNumber }
        if (allergy) query.allergy = allergy;
        if (description) query.description = description;

        const allergyEntryRecord = await this.allergyEntrySchema.find(
            query as FilterQuery<IAllergyEntryPersistence & Document>
        )

        if (allergyEntryRecord != null) {
            var returnValues = [] as AllergyEntry[];
            allergyEntryRecord.forEach(element => {
                returnValues.push(AllergyEntryMap.toDomain(element));
            });
            return returnValues
            //return AllergyEntryMap.toDomain(allergyEntryRecord)
        }
        return null
    }

    exists(t: AllergyEntry): Promise<boolean> {
        throw new Error('Method not implemented.');
    }

}
