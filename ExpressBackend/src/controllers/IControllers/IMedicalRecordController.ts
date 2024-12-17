import { Request, Response, NextFunction } from 'express';

export default interface IMedicalRecordController {
  getMedRecordByMedicalRecordNumber(req: Request, res: Response, next: NextFunction);
  createMedRecord(req: Request, res: Response, next: NextFunction);
}