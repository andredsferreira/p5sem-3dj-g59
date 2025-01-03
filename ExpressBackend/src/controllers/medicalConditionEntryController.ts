import { Request, Response, NextFunction } from 'express';
import { Inject, Service } from 'typedi';
import config from "../../config";

import IMedicalConditionEntryController from './IControllers/IMedicalConditionEntryController';
import IMedicalConditionEntryService from '../services/IServices/IMedicalConditionEntryService';

import { Result } from "../core/logic/Result";
import { BaseController } from '../core/infra/BaseController';
import { IMedicalConditionEntryDTO, IMedicalConditionEntryOptionalDTO } from '../dto/IMedicalConditionEntryDTO';

@Service()
export default class MedicalConditionEntryController implements IMedicalConditionEntryController {

    constructor(@Inject(config.services.medicalConditionEntry.name) private MedicalConditionEntryServiceInstance: IMedicalConditionEntryService) {
    }

    public async createMedicalConditionEntry(req: Request, res: Response, next: NextFunction) {
        try {
            const medicalConditionEntryOrError = await this.MedicalConditionEntryServiceInstance
                .createMedicalConditionEntry(req.body as IMedicalConditionEntryDTO) as Result<IMedicalConditionEntryDTO>
            if (medicalConditionEntryOrError.isFailure) {
                return res.status(402).send()
            }

            const medicalConditionEntryDTO = medicalConditionEntryOrError.getValue()
            return res.json(medicalConditionEntryDTO).status(200)
        } catch (err) {
            return next(err)
        }
    }

    public async getMedicalConditionEntryByMedicalRecordNumber(req: Request, res: Response, next: NextFunction) {
        try {
            const medicalConditionEntryOrError = await this.MedicalConditionEntryServiceInstance
                .getMedicalConditionEntryByMedicalRecordNumber(req.params.medicalRecordNumber as string) as Result<IMedicalConditionEntryDTO[]>
            if (medicalConditionEntryOrError.isFailure) {
                return res.status(402).send()
            }
            const medicalConditionEntryDTO = medicalConditionEntryOrError.getValue()
            return res.json(medicalConditionEntryDTO).status(200)
        } catch (err) {
            return next(err)
        }
    }
    public async searchMedicalConditionEntries(req: Request, res: Response, next: NextFunction){
        try {
            const medicalConditionEntryOrError = await this.MedicalConditionEntryServiceInstance
                .search(req.params.medicalRecordNumber as string, req.body as IMedicalConditionEntryOptionalDTO) as Result<IMedicalConditionEntryDTO[]>
            if (medicalConditionEntryOrError.isFailure) {
                return res.status(402).send()
            }
            const medicalConditionEntryDTO = medicalConditionEntryOrError.getValue()
            return res.json(medicalConditionEntryDTO).status(200)
        } catch (err) {
            return next(err)
        }
    }
    public async editMedicalConditionEntry(req: Request, res: Response, next: NextFunction) {
        try {
            const medicalConditionEntryOrError = await this.MedicalConditionEntryServiceInstance
                .editMedicalConditionEntry(req.params.entryNumber as string, req.body as IMedicalConditionEntryOptionalDTO) as Result<IMedicalConditionEntryDTO>
            if (medicalConditionEntryOrError.isFailure) {
                return res.status(402).send()
            }

            const medicalConditionEntryDTO = medicalConditionEntryOrError.getValue()
            return res.json(medicalConditionEntryDTO).status(200)
        } catch (err) {
            return next(err)
        }
    }
}