import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth/auth.service';
import { CommonModule } from '@angular/common';
import { FamilyHistoryEntry } from '../medicalrecord/entry-types';
import { ProfileService } from './profile-service';
import { Patient } from '../patient-management/patient-types';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';


@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit {

  token: string|null = null;
  name: string|null = null;
  role: string|null = null;
  email: string|null = null;
  confirmDeletionRequest: boolean = false;
  isSettingPassword: boolean = false;
  passwordForm: FormGroup;


  medicalRecordNumber: string | null = null;


  constructor(private authService: AuthService, private service: ProfileService, private router: Router, private fb: FormBuilder){
    this.passwordForm = this.fb.group({
        password: ['', Validators.required]
    })
  }

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
    if (this.passwordForm.valid) {
      const {password} = this.passwordForm.value;
      const mrn = await this.getMRN(this.email!);

      const output = (await this.service.getHistoryZip(this.token, mrn, password)).subscribe(
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
  }

  cancelPasswordSetting() {
    this.passwordForm.reset();
    this.isSettingPassword = false;
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/']);
  }

  async getMRN(email: string){
    return ((await this.service.getMRNFromUserEmail(this.token, email)).body as Patient).MedicalRecordNumber;
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

