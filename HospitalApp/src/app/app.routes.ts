import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { MainpageComponent } from './mainpage/mainpage.component';
import { ErrorComponent } from './error/error.component';
import { NgModule } from '@angular/core';
import { CubeComponent } from './cube/cube.component';
import { Cube2Component } from './cube2/cube2.component';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'mainpage', component: MainpageComponent },
  { path: 'error', component: ErrorComponent },
  { path: 'cube', component: CubeComponent },
  { path: 'cube2', component: Cube2Component },
  { path: '', component: MainpageComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
