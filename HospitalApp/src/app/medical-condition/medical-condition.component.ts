import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormArray,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MedicalConditionService } from './medical-condition-service';
import { Router } from '@angular/router';
import { ConnectableObservable } from 'rxjs';

@Component({
  selector: 'app-staff-management',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './medical-condition.component.html',
  styleUrls: ['./medical-condition.component.css'],
})
export class MedicalConditionComponent {
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

  constructor(private fb: FormBuilder, private medicalConditionService: MedicalConditionService, private router: Router) {
    // Formulário de criação de condition
    this.staffForm = this.fb.group({      
      code: ['', Validators.required],
      designation: ['', Validators.required],
      description: ['', Validators.required],
      symptoms: this.fb.array([], Validators.required), 

    });

    // Formulário de atualização de condition
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

  // Getter para o FormArray
  get symptoms() {
    return this.staffForm.get('symptoms') as FormArray;
  }

  // Adicionar novo sintoma ao FormArray
  addSymptom() {
    this.symptoms.push(this.fb.control('', Validators.required));
  }

  // Remover sintoma do FormArray pelo índice
  removeSymptom(index: number) {
    this.symptoms.removeAt(index);
  }

  

  async createStaff(): Promise<void> {
    this.showModal = true;
    this.formError = null;
  }

  async onSubmit(): Promise<void> {
    if (this.staffForm.valid) {
      try {
        const {
          code,
          designation,
          description,
          symptoms,
        } = this.staffForm.value;
        
        await this.medicalConditionService.createStaff({
          code,
          designation,
          description,
          symptoms,
        });
        
        this.closeModal();
        this.listStaffs();
      } catch (error) {
        this.formError =
          'Failed to create condition. Please check your input and try again.';
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
        
        await this.medicalConditionService.updateStaff(this.selectedStaff.LicenseNumber, {
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


  async listStaffs(): Promise<void> {
    try {
      this.staffList = await this.medicalConditionService.getStaffs();
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

  backToAdmin(): void {

    console.log('Admin');
    this.router.navigate(['/admin']);
  }



  
}