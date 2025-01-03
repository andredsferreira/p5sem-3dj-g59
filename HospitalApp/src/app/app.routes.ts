import { Routes } from '@angular/router';
import { CubeComponent } from './cube/cube.component';
import { HospitalFloorComponent } from './hospitalfloor/hospitalfloor.component';
import { LoginComponent } from './auth/login.component';
import { AdminComponent } from './admin/admin.component';
import { UserListComponent } from './auth/user-list.component';
import { DoctorComponent } from './staff/doctor/doctor.component';
import { MedicalConditionComponent } from './medical-condition/medical-condition.component';

import { PrivacypolicyComponent } from './privacypolicy/privacypolicy.component';
import { AboutusComponent } from './aboutus/aboutus.component';
import { ContactsComponent } from './contacts/contacts.component';
import { PatientManagementComponent } from './patient-management/patient-management.component';
import { MainPageComponent } from './main-page/main-page.component';
import { RegisterPatientComponent } from './auth/register-patient.component';
import { OperationTypeComponent } from './operation-types/operation-type.component';
import { PatientComponent } from './patient/patient.component';
import { PlanningComponent } from './planning/planning.component';
import { StaffManagement } from './staff-management/staff-management.component';
import { AllergyComponent } from './allergy/allergy.component';
import { SpecializationComponent } from './specialization/specialization.component';
import { MedicalRecordComponent } from './medicalrecord/medicalrecord.component';
import { ProfileComponent } from './profile/profile.component';

export const routes: Routes = [
  { path: '', component: MainPageComponent },
  { path: 'profile', component: ProfileComponent},
  { path: 'login', component: LoginComponent },
  { path: 'privacy', component: PrivacypolicyComponent },
  { path: 'aboutus', component: AboutusComponent },
  { path: 'contacts', component: ContactsComponent },
  { path: 'admin', component: AdminComponent },
  { path: 'scheduler', component: PlanningComponent },
  { path: 'patient', component: PatientComponent },
  { path: 'userlist', component: UserListComponent },
  { path: 'doctor', component: DoctorComponent },
  { path: 'allergy', component: AllergyComponent },
  { path: 'registerpatient', component: RegisterPatientComponent },
  { path: 'cube', component: CubeComponent },
  { path: 'hospitalfloor', component: HospitalFloorComponent },
  { path: 'patientmanagement', component: PatientManagementComponent },
  { path: 'operationtypemanagement', component: OperationTypeComponent },
  { path: 'staffmanagement', component: StaffManagement },
  {path: 'specializationmanagement', component: SpecializationComponent},
  { path: 'medicalrecord/:id', component: MedicalRecordComponent},
  { path: 'medicalcondition', component: MedicalConditionComponent }
];
