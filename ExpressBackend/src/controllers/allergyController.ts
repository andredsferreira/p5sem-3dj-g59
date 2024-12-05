import { Request, Response, NextFunction } from 'express';
import { Inject, Service } from 'typedi';
import config from "../../config";

import IAllergyController from './IControllers/IAllergyController';
import IAllergyService from '../services/IServices/IAllergyService';

import { Result } from "../core/logic/Result";
import { BaseController } from '../core/infra/BaseController';

@Service()
export default class AllergyController implements IAllergyController {

    constructor(@Inject(config.services.allergy.name) private AllergyServiceInstance: IAllergyService) {

    }

    createAllergy(req: Request, res: Response, next: NextFunction) {
        throw new Error('Method not implemented.');
    }

    getAllergyByName(req: Request, res: Response, next: NextFunction) {
        throw new Error('Method not implemented.');
    }

}