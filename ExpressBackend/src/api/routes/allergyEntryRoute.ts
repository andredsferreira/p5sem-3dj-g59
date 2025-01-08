import { Router } from 'express';
import { celebrate, Joi } from 'celebrate';

import { Container } from 'typedi';
import IAllergyController from '../../controllers/IControllers/IAllergyController';

import config from "../../../config";
import IAllergyEntryController from '../../controllers/IControllers/IAllergyEntryController';

const route = Router();

export default (app: Router) => {

    app.use("/allergyentries", route)
    const ctrl = Container.get(config.controllers.allergyEntry.name) as IAllergyEntryController

    route.post('',
        celebrate({
            body: Joi.object({
                code: Joi.string().required(),
                name: Joi.string().required(),
                description: Joi.string()
            })
        }),
        (req, res, next) => ctrl.createAllergyEntry(req, res, next))

    route.get(`/:entryNumber`,
        celebrate({
            params: Joi.object({
                entryNumber: Joi.string().required()
            }),
        }),
        (req, res, next) => ctrl.getAllergyEntryByNumber(req, res, next)
    )

    route.patch(`/:entryNumber`,
        celebrate({
            params: Joi.object({
                entryNumber: Joi.string().required(),
            }),
            body: Joi.object({
                medicalRecordNumber: Joi.string(),
                allergy: Joi.string(),
                description: Joi.string()
            })
        }),
        (req, res, next) => ctrl.updateAllergyEntry(req, res, next)
    )


}
