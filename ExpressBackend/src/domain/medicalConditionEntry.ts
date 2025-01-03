import { AggregateRoot } from "../core/domain/AggregateRoot";
import { UniqueEntityID } from "../core/domain/UniqueEntityID";

import { Result } from "../core/logic/Result";
import { MedicalConditionEntryId } from "./medicalConditionEntryId";

import { IMedicalConditionEntryDTO } from "../dto/IMedicalConditionEntryDTO";

interface MedicalConditionEntryProps {
    entryNumber?: string,
    medicalRecordNumber: string,
    condition: string,
    year: number,
}

export class MedicalConditionEntry extends AggregateRoot<MedicalConditionEntryProps> {

    get id(): UniqueEntityID {
        return this._id
    }

    get medicalConditionEntryId(): MedicalConditionEntryId {
        return new MedicalConditionEntryId(this.medicalConditionEntryId.toValue())
    }

    get entryNumber(): string {
        return this.props.entryNumber
    }

    get medicalRecordNumber(): string {
        return this.props.medicalRecordNumber
    }

    get condition(): string {
        return this.props.condition
    }
    
    get year(): number {
        return this.props.year
    }

    set entryNumber(value: string) {
        this.props.entryNumber = value
    }

    set medicalRecordNumber(value: string) {
        this.props.medicalRecordNumber = value
    }

    set condition(value: string) {
        this.props.condition = value
    }

    set year(value: number) {
        this.props.year = value
    }

    constructor(props: MedicalConditionEntryProps, id?: UniqueEntityID) {
        super(props, id)
    }

    public static create(medicalConditionEntryDTO: IMedicalConditionEntryDTO, id?: UniqueEntityID): Result<MedicalConditionEntry> {
        const medicalConditionEntryProps = {
            entryNumber: medicalConditionEntryDTO.entryNumber,
            medicalRecordNumber: medicalConditionEntryDTO.medicalRecordNumber,
            condition: medicalConditionEntryDTO.condition,
            year: medicalConditionEntryDTO.year,
        }

        if (!!medicalConditionEntryProps.condition === false || medicalConditionEntryProps.condition.length === 0) {
            return Result.fail<MedicalConditionEntry>("medicalConditionEntry condition empty")
        }
        if (!!medicalConditionEntryProps.year === false || medicalConditionEntryProps.year.toString().length === 0) {
            return Result.fail<MedicalConditionEntry>("medicalConditionEntry year empty")
        }
        const medicalConditionEntry = new MedicalConditionEntry(medicalConditionEntryProps, id);
        return Result.ok<MedicalConditionEntry>(medicalConditionEntry)
    }

}