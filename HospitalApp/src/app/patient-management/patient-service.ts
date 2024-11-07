// patient.service.ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';
import * as jwt_decode from "jwt-decode";

// Define the Patient interface
interface Patient {
  MedicalRecordNumber: string;
  name: string;
  email: string;
  [key: string]: any;
}

interface PatientSearchAttributes {
  MedicalRecordNumber?: string;
  Email?: string;
  PhoneNumber?: string;
  FullName?: string;
  DateOfBirth?: string | Date;
  Gender?: string;
}

@Injectable({
  providedIn: 'root',
})
export class PatientService {
  constructor(private http: HttpClient) {}
  private patients: Patient[] = [
    { MedicalRecordNumber: "temp1", name: 'Patient 1', email: 'patient1@example.com' },
    { MedicalRecordNumber: "temp2", name: 'Patient 2', email: 'patient2@example.com' },
  ];

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
        this.http.post('https://localhost:5001/api/Patient/Search', body, { headers })
      );
    return patients;
  }

  // Create a new patient
  createPatient(newPatient: Patient): void {
    this.patients.push(newPatient);
  }

  // Edit an existing patient
  editPatient(updatedPatient: Patient): void {
    const index = this.patients.findIndex((p) => p.MedicalRecordNumber === updatedPatient.MedicalRecordNumber);
    if (index !== -1) {
      this.patients[index] = updatedPatient;
    }
  }

  // Delete a patient
  async deletePatient(token: string | null, MedicalRecordNumber: string): Promise<any> {
    if (!token) return undefined;
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });
    const patient = await lastValueFrom(this.http.delete("https://localhost:5001/api/Patient/Delete/" + MedicalRecordNumber, { headers }));
    return patient;
  }
}
