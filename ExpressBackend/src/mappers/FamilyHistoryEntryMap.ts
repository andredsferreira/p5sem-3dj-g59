import { Mapper } from "../core/infra/Mapper";

import { Document, Model } from 'mongoose';
import { IFamilyHistoryEntryPersistence } from "../dataschema/IFamilyHistoryEntryPersistence";

import { IFamilyHistoryEntryDTO } from "../dto/IFamilyHistoryEntryDTO";
import { FamilyHistoryEntry } from "../domain/familyHistoryEntry";

import { UniqueEntityID } from "../core/domain/UniqueEntityID";

export class FamilyHistoryEntryMap extends Mapper<FamilyHistoryEntry> {

    public static toDTO(familyHistoryEntry: FamilyHistoryEntry): IFamilyHistoryEntryDTO {
        return { entryNumber: familyHistoryEntry.entryNumber, medicalRecordNumber: familyHistoryEntry.medicalRecordNumber, relative: familyHistoryEntry.relative, history: familyHistoryEntry.history }
    }

    public static toDomain(familyHistoryEntry: any | Model<IFamilyHistoryEntryPersistence & Document>): FamilyHistoryEntry {
        const familyHistoryEntryOrError = FamilyHistoryEntry.create(familyHistoryEntry, new UniqueEntityID(familyHistoryEntry.domainId))
        familyHistoryEntryOrError.isFailure ? console.log(familyHistoryEntryOrError.error) : ""

        return familyHistoryEntryOrError.isSuccess ? familyHistoryEntryOrError.getValue() : null
    }

    public static toPersistence(familyHistoryEntry: FamilyHistoryEntry): any {
        return {
            domainId: familyHistoryEntry.id.toString(),
            entryNumber: familyHistoryEntry.entryNumber,
            medicalRecordNumber: familyHistoryEntry.medicalRecordNumber,
            relative: familyHistoryEntry.relative,
            history: familyHistoryEntry.history
        }
    }

}
