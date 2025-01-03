import { Mapper } from "../core/infra/Mapper";

import { Document, Model } from 'mongoose';
import { IMedicalConditionEntryPersistence } from "../dataschema/IMedicalConditionEntryPersistence";

import { IMedicalConditionEntryDTO } from "../dto/IMedicalConditionEntryDTO";
import { MedicalConditionEntry } from "../domain/medicalConditionEntry";

import { UniqueEntityID } from "../core/domain/UniqueEntityID";

export class MedicalConditionEntryMap extends Mapper<MedicalConditionEntry> {

    public static toDTO(medicalConditionEntry: MedicalConditionEntry): IMedicalConditionEntryDTO {
        return { entryNumber: medicalConditionEntry.entryNumber, medicalRecordNumber: medicalConditionEntry.medicalRecordNumber, condition: medicalConditionEntry.condition, year: medicalConditionEntry.year }
    }

    public static toDomain(medicalConditionEntry: any | Model<IMedicalConditionEntryPersistence & Document>): MedicalConditionEntry {
        const medicalConditionEntryOrError = MedicalConditionEntry.create(medicalConditionEntry, new UniqueEntityID(medicalConditionEntry.domainId))
        medicalConditionEntryOrError.isFailure ? console.log(medicalConditionEntryOrError.error) : ""

        return medicalConditionEntryOrError.isSuccess ? medicalConditionEntryOrError.getValue() : null
    }

    public static toPersistence(medicalConditionEntry: MedicalConditionEntry): any {
        return {
            domainId: medicalConditionEntry.id.toString(),
            entryNumber: medicalConditionEntry.entryNumber,
            medicalRecordNumber: medicalConditionEntry.medicalRecordNumber,
            condition: medicalConditionEntry.condition,
            year: medicalConditionEntry.year
        }
    }

}
