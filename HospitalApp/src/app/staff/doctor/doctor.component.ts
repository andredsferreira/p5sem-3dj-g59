import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { OperationRequestService } from '../../operation-request/operation-request.service';
import { RequestStatus } from '../../operation-request/request-status.enum';
import { CommonModule } from '@angular/common';
import { RequestPriority } from '../../operation-request/request-priority.enum';
import { OperationTypeService } from '../../operation-types/operation-type.service';
import { OperationTypeName } from '../../operation-types/operation-type-name.enum';

@Component({
  selector: 'app-doctor',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './doctor.component.html',
  styleUrls: ['./doctor.component.css'],
})
export class DoctorComponent {
  
  operationRequests: any[] = [];

  filteredRequests: any[] = [];

  operationTypes: any[] = []

  operationTypeNames = Object.values(OperationTypeName)

  operationRequestForm: FormGroup;

  updateRequestForm: FormGroup;

  filterForm: FormGroup;

  formError: string | null = null;

  requestStatusOptions = Object.values(RequestStatus);

  requestPriorityOptions = Object.values(RequestPriority);

  showModal: boolean = false;

  showUpdateModal: boolean = false;

  notFound: boolean = false;

  updateSuccessMessage: string | null = null;

  selectedRequest: any = null;
  confirmingDelete: boolean = false;
  requestIdToDelete: string | null = null;

  constructor(private fb: FormBuilder, private ors: OperationRequestService, private ots: OperationTypeService) {
    this.operationRequestForm = this.fb.group({
      patientId: ['', Validators.required],
      operationTypeName: ['', Validators.required],
      priority: [RequestPriority.Elective, Validators.required],
      dateTime: ['', Validators.required],
      requestStatus: [RequestStatus.Pending, Validators.required],
    });

    this.updateRequestForm = this.fb.group({
      priority: ['', Validators.required],
      dateTime: ['', Validators.required],
    });

    this.filterForm = this.fb.group({
      filterCriteria: ['patientName', Validators.required],
      filterValue: ['', Validators.required],
    });
  }

  async ngOnInit(): Promise<void> {
    this.operationTypes = await this.ots.listOperationTypes()
  }

  async createOperationRequest(): Promise<void> {
    this.showModal = true;
    this.formError = null;
  }

  async onSubmit(): Promise<void> {
    if (this.operationRequestForm.valid) {
      try {
        const {
          patientId,
          operationTypeName,
          priority,
          dateTime,
          requestStatus,
        } = this.operationRequestForm.value;
        const operationType = this.operationTypes.find(
          (type) => type.name === operationTypeName
        );
        if (!operationType) {
          throw new Error('Invalid operation type selected.');
        }
        const operationTypeId = operationType.id;
        await this.ors.createOperationRequest(
          patientId,
          operationTypeId,
          priority,
          dateTime,
          requestStatus
        );
        this.closeModal();
        this.listOperationRequests();
      } catch (error) {
        this.formError =
          'Failed to create operation request. Please check your input and try again.';
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

  async updateOperationRequest(request: any): Promise<void> {
    this.selectedRequest = request;
    this.updateRequestForm.patchValue({
      priority: request.priority,
      dateTime: request.dateTime,
    });
    this.showUpdateModal = true;
  }

  async onUpdateSubmit(): Promise<void> {
    if (this.updateRequestForm.valid && this.selectedRequest) {
      try {
        const { priority, dateTime } = this.updateRequestForm.value;
        await this.ors.updateOperationRequest(
          this.selectedRequest.operationRequestId,
          priority,
          dateTime
        );
        this.updateSuccessMessage = `Operation request with ID ${this.selectedRequest.operationRequestId} was updated successfully.`;
        this.closeUpdateModal();
        this.listOperationRequests();

        setTimeout(() => {
          this.updateSuccessMessage = null;
        }, 3000);
      } catch (error) {
        console.error('Failed to update operation request:', error);
      }
    }
  }

  deleteOperationRequest(requestId: string): void {
    this.requestIdToDelete = requestId;
    this.confirmingDelete = true;
  }

  async confirmDelete(): Promise<void> {
    if (this.requestIdToDelete) {
      try {
        await this.ors.deleteOperationRequest(this.requestIdToDelete);
        this.confirmingDelete = false;
        this.requestIdToDelete = null;
        this.listOperationRequests();
      } catch (error) {
        console.error('Failed to delete operation request:', error);
        this.confirmingDelete = false;
        this.requestIdToDelete = null;
      }
    }
  }

  cancelDelete(): void {
    this.confirmingDelete = false;
    this.requestIdToDelete = null;
  }

  async listOperationRequests(): Promise<void> {
    try {
      this.operationRequests = await this.ors.getOperationRequests();
      this.filteredRequests = [...this.operationRequests];
      this.notFound = this.filteredRequests.length === 0;
      console.log('Fetched Operation Requests:', this.operationRequests);
    } catch (error) {
      console.error('Error fetching operation requests:', error);
    }
  }

  filterRequests(): void {
    const { filterCriteria, filterValue } = this.filterForm.value;

    this.filteredRequests = this.operationRequests.filter((request) => {
      const value = request[filterCriteria]?.toString().toLowerCase();
      return value.includes(filterValue.toLowerCase());
    });
  }
}
