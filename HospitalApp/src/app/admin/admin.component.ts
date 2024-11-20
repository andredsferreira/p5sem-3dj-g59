import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { StaffService } from '../staff/services/StaffService';
import { Router } from '@angular/router';
import { ConnectableObservable } from 'rxjs';

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

  constructor(private fb: FormBuilder, private staffService: StaffService, private router: Router) {
    // Formulário de criação de Staff
    this.staffForm = this.fb.group({
      StaffRole: ['', Validators.required],
      IdentityUsername: ['', Validators.required],
      Email: ['', [Validators.required, Validators.email]],
      Phone: ['', Validators.required],
      Name: ['', Validators.required],
      LicenseNumber: ['', Validators.required],
    });

    // Formulário de atualização de Staff
    this.updateStaffForm = this.fb.group({
      FullName: ['', Validators.required],
      phone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
    });

    this.filterForm = this.fb.group({
      filterCriteria: ['FullName', Validators.required],
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
          StaffRole,
          IdentityUsername,
          Email,
          Phone,
          Name,
          LicenseNumber,
        } = this.staffForm.value;
        
        await this.staffService.createStaff({
          StaffRole,
          IdentityUsername,
          Email,
          Phone,
          Name,
          LicenseNumber,
        });
        
        this.closeModal();
        this.listStaffs();
      } catch (error) {
        this.formError =
          'Failed to create staff. Please check your input and try again.';
      }
    }
  }

  // metodo para mostrar as opções de operationType management

  operationTypeManagement(): void {

    console.log('Operation Type');
    this.router.navigate(['/operationtypemanagement']);
    
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
      email: staff.Email,
      phone: staff.Phone,
      FullName: staff.Name,
    });
    this.showUpdateModal = true;
  }

  async onUpdateSubmit(): Promise<void> {
    if (this.updateStaffForm.valid ) {
      try {
        const { FullName, phone, email } = this.updateStaffForm.value;
        
        await this.staffService.updateStaff(this.selectedStaff.LicenseNumber, {
          FullName,
          phone,
          email,
        });
        
        this.updateSuccessMessage = `Staff with ID ${this.selectedStaff.LicenseNumber} was updated successfully.`;
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
    console.log('Deleting staff:', staffId);
    this.staffIdToDelete = staffId;
    this.confirmingDelete = true;
  }

  async confirmDelete(): Promise<void> {
    console.log('Deleting staff:', this.staffIdToDelete);
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
