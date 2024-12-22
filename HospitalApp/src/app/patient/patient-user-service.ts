import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';
import { BACKEND_API_PATH } from '../config-path';

@Injectable({
  providedIn: 'root',
})
export class PatientUserService {
  constructor(private http: HttpClient, @Inject(BACKEND_API_PATH) private apiPath:string) {}
  async deleteAccount(token: string | null): Promise<HttpResponse<any>> {
    if (!token) throw new Error("Token is required");
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });
    const response = await lastValueFrom(this.http.delete<any>(`${this.apiPath}/Auth/DeleteProfile/`, { headers, observe: 'response' }));
    return response;
  }
}