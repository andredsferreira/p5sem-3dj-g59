import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PatientService } from './patient-service';
import { FormsModule } from '@angular/forms';
import { Patient, PatientSearchAttributes, PatientEditAttributes, PatientCreateAttributes } from './patient-types';

interface Field {
  selected: boolean;
  value: string;
}

@Component({
  selector: 'app-patient-management',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './patient-management.component.html',
  styleUrl: './patient-management.component.css'
})
export class PatientManagementComponent implements OnInit {

  errorMessage: string | null = null;
  patients: Patient[] = [];
  selectedItem: Patient | null = null;
  isCreating = false;
  isEditing = false;
  isInitialized = false;
  token: string | null = null;
  paginatedPatients: any[] = [];
  currentPage: number = 1;
  pageSize: number = 5;
  confirmingDelete = false;
  showMessage = false;
  messageText = '';
  messageClass = '';
  allergiesList: string[] = [];

  // Define creating, editable and searchable attributes
  creatingAttributes = [
    { key: "Email", label: "Email" },
    { key: "PhoneNumber", label: "Telemóvel" },
    { key: "FullName", label: "Nome Completo" },
    { key: "DateOfBirth", label: "Data de Nascimento" },
    { key: "Gender", label: "Género" },
  ];
  editableAttributes = [
    { key: 'FullName', label: 'Nome Completo' },
    { key: 'Email', label: 'Email' },
    { key: 'PhoneNumber', label: 'Telemóvel' },
    { key: 'Allergies', label: 'Alergias'}
  ];
  searchableAttributes = [
    { key: "MedicalRecordNumber", label: "Medical Record Number" },
    { key: "Email", label: "Email" },
    { key: "PhoneNumber", label: "Telemóvel" },
    { key: "FullName", label: "Nome Completo" },
    { key: "DateOfBirth", label: "Data de Nascimento" },
    { key: "Gender", label: "Género" },
  ];

  createFields: { [key: string]: Field} = {};
  // Fields for search criteria
  searchFields: { [key: string]: Field } = {};
  // Fields for editing selected item
  editingFields: { [key: string]: Field } = {};

  constructor(private patientService: PatientService) {
    this.resetFields();
  }

  async ngOnInit(): Promise<void> {
    this.token = localStorage.getItem('token'); // Get token from local storage

    if (!this.token) {
      this.errorMessage = 'No token found. Please log in first.';
      return;
    }
    if (this.isInitialized) {
      this.loadPatients();
    }
  }

  // Initialize/reset search and edit fields
  resetFields() {
    this.createFields = this.creatingAttributes.reduce(
      (fields, attr) => {
        fields[attr.key] = { selected: false, value: '' };
        return fields;
      },
      {} as { [key: string]: Field }
    );
    this.searchFields = this.searchableAttributes.reduce(
      (fields, attr) => {
        fields[attr.key] = { selected: false, value: '' };
        return fields;
      },
      {} as { [key: string]: Field }
    );
    this.editingFields = this.editableAttributes.reduce(
      (fields, attr) => {
        fields[attr.key] = { selected: false, value: '' };
        return fields;
      },
      {} as { [key: string]: Field }
    );
  }

  // Load patients after initial search
  onInitialize(): void {
    this.isInitialized = true;
    this.loadPatients();
  }

  returnToFilters() {
    this.isInitialized = false;
    this.currentPage = 1;
    this.selectedItem = null;
  }

  // Close the patient details modal
  onCloseDetails() {
    this.selectedItem = null;
  }

  // Confirm delete
  confirmDelete(patient: any) {
    this.selectedItem = patient;
    this.confirmingDelete = true;
  }

  // Cancel delete confirmation
  cancelDelete() {
    this.confirmingDelete = false;
  }

  //------------------------------------------------Search-----------------------------------------------------------------

  async loadPatients(): Promise<void> {
    this.patients = [];
    // Construct search parameters based on selected search fields
    const searchParams: PatientSearchAttributes = {};

    for (const [key, field] of Object.entries(this.searchFields)) {
      if (field.selected && field.value) {
        searchParams[key as keyof PatientSearchAttributes] = field.value;
      }
    }

    try {
      // Pass searchParams to getPatients to filter results
      const response = await this.patientService.getPatients(this.token, searchParams);
      const patients = response.body;
      console.log(response.status);

      if (patients && patients.length > 0) {
        this.patients = patients;
        this.updatePagination();
      } else {
        this.patients = [];
        console.log("No patients found.");
      }
    } catch (error) {
      console.error("Failed to load patients:", error);
    }
  }

  updatePagination(): void {
    const startIndex = (this.currentPage - 1) * this.pageSize;
    const endIndex = startIndex + this.pageSize;
    this.paginatedPatients = this.patients.slice(startIndex, endIndex);
  }

  get totalPages(): number {
    return Math.ceil(this.patients.length / this.pageSize);
  }

