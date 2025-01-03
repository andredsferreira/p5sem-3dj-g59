// medicalrecord-service.ts
import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';
import { EXPRESS_BACKEND_API_PATH } from '../config-path';
import { FamilyHistoryEntry, MedicalConditionEntry } from './entry-types';

@Injectable({
  providedIn: 'root',
})
export class MedicalRecordService {
  constructor(private http: HttpClient, @Inject(EXPRESS_BACKEND_API_PATH) private apiPath:string) {}

    async getMedicalRecord(token: string | null, code: string) : Promise<HttpResponse<string>> {
      if (!token) throw new Error("Token is required");
      const headers = new HttpHeaders({
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
      });
  
      const mr = await lastValueFrom(
          this.http.get<string>(`${this.apiPath}/medicalRecords/${code}`, { headers, observe: 'response' })
        );
      return mr;
    }

    async createFamilyEntry(token:string | null, code: string, attributes: {relative:string, history:string}) : Promise<HttpResponse<FamilyHistoryEntry>> {
      if (!token) throw new Error("Token is required");
      const headers = new HttpHeaders({
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
      });

      const body = {
        ...Object.fromEntries(
          Object.entries(attributes).filter(([_, value]) => value != null) 
        ),
        medicalRecordNumber: code 
      };

      console.log(body);
      const entry = await lastValueFrom(this.http.post<FamilyHistoryEntry>(`${this.apiPath}/familyHistoryEntries`, body, {headers, observe: 'response'}));
      return entry;
    }

    async getFamilyHistoryEntries(token: string | null, code: string, attributes: {relative?: string, history?: string}) : Promise<HttpResponse<FamilyHistoryEntry[]>> {
      if (!token) throw new Error("Token is required");
      const headers = new HttpHeaders({
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
      });

      const body = Object.fromEntries(
        Object.entries(attributes).filter(([_, value]) => value != null) // Remove properties with null or undefined values
      );
  
      const mr = await lastValueFrom(
          this.http.post<FamilyHistoryEntry[]>(`${this.apiPath}/familyHistoryEntries/search/${code}`, body, { headers, observe: 'response' })
        );
      return mr;
    }

    async editFamilyHistoryEntry(token: string | null, entryNumber: string, attributes: {relative?: string, history?: string}) : Promise<HttpResponse<FamilyHistoryEntry[]>> {
      if (!token) throw new Error("Token is required");
      const headers = new HttpHeaders({
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
      });

      const body = Object.fromEntries(
        Object.entries(attributes).filter(([_, value]) => value != null) // Remove properties with null or undefined values
      );
  
      const mr = await lastValueFrom(
          this.http.patch<FamilyHistoryEntry[]>(`${this.apiPath}/familyHistoryEntries/${entryNumber}`, body, { headers, observe: 'response' })
        );
      return mr;
    }

    async getMedicalConditionEntries(token: string | null, code: string, attributes: {condition?: string, year?: string}) : Promise<HttpResponse<MedicalConditionEntry[]>> {
      if (!token) throw new Error("Token is required");
      const headers = new HttpHeaders({
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
      });
      
      const body = Object.fromEntries(
        Object.entries(attributes).filter(([_, value]) => value != null) // Remove properties with null or undefined values
      );
      
      console.log(code)
      console.log(attributes.condition)
      console.log(attributes.year)

      const mr = await lastValueFrom(
          this.http.post<MedicalConditionEntry[]>(`${this.apiPath}/medicalConditionEntries/search/${code}`, body, { headers, observe: 'response' })
        );
      return mr;
    }

    async createMedicalConditionEntry(token:string | null, code: string, attributes: {condition:string, year:string}) : Promise<HttpResponse<MedicalConditionEntry>> {
      if (!token) throw new Error("Token is required");
      const headers = new HttpHeaders({
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
      });

      const body = {
        ...Object.fromEntries(
          Object.entries(attributes).filter(([_, value]) => value != null) 
        ),
        medicalRecordNumber: code 
      };

      console.log(body);
      const entry = await lastValueFrom(this.http.post<MedicalConditionEntry>(`${this.apiPath}/medicalConditionEntries`, body, {headers, observe: 'response'}));
      return entry;
    }

    async editMedicalConditionEntry(token: string | null, entryNumber: string, attributes: {condition?: string, year?: string}) : Promise<HttpResponse<MedicalConditionEntry[]>> {
      if (!token) throw new Error("Token is required");
      const headers = new HttpHeaders({
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
      });

      const body = Object.fromEntries(
        Object.entries(attributes).filter(([_, value]) => value != null) // Remove properties with null or undefined values
      );
  
      const mr = await lastValueFrom(
          this.http.patch<MedicalConditionEntry[]>(`${this.apiPath}/medicalConditionEntries/${entryNumber}`, body, { headers, observe: 'response' })
        );
      return mr;
    }
}
