import { Result } from "../../core/logic/Result";
import IMedConditionDTO from "../../dto/IMedConditionDTO";

export default interface IMedConditionService {
  
  createMedCondition(medConditionDTO: IMedConditionDTO): Promise<Result<IMedConditionDTO>>;
  updateMedCondition(medConditionDTO: IMedConditionDTO): Promise<Result<IMedConditionDTO>>;
  removeMedCondition(medConditionDTO: IMedConditionDTO): Promise<Result<IMedConditionDTO>>;

  getMedConditionByCode(medConditionCode: string): Promise<Result<IMedConditionDTO>>;

  getAllMedConditions(): Promise<Result<IMedConditionDTO[]>>;
}
