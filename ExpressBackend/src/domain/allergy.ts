import { AggregateRoot } from "../core/domain/AggregateRoot";
import { UniqueEntityID } from "../core/domain/UniqueEntityID";

import { Result } from "../core/logic/Result";
import { AllergyId } from "./allergyId";

import IAllergyDTO from "../dto/IAllergyDTO";

interface AllergyProps {
    code: string,
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

    get code(): string {
        return this.props.code
    }
    
    get name(): string {
        return this.props.name
    }

    get description(): string {
        return this.props.description
    }

    set code(value: string) {
        this.props.code = value
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
            code: allergyDTO.code,
            name: allergyDTO.name,
            description: allergyDTO.description
        }

        if (!!allergyProps.name === false || allergyProps.name.length === 0) {
            return Result.fail<Allergy>("allergy name empty")
        }

        const allergy = new Allergy(allergyProps, id);
        return Result.ok<Allergy>(allergy)
    }

}