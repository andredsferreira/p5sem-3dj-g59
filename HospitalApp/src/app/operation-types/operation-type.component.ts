import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OperationTypeService } from './operation-type.service';
import { FormsModule, FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { Status } from './status.enum';
import { Specialization } from './specialization.enum';
import { Router } from '@angular/router';


// Define the OperationType interface
interface OperationType {
    id: string;
    name: string;
    anaesthesiaTime: number;
    surgeryTime: number;
    cleaningTime: number;
    status: Status;
    specializations: Specialization;
    minDoctor: number;
    minAnaesthetist: number;
    minInstrumentNurse: number;
    minNurseAnaesthetist: number;
    minXRay: number;
    minMedicalAssistant: number;
}

interface field {
    selected: boolean;
    value: string;
}

@Component({
    selector: 'app-operation-type',
    standalone: true,
    imports: [CommonModule, ReactiveFormsModule],
    templateUrl: './operation-type.component.html',
    styleUrl: './operation-type.component.css'
})
export class OperationTypeComponent /*implements OnInit*/ {

    errorMessage: string | null = null;
    operationTypes: any[] = [];
    selectedItem: OperationType | null = null;
    isEditing = false;
    isInitialized = false;
    token: string | null = null;
    paginatedOperationTypes: any[] = [];
    currentPage: number = 1;
    pageSize: number = 5;
    confirmingDelete: boolean = false;
    typeToDelete: string | null = null;
    showMessage = false;
    messageText = '';
    messageClass = '';
    notFound: boolean = false;
    showModal: boolean = false;
    formError: string | null = null;
    addForm: FormGroup;


    // Define editable and searchable attributes
    editableAttributes = [
        { key: 'name', label: 'Nome' },
        { key: 'anaesthesiaTime', label: 'Tempo de Anestesia' },
        { key: 'surgeryTime', label: 'Tempo de Cirurgia' },
        { key: 'cleaningTime', label: 'Tempo de Limpeza' },
        { key: 'status', label: 'Estado' },
        { key: 'specializations', label: 'Especialização' },
        { key: 'minDoctor', label: 'Mínimo de Médicos' },
        { key: 'minAnaesthetist', label: 'Mínimo de Anestesistas' },
        { key: 'minInstrumentNurse', label: 'Mínimo de Enfermeiros Instrumentistas' },
        { key: 'minNurseAnaesthetist', label: 'Mínimo de Enfermeiros Anestesistas' },
        { key: 'minXRay', label: 'Mínimo de Técnicos de Raio-X' },
        { key: 'minMedicalAssistant', label: 'Mínimo de Assistentes Médicos' }
    ];
    searchableAttributes = [
        { key: 'name', label: 'Nome' },
        { key: 'anaesthesiaTime', label: 'Tempo de Anestesia' },
        { key: 'surgeryTime', label: 'Tempo de Cirurgia' },
        { key: 'cleaningTime', label: 'Tempo de Limpeza' },
        { key: 'status', label: 'Estado' },
        { key: 'specializations', label: 'Especialização' },
        { key: 'minDoctor', label: 'Mínimo de Médicos' },
        { key: 'minAnaesthetist', label: 'Mínimo de Anestesistas' },
        { key: 'minInstrumentNurse', label: 'Mínimo de Enfermeiros Instrumentistas' },
        { key: 'minNurseAnaesthetist', label: 'Mínimo de Enfermeiros Anestesistas' },
        { key: 'minXRay', label: 'Mínimo de Técnicos de Raio-X' },
        { key: 'minMedicalAssistant', label: 'Mínimo de Assistentes Médicos' }
    ];

    constructor(private fb: FormBuilder, private operationTypeService: OperationTypeService, private router: Router) {
        this.addForm = this.fb.group({
            name: ['', Validators.required,],
            anaesthesiaTime: ['', Validators.required],
            surgeryTime: ['', Validators.required],
            cleaningTime: ['', Validators.required],
            status: ['', Validators.required],
            Specialization: ['', Validators.required],
            minDoctor: ['', Validators.required],
            minAnaesthetist: ['', Validators.required],
            minInstrumentNurse: ['', Validators.required],
            minCirculatingNurse: ['', Validators.required],
            minNurseAnaesthetist: ['', Validators.required],
            minXRay: ['', Validators.required],
            minMedicalAssistant: ['', Validators.required]
        });
    }

    backToAdmin(): void {

        console.log('Admin');
        this.router.navigate(['/admin']);
    }

    async listOperationType(): Promise<any> {

        try {
            this.operationTypes = await this.operationTypeService.listOperationTypes();
            this.notFound = this.operationTypes.length === 0;
            console.log('Operation Types:', this.operationTypes);
        } catch (error) {
            console.error('Error listing operation types:', error);
        }
    }

    async deleteOperationType(id: string): Promise<void> {
        console.log('Deleting operation type:', id);
        this.typeToDelete = id;
        this.confirmingDelete = true;
    }

    confirmDelete(): void {
        console.log('Deleting operation type:', this.selectedItem?.id);
        if (this.typeToDelete) {

            try {
                this.operationTypeService.deleteOperationType(this.typeToDelete);
                this.confirmingDelete = false;
                this.typeToDelete = null;
                console.log('Operation Type deleted');
                this.listOperationType();
            } catch (error) {
                console.error('Failed to delete operation type:', error);
                this.confirmingDelete = false;
                this.typeToDelete = null;
            }

        }
    }

    cancelDelete(): void {
        this.confirmingDelete = false;
        this.typeToDelete = null;
    }

    async createOperationType(): Promise<void> {
        this.showModal = true;
        this.formError = null;

    }

    closeModal(): void {
        this.showModal = false;
        this.addForm.reset();

    }

    async onSubmit(): Promise<void> {
        if (this.addForm.valid) {
            try {
                const {
                    name,
                    anaesthesiaTime,
                    surgeryTime,
                    cleaningTime,
                    status,
                    specializations,
                    minDoctor,
                    minAnaesthetist,
                    minInstrumentNurse,
                    minNurseAnaesthetist,
                    minXRay,
                    minMedicalAssistant
                } = this.addForm.value;

                await this.operationTypeService.createOperationType(
                    name,
                    anaesthesiaTime,
                    surgeryTime,
                    cleaningTime,
                    status,
                    specializations,
                    minDoctor,
                    minAnaesthetist,
                    minInstrumentNurse,
                    minNurseAnaesthetist,
                    minXRay,
                    minMedicalAssistant
                );

                this.closeModal();
                this.listOperationType();
            } catch (error) {
                this.formError = 'Failed to create operation type. Please check your input and try again.';
            }
        }
    }
}


