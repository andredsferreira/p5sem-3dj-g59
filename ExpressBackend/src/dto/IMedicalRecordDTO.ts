import IAllergyDTO from "./IAllergyDTO";
import IMedConditionDTO from "./IMedConditionDTO";

export default interface IMedicalRecordDTO {
    code: string,
    allergies: IAllergyDTO[],
    medicalConditions: IMedConditionDTO[],
    freeText: string[]
}