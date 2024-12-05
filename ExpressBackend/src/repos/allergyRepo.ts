import { Service, Inject } from 'typedi';

import IAllergyRepo from '../services/IRepos/IAllergyRepo';
import { Allergy } from '../domain/allergy';
import { AllergyId } from '../domain/allergyId';
import { AllergyMap } from '../mappers/AllergyMap';

import { Document, FilterQuery, Model } from 'mongoose';
import { IAllergyPersistence } from '../dataschema/IAllergyPersistence';
import { RoleMap } from '../mappers/RoleMap';

@Service()
export default class AllergyRepo implements IAllergyRepo {

    constructor(@Inject("allergySchema") private allergySchema: Model<IAllergyPersistence & Document>) {

    }

    public async save(allergy: Allergy): Promise<Allergy> {
        const query = { domainId: allergy.id.toString() }
        const allergyDoc = await this.allergySchema.findOne(query)

        try {
            if (allergyDoc === null) {
                const rawAllergy: any = AllergyMap.toPersistence(allergy)
                const allergyCreated = await this.allergySchema.create(rawAllergy)
                return AllergyMap.toDomain(allergyCreated)
            }

            allergyDoc.name = allergy.name
            allergyDoc.description = allergy.description
            await allergyDoc.save()
        } catch (error) {
            throw error
        }


        throw new Error('Method not implemented.');
    }

    findByName(name: string): Promise<Allergy> {
        throw new Error('Method not implemented.');
    }

    exists(t: Allergy): Promise<boolean> {
        throw new Error('Method not implemented.');
    }

}
