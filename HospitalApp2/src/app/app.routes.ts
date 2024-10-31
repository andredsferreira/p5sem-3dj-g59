import { Routes } from '@angular/router';
import { CubeComponent } from './cube/cube.component';
import { Cube2Component } from './cube2/cube2.component';

export const routes: Routes = [
    { path: '', redirectTo: '/cube', pathMatch: 'full'},
    { path: 'cube', component: CubeComponent},
    { path: 'cube2', component: Cube2Component},
];
