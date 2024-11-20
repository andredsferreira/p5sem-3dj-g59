import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import {Status} from './status.enum';
import { Specialization } from './specialization.enum';

@Injectable({
  providedIn: 'root'
})
export class OperationTypeService{

  private token = localStorage.getItem('token');

  constructor(private http: HttpClient) {
  }

  async createOperationType(name: string, anaesthesiaTime: number, surgeryTime: number, cleaningTime: number, 
    status: Status, specializations: Specialization, minDoctor: number, minAnaesthetist: number, minInstrumentNurse: number,
    minNurseAnaesthetist: number, minXRay: number, minMedicalAssistant: number): Promise<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.token}`,
      'Content-Type': 'application/json'
    });
    const body = {
      name,
      anaesthesiaTime,
      surgeryTime,
      cleaningTime,
      status,
      specializations,
      minDoctor,
      minAnaesthetist,
      minInstrumentNurse,
      minNurseAnaesthetist,
      minXRay,
      minMedicalAssistant
    };
    
    try{
      const response = await lastValueFrom(
        this.http.post('https://localhost:5001/api/operationtype/Add',
          body, { headers })
      );
      return response;
    } catch (error) {
      console.error('Error creating operation type:', error);
      throw error;
    }

  }
  


}
