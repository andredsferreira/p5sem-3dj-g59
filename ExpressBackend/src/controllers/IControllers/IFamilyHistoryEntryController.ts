import { Request, Response, NextFunction } from 'express';

export default interface IFamilyHistoryEntryController {

    createFamilyHistoryEntry(req: Request, res: Response, next: NextFunction)

    getFamilyHistoryEntryByMedicalRecordNumber(req: Request, res: Response, next: NextFunction)

    searchFamilyHistoryEntries(req: Request, res: Response, next: NextFunction)

    editFamilyHistoryEntry(req: Request, res: Response, next: NextFunction)
    
}