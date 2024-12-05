import { Repo } from "../../core/infra/Repo";
import { MedCondition } from "../../domain/medCondition";
import { MedConditionId } from "../../domain/medConditionId";

export default interface IMedConditionRepo extends Repo<MedCondition> {
  save(MedCondition: MedCondition): Promise<MedCondition>;
  findByDomainId (MedConditionId: MedConditionId | string): Promise<MedCondition>;
  remove(MedConditionId: MedConditionId | string): Promise<MedCondition>;
    
  //findByIds (MedConditionsIds: MedConditionId[]): Promise<MedCondition[]>;
  //saveCollection (MedConditions: MedCondition[]): Promise<MedCondition[]>;
  //removeByMedConditionIds (MedConditions: MedConditionId[]): Promise<any>
}