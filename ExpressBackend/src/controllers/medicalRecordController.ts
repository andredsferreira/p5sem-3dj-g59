import { Request, Response, NextFunction } from 'express';
import { Inject, Service } from 'typedi';
import config from "../../config";

import IMedicalRecordController from "./IControllers/IMedicalRecordController";
import IMedicalRecordService from '../services/IServices/IMedicalRecordService';
import IMedicalRecordDTO from '../dto/IMedicalRecordDTO';

import { Result } from "../core/logic/Result";
import { BaseController } from '../core/infra/BaseController';

@Service()
export default class MedicalRecordController /* TODO extends BaseController */ implements IMedicalRecordController {
  constructor(
      @Inject(config.services.medicalRecord.name) private MedRecordServiceInstance : IMedicalRecordService
  ){}

  public async getMedRecordByMedicalRecordNumber(req: Request, res: Response, next: NextFunction) {
    try {
      const MedRecordOrError = await this.MedRecordServiceInstance.getMedRecordByMedicalRecordNumber(req.params.code as string) as Result<IMedicalRecordDTO>;

      if (MedRecordOrError.isFailure) {
        return res.status(402).send();
      }

      const MedRecordDTO = MedRecordOrError.getValue();
      return res.json( MedRecordDTO ).status(200);
    }
    catch (e) {
      return next(e);
    }
  };

  public async createMedRecord(req: Request, res: Response, next: NextFunction) {
    try {
      const MedRecordOrError = await this.MedRecordServiceInstance.createMedRecord(req.body as IMedicalRecordDTO) as Result<IMedicalRecordDTO>;
        
      if (MedRecordOrError.isFailure) {
        return res.status(402).send();
      }

      const MedRecordDTO = MedRecordOrError.getValue();
      return res.json( MedRecordDTO ).status(201);
    }
    catch (e) {
      return next(e);
    }
  };

  public async getHistory(req: Request, res: Response, next: NextFunction) {
    try {
      const zipOrError = await this.MedRecordServiceInstance.generateMedicalRecordZip(
        req.params.code as string, req.body.password as string
      ) as Result<string>;

      if (zipOrError.isFailure) {
        return res.status(402).send(zipOrError.errorValue());
      }

      const zipPath = zipOrError.getValue();

      res.download(zipPath, 'medical_record.zip', () => {
        this.MedRecordServiceInstance.cleanUp(zipPath);
      });
    }
    catch (e) {
      return next(e);
    }
  }
}