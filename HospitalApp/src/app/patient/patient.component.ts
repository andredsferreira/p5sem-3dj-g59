import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { PatientUserService } from './patient-user-service';

@Component({
  selector: 'app-patient',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './patient.component.html',
  styleUrl: './patient.component.css'
})
export class PatientComponent {
  showConfirmation: boolean = false;
  showNotification: boolean = false;
  token: string | null = null;
  messageText = '';
  messageClass = '';

  constructor(private service: PatientUserService) {}

  async ngOnInit(): Promise<void> {
    this.token = localStorage.getItem('token'); // Get token from local storage
  }

  onDeleteProfileClick(): void {
    this.showConfirmation = true;
  }
  cancelDelete(): void {
    this.showConfirmation = false;
  }

  async confirmDelete(): Promise<void> {
    try {
      const response = await this.service.deleteAccount(this.token);

      this.showNotification = true;
      if (response) {
        this.messageText = "Um link de confirmação foi enviado para o seu email";
        this.messageClass = 'bg-green-500 text-white';
      }
      
    } catch (error) {
      console.error("Erro ao eliminar conta:", error);
      this.showNotification = true;
      this.messageText = 'Erro ao eliminar conta. Tente novamente.';
      this.messageClass = 'bg-red-500 text-white';
    } finally {
      this.showConfirmation = false;

      setTimeout(() => {
        this.showNotification = false;
      }, 3000);
    }
  }
}
