// patient.service.ts
import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';
import { API_PATH } from '../config-path';

// Define the Patient interface
interface Patient {
  MedicalRecordNumber: string;
  name: string;
  email: string;
  [key: string]: any;
}

interface PatientCreateAttributes {
  Email: string;
  PhoneNumber: string;
  FullName: string;
  DateOfBirth: string | Date;
  Gender: string;
  Allergies: string;
}

interface PatientSearchAttributes {
  MedicalRecordNumber?: string;
  Email?: string;
  PhoneNumber?: string;
  FullName?: string;
  DateOfBirth?: string | Date;
  Gender?: string;
}

interface PatientEditAttributes {
  Email?: string;
  PhoneNumber?: string;
  FullName?: string;
  Allergies?: string;
}

@Injectable({
  providedIn: 'root',
})
export class PatientService {
  constructor(private http: HttpClient, @Inject(API_PATH) private apiPath:string) {}

  // Retrieve patients list
  async getPatients(token: string | null, searchableAttributes: PatientSearchAttributes) : Promise<any> {
    if (!token) return undefined;
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });

    const body = Object.fromEntries(
        Object.entries(searchableAttributes).filter(([_, value]) => value != null) // Remove properties with null or undefined values
    );

    const patients = await lastValueFrom(
        this.http.post(`${this.apiPath}/Patient/Search`, body, { headers })
      );
    return patients;
  }

  // Create a new patient
  async createPatient(token: string | null, attributes: PatientCreateAttributes): Promise<any> {
    null;
  }

  // Edit an existing patient
  async editPatient(token: string | null, MedicalRecordNumber: string, attributes: PatientEditAttributes): Promise<any> {
    if (!token) return undefined;
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });
    const body = Object.fromEntries(
      Object.entries(attributes).filter(([_, value]) => value != null) // Remove properties with null or undefined values
    );
    const patient = await lastValueFrom(this.http.put(`${this.apiPath}/Patient/Edit/${MedicalRecordNumber}`, body, { headers }));
    return patient;
  }

  // Delete a patient
  async deletePatient(token: string | null, MedicalRecordNumber: string): Promise<any> {
    if (!token) return undefined;
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });
    const patient = await lastValueFrom(this.http.delete(`${this.apiPath}/Patient/Delete/${MedicalRecordNumber}`, { headers }));
    return patient;
  }
}
