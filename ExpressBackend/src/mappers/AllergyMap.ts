import { Mapper } from "../core/infra/Mapper";

import { Document, Model } from 'mongoose';
import { IAllergyPersistence } from "../dataschema/IAllergyPersistence";

import IAllergyDTO from "../dto/IAllergyDTO";
import { Allergy } from "../domain/allergy";

import { UniqueEntityID } from "../core/domain/UniqueEntityID";

export class AllergyMap extends Mapper<Allergy> {

    public static toDTO(allergy: Allergy): IAllergyDTO {
        throw new Error("not implemented yet")
    }

    public static toDomain(allergy: any | Model<IAllergyPersistence & Document>): Allergy {
        throw new Error("not implemented yet")
    }

    public static toPersistence(allergy: Allergy): any {
        throw new Error("not implemented yet")
    }

}
