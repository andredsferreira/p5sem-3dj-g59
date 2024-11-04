import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OperationRequestService {

  private token = localStorage.getItem('token');

  constructor(private http: HttpClient) {

  }

  async createOperationRequest(patientId: string, operationTypeId: string, priority: string, dateTime: string, requestStatus: string): Promise<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.token}`
    });

    try {
      return await lastValueFrom(
        this.http.post('https://localhost:5001/api/operationrequest/create', {
          patientId,
          operationTypeId,
          priority,
          dateTime,
          requestStatus
        }, { headers })
      );
    } catch (error) {
      if (error instanceof HttpErrorResponse) {
        console.error('Error creating operation request:', error.message);
        throw error;
      }
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

