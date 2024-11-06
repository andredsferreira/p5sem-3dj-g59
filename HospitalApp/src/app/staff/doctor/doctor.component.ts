import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { OperationRequestService } from '../../operation-request/operation-request.service';
import { RequestStatus } from '../../operation-request/request-status.enum';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-doctor',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './doctor.component.html',
  styleUrls: ['./doctor.component.css']
})
export class DoctorComponent {

  operationRequests: any[] = [];

  operationRequestForm: FormGroup;

  updateRequestForm: FormGroup;

  requestStatusOptions = Object.values(RequestStatus);

  showModal: boolean = false;

  showUpdateModal: boolean = false;

  formError: string | null = null;

  selectedRequest: any = null;

  constructor(private fb: FormBuilder, private ors: OperationRequestService) {
    this.operationRequestForm = this.fb.group({
      patientId: ['', Validators.required],
      operationTypeId: ['', Validators.required],
      priority: ['', Validators.required],
      dateTime: ['', Validators.required],
      requestStatus: [RequestStatus.Pending, Validators.required]
    });

    this.updateRequestForm = this.fb.group({
      priority: ['', Validators.required],
      dateTime: ['', Validators.required]
    });
  }

  async createOperationRequest(): Promise<void> {
    this.showModal = true;
    this.formError = null;
  }

  async onSubmit(): Promise<void> {
    if (this.operationRequestForm.valid) {
      try {
        const { patientId, operationTypeId, priority, dateTime, requestStatus } = this.operationRequestForm.value;
        await this.ors.createOperationRequest(patientId, operationTypeId, priority, dateTime, requestStatus);
        this.closeModal();
        this.listOperationRequests();
      } catch (error) {
        this.formError = 'Failed to create operation request. Please check your input and try again.';
      }
    }
  }

  closeModal(): void {
    this.showModal = false;
    this.operationRequestForm.reset();
  }

  closeUpdateModal(): void {
    this.showUpdateModal = false;
    this.updateRequestForm.reset();
  }

  async listOperationRequests(): Promise<void> {
    try {
      this.operationRequests = await this.ors.getOperationRequests();
      console.log("Fetched Operation Requests:", this.operationRequests);
    } catch (error) {
      console.error("Error fetching operation requests:", error);
    }
  }
  
  async updateOperationRequest(request: any): Promise<void> {
    this.selectedRequest = request;
    this.updateRequestForm.patchValue({
      priority: request.priority,
      dateTime: request.dateTime
    });
    this.showUpdateModal = true;
  }

  // Use `operationRequestId` here
  async onUpdateSubmit(): Promise<void> {
    if (this.updateRequestForm.valid && this.selectedRequest) {
      try {
        const { priority, dateTime } = this.updateRequestForm.value;
        await this.ors.updateOperationRequest(this.selectedRequest.operationRequestId, priority, dateTime);
        this.closeUpdateModal();
        this.listOperationRequests();
      } catch (error) {
        console.error('Failed to update operation request:', error);
      }
    }
  }

  // Use `operationRequestId` here as well
  async deleteOperationRequest(requestId: string): Promise<void> {
    try {
      await this.ors.deleteOperationRequest(requestId);
      this.listOperationRequests();
    } catch (error) {
      console.error('Failed to delete operation request:', error);
    }
  }


}
