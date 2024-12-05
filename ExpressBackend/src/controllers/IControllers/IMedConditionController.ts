import { Request, Response, NextFunction } from 'express';

export default interface IMedConditionController {
  getMedConditionByCode(req: Request, res: Response, next: NextFunction);
  createMedCondition(req: Request, res: Response, next: NextFunction);
  updateMedCondition(req: Request, res: Response, next: NextFunction);
}