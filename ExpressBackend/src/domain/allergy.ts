import { AggregateRoot } from "../core/domain/AggregateRoot";
import { UniqueEntityID } from "../core/domain/UniqueEntityID";

import { Result } from "../core/logic/Result";
import { AllergyId } from "./allergyId";

import IAllergyDTO from "../dto/IAllergyDTO";

interface AllergyProps {
    name: string,
    description: string
}

export class Allergy extends AggregateRoot<AllergyProps> {

    get id(): UniqueEntityID {
        return this._id
    }

    get allergyId(): AllergyId {
        return new AllergyId(this.allergyId.toValue())
    }

    get name(): string {
        return this.props.name
    }

    get description(): string {
        return this.props.description
    }

    set name(value: string) {
        this.props.name = value
    }

    set description(value: string) {
        this.props.description = value
    }

    constructor(props: AllergyProps, id?: UniqueEntityID) {
        super(props, id)
    }

    public static create(allergyDTO: IAllergyDTO, id?: UniqueEntityID): Result<Allergy> {
        const allergyProps = {
            name: allergyDTO.name,
            description: allergyDTO.description
        }

        // TODO: Validate fields of allergy

        const allergy = new Allergy(allergyProps, id);
        return Result.ok<Allergy>(allergy)
    }

}