import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PatientService } from './patient-service';
import { FormsModule } from '@angular/forms';

// Define the Patient interface
interface Patient {
  MedicalRecordNumber: string;
  name: string;
  email: string;
  [key: string]: any;
}

interface Field {
  selected: boolean;
  value: string;
}

interface PatientSearchAttributes {
  MedicalRecordNumber?: string;
  Email?: string;
  PhoneNumber?: string;
  FullName?: string;
  DateOfBirth?: string | Date;
  Gender?: string;
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
  patients: any[] = [];
  selectedItem: Patient | null = null;
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

  // Define editable and searchable attributes
  editableAttributes = [
    { key: 'FullName', label: 'Nome' },
    { key: 'Email', label: 'Email' },
    { key: 'PhoneNumber', label: 'Telemóvel'}
  ];
  searchableAttributes = [
    { key: "MedicalRecordNumber", label: "Medical Record Number" },
    { key: "Email", label: "Email" },
    { key: "PhoneNumber", label: "Telemóvel" },
    { key: "FullName", label: "Nome Completo" },
    { key: "DateOfBirth", label: "Data de Nascimento" },
    { key: "Gender", label: "Género" },
  ];

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
        const patients = await this.patientService.getPatients(this.token, searchParams);

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

  // Start editing a patient
  onEdit(patient: Patient): void {
    this.selectedItem = { ...patient };
    this.isEditing = true;
    this.editableAttributes.forEach((attr) => {
      this.editingFields[attr.key].value = patient[attr.key] || '';
    });
  }

  // Submit edits to the service
  submitEdit(): void {
    if (this.selectedItem) {
      this.editableAttributes.forEach((attr) => {
        if (this.editingFields[attr.key].selected) {
          this.selectedItem![attr.key] = this.editingFields[attr.key].value;
        }
      });
      this.patientService.editPatient(this.selectedItem);
      this.isEditing = false;
      this.loadPatients();
    }
  }

  // Cancel edit operation
  cancelEdit(): void {
    this.isEditing = false;
  }

  // Create a new patient
  onCreate(): void {
    const newPatient: Patient = {
      MedicalRecordNumber: "temp",
      name: `New Patient ${this.patients.length + 1}`,
      email: `newpatient${this.patients.length + 1}@example.com`,
    };
    this.patientService.createPatient(newPatient);
    this.loadPatients();
  }

  // Delete a patient
  async onDelete(patient: Patient | null): Promise<void> {
    if (patient) {
      try {
        const response = await this.patientService.deletePatient(this.token, patient.MedicalRecordNumber);
  
        this.showMessage = true;
        if (response) {
          this.messageText = 'Paciente eliminado com sucesso!';
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
