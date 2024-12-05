import { Router } from 'express';
import { celebrate, Joi } from 'celebrate';

import { Container } from 'typedi';
import IMedConditionController from '../../controllers/IControllers/IMedConditionController'; 

import config from "../../../config";

const route = Router();

export default (app: Router) => {
  app.use('/medConditions', route);

  const ctrl = Container.get(config.controllers.medCondition.name) as IMedConditionController;

  route.post('',
    celebrate({
      body: Joi.object({
        name: Joi.string().required(),
        code: Joi.string().required(),
        description: Joi.string()
      })
    }),
    (req, res, next) => ctrl.createMedCondition(req, res, next) );

  route.put('',
    celebrate({
      body: Joi.object({
        id: Joi.string().required(),
        name: Joi.string().required()
      }),
    }),
    (req, res, next) => ctrl.updateMedCondition(req, res, next) );
};