import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class OperationTypeService{

  private token = localStorage.getItem('token');

  constructor(private http: HttpClient) {
  }

  async createOperationType(name: string, anaesthesiaTime: number, surgeryTime: number, cleaningTime: number, 
    specialization: string, minDoctor: number, minAnaesthetist: number, minInstrumentingNurse: number,
    minCirculatingNurse: number, minNurseAnaesthetist: number, minXRayTechnician: number, minMedicalActionAssistant: number): Promise<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.token}`,
      'Content-Type': 'application/x-www-form-urlencoded'
    });
    const body = new HttpParams()
      .set('name', name)
      .set('anaesthesiaTime', anaesthesiaTime.toString())
      .set('surgeryTime', surgeryTime.toString())
      .set('cleaningTime', cleaningTime.toString())
      .set('status', "ACTIVE")
      .set('specialization', specialization)
      .set('minDoctor', minDoctor.toString())
      .set('minAnaesthetist', minAnaesthetist.toString())
      .set('minInstrumentingNurse', minInstrumentingNurse.toString())
      .set('minCirculatingNurse', minCirculatingNurse.toString())
      .set('minNurseAnaesthetist', minNurseAnaesthetist.toString())
      .set('minXRayTechnician', minXRayTechnician.toString())
      .set('minMedicalActionAssistant', minMedicalActionAssistant.toString());

    console.log(body);
    
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

    async listOperationTypes(): Promise<any> {
    
      const headers = new HttpHeaders({
        'Authorization': `Bearer ${this.token}`,
      });

      try {
        const response = await lastValueFrom(
          this.http.get('https://localhost:5001/api/operationtype/All', { headers })
        );
        return response;
      } catch (error) {
        console.error('Error listing operation types:', error);
        throw error;
      }

    }

    async deleteOperationType(id: string): Promise<any> {
      const headers = new HttpHeaders({
        'Authorization': `Bearer ${this.token}`
      });

      try {
        const response = await lastValueFrom(
          this.http.delete(`https://localhost:5001/api/operationtype/Deactivate/${id}`, { headers })
        );
        return response;
      } catch (error) {
        console.error('Error deleting operation type:', error);
        throw error;
      }
    }

  


}
