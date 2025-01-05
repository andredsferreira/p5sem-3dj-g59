import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MedicalConditionService {

  private token = localStorage.getItem('token');

  constructor(private http: HttpClient) {}

  // Método para criar um condition
  
  async createStaff(condition: {
    code: string,
    designation: string,
    description: string,
    symptoms: Array<string>,
  }): Promise<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.token}`,
      'Content-Type': 'application/json'
    });
    
    try {
      const response = await lastValueFrom(
        this.http.post('http://localhost:4000/api/medConditions', condition, { headers })
        
      );
      return response;
    } catch (error) {
      console.error('Error creating condition:', error);
      throw error;
    }
  }

  // Método para atualizar um condition
  async updateStaff(id: string, condition: {
    designation: string,
    description: string,
    symptoms: Array<string>
  }): Promise<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.token}`,
      'Content-Type': 'application/json'
    });

    const body = { designation: condition.designation , description: condition.description, symptoms: condition.symptoms,};
    console.log(JSON.stringify(body));

    try {
      const response = await lastValueFrom(
        this.http.patch(`http://localhost:4000/api/medConditions/${id}`, body, { headers })
      );
      return response;
    } catch (error) {
      console.error('Error updating condition:', error);
      throw error;
    }
  }


  // Método para buscar todos os conditions
  async getStaffs(): Promise<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.token}`
    });
    
    try {
      const response = await lastValueFrom(
        this.http.get('http://localhost:4000/api/medConditions', { headers })
      );
      return response;
    } catch (error) {
      console.error('Error fetching conditions:', error);
      throw error;
    }
  }

}
