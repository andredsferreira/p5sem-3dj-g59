import { AggregateRoot } from "../core/domain/AggregateRoot";
import { UniqueEntityID } from "../core/domain/UniqueEntityID";

import { Result } from "../core/logic/Result";
import { MedConditionId } from "./medConditionId";

import IMedConditionDTO from "../dto/IMedConditionDTO";

interface MedConditionProps {
  code: string;
  name: string;
  description?: string;
}

export class MedCondition extends AggregateRoot<MedConditionProps> {
  get id (): UniqueEntityID {
    return this._id;
  }

  get medConditionId (): MedConditionId {
    return new MedConditionId(this.medConditionId.toValue());
  }

  get name (): string {
    return this.props.name;
  }

  set name ( value: string) {
    this.props.name = value;
  }
  
  get code (): string {
    return this.props.code;
  }

  set code ( value: string) {
    this.props.code = value;
  }
  
  get description (): string {
    return this.props.description;
  }

  set description ( value: string) {
    this.props.description = value;
  }

  private constructor (props: MedConditionProps, id?: UniqueEntityID) {
    super(props, id);
  }

  public static create (medConditionDTO: IMedConditionDTO, id?: UniqueEntityID): Result<MedCondition> {
    const name = medConditionDTO.name;
    const code = medConditionDTO.code;
    const description = medConditionDTO.description;

    if (!!name === false || name.length === 0) {
      return Result.fail<MedCondition>('Must provide a medical condition name')
    } else {
      const medCondition = new MedCondition({ code: code, name: name, description: description }, id);
      return Result.ok<MedCondition>( medCondition )
    }
  }
}
