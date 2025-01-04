import { Request, Response, NextFunction } from 'express';
import { Inject, Service } from 'typedi';
import config from "../../config";

import IMedConditionController from "./IControllers/IMedConditionController";
import IMedConditionService from '../services/IServices/IMedConditionService';
import IMedConditionDTO from '../dto/IMedConditionDTO';

import { Result } from "../core/logic/Result";
import { BaseController } from '../core/infra/BaseController';

@Service()
export default class MedConditionController /* TODO extends BaseController */ implements IMedConditionController {
  constructor(
      @Inject(config.services.medCondition.name) private MedConditionServiceInstance : IMedConditionService
  ){}

  public async getMedConditionByCode(req: Request, res: Response, next: NextFunction) {
    try {
      const MedConditionOrError = await this.MedConditionServiceInstance.getMedConditionByCode(req.params.code as string) as Result<IMedConditionDTO>;

      if (MedConditionOrError.isFailure) {
        return res.status(404).send();
      }

      const MedConditionDTO = MedConditionOrError.getValue();
      return res.json( MedConditionDTO ).status(200);
    }
    catch (e) {
      return next(e);
    }
  };

  public async createMedCondition(req: Request, res: Response, next: NextFunction) {
    try {
      const MedConditionOrError = await this.MedConditionServiceInstance.createMedCondition(req.body as IMedConditionDTO) as Result<IMedConditionDTO>;
        
      if (MedConditionOrError.isFailure) {
        return res.status(404).send();
      }

      const MedConditionDTO = MedConditionOrError.getValue();
      return res.json( MedConditionDTO ).status(201);
    }
    catch (e) {
      return next(e);
    }
  };

  public async updateMedCondition(req: Request, res: Response, next: NextFunction) {
    try {
      const medConditionDTO: IMedConditionDTO = {
        code: req.params.code,
        designation: req.body.designation,
        description: req.body.description,
        symptoms: req.body.symptoms
      }
      const MedConditionOrError = await this.MedConditionServiceInstance.updateMedCondition(medConditionDTO) as Result<IMedConditionDTO>;

      if (MedConditionOrError.isFailure) {
        return res.status(404).send();
      }

      const MedConditionDTO = MedConditionOrError.getValue();
      return res.status(200).json( MedConditionDTO );
    }
    catch (e) {
      return next(e);
    }
  };

  public async getAllMedConditions(req: Request, res: Response, next: NextFunction) {
    try {
      const allMedConditionsOrError = await this.MedConditionServiceInstance.getAllMedConditions() as Result<IMedConditionDTO[]>;
  
      if (allMedConditionsOrError.isFailure) {
        return res.status(404).send();
      }
  
      const allMedConditionsDTO = allMedConditionsOrError.getValue();
      return res.status(200).json(allMedConditionsDTO);
    } catch (e) {
      return next(e);
    }
  }
  



}