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
  async updateStaff(id: string, staff: {
    FullName: string,
    phone: string,
    email: string
  }): Promise<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.token}`,
      'Content-Type': 'application/json'
    });

    const body = { FullName: staff.FullName, phone: staff.phone, email: staff.email };
    console.log(JSON.stringify(body));

    try {
      const response = await lastValueFrom(
        this.http.put(`https://localhost:5001/api/staff/edit/${id}`, body, { headers })
      );
      return response;
    } catch (error) {
      console.error('Error updating staff:', error);
      throw error;
    }
  }


  // Método para buscar todos os Staffs
  async getStaffs(): Promise<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.token}`
    });
    
    try {
      const response = await lastValueFrom(
        this.http.get('https://localhost:5001/api/staff/all', { headers })
      );
      return response;
    } catch (error) {
      console.error('Error fetching staffs:', error);
      throw error;
    }
  }

}
