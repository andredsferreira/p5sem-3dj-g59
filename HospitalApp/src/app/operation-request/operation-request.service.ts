import { Injectable } from '@angular/core';
import { OperationRequest } from './operation-request';

@Injectable({
  providedIn: 'root'
})
export class OperationRequestService {

  constructor() { }

  async createOperationRequest(or: OperationRequest) {
    throw new Error("Not implemented yet")
  }

  async updateOperationRequest(id: number) {
    throw new Error("Not implemented yet")
  }

  async deleteOperationRequest(id: number) {
    throw new Error("Not implemented yet")
  }
  
  async getDoctorOperationRequests(): Promise<OperationRequest[]> {
    throw new Error("Not implemented yet")
  }

}
