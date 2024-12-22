import { AggregateRoot } from "../core/domain/AggregateRoot";
import { UniqueEntityID } from "../core/domain/UniqueEntityID";

import { Result } from "../core/logic/Result";
import { FamilyHistoryEntryId } from "./familyHistoryEntryId";

import { IFamilyHistoryEntryDTO } from "../dto/IFamilyHistoryEntryDTO";

interface FamilyHistoryEntryProps {
    entryNumber?: string,
    medicalRecordNumber: string,
    relative: string,
    history: string,
}

export class FamilyHistoryEntry extends AggregateRoot<FamilyHistoryEntryProps> {

    get id(): UniqueEntityID {
        return this._id
    }

    get familyHistoryEntryId(): FamilyHistoryEntryId {
        return new FamilyHistoryEntryId(this.familyHistoryEntryId.toValue())
    }

    get entryNumber(): string {
        return this.props.entryNumber
    }

    get medicalRecordNumber(): string {
        return this.props.medicalRecordNumber
    }

    get relative(): string {
        return this.props.relative
    }
    
    get history(): string {
        return this.props.history
    }

    set entryNumber(value: string) {
        this.props.entryNumber = value
    }

    set medicalRecordNumber(value: string) {
        this.props.medicalRecordNumber = value
    }

    set relative(value: string) {
        this.props.relative = value
    }

    set history(value: string) {
        this.props.history = value
    }

    constructor(props: FamilyHistoryEntryProps, id?: UniqueEntityID) {
        super(props, id)
    }

    public static create(familyHistoryEntryDTO: IFamilyHistoryEntryDTO, id?: UniqueEntityID): Result<FamilyHistoryEntry> {
        const familyHistoryEntryProps = {
            entryNumber: familyHistoryEntryDTO.entryNumber,
            medicalRecordNumber: familyHistoryEntryDTO.medicalRecordNumber,
            relative: familyHistoryEntryDTO.relative,
            history: familyHistoryEntryDTO.history,
        }

        if (!!familyHistoryEntryProps.relative === false || familyHistoryEntryProps.relative.length === 0) {
            return Result.fail<FamilyHistoryEntry>("familyHistoryEntry relative empty")
        }
        if (!!familyHistoryEntryProps.history === false || familyHistoryEntryProps.history.length === 0) {
            return Result.fail<FamilyHistoryEntry>("familyHistoryEntry history empty")
        }
        const familyHistoryEntry = new FamilyHistoryEntry(familyHistoryEntryProps, id);
        return Result.ok<FamilyHistoryEntry>(familyHistoryEntry)
    }

}