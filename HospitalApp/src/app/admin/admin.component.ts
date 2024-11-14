import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { StaffService } from '../staff/services/StaffService';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css'],
})
export class AdminComponent {
  staffList: any[] = [];
  filteredStaffs: any[] = [];
  
  staffForm: FormGroup; 
  updateStaffForm: FormGroup; 
  filterForm: FormGroup;

  formError: string | null = null;
  showModal: boolean = false;
  showUpdateModal: boolean = false;
  notFound: boolean = false;
  updateSuccessMessage: string | null = null;
  selectedStaff: any = null;
  confirmingDelete: boolean = false;
  staffIdToDelete: string | null = null;

  constructor(private fb: FormBuilder, private staffService: StaffService) {
    // Formulário de criação de Staff
    this.staffForm = this.fb.group({
      staffRole: ['', Validators.required],
      identityUsername: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', Validators.required],
      name: ['', Validators.required],
      licenseNumber: ['', Validators.required],
    });

    // Formulário de atualização de Staff
    this.updateStaffForm = this.fb.group({
      staffRole: ['', Validators.required],
      phone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
    });

    this.filterForm = this.fb.group({
      filterCriteria: ['name', Validators.required],
      filterValue: ['', Validators.required],
    });
  }

  async createStaff(): Promise<void> {
    this.showModal = true;
    this.formError = null;
  }

  async onSubmit(): Promise<void> {
    if (this.staffForm.valid) {
      try {
        const {
          staffRole,
          identityUsername,
          email,
          phone,
          name,
          licenseNumber,
        } = this.staffForm.value;
        
        await this.staffService.createStaff({
          staffRole,
          identityUsername,
          email,
          phone,
          name,
          licenseNumber,
        });
        
        this.closeModal();
        this.listStaffs();
      } catch (error) {
        this.formError =
          'Failed to create staff. Please check your input and try again.';
      }
    }
  }

  closeModal(): void {
    this.showModal = false;
    this.staffForm.reset();
  }

  closeUpdateModal(): void {
    this.showUpdateModal = false;
    this.updateStaffForm.reset();
  }

  async updateStaff(staff: any): Promise<void> {
    this.selectedStaff = staff;
    this.updateStaffForm.patchValue({
      staffRole: staff.staffRole,
      phone: staff.phone,
      email: staff.email,
    });
    this.showUpdateModal = true;
  }

  async onUpdateSubmit(): Promise<void> {
    if (this.updateStaffForm.valid && this.selectedStaff) {
      try {
        const { staffRole, phone, email } = this.updateStaffForm.value;
        
        await this.staffService.updateStaff(this.selectedStaff.id, {
          staffRole,
          phone,
          email,
        });
        
        this.updateSuccessMessage = `Staff with ID ${this.selectedStaff.id} was updated successfully.`;
        this.closeUpdateModal();
        this.listStaffs();

        setTimeout(() => {
          this.updateSuccessMessage = null;
        }, 3000);
      } catch (error) {
        console.error('Failed to update staff:', error);
      }
    }
  }

  deleteStaff(staffId: string): void {
    this.staffIdToDelete = staffId;
    this.confirmingDelete = true;
  }

  async confirmDelete(): Promise<void> {
    if (this.staffIdToDelete) {
      try {
        await this.staffService.deleteStaff(this.staffIdToDelete);
        this.confirmingDelete = false;
        this.staffIdToDelete = null;
        this.listStaffs();
      } catch (error) {
        console.error('Failed to delete staff:', error);
        this.confirmingDelete = false;
        this.staffIdToDelete = null;
      }
    }
  }

  cancelDelete(): void {
    this.confirmingDelete = false;
    this.staffIdToDelete = null;
  }

  async listStaffs(): Promise<void> {
    try {
      this.staffList = await this.staffService.getStaffs();
      this.filteredStaffs = [...this.staffList];
      this.notFound = this.filteredStaffs.length === 0;
      console.log('Fetched Staffs:', this.staffList);
    } catch (error) {
      console.error('Error fetching staffs:', error);
    }
  }

  filterStaffs(): void {
    const { filterCriteria, filterValue } = this.filterForm.value;

    this.filteredStaffs = this.staffList.filter((staff) => {
      const value = staff[filterCriteria]?.toString().toLowerCase();
      return value.includes(filterValue.toLowerCase());
    });
  }
}
