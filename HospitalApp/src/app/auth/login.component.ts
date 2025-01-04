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
    registForm: FormGroup;
    errorMessage: string | null = null;
    success: boolean = false
    isRegistering: boolean = false;

    private authService = inject(AuthService);
    private router = inject(Router);

    constructor(private fb: FormBuilder) {
        this.loginForm = this.fb.group({
            username: ['', Validators.required],
            password: ['', Validators.required]
        });
        this.registForm = this.fb.group({
            username: ['', Validators.required],
            password: ['', Validators.required],
            patientEmail: ['', Validators.required],
            phoneNumber: ['', Validators.required]
        })
    }

    async onLogin() {
        this.errorMessage = null;
        if (this.loginForm.valid) {
            try {
                const { username, password } = this.loginForm.value;
                let token = (await this.authService.login(username, password)).body!.token;
                console.log("Token!!!!!", token);
                localStorage.setItem('token', token!);
                this.success = true
                let role = this.authService.getRoleFromToken(token!);
                switch (role.toLowerCase()) {
                    case 'admin':
                        this.router.navigate(['/admin']);
                        break;
                    case 'doctor':
                        this.router.navigate(['/doctor']);
                        break;
                    case 'patient':
                        this.router.navigate(['/patient']);
                        break;    
                }
                console.log("Logged in successfully. Token saved.");
            } catch (error) {
                this.errorMessage = "A autenticação falhou. Por favor verifique as credenciais e tente outra vez.";
                console.error("Login error:", error);
            }
        } else {
            this.errorMessage = "Por favor preencha todos os campos.";
        }
    }

    async onRegister() {
        this.errorMessage = null;
        if(this.registForm.valid){
            try{
                const {username, password, patientEmail, phoneNumber} = this.registForm.value;
                let token = await this.authService.registerPatient(username,patientEmail,phoneNumber,password);
                localStorage.setItem('token', token);
                this.success = true
                this.router.navigate(['/patient']);
            } catch (error) {
                this.errorMessage = "A autenticação falhou. Por favor verifique as credenciais e tente outra vez.";
                console.error("Login error:", error);
            }
        } else {
            this.errorMessage = "Por favor preencha todos os campos.";
        }
    }
    
}
