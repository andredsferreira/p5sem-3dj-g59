import { Router } from 'express';
import { celebrate, Joi } from 'celebrate';

import { Container } from 'typedi';
import IMedicalConditionEntryController from '../../controllers/IControllers/IMedicalConditionEntryController';

import config from "../../../config";
import { MedCondition } from '../../domain/medCondition';

const route = Router();

export default (app: Router) => {

    app.use("/medicalConditionEntries", route)
    const ctrl = Container.get(config.controllers.medicalConditionEntry.name) as IMedicalConditionEntryController

    route.post('', //Create
        celebrate({
            body: Joi.object({
                medicalRecordNumber: Joi.string().required(),
                condition: Joi.string().required(),
                year: Joi.number().required()
            })
        }),
        (req, res, next) => ctrl.createMedicalConditionEntry(req, res, next))

    route.get(`/:medicalRecordNumber`, //Get by MRN
        celebrate({
            params: Joi.object({
                medicalRecordNumber: Joi.string().required()
            }),
        }),
        (req, res, next) => ctrl.getMedicalConditionEntryByMedicalRecordNumber(req, res, next)
    )
    route.post(`/search/:medicalRecordNumber`, //Get by MRN and filter after
        celebrate({
            params: Joi.object({
                medicalRecordNumber: Joi.string().required()
            }),
            body: Joi.object({
                condition: Joi.string(),
                year: Joi.number()
            })
        }),
        (req, res, next) => ctrl.searchMedicalConditionEntries(req, res, next)
    )
    route.patch(`/:entryNumber`, //Edit
        celebrate({
            params: Joi.object({
                entryNumber: Joi.string().required()
            }),
            body: Joi.object({
                condition: Joi.string(),
                year: Joi.number()
            })
        }),
        (req, res, next) => ctrl.editMedicalConditionEntry(req, res, next)
    )
}
