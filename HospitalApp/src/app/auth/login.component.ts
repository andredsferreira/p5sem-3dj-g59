import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from './auth.service';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
    selector: 'app-login',
    standalone: true,
    imports: [CommonModule, ReactiveFormsModule],
    templateUrl: './login.component.html',
    styleUrl: './login.component.css'
})
export class LoginComponent {
    loginForm: FormGroup;
    errorMessage: string | null = null;
    success: boolean = false

    private authService = inject(AuthService);
    private router = inject(Router);

    constructor(private fb: FormBuilder) {
        this.loginForm = this.fb.group({
            username: ['', Validators.required],
            password: ['', Validators.required]
        });
    }

    async onLogin() {
        this.errorMessage = null;
        if (this.loginForm.valid) {
            try {
                const { username, password } = this.loginForm.value;
                let token = await this.authService.login(username, password);
                localStorage.setItem('token', token);
                this.success = true
                let role = this.authService.getRoleFromToken(token);
                switch (role) {
                    case 'Admin':
                        this.router.navigate(['/userlist']);
                        break;
                    case 'Doctor':
                        this.router.navigate(['/doctor']);
                        break;
                }
                console.log("Logged in successfully. Token saved.");
            } catch (error) {
                this.errorMessage = "Login failed. Please check your credentials and try again.";
                console.error("Login error:", error);
            }
        } else {
            this.errorMessage = "Please fill in all fields.";
        }
    }
}