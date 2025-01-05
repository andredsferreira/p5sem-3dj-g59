import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AuthService } from '../../auth/auth.service';

@Component({
  selector: 'app-doctor',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './doctor.component.html',
  styleUrls: ['./doctor.component.css'],
})
export class DoctorComponent implements OnInit {

  errorMessage: string | null = null;
  token: string | null = null;
  showPage = false;

  constructor(private authService: AuthService, private router: Router) {

  }

  async ngOnInit(): Promise<void> {
    this.token = localStorage.getItem('token'); // Get token from local storage

    if(!this.token){
      this.errorMessage = 'No token found. Please log in first';
      return;
    }

    if (this.authService.getRoleFromToken(this.token).toLowerCase() !== "doctor") {
      this.errorMessage = 'You do not have the right permissions for this functionality.';
      return;
    }
    this.showPage = true;
  }

  openOperationRequests(){
    this.router.navigate([`/operation-requests`]);
  }

  openAppointments(){
    this.router.navigate([`/appointments`]);
  }

  openMedicalCondition(){
    this.router.navigate([`/medicalcondition`]);
  }
  
}
