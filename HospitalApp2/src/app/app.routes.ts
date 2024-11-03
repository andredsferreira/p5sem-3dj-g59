import { Routes } from '@angular/router';
import { CubeComponent } from './cube/cube.component';
import { HospitalFloorComponent } from './hospitalfloor/hospitalfloor.component';
import { LoginComponent } from './auth/login.component';
import { UserListComponent } from './auth/user-list.component';
import { DoctorComponent } from './staff/doctor/doctor.component';

export const routes: Routes = [
    { path: '', component: LoginComponent },
    { path: 'userlist', component: UserListComponent },
    { path: 'doctor', component: DoctorComponent },
    { path: 'cube', component: CubeComponent },
    { path: 'hospitalfloor', component: HospitalFloorComponent },
];
