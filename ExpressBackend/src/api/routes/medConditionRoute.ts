import { Router } from 'express';
import { celebrate, Joi } from 'celebrate';

import { Container } from 'typedi';
import IMedConditionController from '../../controllers/IControllers/IMedConditionController'; 

import config from "../../../config";

const route = Router();

export default (app: Router) => {
  app.use('/medConditions', route);

  const ctrl = Container.get(config.controllers.medCondition.name) as IMedConditionController;
  
  route.get(`/:code`,
    celebrate({
      params: Joi.object({
        code: Joi.string().required()
      }),
    }),
    (req, res, next) => ctrl.getMedConditionByCode(req, res, next));

  route.post('',
    celebrate({
      body: Joi.object({
        designation: Joi.string().required(),
        code: Joi.string().required(),
        description: Joi.string()
      })
    }),
    (req, res, next) => ctrl.createMedCondition(req, res, next) );

  route.put(`/:code`,
    celebrate({
      params: Joi.object({
        code: Joi.string().required()
      }),
      body: Joi.object({
        designation: Joi.string().required(),
        description: Joi.string().required()
      }),
    }),
    (req, res, next) => ctrl.updateMedCondition(req, res, next));
};