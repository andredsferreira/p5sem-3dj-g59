export interface Tab {
    title: string;
    key: string;
    attributes: { key: string; label: string }[];
    serviceMethods: {
      list: (params: any) => Promise<any[]>;
      create: (params: any) => Promise<any>;
      edit: (id: string, params: any) => Promise<any>;
    };
  }
  

export interface FamilyHistoryEntry {
    entryNumber: string,
    relative: string,
    history: string
}

export interface MedicalConditionEntry {
  entryNumber: string,
  condition: string,
  year: string
}