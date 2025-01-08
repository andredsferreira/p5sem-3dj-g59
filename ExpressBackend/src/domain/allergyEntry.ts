import { AggregateRoot } from "../core/domain/AggregateRoot";
import { UniqueEntityID } from "../core/domain/UniqueEntityID";

import { Result } from "../core/logic/Result";
import { MedicalConditionEntryId } from "./medicalConditionEntryId";

import { IMedicalConditionEntryDTO } from "../dto/IMedicalConditionEntryDTO";

interface AllergyEntryProps {
    entryNumber?: string,
    medicalRecordNumber: string,
    allergy: string,
    description: string,
}

export class AllergyEntry extends AggregateRoot<AllergyEntryProps> {

    get id(): UniqueEntityID {
        return this._id
    }

    get medicalConditionEntryId(): MedicalConditionEntryId {
        return new MedicalConditionEntryId(this.medicalConditionEntryId.toValue())
    }

    get entryNumber(): string | undefined {
        return this.props.entryNumber;
    }

    set entryNumber(value: string | undefined) {
        this.props.entryNumber = value;
    }

    get medicalRecordNumber(): string {
        return this.props.medicalRecordNumber;
    }

    set medicalRecordNumber(value: string) {
        this.props.medicalRecordNumber = value;
    }

    get allergy(): string {
        return this.props.allergy;
    }

    set allergy(value: string) {
        this.props.allergy = value;
    }

    get description(): string {
        return this.props.description;
    }

    set description(value: string) {
        this.props.description = value;
    }

    constructor(props: AllergyEntryProps, id?: UniqueEntityID) {
        super(props, id)
    }

    public static create(allergyEntryDTO: IAllergyEntryDTO, id?: UniqueEntityID): Result<AllergyEntry> {
        const allergyEntryProps = {
            entryNumber: allergyEntryDTO.entryNumber,
            medicalRecordNumber: allergyEntryDTO.medicalRecordNumber,
            allergy: allergyEntryDTO.allergy,
            description: allergyEntryDTO.description
        }
        if (allergyEntryProps.entryNumber === null || allergyEntryProps.medicalRecordNumber === null) {
            return Result.fail<AllergyEntry>("allergy name empty")
        }
        const allergy = new AllergyEntry(allergyEntryProps, id);
        return Result.ok<AllergyEntry>(allergy)
    }


}