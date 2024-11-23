import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';


@Injectable({
  providedIn: 'root'
})

export class OperationTypeService {

  private token = localStorage.getItem('token');

  constructor(private http: HttpClient) {
  }

  
  // 100% working

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

    try {
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

  // falta filtrar

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

  // 100% working

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

  //100% working
  async updateOperationType(id: string, editedType: HttpParams): Promise<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.token}`,
      'Content-Type': 'application/x-www-form-urlencoded'
    });

    try {
      const response = await lastValueFrom(
        this.http.put(`https://localhost:5001/api/operationtype/Edit/${id}`, editedType, { headers })
      );
      return response;
    } catch (error) {
      console.error('Error updating operation type:', error);
      throw error;
    }
  }

  async getOperationType(id: string): Promise<any> {
  
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.token}`
    });

    try {
      const response = await lastValueFrom(
        this.http.get(`https://localhost:5001/api/operationtype/Get/${id}`, { headers })
      );
      return response;
    } catch (error) {
      console.error('Error fetching operation type:', error);
      throw error;
    }
  
  }



}
