import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { OperationRequestService } from '../../operation-request/operation-request.service';

@Component({
  selector: 'app-doctor',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './doctor.component.html',
  styleUrls: ['./doctor.component.css']
})
export class DoctorComponent {
  operationRequestForm: FormGroup;
  showModal: boolean = false;
  operationRequests: any[] = [];

  constructor(private fb: FormBuilder, private ors: OperationRequestService) {
    this.operationRequestForm = this.fb.group({
      patientId: ['', Validators.required],
      operationTypeId: ['', Validators.required],
      priority: ['', Validators.required],
      dateTime: ['', Validators.required],
      requestStatus: ['', Validators.required]
    });
  }

  async createOperationRequest(): Promise<void> {
    this.showModal = true;
  }

  onSubmit(): void {
    if (this.operationRequestForm.valid) {
      const newRequest = this.operationRequestForm.value;
      console.log("Creating Operation Request: ", newRequest);
      this.closeModal();
    }
  }

  closeModal(): void {
    this.showModal = false;
    this.operationRequestForm.reset();
  }

  async listOperationRequests(): Promise<void> {
    try {
      this.operationRequests = await this.ors.getDoctorOperationRequests();
      console.log("Fetched Operation Requests:", this.operationRequests);
    } catch (error) {
      console.error("Error fetching operation requests:", error);
    }
  }

  updateOperationRequest() {

  }

  deleteOperationRequest() {

  }


}
