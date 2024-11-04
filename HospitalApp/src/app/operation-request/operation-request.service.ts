import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OperationRequestService {

  private token = localStorage.getItem('token');

  constructor(private http: HttpClient) {

  }

  async createOperationRequest(patientId: string, operationTypeId: string, priority: string,
    dateTime: string, requestStatus: string) {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.token}`
    })
    this.http.post('https://localhost:5001/api/operationrequest/create', {
      patientId: patientId,
      operationTypeId: operationTypeId,
      priority: priority,
      dateTime: dateTime,
      requestStatus: requestStatus
    }, { headers })
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

