import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { SchedulerResponse } from './schedule-type';


@Injectable({
  providedIn: 'root'
})

export class PlanningService {

  constructor(private http: HttpClient) {
  }

  async scheduleSurgeries(): Promise<SchedulerResponse> {

    try {
      const body = {
        room:"or1",
        day:20241028
      }
      const headers = new HttpHeaders({
        'Content-Type': 'application/json'
      });
      const response = await lastValueFrom(
        this.http.post<SchedulerResponse>('http://localhost:2000/schedule/best', body, {headers, observe:'response'})
      );
      return response.body!;
    } catch (error) {
      console.error('Error listing operation types:', error);
      throw error;
    }
  }

}
