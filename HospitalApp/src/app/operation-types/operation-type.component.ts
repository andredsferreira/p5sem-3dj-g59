import {Component, OnInit} from '@angular/core';
import {CommonModule} from '@angular/common';
import { OperationTypeService } from './operation-type.service';
import { FormsModule, FormGroup, FormBuilder, Validators } from '@angular/forms';
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
    imports: [CommonModule, FormsModule],
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
        typeToDelete: string | null =null;
        showMessage = false;
        messageText = '';
        messageClass = '';
        notFound: boolean = false;
        //addForm: FormGroup;
    
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

        constructor(private operationTypeService: OperationTypeService, private router: Router) {
        }

        
        backToAdmin(): void {

            console.log('Admin');
            this.router.navigate(['/admin']);
        }

        async listOperationType(): Promise<any> {

            try{
                this.operationTypes = await this.operationTypeService.listOperationTypes();
                this.notFound = this.operationTypes.length === 0;
                console.log('Operation Types:', this.operationTypes);
            }catch(error){
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

            try{
                this.operationTypeService.deleteOperationType(this.typeToDelete);
                this.confirmingDelete = false;
                this.typeToDelete = null;
                console.log('Operation Type deleted');
                this.listOperationType();
            }catch(error){
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
    
}

