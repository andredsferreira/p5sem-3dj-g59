import { Request, Response, NextFunction } from 'express';
import { Inject, Service } from 'typedi';
import config from "../../config";

import IAllergyController from './IControllers/IAllergyController';
import IAllergyEntryService from '../services/IServices/IAllergyEntryService';

import { Result } from "../core/logic/Result";
import { BaseController } from '../core/infra/BaseController';
import IAllergyDTO from '../dto/IAllergyDTO';
import IAllergyEntryController from './IControllers/IAllergyEntryController';

@Service()
export default class AllergyEntryController implements IAllergyEntryController {

    constructor(@Inject(config.services.allergyEntry.name) private AllergyEntryServiceInstance: IAllergyEntryService) {
    }

    public async createAllergyEntry(req: Request, res: Response, next: NextFunction) {
        try {
            const allergyOrError = await this.AllergyEntryServiceInstance
                .createAllergyEntry(req.body as IAllergyEntryDTO) as Result<IAllergyEntryDTO>
            if (allergyOrError.isFailure) {
                return res.status(402).send()
            }

            const allergyEntryDTO = allergyOrError.getValue()
            return res.json(allergyEntryDTO).status(200)
        } catch (err) {
            return next(err)
        }
    }

    public async searchAllergyEntries(req: Request, res: Response, next: NextFunction){
        try {
            const allergyEntryOrError = await this.AllergyEntryServiceInstance
                .search(req.params.medicalRecordNumber as string, req.body as IAllergyEntryOptionalDTO) as Result<IAllergyEntryDTO[]>
            if (allergyEntryOrError.isFailure) {
                return res.status(402).send()
            }
            const allergyEntryDTO = allergyEntryOrError.getValue()
            return res.json(allergyEntryDTO).status(200)
        } catch (err) {
            return next(err)
        }
    }

    public async getAllergyEntryByMedicalRecordNumber(req: Request, res: Response, next: NextFunction) {
        try {
            const allergyEntryOrError = await this.AllergyEntryServiceInstance
                .getAllergyEntryByMedicalRecordNumber(req.params.medicalRecordNumber as string) as Result<IAllergyEntryDTO[]>
            if (allergyEntryOrError.isFailure) {
                return res.status(402).send()
            }
            const allergyEntryDTO = allergyEntryOrError.getValue()
            return res.json(allergyEntryDTO).status(200)
        } catch (err) {
            return next(err)
        }
    }

    public async updateAllergyEntry(req: Request, res: Response, next: NextFunction) {
        try {
            const allergyOrError = await this.AllergyEntryServiceInstance
                .editAllergyEntry(req.params.entryNumber as string, req.body as IAllergyEntryDTO) as Result<IAllergyEntryDTO>

            if (allergyOrError.isFailure) {
                return res.status(402).send()
            }
            const allergyEntryDTO = allergyOrError.getValue()
            return res.json(allergyEntryDTO).status(200)
        } catch (err) {
            return next(err)
        }


    }

}