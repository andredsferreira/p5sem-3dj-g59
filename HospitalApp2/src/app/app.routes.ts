import { Routes } from '@angular/router';
import { CubeComponent } from './cube/cube.component';
import { HospitalFloorComponent } from './hospitalfloor/hospitalfloor.component';

export const routes: Routes = [
    { path: '', redirectTo: '/cube', pathMatch: 'full'},
    { path: 'cube', component: CubeComponent},
    { path: 'hospitalfloor', component: HospitalFloorComponent},
];
