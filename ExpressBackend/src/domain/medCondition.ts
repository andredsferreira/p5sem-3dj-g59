import { AggregateRoot } from "../core/domain/AggregateRoot";
import { UniqueEntityID } from "../core/domain/UniqueEntityID";

import { Result } from "../core/logic/Result";
import { MedConditionId } from "./medConditionId";

import IMedConditionDTO from "../dto/IMedConditionDTO";

interface MedConditionProps {
  code: string;
  designation: string;
  description: string;
}

export class MedCondition extends AggregateRoot<MedConditionProps> {

  get id(): UniqueEntityID {
    return this._id;
  }

  get medConditionId(): MedConditionId {
    return new MedConditionId(this.medConditionId.toValue());
  }

  get code(): string {
    return this.props.code;
  }

  get designation(): string {
    return this.props.designation;
  }

  set designation(value: string) {
    this.props.designation = value;
  }

  set code(value: string) {
    this.props.code = value;
  }

  get description(): string {
    return this.props.description;
  }

  set description(value: string) {
    this.props.description = value;
  }

  private constructor(props: MedConditionProps, id?: UniqueEntityID) {
    super(props, id);
  }

  public static create(medConditionDTO: IMedConditionDTO, id?: UniqueEntityID): Result<MedCondition> {
    const designation = medConditionDTO.designation;
    const code = medConditionDTO.code;
    const description = medConditionDTO.description;

    if (!!designation === false || designation.length === 0) {
      return Result.fail<MedCondition>('Must provide a medical condition name') //TO CHANGE
    } else {
      const medCondition = new MedCondition({ code: code, designation: designation, description: description }, id);
      return Result.ok<MedCondition>(medCondition)
    }
  }
}
