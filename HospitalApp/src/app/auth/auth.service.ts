import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';
import * as jwt_decode from "jwt-decode";

@Injectable({ providedIn: 'root' })
export class AuthService {

  constructor(private http: HttpClient) {

  }

  async login(username: string, password: string): Promise<string> {
    try {
      const response: any = await lastValueFrom(
        this.http.post('https://localhost:5001/api/auth/login', {
          Username: username,
          Password: password
        })
      );
      console.log("Token received:", response.token);
      return response.token;
    } catch (error) {
      console.error("Error fetching token:", error);
      throw error;
    }
  }

  getRoleFromToken(token: string): string {
    try {
      const decodedToken: any = jwt_decode.jwtDecode(token);
      console.log("Decoded role" + decodedToken.role);
      return decodedToken?.role;
    } catch (error) {
      console.error("Error decoding token:", error);
      return ""
    }
  }

  async getUsers(token: string): Promise<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    })
    try {
      const users = await lastValueFrom(
        this.http.get('https://localhost:5001/api/auth/users', { headers })
      );
      return users
    } catch (error) {
      console.error('Error fetching users:', error)
      throw error
    }
  }

}
