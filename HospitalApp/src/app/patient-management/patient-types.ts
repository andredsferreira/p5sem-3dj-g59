  export interface Patient {
    MedicalRecordNumber: string;
    FullName: string;
    Email: string;
    PhoneNumber: string;
    DateOfBirth: string | Date;
    Gender: string;
    Allergies: string;
    [key: string]: any;
  }
  
  export interface PatientCreateAttributes {
    Email: string;
    PhoneNumber: string;
    FullName: string;
    DateOfBirth: string | Date;
    Gender: string;
    Allergies: string;
  }
  
  export interface PatientSearchAttributes {
    MedicalRecordNumber?: string;
    Email?: string;
    PhoneNumber?: string;
    FullName?: string;
    DateOfBirth?: string | Date;
    Gender?: string;
  }
  
  export interface PatientEditAttributes {
    Email?: string;
    PhoneNumber?: string;
    FullName?: string;
    Allergies?: string;
  }