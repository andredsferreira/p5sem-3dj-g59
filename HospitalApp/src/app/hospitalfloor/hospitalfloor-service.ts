import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';
import { BACKEND_API_PATH } from '../config-path';
import { Occupied, Room } from './types';

@Injectable({
  providedIn: 'root',
})
export class HospitalFloorService {
  constructor(private http: HttpClient, @Inject(BACKEND_API_PATH) private apiPath:string) {}

  async getRooms(token: string | null) : Promise<HttpResponse<Room[]>> {
    if (!token) throw new Error("Token is required");
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });

    const rooms = await lastValueFrom(
        this.http.get<Room[]>(`${this.apiPath}/SurgeryRoom/All`,{ headers, observe: 'response' })
      );
    rooms.body!.forEach(element => {
      console.log("Number: " + element.Number);
    });
    return rooms;
  }
  async isRoomOccupied(token: string | null, RoomNumber: number, date: string) : Promise<HttpResponse<Occupied>> {
    if (!token) throw new Error("Token is required");
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });

    const rooms = await lastValueFrom(
        this.http.get<Occupied>(`${this.apiPath}/SurgeryRoom/Occupied/${RoomNumber}/${date}`,{ headers, observe: 'response' })
      );
    return rooms;
  }
}
