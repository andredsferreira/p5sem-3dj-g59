import { Service, Inject } from 'typedi';

import IMedConditionRepo from "../services/IRepos/IMedConditionRepo";
import { MedCondition } from "../domain/medCondition";
import { MedConditionId } from "../domain/medConditionId";
import { MedConditionMap } from "../mappers/MedConditionMap";

import { Document, FilterQuery, Model } from 'mongoose';
import { IMedConditionPersistence } from '../dataschema/IMedConditionPersistence';

@Service()
export default class MedConditionRepo implements IMedConditionRepo {
  private models: any;

  constructor(
    @Inject('medConditionSchema') private MedConditionSchema : Model<IMedConditionPersistence & Document>,
  ) {}

  private createBaseQuery (): any {
    return {
      where: {},
    }
  }

  public async exists(MedCondition: MedCondition): Promise<boolean> {
    
    const idX = MedCondition.id instanceof MedConditionId ? (<MedConditionId>MedCondition.id).toValue() : MedCondition.id;

    const query = { domainId: idX}; 
    const MedConditionDocument = await this.MedConditionSchema.findOne( query as FilterQuery<IMedConditionPersistence & Document>);

    return !!MedConditionDocument === true;
  }

  public async save (MedCondition: MedCondition): Promise<MedCondition> {
    const query = { domainId: MedCondition.id.toString()}; 

    const MedConditionDocument = await this.MedConditionSchema.findOne( query );

    try {
      if (MedConditionDocument === null ) {
        const rawMedCondition: any = MedConditionMap.toPersistence(MedCondition);

        const MedConditionCreated = await this.MedConditionSchema.create(rawMedCondition);

        return MedConditionMap.toDomain(MedConditionCreated);
      } else {
        MedConditionDocument.name = MedCondition.name;
        await MedConditionDocument.save();

        return MedCondition;
      }
    } catch (err) {
      throw err;
    }
  }

  public async remove (MedConditionId: MedConditionId | string): Promise<MedCondition> {
    const MedCondition = await this.findByDomainId(MedConditionId);

    try {
      if (MedCondition === null ) return null;
      else {

        const rawMedCondition: any = MedConditionMap.toPersistence(MedCondition);
        const medConditionDeleted = await this.MedConditionSchema.remove(rawMedCondition);

        return MedConditionMap.toDomain(medConditionDeleted);
      }
    } catch (err) {
      throw err;
    }
  }

  public async findByDomainId (MedConditionId: MedConditionId | string): Promise<MedCondition> {
    const query = { domainId: MedConditionId};
    const MedConditionRecord = await this.MedConditionSchema.findOne( query as FilterQuery<IMedConditionPersistence & Document> );

    if( MedConditionRecord != null) {
      return MedConditionMap.toDomain(MedConditionRecord);
    }
    else
      return null;
  }
}