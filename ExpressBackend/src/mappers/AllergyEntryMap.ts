import { Mapper } from "../core/infra/Mapper";

import { Document, Model } from 'mongoose';
import { IAllergyPersistence } from "../dataschema/IAllergyPersistence";

import { UniqueEntityID } from "../core/domain/UniqueEntityID";
import { AllergyEntry } from "../domain/allergyEntry";

export class AllergyEntryMap extends Mapper<AllergyEntry> {

    public static toDTO(allergyEntry: AllergyEntry): IAllergyEntryDTO {
        return { entryNumber: allergyEntry.entryNumber, medicalRecordNumber: allergyEntry.medicalRecordNumber, allergy: allergyEntry.allergy, description: allergyEntry.description }
    }

    public static toDomain(allergyEntry: any | Model<IAllergyPersistence & Document>): AllergyEntry {
        const allergyOrError = AllergyEntry.create(allergyEntry, new UniqueEntityID(allergyEntry.domainId))
        allergyOrError.isFailure ? console.log(allergyOrError.error) : ""

        return allergyOrError.isSuccess ? allergyOrError.getValue() : null
    }

    public static toPersistence(allergyEntry: AllergyEntry): any {
        return {
            domainId: allergyEntry.id.toString(),
            entryNumber: allergyEntry.entryNumber,
            medicalRecordNumber: allergyEntry.medicalRecordNumber,
            allergy: allergyEntry.allergy,
            description: allergyEntry.description
        }
    }

}
