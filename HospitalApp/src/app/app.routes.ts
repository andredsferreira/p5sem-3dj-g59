import { Routes } from '@angular/router';
import { CubeComponent } from './cube/cube.component';
import { HospitalFloorComponent } from './hospitalfloor/hospitalfloor.component';
import { LoginComponent } from './auth/login.component';
import { UserListComponent } from './auth/user-list.component';
import { DoctorComponent } from './staff/doctor/doctor.component';
import { PrivacypolicyComponent } from './privacypolicy/privacypolicy.component';
import { AboutusComponent } from './aboutus/aboutus.component';
import { ContactsComponent } from './contacts/contacts.component';
import { PatientManagementComponent } from './patient-management/patient-management.component';

export const routes: Routes = [
    { path: '', component: LoginComponent },
    { path: 'privacy', component: PrivacypolicyComponent},
    { path: 'aboutus', component: AboutusComponent},
    { path: 'contacts', component: ContactsComponent},
    { path: 'userlist', component: UserListComponent },
    { path: 'doctor', component: DoctorComponent },
    { path: 'cube', component: CubeComponent },
    { path: 'hospitalfloor', component: HospitalFloorComponent },
    { path: 'patientmanagement', component: PatientManagementComponent },
];
