import { Router } from 'express';
import { celebrate, Joi } from 'celebrate';

import { Container } from 'typedi';
import IFamilyHistoryEntryController from '../../controllers/IControllers/IFamilyHistoryEntryController';

import config from "../../../config";

const route = Router();

export default (app: Router) => {

    app.use("/familyHistoryEntries", route)
    const ctrl = Container.get(config.controllers.familyHistoryEntry.name) as IFamilyHistoryEntryController

    route.post('', //Create
        celebrate({
            body: Joi.object({
                medicalRecordNumber: Joi.string().required(),
                relative: Joi.string().required(),
                history: Joi.string().required()
            })
        }),
        (req, res, next) => ctrl.createFamilyHistoryEntry(req, res, next))

    route.get(`/:medicalRecordNumber`, //Get by MRN
        celebrate({
            params: Joi.object({
                medicalRecordNumber: Joi.string().required()
            }),
        }),
        (req, res, next) => ctrl.getFamilyHistoryEntryByMedicalRecordNumber(req, res, next)
    )
    route.post(`/search/:medicalRecordNumber`, //Get by MRN and filter after
        celebrate({
            params: Joi.object({
                medicalRecordNumber: Joi.string().required()
            }),
            body: Joi.object({
                relative: Joi.string(),
                history: Joi.string()
            })
        }),
        (req, res, next) => ctrl.searchFamilyHistoryEntries(req, res, next)
    )
    route.patch(`/:entryNumber`, //Edit
        celebrate({
            params: Joi.object({
                entryNumber: Joi.string().required()
            }),
            body: Joi.object({
                relative: Joi.string(),
                history: Joi.string()
            })
        }),
        (req, res, next) => ctrl.editFamilyHistoryEntry(req, res, next)
    )
}
