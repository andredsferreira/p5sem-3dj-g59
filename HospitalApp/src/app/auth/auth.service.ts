import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { BehaviorSubject, lastValueFrom } from 'rxjs';
import * as jwt_decode from 'jwt-decode';
import { IAM_API_PATH } from '../config-path';

export interface token {
  token: string,
}

@Injectable({ providedIn: 'root' })
export class AuthService {

  private readonly TOKEN_KEY = ""

  constructor(private http: HttpClient, @Inject(IAM_API_PATH) private iamPath:string) { }

  private tokenSubject = new BehaviorSubject<string|null>(localStorage.getItem(this.TOKEN_KEY));
  token$ = this.tokenSubject.asObservable();

  async login(username: string, password: string): Promise<HttpResponse<token>> {
    try {
      const headers = new HttpHeaders({
        'Content-Type': 'application/json'
      });
      const body = {
        Username: username,
        Password: password,
      }
      const response: any = await lastValueFrom(
        this.http.post<token>(`${this.iamPath}/auth/login`, body, {headers, observe:'response'})
      );
      console.log('Token received:', response.body.token);
      this.storeToken(response.body.token);
      this.tokenSubject.next(response.body.token);
      return response;
    } catch (error) {
      console.error('Error fetching token:', error);
      throw error;
    }
  }

  logout(): void {
    localStorage.removeItem(this.TOKEN_KEY);
    console.log('User logged out successfully.');
    this.tokenSubject.next(null);
  }

  async registerPatient(username: string, email: string, phone: string, password: string): Promise<any> {
    try {
      const headers = new HttpHeaders({
        'Content-Type': 'application/json'
      });
      const response: any = await lastValueFrom(
        this.http.post(`${this.iamPath}/auth/register/patient`, {
          Username: username,
          Email: email,
          Phone: phone,
          Password: password,
        }, {headers, observe: 'response'})
      );
      return response;
    } catch (error) {
      console.error('Error registering the patient', error);
    }
  }

  getRoleFromToken(token: string): string {
    try {
      if (typeof token !== 'string') {
        console.error('Provided token is not a string:', token);
        return typeof token;
      }
      const decodedToken: any = jwt_decode.jwtDecode(token);
      console.log('Decoded role:', decodedToken.role);
      return decodedToken?.role;
    } catch (error) {
      console.error('Error decoding token:', error);
      return '';
    }
  }

  getUsernameFromToken(token: string): string {
    try {
      const decodedToken: any = jwt_decode.jwtDecode(token);
      console.log('Decoded username:', decodedToken.username);
      return decodedToken?.username;
    } catch (error) {
      console.error('Error decoding token:', error);
      return '';
    }
  }

  getEmailFromToken(token: string): string {
    try {
      const decodedToken: any = jwt_decode.jwtDecode(token);
      console.log('Decoded email:', decodedToken.email);
      return decodedToken?.email;
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
        this.http.get(`${this.iamPath}/auth/users`, { headers })
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
