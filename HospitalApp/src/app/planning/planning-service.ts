import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { Assignment, AssignmentOutput, DecideOutput, SchedulerResponse } from './schedule-type';
import { PLANNING_API_PATH } from '../config-path';


@Injectable({
  providedIn: 'root'
})

export class PlanningService {

  constructor(private http: HttpClient, @Inject(PLANNING_API_PATH) private apiPath:string) {
  }

  async scheduleSurgeriesObtainBetter(room:string): Promise<SchedulerResponse> {

    try {
      const body = {
        room:room,
        day:20241028
      }
      const headers = new HttpHeaders({
        'Content-Type': 'application/json'
      });
      const response = await lastValueFrom(
        this.http.post<SchedulerResponse>(`${this.apiPath}/schedule/best`, body, {headers, observe:'response'})
      );
      return response.body!;
    } catch (error) {
      console.error('Error listing operation types:', error);
      throw error;
    }
  }
  async scheduleSurgeriesGenetic(room:string,gen:number,pop:number, cross:number, mutation:number): Promise<SchedulerResponse> {

    try {
      const body = {
        room:room,
        day:20241028,
        num_generations:gen,
        population_size:pop,
        crossover_chance:cross,
        mutation_chance:mutation
      }
      const headers = new HttpHeaders({
        'Content-Type': 'application/json'
      });
      const response = await lastValueFrom(
        this.http.post<SchedulerResponse>(`${this.apiPath}/schedule/genetic`, body, {headers, observe:'response'})
      );
      return response.body!;
    } catch (error) {
      console.error('Error listing operation types:', error);
      throw error;
    }
  }
  async assignSurgeriesToRooms(): Promise<AssignmentOutput> {

    try {
      const body = {
        day:20241028
      }
      const headers = new HttpHeaders({
        'Content-Type': 'application/json'
      });
      const response = await lastValueFrom(
        this.http.post<AssignmentOutput>(`${this.apiPath}/assign`, body, {headers, observe:'response'})
      );
      return response.body!;
    } catch (error) {
      console.error('Error listing operation types:', error);
      throw error;
    }
  }
  async decideBestMethod(room:string, time:number): Promise<DecideOutput> {

    try {
      const body = {
        room:room,
        timeToUse:time
      }
      const headers = new HttpHeaders({
        'Content-Type': 'application/json'
      });
      const response = await lastValueFrom(
        this.http.post<DecideOutput>(`${this.apiPath}/decide`, body, {headers, observe:'response'})
      );
      return response.body!;
    } catch (error) {
      console.error('Error listing operation types:', error);
      throw error;
    }
  }

}
