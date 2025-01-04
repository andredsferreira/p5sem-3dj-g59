import { Router } from 'express';
import { celebrate, Joi } from 'celebrate';

import { Container } from 'typedi';
import IMedConditionController from '../../controllers/IControllers/IMedConditionController';

import config from "../../../config";

const route = Router();

export default (app: Router) => {
  app.use('/medConditions', route);

  const ctrl = Container.get(config.controllers.medCondition.name) as IMedConditionController;

  // Rota para pegar uma condição médica pelo código
  route.get(`/:code`,
    celebrate({
      params: Joi.object({
        code: Joi.string().required()
      }),
    }),
    (req, res, next) => ctrl.getMedConditionByCode(req, res, next));

  // Rota para pegar todas as condições médicas
  route.get('',
    (req, res, next) => ctrl.getAllMedConditions(req, res, next));

  // Rota para criar uma nova condição médica
  route.post('',
    celebrate({
      body: Joi.object({
        designation: Joi.string().required(),
        code: Joi.string().required(),
        description: Joi.string(),
        symptoms: Joi.array().items(Joi.string()).required()
      })
    }),
    (req, res, next) => ctrl.createMedCondition(req, res, next));

  // Rota para atualizar uma condição médica
  route.patch(`/:code`,
    celebrate({
      params: Joi.object({
        code: Joi.string().required()
      }),
      body: Joi.object({
        designation: Joi.string().optional(),
        description: Joi.string().optional(),
        symptoms: Joi.array().items(Joi.string()).optional()
      }),
    }),
    (req, res, next) => ctrl.updateMedCondition(req, res, next));
};
