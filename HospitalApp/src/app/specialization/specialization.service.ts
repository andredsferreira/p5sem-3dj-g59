import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';

@Injectable({
    providedIn: 'root'
})

export class SpecializationService {

    private token = localStorage.getItem('token');

    constructor(private http: HttpClient) {
    }

    async createSpecialization(code: string, designation: string, description: string): Promise<any> {
    
        const headers = new HttpHeaders({
            'Authorization': `Bearer ${this.token}`,
            'Content-Type': 'application/x-www-form-urlencoded'
        });
        const body = new HttpParams()
            .set('code', code)
            .set('designation', designation)
            .set('description', description);
            

        console.log(body);

        try {
            const response = await lastValueFrom(
                this.http.post('https://localhost:5001/api/specialization/Add',
                    body, { headers })
            );
            return response;
        } catch (error) {
            console.error('Error creating specialization:', error);
            throw error;
        }
    
    }


}