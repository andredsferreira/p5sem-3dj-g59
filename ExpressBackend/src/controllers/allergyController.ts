import { Request, Response, NextFunction } from 'express';
import { Inject, Service } from 'typedi';
import config from "../../config";

import IAllergyController from './IControllers/IAllergyController';
import IAllergyService from '../services/IServices/IAllergyService';

import { Result } from "../core/logic/Result";
import { BaseController } from '../core/infra/BaseController';
import IAllergyDTO from '../dto/IAllergyDTO';

@Service()
export default class AllergyController implements IAllergyController {

    constructor(@Inject(config.services.allergy.name) private AllergyServiceInstance: IAllergyService) {

    }

    createAllergy(req: Request, res: Response, next: NextFunction) {
        throw new Error('Method not implemented.');
    }

    public async getAllergyByName(req: Request, res: Response, next: NextFunction) {
        try {
            const allergyOrError = await this.AllergyServiceInstance
                .getAllergyByName(req.params.name as string) as Result<IAllergyDTO>
            if (allergyOrError.isFailure) {
                return res.status(402).send()
            }

            const allergyDTO = allergyOrError.getValue()
            return res.json(allergyDTO).status(200)
        } catch (err) {
            return next(err)
        }
    }

}