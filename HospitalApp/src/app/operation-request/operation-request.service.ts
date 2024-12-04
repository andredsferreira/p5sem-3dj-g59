import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { RequestStatus } from './request-status.enum';
import { PatientService } from '../patient-management/patient-service';

@Injectable({
  providedIn: 'root'
})
export class OperationRequestService {

  private token = localStorage.getItem('token');

  constructor(private http: HttpClient, private patientService: PatientService) {

  }

  async createOperationRequest(medicalRecordNumber: string, operationTypeId: string,
    priority: string, dateTime: string, requestStatus: RequestStatus): Promise<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.token}`,
      'Content-Type': 'application/json'
    });
    const body = {
      medicalRecordNumber,
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

  async updateOperationRequest(id: string, priority: string, dateTime: string): Promise<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.token}`,
      'Content-Type': 'application/json'
    });
    const body = { updatedId: id, priority, dateTime };

    try {
      const response = await lastValueFrom(
        this.http.put('https://localhost:5001/api/operationrequest/update',
          body, { headers })
      );
      return response;
    } catch (error) {
      console.error('Error updating operation request:', error);
      throw error;
    }
  }

  async deleteOperationRequest(id: string): Promise<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.token}`
    });

    try {
      const response = await lastValueFrom(
        this.http.delete(`https://localhost:5001/api/operationrequest/delete?id=${id}`,
          { headers })
      );
      return response;
    } catch (error) {
      console.error('Error deleting operation request:', error);
      throw error;
    }
  }

  async getOperationRequests(): Promise<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.token}`
    })
    try {
      const response = await lastValueFrom(
        this.http.get('https://localhost:5001/api/operationrequest/list', { headers })
      );
      return response
    } catch (error) {
      console.error('Error fetching operation requests:', error)
      throw error
    }
  }

  async getPatientById(id: string): Promise<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.token}`
    })
    try {
      const response = await lastValueFrom(
        this.http.get('https://localhost:5001/api/patient/Get/${id}', { headers })
      );
      return response
    } catch (error) {
      console.error('Error fetching operation requests:', error)
      throw error
    }
  }

}

