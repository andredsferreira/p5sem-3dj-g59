import { Router } from 'express';
import { celebrate, Joi } from 'celebrate';

import { Container } from 'typedi';
import IMedicalRecordController from '../../controllers/IControllers/IMedicalRecordController';

import config from "../../../config";

const route = Router();

export default (app: Router) => {
  app.use('/medicalRecords', route);

  const ctrl = Container.get(config.controllers.medicalRecord.name) as IMedicalRecordController;

  route.get(`/:code`,
    celebrate({
      params: Joi.object({
        code: Joi.string().required()
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

  route.post('/getHistory/:code',
    celebrate({
      params: Joi.object({
        code: Joi.string().required()
      }),
      body: Joi.object({
        password: Joi.string().required()
      })
    }),
    (req, res, next) => ctrl.getHistory(req,res,next));
};