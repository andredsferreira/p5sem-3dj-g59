export interface IMedConditionPersistence {
  domainId: string;
  code: string;
  designation: string;
  description?: string;
  symptoms: string[];
}