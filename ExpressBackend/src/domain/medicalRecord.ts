import { AggregateRoot } from "../core/domain/AggregateRoot";
import { UniqueEntityID } from "../core/domain/UniqueEntityID";

import { Result } from "../core/logic/Result";
import { MedicalRecordId } from "./medicalRecordId";

import IMedicalRecordDTO from "../dto/IMedicalRecordDTO";

interface MedicalRecordProps {
  medicalRecordNumber: string;
}

export class MedicalRecord extends AggregateRoot<MedicalRecordProps> {
  
  get id(): UniqueEntityID {
    return this._id;
  }

  get MedicalRecordId(): MedicalRecordId {
    return new MedicalRecordId(this.MedicalRecordId.toValue());
  }

  get medicalRecordNumber(): string {
    return this.props.medicalRecordNumber;
  }

  set medicalRecordNumber(value: string) {
    this.props.medicalRecordNumber = value;
  }

  private constructor(props: MedicalRecordProps, id?: UniqueEntityID) {
    super(props, id);
  }

  public static create(MedicalRecordDTO: IMedicalRecordDTO, id?: UniqueEntityID): Result<MedicalRecord> {
    const medicalRecordNumber = MedicalRecordDTO.medicalRecordNumber;

    if (!!medicalRecordNumber === false || medicalRecordNumber.length === 0) {
      return Result.fail<MedicalRecord>('Must provide a Medical Record Number')
    } else {
      const medicalRecord = new MedicalRecord({ medicalRecordNumber: medicalRecordNumber }, id);
      return Result.ok<MedicalRecord>(medicalRecord)
    }
  }
}
