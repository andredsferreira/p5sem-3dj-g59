import { Routes } from '@angular/router';
import { CubeComponent } from './cube/cube.component';
import { HospitalFloorComponent } from './hospitalfloor/hospitalfloor.component';
import { LoginComponent } from './auth/login.component';
import { AdminComponent } from './admin/admin.component';
import { UserListComponent } from './auth/user-list.component';
import { DoctorComponent } from './staff/doctor/doctor.component';
import { PrivacypolicyComponent } from './privacypolicy/privacypolicy.component';
import { AboutusComponent } from './aboutus/aboutus.component';
import { ContactsComponent } from './contacts/contacts.component';
import { PatientManagementComponent } from './patient-management/patient-management.component';
import { MainPageComponent } from './main-page/main-page.component';
import { RegisterPatientComponent } from './auth/register-patient.component';

export const routes: Routes = [
  { path: '', component: MainPageComponent },
  { path: 'login', component: LoginComponent },
  { path: 'privacy', component: PrivacypolicyComponent },
  { path: 'aboutus', component: AboutusComponent },
  { path: 'contacts', component: ContactsComponent },
  { path : 'admin', component: AdminComponent },
  { path: 'userlist', component: UserListComponent },
  { path: 'doctor', component: DoctorComponent },
  { path: 'registerpatient', component: RegisterPatientComponent },
  { path: 'cube', component: CubeComponent },
  { path: 'hospitalfloor', component: HospitalFloorComponent },
  { path: 'patientmanagement', component: PatientManagementComponent },
];
