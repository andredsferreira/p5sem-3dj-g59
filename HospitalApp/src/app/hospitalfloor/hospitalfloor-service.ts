import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';
import { API_PATH } from '../config-path';
import { RoomType } from './room';

@Injectable({
  providedIn: 'root',
})
export class HospitalFloorService {
  constructor(private http: HttpClient, @Inject(API_PATH) private apiPath:string) {}

  async getRooms(token: string | null) : Promise<HttpResponse<RoomType[]>> {
    if (!token) throw new Error("Token is required");
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });

    const rooms = await lastValueFrom(
        this.http.get<RoomType[]>(`${this.apiPath}/SurgeryRoom/All`,{ headers, observe: 'response' })
      );
    return rooms;
  }
  async isRoomOccupied(token: string | null, RoomNumber: number, date: string) : Promise<HttpResponse<boolean>> {
    if (!token) throw new Error("Token is required");
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });

    const rooms = await lastValueFrom(
        this.http.get<boolean>(`${this.apiPath}/SurgeryRoom/Occupied/${RoomNumber}/${date}`,{ headers, observe: 'response' })
      );
    return rooms;
  }
}
