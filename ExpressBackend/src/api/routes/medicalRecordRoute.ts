import { Router } from 'express';
import { celebrate, Joi } from 'celebrate';

import { Container } from 'typedi';
import IMedicalRecordController from '../../controllers/IControllers/IMedicalRecordController';

import config from "../../../config";

const route = Router();

export default (app: Router) => {
  app.use('/medicalRecords', route);

  const ctrl = Container.get(config.controllers.medicalRecord.name) as IMedicalRecordController;

  route.get(`/:medicalRecordNumber`,
    celebrate({
      params: Joi.object({
        medicalRecordNumber: Joi.string().required()
      }),
    }),
    (req, res, next) => ctrl.getMedRecordByMedicalRecordNumber(req, res, next));

  route.post('',
    celebrate({
      body: Joi.object({
        medicalRecordNumber: Joi.string().required()
      })
    }),
    (req, res, next) => ctrl.createMedRecord(req, res, next));
};