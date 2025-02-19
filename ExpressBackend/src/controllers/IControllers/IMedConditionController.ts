import { Request, Response, NextFunction } from 'express';

export default interface IMedConditionController {
  getAllMedConditions(req: Request, res: Response, next: NextFunction);
  getMedConditionByCode(req: Request, res: Response, next: NextFunction);
  createMedCondition(req: Request, res: Response, next: NextFunction);
  updateMedCondition(req: Request, res: Response, next: NextFunction);
}
