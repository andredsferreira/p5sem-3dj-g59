import { Request, Response, NextFunction } from 'express';
import { Inject, Service } from 'typedi';
import config from "../../config";

import IFamilyHistoryEntryController from './IControllers/IFamilyHistoryEntryController';
import IFamilyHistoryEntryService from '../services/IServices/IFamilyHistoryEntryService';

import { Result } from "../core/logic/Result";
import { BaseController } from '../core/infra/BaseController';
import { IFamilyHistoryEntryDTO, IFamilyHistoryEntryOptionalDTO } from '../dto/IFamilyHistoryEntryDTO';

@Service()
export default class FamilyHistoryEntryController implements IFamilyHistoryEntryController {

    constructor(@Inject(config.services.familyHistoryEntry.name) private FamilyHistoryEntryServiceInstance: IFamilyHistoryEntryService) {
    }

    public async createFamilyHistoryEntry(req: Request, res: Response, next: NextFunction) {
        try {
            const familyHistoryEntryOrError = await this.FamilyHistoryEntryServiceInstance
                .createFamilyHistoryEntry(req.body as IFamilyHistoryEntryDTO) as Result<IFamilyHistoryEntryDTO>
            if (familyHistoryEntryOrError.isFailure) {
                return res.status(402).send()
            }

            const familyHistoryEntryDTO = familyHistoryEntryOrError.getValue()
            return res.json(familyHistoryEntryDTO).status(200)
        } catch (err) {
            return next(err)
        }
    }

    public async getFamilyHistoryEntryByMedicalRecordNumber(req: Request, res: Response, next: NextFunction) {
        try {
            const familyHistoryEntryOrError = await this.FamilyHistoryEntryServiceInstance
                .getFamilyHistoryEntryByMedicalRecordNumber(req.params.medicalRecordNumber as string) as Result<IFamilyHistoryEntryDTO[]>
            if (familyHistoryEntryOrError.isFailure) {
                return res.status(402).send()
            }
            const familyHistoryEntryDTO = familyHistoryEntryOrError.getValue()
            return res.json(familyHistoryEntryDTO).status(200)
        } catch (err) {
            return next(err)
        }
    }
    public async searchFamilyHistoryEntries(req: Request, res: Response, next: NextFunction){
        try {
            const familyHistoryEntryOrError = await this.FamilyHistoryEntryServiceInstance
                .search(req.params.medicalRecordNumber as string, req.body as IFamilyHistoryEntryOptionalDTO) as Result<IFamilyHistoryEntryDTO[]>
            if (familyHistoryEntryOrError.isFailure) {
                return res.status(402).send()
            }
            const familyHistoryEntryDTO = familyHistoryEntryOrError.getValue()
            return res.json(familyHistoryEntryDTO).status(200)
        } catch (err) {
            return next(err)
        }
    }
    public async editFamilyHistoryEntry(req: Request, res: Response, next: NextFunction) {
        try {
            const familyHistoryEntryOrError = await this.FamilyHistoryEntryServiceInstance
                .editFamilyHistoryEntry(req.params.entryNumber as string, req.body as IFamilyHistoryEntryOptionalDTO) as Result<IFamilyHistoryEntryDTO>
            if (familyHistoryEntryOrError.isFailure) {
                return res.status(402).send()
            }

            const familyHistoryEntryDTO = familyHistoryEntryOrError.getValue()
            return res.json(familyHistoryEntryDTO).status(200)
        } catch (err) {
            return next(err)
        }
    }
}