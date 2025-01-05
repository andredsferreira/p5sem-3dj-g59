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
      designation: ['', Validators.required],  
      description: ['', Validators.required],
      symptoms: this.fb.array([], Validators.required),
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

  // Getter para o FormArray no updateStaffForm
  get updateSymptoms() {
    return this.updateStaffForm.get('symptoms') as FormArray;
  }


  addUpdateSymptom() {
    this.updateSymptoms.push(this.fb.control('', Validators.required));
  }
  
  removeUpdateSymptom(index: number) {
    this.updateSymptoms.removeAt(index);
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

  async updateStaff(condition: any): Promise<void> {
    if (!condition) {
      console.error('Condition is undefined');
      return;
    }
  
    this.selectedStaff = condition.props;
  
    // Verificar se symptoms existe e é um array
    if (!Array.isArray(condition.symptoms)) {
      console.error('Condition symptoms is not an array or is undefined');
      this.updateSymptoms.clear();
    } else {
      this.updateSymptoms.clear();
      condition.symptoms.forEach((symptom: string) => {
        this.updateSymptoms.push(this.fb.control(symptom, Validators.required));
      });
    }
  
    // Preencher o formulário com os dados da condition
    this.updateStaffForm.patchValue({
      designation: condition.designation || '',  // Adicionar designation
      description: condition.description || '',
    });
  
    this.showUpdateModal = true;
  }
  
  
  

  async onUpdateSubmit(): Promise<void> {
    if (this.updateStaffForm.valid) {
      try {
        const { designation, description, symptoms } = this.updateStaffForm.value;
        
        // Enviar o designation também na atualização
        await this.medicalConditionService.updateStaff(this.selectedStaff.code, {
          designation,
          description,
          symptoms,
        });
        
        this.updateSuccessMessage = `Condition with Code ${this.selectedStaff.code} was updated successfully.`;
        this.closeUpdateModal();
        this.listStaffs();
  
        setTimeout(() => {
          this.updateSuccessMessage = null;
        }, 3000);
      } catch (error) {
        console.error('Failed to update condition:', error);
      }
    }
  }
  


  async listStaffs(): Promise<void> {
    try {
      this.staffList = await this.medicalConditionService.getStaffs();
  
      // Garantir que cada staff tem a estrutura esperada
      this.staffList = this.staffList.map((staff: any) => ({
        ...staff,
        symptoms: staff.symptoms || [], // Garantir que symptoms é sempre um array
      }));
  
      this.filteredStaffs = [...this.staffList];
      this.notFound = this.filteredStaffs.length === 0;
      console.log('Fetched conditions:', this.staffList);
    } catch (error) {
      console.error('Error fetching conditions:', error);
    }
  }
  



  backToAdmin(): void {

    console.log('Admin');
    this.router.navigate(['/doctor']);
  }



  
}