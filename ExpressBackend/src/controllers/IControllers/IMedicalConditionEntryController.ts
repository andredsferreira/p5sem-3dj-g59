import { Request, Response, NextFunction } from 'express';

export default interface IMedicalConditionEntryController {

    createMedicalConditionEntry(req: Request, res: Response, next: NextFunction)

    getMedicalConditionEntryByMedicalRecordNumber(req: Request, res: Response, next: NextFunction)

    searchMedicalConditionEntries(req: Request, res: Response, next: NextFunction)

    editMedicalConditionEntry(req: Request, res: Response, next: NextFunction)
    
}