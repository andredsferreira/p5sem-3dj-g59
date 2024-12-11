import { AggregateRoot } from "../core/domain/AggregateRoot"
import { UniqueEntityID } from "../core/domain/UniqueEntityID"
import { Result } from "../core/logic/Result"
import IMedicalRecordDTO from "../dto/IMedicalRecordDTO"
import { AllergyMap } from "../mappers/AllergyMap"
import { Allergy } from "./allergy"
import { MedCondition } from "./medCondition"
import { MedicalRecordId } from "./medicalRecordId"


interface MedicalRecordProps {
    code: string,
    allergies: Allergy[],
    medicalConditions: MedCondition[],
    freeText: string[]
}

export class MedicalRecord extends AggregateRoot<MedicalRecordProps> {

    get id(): UniqueEntityID {
        return this._id
    }

    get medicalRecordId(): MedicalRecordId {
        return new MedicalRecordId(this.medicalRecordId.toValue())
    }

    get code(): string {
        return this.props.code
    }

    get allergies(): Allergy[] {
        return this.props.allergies
    }

    get medicalConditions(): MedCondition[] {
        return this.props.medicalConditions
    }

    get freeText(): string[] {
        return this.props.freeText
    }

    set code(value: string) {
        this.props.code = value
    }

    set allergies(value: Allergy[]) {
        this.props.allergies = value
    }

    set medicalConditions(value: MedCondition[]) {
        this.props.medicalConditions = value
    }

    set freeText(value: string[]) {
        this.props.freeText = value
    }

    constructor(props: MedicalRecordProps, id?: MedicalRecordId) {
        super(props, id)
    }

    public static create(medicalRecordDTO: IMedicalRecordDTO, id?: UniqueEntityID): Result<MedicalRecord> {
        const allergies: Allergy[] = medicalRecordDTO.allergies
            .map(allergyDTO => Allergy.create(allergyDTO))
            .filter(result => result.isSuccess)
            .map(result => result.getValue())
        const medicalConditions: MedCondition[] = medicalRecordDTO.medicalConditions
            .map(medicalConditionDTO => MedCondition.create(medicalConditionDTO))
            .filter(result => result.isSuccess)
            .map(result => result.getValue())
        const medicalRecordProps = {
            code: medicalRecordDTO.code,
            allergies: allergies,
            medicalConditions: medicalConditions,
            freeText: medicalRecordDTO.freeText
        }
        const medicalRecord = new MedicalRecord(medicalRecordProps, id)
        return Result.ok<MedicalRecord>(medicalRecord)
    }



}