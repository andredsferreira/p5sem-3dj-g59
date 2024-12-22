// patient.service.ts
import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';
import { BACKEND_API_PATH } from '../config-path';
import { Patient, PatientCreateAttributes, PatientEditAttributes, PatientSearchAttributes } from './patient-types';

@Injectable({
  providedIn: 'root',
})
export class PatientService {
  constructor(private http: HttpClient, @Inject(BACKEND_API_PATH) private apiPath:string) {}

  // Retrieve patients list
  async getPatients(token: string | null, searchableAttributes: PatientSearchAttributes) : Promise<HttpResponse<Patient[]>> {
    if (!token) throw new Error("Token is required");
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });

    const body = Object.fromEntries(
        Object.entries(searchableAttributes).filter(([_, value]) => value != null) // Remove properties with null or undefined values
    );

    const patients = await lastValueFrom(
        this.http.post<Patient[]>(`${this.apiPath}/Patient/Search`, body, { headers, observe: 'response' })
      );
    return patients;
  }

  // Create a new patient
  async createPatient(token: string | null, attributes: PatientCreateAttributes): Promise<HttpResponse<Patient>> {
    if (!token) throw new Error("Token is required");
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });
    const body = Object.fromEntries(
      Object.entries(attributes).filter(([_, value]) => value != null) // Remove properties with null or undefined values
    );
    console.log(body);
    const patient = await lastValueFrom(this.http.post<Patient>(`${this.apiPath}/Patient/Create`, body, {headers, observe: 'response'}));
    return patient;
  }

  // Edit an existing patient
  async editPatient(token: string | null, MedicalRecordNumber: string, attributes: PatientEditAttributes): Promise<HttpResponse<Patient>> {
    if (!token) throw new Error("Token is required");
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });
    const body = Object.fromEntries(
      Object.entries(attributes).filter(([_, value]) => value != null) // Remove properties with null or undefined values
    );
    const patient = await lastValueFrom(this.http.patch<Patient>(`${this.apiPath}/Patient/Edit/${MedicalRecordNumber}`, body, { headers, observe: 'response' }));
    return patient;
  }

  // Delete a patient
  async deletePatient(token: string | null, MedicalRecordNumber: string): Promise<HttpResponse<Patient>> {
    if (!token) throw new Error("Token is required");
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });
    const patient = await lastValueFrom(this.http.delete<Patient>(`${this.apiPath}/Patient/Delete/${MedicalRecordNumber}`, { headers, observe: 'response' }));
    return patient;
  }
}
