import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';
import * as jwt_decode from 'jwt-decode';

@Injectable({ providedIn: 'root' })
export class AuthService {

  private readonly TOKEN_KEY = ""

  constructor(private http: HttpClient) { }

  async login(username: string, password: string): Promise<string> {
    try {
      const response: any = await lastValueFrom(
        this.http.post('https://localhost:5001/api/auth/login', {
          Username: username,
          Password: password,
        })
      );
      console.log('Token received:', response.token);
      this.storeToken(response.token);
      return response.token;
    } catch (error) {
      console.error('Error fetching token:', error);
      throw error;
    }
  }

  logout(): void {
    localStorage.removeItem(this.TOKEN_KEY);
    console.log('User logged out successfully.');
  }

  async registerPatient(username: string, email: string, phone: string, password: string): Promise<any> {
    try {
      const response: any = await lastValueFrom(
        this.http.post('https://localhost:5001/api/auth/registerpatient', {
          Username: username,
          Email: email,
          Phone: phone,
          Password: password,
        })
      );
      return response;
    } catch (error) {
      console.error('Error registering the patient', error);
    }
  }

  getRoleFromToken(token: string): string {
    try {
      const decodedToken: any = jwt_decode.jwtDecode(token);
      console.log('Decoded role:', decodedToken.role);
      return decodedToken?.role;
    } catch (error) {
      console.error('Error decoding token:', error);
      return '';
    }
  }

  async getUsers(token: string): Promise<any> {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`,
    });
    try {
      const users = await lastValueFrom(
        this.http.get('https://localhost:5001/api/auth/users', { headers })
      );
      return users;
    } catch (error) {
      console.error('Error fetching users:', error);
      throw error;
    }
  }

  private storeToken(token: string): void {
    localStorage.setItem(this.TOKEN_KEY, token);
  }

  getToken(): string | null {
    return localStorage.getItem(this.TOKEN_KEY);
  }

  isTokenValid(): boolean {
    const token = this.getToken();
    if (!token) {
      return false; 
    }
    try {
      const decodedToken: any = jwt_decode.jwtDecode(token);
      const currentTime = Math.floor(Date.now() / 1000); 
      return decodedToken.exp && decodedToken.exp > currentTime;
    } catch (error) {
      console.error('Error decoding token:', error);
      return false; 
    }
  }

}
