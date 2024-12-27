import { Service, Inject } from 'typedi';

import IFamilyHistoryEntryRepo from '../services/IRepos/IFamilyHistoryEntryRepo';
import { FamilyHistoryEntry } from '../domain/familyHistoryEntry';
import { FamilyHistoryEntryMap } from '../mappers/FamilyHistoryEntryMap';

import { Document, FilterQuery, Model } from 'mongoose';
import { IFamilyHistoryEntryPersistence } from '../dataschema/IFamilyHistoryEntryPersistence';

@Service()
export default class FamilyHistoryEntryRepo implements IFamilyHistoryEntryRepo {

    constructor(@Inject("familyHistoryEntrySchema") private familyHistoryEntrySchema: Model<IFamilyHistoryEntryPersistence & Document>) {

    }

    public async save(familyHistoryEntry: FamilyHistoryEntry): Promise<FamilyHistoryEntry> {
        const query = { domainId: familyHistoryEntry.id.toString() }
        const familyHistoryEntryDoc = await this.familyHistoryEntrySchema.findOne(query)

        try {

            if (familyHistoryEntryDoc === null) {
                const rawFamilyHistoryEntry: any = FamilyHistoryEntryMap.toPersistence(familyHistoryEntry)
                const familyHistoryEntryCreated = await this.familyHistoryEntrySchema.create(rawFamilyHistoryEntry)
                return FamilyHistoryEntryMap.toDomain(familyHistoryEntryCreated)
            }

            familyHistoryEntryDoc.medicalRecordNumber = familyHistoryEntry.medicalRecordNumber
            familyHistoryEntryDoc.relative = familyHistoryEntry.relative
            familyHistoryEntryDoc.history = familyHistoryEntry.history
            familyHistoryEntryDoc.entryNumber = familyHistoryEntry.entryNumber
            await familyHistoryEntryDoc.save()

            return familyHistoryEntry
        } catch (error) {
            throw error
        }
    }

    public async findByMedicalRecordNumber(medicalRecordNumber: string): Promise<FamilyHistoryEntry[]> {
        const query = { medicalRecordNumber: medicalRecordNumber }
        const familyHistoryEntryRecord = await this.familyHistoryEntrySchema.find(
            query as FilterQuery<IFamilyHistoryEntryPersistence & Document>
        )

        if (familyHistoryEntryRecord != null) {
            var returnValues = [] as FamilyHistoryEntry[];
            familyHistoryEntryRecord.forEach(element => {
                returnValues.push(FamilyHistoryEntryMap.toDomain(element));
            });
            return returnValues
            //return FamilyHistoryEntryMap.toDomain(familyHistoryEntryRecord)
        }
        return null
    }

    public async findByEntryNumber(entryNumber: string): Promise<FamilyHistoryEntry> {
        const query = { entryNumber: entryNumber }
        const familyHistoryEntryRecord = await this.familyHistoryEntrySchema.findOne(
            query as FilterQuery<IFamilyHistoryEntryPersistence & Document>
        )

        if (familyHistoryEntryRecord != null) {
            return FamilyHistoryEntryMap.toDomain(familyHistoryEntryRecord)
        }
        return null
    }

    public async search(medicalRecordNumber: string, relative?: string, history?: string): Promise<FamilyHistoryEntry[]> {
        const query: any = { medicalRecordNumber: medicalRecordNumber }
        if (relative) query.relative = relative;
        if (history) query.history = history;

        const familyHistoryEntryRecord = await this.familyHistoryEntrySchema.find(
            query as FilterQuery<IFamilyHistoryEntryPersistence & Document>
        )

        if (familyHistoryEntryRecord != null) {
            var returnValues = [] as FamilyHistoryEntry[];
            familyHistoryEntryRecord.forEach(element => {
                returnValues.push(FamilyHistoryEntryMap.toDomain(element));
            });
            return returnValues
        }
        return null
    }

    exists(t: FamilyHistoryEntry): Promise<boolean> {
        throw new Error('Method not implemented.');
    }

}
