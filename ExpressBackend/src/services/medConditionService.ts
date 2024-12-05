import { Service, Inject } from 'typedi';
import config from "../../config";
import IMedConditionDTO from '../dto/IMedConditionDTO';
import { MedCondition } from "../domain/medCondition";
import IMedConditionRepo from './IRepos/IMedConditionRepo';
import IMedConditionService from './IServices/IMedConditionService';
import { Result } from "../core/logic/Result";
import { MedConditionMap } from "../mappers/MedConditionMap";

@Service()
export default class MedConditionService implements IMedConditionService {
  constructor(
      @Inject(config.repos.medCondition.name) private MedConditionRepo : IMedConditionRepo
  ) {}

  public async getMedConditionByCode( MedConditionCode: string): Promise<Result<IMedConditionDTO>> {
    try {
      const MedCondition = await this.MedConditionRepo.findByCode(MedConditionCode);

      if (MedCondition === null) {
        return Result.fail<IMedConditionDTO>("MedCondition not found");
      }
      else {
        const MedConditionDTOResult = MedConditionMap.toDTO( MedCondition ) as IMedConditionDTO;
        return Result.ok<IMedConditionDTO>( MedConditionDTOResult )
        }
    } catch (e) {
      throw e;
    }
  }


  public async createMedCondition(MedConditionDTO: IMedConditionDTO): Promise<Result<IMedConditionDTO>> {
    try {

      const MedConditionOrError = await MedCondition.create( MedConditionDTO );

      if (MedConditionOrError.isFailure) {
        return Result.fail<IMedConditionDTO>(MedConditionOrError.errorValue());
      }

      const MedConditionResult = MedConditionOrError.getValue();

      await this.MedConditionRepo.save(MedConditionResult);

      const MedConditionDTOResult = MedConditionMap.toDTO( MedConditionResult ) as IMedConditionDTO;
      return Result.ok<IMedConditionDTO>( MedConditionDTOResult )
    } catch (e) {
      throw e;
    }
  }

  public async updateMedCondition(MedConditionDTO: IMedConditionDTO): Promise<Result<IMedConditionDTO>> {
    try {
      const MedCondition = await this.MedConditionRepo.findByCode(MedConditionDTO.code);

      if (MedCondition === null) {
        return Result.fail<IMedConditionDTO>("MedCondition not found");
      }
      else {
        MedCondition.designation = MedConditionDTO.designation;
        MedCondition.description = MedConditionDTO.description;
        await this.MedConditionRepo.save(MedCondition);

        const MedConditionDTOResult = MedConditionMap.toDTO( MedCondition ) as IMedConditionDTO;
        return Result.ok<IMedConditionDTO>( MedConditionDTOResult )
        }
    } catch (e) {
      throw e;
    }
  }

  public async removeMedCondition(MedConditionDTO: IMedConditionDTO): Promise<Result<IMedConditionDTO>> {
    try {
      const MedCondition = await this.MedConditionRepo.findByCode(MedConditionDTO.code);

      if (MedCondition === null) {
        return Result.fail<IMedConditionDTO>("MedCondition not found");
      }
      else {
        await this.MedConditionRepo.remove(MedCondition.code);

        const MedConditionDTOResult = MedConditionMap.toDTO( MedCondition ) as IMedConditionDTO;
        return Result.ok<IMedConditionDTO>( MedConditionDTOResult )
        }
    } catch (e) {
      throw e;
    }
  }
}
