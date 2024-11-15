import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StaffService {

  private token = localStorage.getItem('token');

  constructor(private http: HttpClient) {}

  // Método para criar um Staff
  async createStaff(staff: {
    staffRole: string,
    identityUsername: string,
    email: string,
    phone: string,
    FullName: string,
    licenseNumber: string
  }): Promise<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.token}`,
      'Content-Type': 'application/json'
    });
    
    try {
      const response = await lastValueFrom(
        this.http.post('https://localhost:5001/api/staff/create', staff, { headers })
      );
      return response;
    } catch (error) {
      console.error('Error creating staff:', error);
      throw error;
    }
  }

  // Método para atualizar um Staff
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

  // Método para excluir um Staff
  async deleteStaff(id: string): Promise<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.token}`
    });

    try {
      const response = await lastValueFrom(
        this.http.delete(`https://localhost:5001/api/staff/delete?id=${id}`, { headers })
      );
      return response;
    } catch (error) {
      console.error('Error deleting staff:', error);
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
