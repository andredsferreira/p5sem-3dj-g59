import { Component } from '@angular/core';
import { AllergyService } from './allergy.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-allergy',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './allergy.component.html',
  styleUrl: './allergy.component.css'
})
export class AllergyComponent {
  allergyForm: FormGroup;
  showCreateForm: boolean = false;

  selectedAllergy: any = null;
  updateForm: FormGroup;
  showUpdateForm: boolean = false;

  searchName: string = '';
  allergyResults: any[] = [];

  errorMessage: string = '';
  successMessage: string = ''; // New success message

  constructor(private allergyService: AllergyService, private fb: FormBuilder) {
    this.allergyForm = this.fb.group({
      allergyCode: ['', Validators.required],
      allergyName: ['', Validators.required],
      allergyDescription: ['', Validators.required],
    });
    this.updateForm = this.fb.group({
      allergyCode: [''],
      allergyName: [''],
      allergyDescription: [''],
    });
  }

  async addAllergy() {
    try {
      const response = await this.allergyService.addAllergy(
        this.allergyForm.value.allergyCode,
        this.allergyForm.value.allergyName,
        this.allergyForm.value.allergyDescription
      );
      this.toggleCreateForm();
      this.allergyForm.reset();
      this.successMessage = 'Allergy added successfully!';
      this.errorMessage = '';
      console.log(response);

      setTimeout(() => (this.successMessage = ''), 3000); // Clear success message after 3 seconds
    } catch (error) {
      this.errorMessage = 'Failed to add allergy';
      console.error(error);
    }
  }

  async getAllergyByName(name: string) {
    try {
      this.allergyResults = []; // Clear previous list
      const response = await this.allergyService.getAllergyByName(name);
      this.errorMessage = '';
      this.allergyResults = Array.isArray(response) ? response : [response];
    } catch (error) {
      this.errorMessage = 'No allergy found';
      console.error(error);
    }
  }

  async updateAllergy() {
    const { allergyCode, allergyName, allergyDescription } = this.updateForm.value;
    try {
      const response = await this.allergyService.updateAllergy(
        this.selectedAllergy.name,
        allergyCode,
        allergyName,
        allergyDescription
      );
      this.toggleUpdateForm();
      this.getAllergyByName(this.searchName); // Refresh results
      this.successMessage = 'Allergy updated successfully!';
      this.errorMessage = '';
      console.log(response);

      setTimeout(() => (this.successMessage = ''), 3000); // Clear success message after 3 seconds
    } catch (error) {
      this.errorMessage = 'Failed to update allergy';
      console.error(error);
    }
  }

  toggleCreateForm() {
    this.showCreateForm = !this.showCreateForm;
  }

  toggleUpdateForm(allergy?: any) {
    this.showUpdateForm = !this.showUpdateForm;
    if (allergy) {
      this.selectedAllergy = allergy;
      this.updateForm.setValue({
        allergyCode: allergy.code,
        allergyName: allergy.name,
        allergyDescription: allergy.description,
      });
    }
  }
}