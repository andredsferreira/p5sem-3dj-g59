import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OperationTypeService } from './operation-type.service';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { Status } from './status.enum';
import { Specialization } from './specialization.enum';
import { Router } from '@angular/router';
import { HttpParams } from '@angular/common/http';


// Define the OperationType interface

interface Field {
    selected: boolean;
    value: string | number;
  }

interface OperationType {
    id: string;
    name: string;
    anaesthesiaTime: number;
    surgeryTime: number;
    cleaningTime: number;
    minDoctor: number;
    minAnaesthetist: number;
    minNurseAnaesthetist: number;
    minInstrumentingNurse: number;
    minCirculatingNurse: number;
    minXRayTechnician: number;
    minMedicalActionAssistant: number;
    Status: Status;
    Specialization: Specialization;
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
    isSubmiting: boolean = false;
    showUpdateModal: boolean = false;
    editId: string | null = null;
    isCanceled: boolean = false;

    nomeFilter: string = '';
    statusFilter: string = '';
    specializationFilter: string = '';

    addForm: FormGroup;
    editForm: FormGroup;
    filterForm: FormGroup;

    filtering: boolean = false;
    listing: boolean = false;

    createFields: { [key: string]: Field} = {};
    editingFields: { [key: string]: Field} = {};




    constructor(private fb: FormBuilder, private operationTypeService: OperationTypeService, private router: Router) {
        this.addForm = this.fb.group({
            name: ['', Validators.required],
            anaesthesiaTime: ['', Validators.required],
            surgeryTime: ['', Validators.required],
            cleaningTime: ['', Validators.required],
            specialization: [Validators.required],
            minDoctor: ['',],
            minAnaesthetist: [''],
            minInstrumentingNurse: [''],
            minCirculatingNurse: [''],
            minNurseAnaesthetist: [''],
            minXRayTechnician: [''],
            minMedicalActionAssistant: ['']
        });

        this.editForm = this.fb.group({
            name: ['', Validators.required],
            anaesthesiaTime: [''],
            surgeryTime: [''],
            cleaningTime: [''],
            minDoctor: [],
            minAnaesthetist: [''],
            minInstrumentingNurse: [''],
            minCirculatingNurse: [''],
            minNurseAnaesthetist: [''],
            minXRayTechnician: [''],
            minMedicalActionAssistant: ['']
        });

        this.filterForm = this.fb.group({
            name: [''],
            status: [''],
            specialization: ['']
        });
    }

    backToAdmin(): void {

        console.log('Admin');
        this.router.navigate(['/admin']);
    }

