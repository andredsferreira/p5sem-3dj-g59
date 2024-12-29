import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth/auth.service';
import { CommonModule } from '@angular/common';
import { FamilyHistoryEntry } from '../medicalrecord/entry-types';
import { ProfileService } from './profile-service';


@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit {
  token: string|null = null;
  name: string|null = null;
  role: string|null = null;
  email: string|null = null;
  confirmDeletionRequest: boolean = false;


  medicalRecordNumber: string | null = null;


  constructor(private authService: AuthService, private service: ProfileService){}

  ngOnInit(): void {
      this.token = localStorage.getItem('token');
      if (!this.token) return;
      this.name = this.authService.getUsernameFromToken(this.token);
      this.role = this.authService.getRoleFromToken(this.token);
      this.email = this.authService.getEmailFromToken(this.token);
  }

  /*downloadTxtFile() {
    const content = "Olá";
    const blob = new Blob([content], { type: 'text/plain' });
    const url = window.URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = 'mensagem.txt';
    a.click();
    window.URL.revokeObjectURL(url);
  }*/

  async downloadMedicalRecord() {
    const mrn = this.getMRN(this.email!);

    const output = (await this.service.getHistoryZip(this.token, mrn)).subscribe(
      (blob) => {
        const link = document.createElement('a');
        link.href = window.URL.createObjectURL(blob);
        link.download = `medical_record_${mrn}.zip`;
        link.click();
      },
      (error) => {
        console.error('Error downloading medical record:', error);
      }
    );
  }

  getMRN(email: string){
    return "202412000002";
  }

  getFamilyHistoryEntries(mrn: string, familyHistoryEntries: FamilyHistoryEntry[]){
    return {
      medicalRecordNumber: mrn,
      familyHistoryEntries: familyHistoryEntries.map(({ relative, history }) => ({
        relative,
        history,
      })),
    };
  }

  onDelete(){
    console.log("Deleting :)");
    this.confirmDeletionRequest = false;
  }
}

