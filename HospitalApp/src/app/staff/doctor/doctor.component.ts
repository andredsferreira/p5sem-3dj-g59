import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { OperationRequestService } from '../../operation-request/operation-request.service';
import { RequestStatus } from '../../operation-request/request-status.enum';

@Component({
  selector: 'app-doctor',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './doctor.component.html',
  styleUrls: ['./doctor.component.css']
})
export class DoctorComponent {
  operationRequestForm: FormGroup;
  requestStatusOptions = Object.values(RequestStatus);
  showModal: boolean = false;
  formError: string | null = null
  operationRequests: any[] = [];

  constructor(private fb: FormBuilder, private ors: OperationRequestService) {
    this.operationRequestForm = this.fb.group({
      patientId: ['', Validators.required],
      operationTypeId: ['', Validators.required],
      priority: ['', Validators.required],
      dateTime: ['', Validators.required],
      requestStatus: [RequestStatus.Pending, Validators.required]
    });
  }

  async createOperationRequest(): Promise<void> {
    this.showModal = true;
    this.formError = null;
  }

  async onSubmit(): Promise<void> {
    if (this.operationRequestForm.valid) {
      try {
        const { patientId, operationTypeId, priority, dateTime, requestStatus } =
          this.operationRequestForm.value;
        console.log({
          patientId,
          operationTypeId,
          priority,
          dateTime,
          requestStatus
        });
        await this.ors.createOperationRequest(patientId, operationTypeId, priority, dateTime, requestStatus);
        this.closeModal();
      } catch (error) {
        this.formError = 'Failed to create operation request. Please check your input and try again.';
      }
    }
  }

  closeModal(): void {
    this.showModal = false;
    this.operationRequestForm.reset();
  }

  updateOperationRequest() {

  }

  deleteOperationRequest() {

  }

  async listOperationRequests(): Promise<void> {
    try {
      this.operationRequests = await this.ors.getDoctorOperationRequests();
      console.log("Fetched Operation Requests:", this.operationRequests);
    } catch (error) {
      console.error("Error fetching operation requests:", error);
    }
  }

}
