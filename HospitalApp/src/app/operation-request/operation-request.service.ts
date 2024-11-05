import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { RequestStatus } from './request-status.enum';

@Injectable({
  providedIn: 'root'
})
export class OperationRequestService {

  private token = localStorage.getItem('token');

  constructor(private http: HttpClient) {

  }

  async createOperationRequest(patientId: string, operationTypeId: string,
    priority: string, dateTime: string, requestStatus: RequestStatus): Promise<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.token}`,
      'Content-Type': 'application/json'
    });
    const body = {
      patientId,
      operationTypeId,
      priority,
      dateTime,
      requestStatus
    };

    try {
      const response = await lastValueFrom(
        this.http.post('https://localhost:5001/api/operationrequest/create',
          body, { headers })
      );
      return response;
    } catch (error) {
      console.error('Error creating operation request:', error);
      throw error;
    }
  }
  async updateOperationRequest(id: number) {
    throw new Error("Not implemented yet")
  }

  async deleteOperationRequest(id: number) {
    throw new Error("Not implemented yet")
  }

  async getDoctorOperationRequests(): Promise<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.token}`
    })
    try {
      const users = await lastValueFrom(
        this.http.get('https://localhost:5001/api/operationrequest/list', { headers })
      );
      return users
    } catch (error) {
      console.error('Error fetching operation requests:', error)
      throw error
    }
  }

}

