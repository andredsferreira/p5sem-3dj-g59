import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { AuthService } from './auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-register-patient',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: 'register-patient.component.html',
  styleUrl: 'register-patient.component.css',
})
export class RegisterPatientComponent {
  registerForm: FormGroup;
  loading: boolean = false;
  errorMessage: string = '';
  successMessage: string = '';

  constructor(private fb: FormBuilder, private authService: AuthService) {
    this.registerForm = this.fb.group({
      username: ['', [Validators.required]],
      email: ['', [Validators.required]],
      phone: ['', [Validators.required]],
      password: ['', [Validators.required]],
    });
  }

  async onSubmit() {
    if (this.registerForm.invalid) {
      this.errorMessage = 'Please fill in all required fields correctly.';
      return;
    }

    this.loading = true;
    this.errorMessage = '';
    this.successMessage = '';

    const { username, email, phone, password } = this.registerForm.value;

    try {
      await this.authService.registerPatient(username, email, phone, password);
      this.successMessage = 'Patient registered successfully!';
      this.registerForm.reset();
    } catch (error) {
      this.errorMessage = 'Failed to register patient. Please try again.';
      console.error('Error during patient registration:', error);
    } finally {
      this.loading = false;
    }
  }
}