  nextPage(): void {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
      this.updatePagination();
    }
  }

  previousPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.updatePagination();
    }
  }


  // Select a patient for editing
  onSelect(patient: Patient): void {
    this.selectedItem = patient;
  }

  //------------------------------------------------Edit-----------------------------------------------------------------

  // Start editing a patient
  onEdit(patient: Patient): void {
    this.selectedItem = { ...patient };
    this.isEditing = true;
    
    this.editableAttributes.forEach((attr) => {
      const value = patient[attr.key] || '';
      this.editingFields[attr.key].value = value;
      
      // Initialize allergies list from comma-separated string
      if (attr.key === 'Allergies') {
        this.allergiesList = value ? value.split(',').map((item: string) => item.trim()) : [];
      }
    });
  }
  addAllergy(): void {
    this.allergiesList.push('');
  }
  removeAllergy(index: number): void {
    this.allergiesList.splice(index, 1);
  }
  trackByIndex(index: number): number {
    return index;
  }

  // Submit edits to the service
  async submitEdit(patient: Patient | null): Promise<void> {
    const editParams: PatientEditAttributes = {};

    // Loop through each field, adding only selected fields
    for (const [key, field] of Object.entries(this.editingFields)) {
      if (field.selected) {
        // Handle allergies field as comma-separated string
        if (key === 'Allergies') {
          editParams[key as keyof PatientEditAttributes] = this.allergiesList.join(', ');
        } else {
          editParams[key as keyof PatientEditAttributes] = field.value;
        }
      }
    }

    if (patient) {
      try {
        const response = await this.patientService.editPatient(this.token, patient.MedicalRecordNumber, editParams);

        this.showMessage = true;
        if (response) {
          this.messageText = `Paciente ${response.body?.MedicalRecordNumber} editado com sucesso!`;
          this.messageClass = 'bg-green-500 text-white';
        }
      } catch (error) {
        console.error("Erro ao editar paciente:", error);
        this.showMessage = true;
        this.messageText = 'Erro ao editar paciente. Tente novamente.';
        this.messageClass = 'bg-red-500 text-white';
      } finally {
        this.isEditing = false;
        this.selectedItem = null;
        await this.loadPatients();

        setTimeout(() => {
          this.showMessage = false;
        }, 3000);
      }
    } else {
      console.error("O Paciente não existe.");
    }
  }

  // Cancel edit operation
  cancelEdit(): void {
    this.isEditing = false;
  }

//------------------------------------------------Create-----------------------------------------------------------------

  startCreate() : void {
    this.isCreating = true;
  }

  // Create a new patient
  async onCreate(): Promise<void> {
    const createParams: PatientCreateAttributes = {Email: '',PhoneNumber: '',FullName: '',DateOfBirth: '',Gender: '',Allergies: ''};
    for (const [key, field] of Object.entries(this.createFields)) {
      if (key === 'Allergies') {
        createParams[key as keyof PatientCreateAttributes] = this.allergiesList.join(', ');
      } else {
        console.log(field.value);
        createParams[key as keyof PatientCreateAttributes] = field.value;
      }
    }
    try{
      const response = await this.patientService.createPatient(this.token, createParams);
      this.showMessage = true;
      if (response) {
        this.messageText = `Paciente ${response.body?.MedicalRecordNumber} criado com sucesso!`;
        this.messageClass = 'bg-green-500 text-white';
      }
      
    } catch (error) {
      console.error("Erro ao criar paciente:", error);
      this.showMessage = true;
      this.messageText = 'Erro ao criar paciente. Tente novamente.';
      this.messageClass = 'bg-red-500 text-white';
    } finally {
      this.isCreating = false;
      this.selectedItem = null;
      await this.loadPatients();

      setTimeout(() => {
        this.showMessage = false;
      }, 3000);
    }
  }

  cancelCreate(){
    this.isCreating = false;
  }

  //------------------------------------------------Delete-----------------------------------------------------------------

  // Delete a patient
  async onDelete(patient: Patient | null): Promise<void> {
    if (patient) {
      try {
        const response = await this.patientService.deletePatient(this.token, patient.MedicalRecordNumber);
  
        this.showMessage = true;
        if (response) {
          this.messageText = `Paciente ${response.body?.MedicalRecordNumber} eliminado com sucesso!`;
          this.messageClass = 'bg-green-500 text-white';
        }
        
      } catch (error) {
        console.error("Erro ao eliminar paciente:", error);
        this.showMessage = true;
        this.messageText = 'Erro ao eliminar paciente. Tente novamente.';
        this.messageClass = 'bg-red-500 text-white';
      } finally {
        this.confirmingDelete = false;
        this.selectedItem = null;
        await this.loadPatients();
  
        setTimeout(() => {
          this.showMessage = false;
        }, 3000);
      }
    } else {
      console.error("O Paciente não existe.");
    }
  }
}
