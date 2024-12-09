import { Component } from '@angular/core';
import { AllergyService } from './allergy.service';

@Component({
  selector: 'app-allergy',
  standalone: true,
  imports: [],
  templateUrl: './allergy.component.html',
  styleUrl: './allergy.component.css'
})
export class AllergyComponent {

  allergyName: string = ""
  allergyDescription: string = ""
  errorMessage: string = ""

  constructor(private allergyService: AllergyService) {

  }

  async addAllergy() {
    try {
      const response = await this.allergyService.addAllergy(this.allergyName, this.allergyDescription)
      this.errorMessage = ""
      alert("Allergy successfully added")
    } catch (error) {
      this.errorMessage = "Failed to add allergy"
      console.error(error)
    }
  }

  async getAllergyByName(name: string) {
    try {
      const response = await this.allergyService.getAllergyByName(this.allergyName)
      this.errorMessage = ""
    } catch (error) {
      this.errorMessage = "Failed getting allergy"
      console.error(error)
    }
  }
  
}
