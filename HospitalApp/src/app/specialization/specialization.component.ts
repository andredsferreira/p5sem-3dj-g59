import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SpecializationService } from './specialization.service';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';

import { Router } from '@angular/router';
import { HttpParams } from '@angular/common/http';

interface Specialization {

    codeSpec: string;
    designation: string;
    description: string;

}

@Component({
    selector: 'app-specialization',
    standalone: true,
    imports: [CommonModule, ReactiveFormsModule],
    templateUrl: './specialization.component.html'
})
export class SpecializationComponent {

    addForm: FormGroup;
    editForm: FormGroup;

    isSubmiting: boolean = false;
    showModal: boolean = false;
    isCanceled: boolean = false;

    showEditModal: boolean = false;

    listing: boolean = false;

    specializations: any[] = [];

    formError: string | null = null;

    constructor(private specializationService: SpecializationService, private formBuilder: FormBuilder, private router: Router) {
        this.addForm = this.formBuilder.group({
            codeSpec: ['', Validators.required],
            Designation: ['', Validators.required],
            description: ['', Validators.required]
        });
        this.editForm = this.formBuilder.group({
            codeSpec: ['', Validators.required],
            Designation: ['', Validators.required],
            description: ['', Validators.required]
        });
    }

    backToAdmin(): void {
        this.router.navigate(['/admin']);
    }

    async onSubmit(): Promise<void> {
        if (!this.isSubmiting) {
            this.isSubmiting = true;
            if (this.addForm.valid) {
                try {
                    const {
                        codeSpec,
                        Designation,
                        description
                    } = this.addForm.value;

                    console.log(this.addForm.value);

                    await this.specializationService.createSpecialization(codeSpec, Designation, description);
                } catch (error) {
                    console.error('Error creating specialization:', error);
                    this.formError = 'Failed to create specialization';
                    alert('Ocorreu um erro ao criar a especialização. Por favor, tente novamente mais tarde.');
                }
            } else if (!this.isCanceled) {
                this.formError = 'Please fill all fields';
                alert("O Formulário não foi preenchido corretamente. Por favor, preencha todos os campos.");
            }
            this.isSubmiting = false;
            this.isCanceled = false;
        }

    }

    async listSpecializations(): Promise<any> {
    
        this.listing = true;
        try {
            const response = await this.specializationService.getSpecialization();
            console.log(response);
            this.listing = false;
            return response;
        } catch (error) {
            console.error('Error getting specialization:', error);
            this.listing = false;
            throw error;
        }
        
    }

    

    createSpecialization(): void {
        this.showModal = true;
        this.formError = null;
    }

    closeModal(): void {
        this.showModal = false;
        this.isCanceled = true;
        this.addForm.reset();
    }

    editSpecialization(): void {
        this.showEditModal = true;
        this.isCanceled = false;
        this.editForm.reset();
    }



}