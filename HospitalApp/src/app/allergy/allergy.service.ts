import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AllergyService {

  private token = localStorage.getItem('token');

  constructor(private http: HttpClient) {

  }

  async addAllergy(code: string, name: string, description: string): Promise<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.token}`,
      'Content-Type': 'application/json'
    });
    const body = {
      code,
      name,
      description
    }
    try {
      const response = await lastValueFrom(
        this.http.post('http://localhost:4000/api/allergies', body, { headers })
      );
      return response;
    } catch (error) {
      console.error('Error creating allergy:', error);
      throw error;
    }
  }

  async getAllergyByName(name: string): Promise<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.token}`,
      'Content-Type': 'application/json'
    });
    try {
      const url = `http://localhost:4000/api/allergies/${name}`;
      const response = await lastValueFrom(
        this.http.get<string>(url, { headers })
      );
      return response;
    } catch (error) {
      console.error('Error fetching allergy:', error);
      throw error;
    }
  }

  async updateAllergy(originalName: string, code: string, name: string, description: string): Promise<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.token}`,
      'Content-Type': 'application/json'
    });
    const body = {
      code,
      name,
      description
    }
    try {
      const url = `http://localhost:4000/api/allergies/${originalName}`;
      const response = await lastValueFrom(
        this.http.patch<string>(url, body, { headers })
      );
      return response;
    } catch (error) {
      console.error('Error updating allergy:', error);
      throw error;
    }
  }

}
