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
      const response = await lastValueFrom(
        this.http.post<SchedulerResponse>('http://localhost:2000/schedule', null)
      );
      return response;
    } catch (error) {
      console.error('Error listing operation types:', error);
      throw error;
    }
  }

}
