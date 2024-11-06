import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PatientService } from './patient-service';
import { FormsModule } from '@angular/forms';

// Define the Patient interface
interface Patient {
  id: number;
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

  // Define editable and searchable attributes
  editableAttributes = [
    { key: 'FullName', label: 'Nome' },
    { key: 'Email', label: 'Email' },
    { key: 'PhoneNumber', label: 'Telemóvel'}
  ];
  searchableAttributes = [
    { key: 'FullName', label: 'Name' },
    { key: 'Email', label: 'Email' },
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

  async loadPatients(): Promise<void> {
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
        } else {
            this.patients = [];
            console.log("No patients found.");
        }
    } catch (error) {
        console.error("Failed to load patients:", error);
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
      id: Date.now(),
      name: `New Patient ${this.patients.length + 1}`,
      email: `newpatient${this.patients.length + 1}@example.com`,
    };
    this.patientService.createPatient(newPatient);
    this.loadPatients();
  }

  // Delete a patient
  onDelete(patient: Patient): void {
    this.patientService.deletePatient(patient.id);
    this.loadPatients();
  }
}
