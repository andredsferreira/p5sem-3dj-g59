import { Mapper } from "../core/infra/Mapper";

import { Document, Model } from 'mongoose';
import { IMedConditionPersistence } from '../dataschema/IMedConditionPersistence';

import IMedConditionDTO from "../dto/IMedConditionDTO";
import { MedCondition } from "../domain/medCondition";

import { UniqueEntityID } from "../core/domain/UniqueEntityID";

export class MedConditionMap extends Mapper<MedCondition> {
  
  public static toDTO( medCondition: MedCondition): IMedConditionDTO {
    return {
      code: medCondition.code,
      designation: medCondition.designation,
      description: medCondition.description,
    } as IMedConditionDTO;
  }

  public static toDomain (medCondition: any | Model<IMedConditionPersistence & Document> ): MedCondition {
    const medConditionOrError = MedCondition.create(
      medCondition,
      new UniqueEntityID(medCondition.domainId)
    );

    medConditionOrError.isFailure ? console.log(medConditionOrError.error) : '';

    return medConditionOrError.isSuccess ? medConditionOrError.getValue() : null;
  }

  public static toPersistence (medCondition: MedCondition): any {
    return {
      domainId: medCondition.id.toString(),
      code: medCondition.code,
      designation: medCondition.designation,
      description: medCondition.description,
    }
  }
}