    async listOperationType(): Promise<any> {

        try {
            this.operationTypes = await this.operationTypeService.listOperationTypes();
            
            if(this.nomeFilter !== '') {
                this.operationTypes = this.operationTypes.filter((operationType) => operationType.name.toLowerCase().includes(this.nomeFilter.toLowerCase()));
            }
            if(this.specializationFilter !== ''){
                this.operationTypes = this.operationTypes.filter((operationType) => operationType.Specialization.toString() === this.filterForm.value.specialization);
            }
            if(this.statusFilter !== ''){
                this.operationTypes = this.operationTypes.filter((operationType) => operationType.Status.toString() === this.filterForm.value.status);
            }

            this.paginatedOperationTypes = this.operationTypes.slice(0, this.pageSize);

            this.notFound = this.operationTypes.length === 0;
            console.log('Operation Types:', this.operationTypes);
        } catch (error) {
            console.error('Error listing operation types:', error);
        }
        this.listing = true;
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
                
            } catch (error) {
                console.error('Failed to delete operation type:', error);
                this.confirmingDelete = false;
                this.typeToDelete = null;
            }

        }
        this.listOperationType();
    }

    updateOperationType(id: string): void {

        //replace teh blank form data with the data from the operation type

        this.selectedItem = this.operationTypes.find((operationType) => operationType.id === id);
        console.log('Selected item:', this.selectedItem);



        if (!this.selectedItem) {
            console.error('Operation type not found:', id);
            return;
        }

        this.editForm.patchValue({
            name: this.selectedItem.name,
            anaesthesiaTime: this.selectedItem.anaesthesiaTime,
            surgeryTime: this.selectedItem.surgeryTime,
            cleaningTime: this.selectedItem.cleaningTime,
            minDoctor: this.selectedItem.minDoctor,
            minAnaesthetist: this.selectedItem.minAnaesthetist,
            minInstrumentingNurse: this.selectedItem.minInstrumentingNurse,
            minCirculatingNurse: this.selectedItem.minCirculatingNurse,
            minNurseAnaesthetist: this.selectedItem.minNurseAnaesthetist,
            minXRayTechnician: this.selectedItem.minXRayTechnician,
            minMedicalActionAssistant: this.selectedItem.minMedicalActionAssistant
        });

        this.editId = this.selectedItem.id;
        console.log(this.selectedItem.id);

        this.showUpdateModal = true;
        this.formError = null;

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
        this.isCanceled = true
        this.addForm.reset();

    }
    closeUpdate(): void {
        this.showUpdateModal = false;
        this.isCanceled = true
        this.editForm.reset();
    }

    closeFilter(): void {
        this.filtering = false;
        this.filterForm.reset();
    }

    showFilter(): void {

        this.filtering = true

    }
    submitFilter(): void {
        
        this.nomeFilter = this.filterForm.value.name;
        this.statusFilter = this.filterForm.value.status;
        this.specializationFilter = this.filterForm.value.specialization;
        
        console.log(this.filterForm.value);
        console.log(this.nomeFilter);
        console.log(this.statusFilter);
        
        this.filtering = false;
        this.listOperationType();
    }

    async onSubmit(): Promise<any> {
        if (!this.isSubmiting) {
            this.isSubmiting = true;
            console.log('Form submitted:', this.addForm.value);
            if (this.addForm.valid) {
                console.log('Form is valid');
                try {
                    const {
                        name,
                        anaesthesiaTime,
                        surgeryTime,
                        cleaningTime,
                        specialization,
                        minDoctor,
                        minAnaesthetist,
                        minInstrumentingNurse,
                        minNurseAnaesthetist,
                        minCirculatingNurse,
                        minXRayTechnician,
                        minMedicalActionAssistant
                    } = this.addForm.value;

                    console.log(specialization);

                    await this.operationTypeService.createOperationType(
                        name,
                        anaesthesiaTime,
                        surgeryTime,
                        cleaningTime,
                        specialization,
                        minDoctor,
                        minAnaesthetist,
                        minInstrumentingNurse,
                        minCirculatingNurse,
                        minNurseAnaesthetist,
                        minXRayTechnician,
                        minMedicalActionAssistant
                    );

                    this.closeModal();
                    this.listOperationType();
                } catch (error) {
                    this.formError = 'Failed to create operation type. Please check your input and try again.';
                    alert("Ocorreu um erro ao criar o tipo de operação. Por favor, tente novamente mais tarde")
                }
            } else if(!this.isCanceled){
                this.formError = 'Failed to create operation type. Please check your input and try again.';
                alert("O formulário não é válido");
            }
            this.isSubmiting = false;
            this.isCanceled = false;
        }
    }

    async submitUpdate(): Promise<void> {

        if (!this.isSubmiting && this.selectedItem) {
            this.isSubmiting = true;
            console.log('Form submitted:', this.editForm.value);
            if (this.editForm.valid) {
                console.log('Form is valid');
                try {
                    const {
                        name,
                        anaesthesiaTime,
                        surgeryTime,
                        cleaningTime,
                        minDoctor,
                        minAnaesthetist,
                        minInstrumentingNurse,
                        minNurseAnaesthetist,
                        minCirculatingNurse,
                        minXRayTechnician,
                        minMedicalActionAssistant
                    } = this.editForm.value;

                    console.log(this.selectedItem?.id);

                    const updatedOperationType = new HttpParams()
                        .set('name', name)
                        .set('anaesthesiaTime', anaesthesiaTime.toString())
                        .set('surgeryTime', surgeryTime.toString())
                        .set('cleaningTime', cleaningTime.toString())
                        .set('minDoctor', minDoctor.toString())
                        .set('minAnaesthetist', minAnaesthetist.toString())
                        .set('minInstrumentingNurse', minInstrumentingNurse.toString())
                        .set('minCirculatingNurse', minCirculatingNurse.toString())
                        .set('minNurseAnaesthetist', minNurseAnaesthetist.toString())
                        .set('minXRayTechnician', minXRayTechnician.toString())
                        .set('minMedicalActionAssistant', minMedicalActionAssistant.toString());

                      
                        
                    await this.operationTypeService.updateOperationType(this.selectedItem.id, updatedOperationType);

                    this.closeUpdate();
                    this.listOperationType();
                } catch (error) {
                    this.formError = 'Failed to update operation type. Please check your input and try again.';
                    alert("Ocorreu um erro ao editar o tipo de operação. Por favor, tente novamente mais tarde")
                }
            } else if(!this.isCanceled) {
                this.formError = 'Failed to update operation type. Please check your input and try again.';
                alert("O formulário não é válido");
            }
            this.isSubmiting = false;
            this.isCanceled = false;

        }
    }
}

