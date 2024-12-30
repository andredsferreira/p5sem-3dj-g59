// patient.service.ts
import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';
import { BACKEND_API_PATH, EXPRESS_BACKEND_API_PATH } from '../config-path';
import { Patient } from '../patient-management/patient-types';

@Injectable({
  providedIn: 'root',
})
export class ProfileService {
  constructor(
    private http: HttpClient, 
    @Inject(BACKEND_API_PATH) private backendApiPath:string,
    @Inject(EXPRESS_BACKEND_API_PATH) private expressApiPath: string,
  ) {}

  async getMRNFromUserEmail(token: string | null, email: string) {
    if (!token) throw new Error("Token is required");
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });
    return await lastValueFrom(this.http.get<Patient>(`${this.backendApiPath}/Patient/${email}`, {headers, observe: 'response'}));
  }

  async getHistoryZip(token: string | null, medicalRecordNumber: string, password: string) {
    if (!token) throw new Error("Token is required");
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });

    return this.http.post(`${this.expressApiPath}/medicalRecords/getHistory/${medicalRecordNumber}`, {password: password}, {headers, responseType: 'blob'});
  }
}
