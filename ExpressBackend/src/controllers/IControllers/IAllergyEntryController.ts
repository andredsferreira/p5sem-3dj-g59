import { Request, Response, NextFunction } from 'express';

export default interface IAllergyEntryController {

    createAllergyEntry(req: Request, res: Response, next: NextFunction)

    getAllergyEntryByNumber(req: Request, res: Response, next: NextFunction)

    updateAllergyEntry(req: Request, res: Response, next: NextFunction)
    
}