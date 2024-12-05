import { Service, Inject } from 'typedi';

import IAllergyRepo from '../services/IRepos/IAllergyRepo';
import { Allergy } from '../domain/allergy';
import { AllergyId } from '../domain/allergyId';
import { AllergyMap } from '../mappers/AllergyMap';

import { Document, FilterQuery, Model } from 'mongoose';
import { IMedConditionPersistence } from '../dataschema/IMedConditionPersistence';

@Service()
export default class AllergyRepo implements IAllergyRepo {

    save(allergy: Allergy): Promise<Allergy> {
        throw new Error('Method not implemented.');
    }

    findByName(name: string): Promise<Allergy> {
        throw new Error('Method not implemented.');
    }
    
    exists(t: Allergy): Promise<boolean> {
        throw new Error('Method not implemented.');
    }

}
