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

  allergyForm: FormGroup
  showCreateForm: boolean = false;

  errorMessage: string = ""

  constructor(private allergyService: AllergyService, private fb: FormBuilder) {
    this.allergyForm = this.fb.group({
      allergyName: ["", Validators.required],
      allergyDescription: ["", Validators.required]
    })
  }

  async addAllergy() {
    try {
      const response = await this.allergyService.addAllergy(
        this.allergyForm.value.allergyName,
        this.allergyForm.value.allergyDescription)
    } catch (error) {
      this.errorMessage = "Failed to add allergy"
      console.error(error)
    }
  }

  async getAllergyByName(name: string) {
    try {
      const response = await this.allergyService.getAllergyByName(
        this.allergyForm.value.allergyName
      )
      this.errorMessage = ""
    } catch (error) {
      this.errorMessage = "Failed getting allergy"
      console.error(error)
    }
  }

  toggleCreateForm() {
    this.showCreateForm = !this.showCreateForm;
  }

}
