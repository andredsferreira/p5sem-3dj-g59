import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { LoginComponent } from "./login/login.component";
import { ErrorComponent } from "./error/error.component";
import { MainpageComponent } from "./mainpage/mainpage.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, FormsModule, LoginComponent, ErrorComponent, MainpageComponent],
  templateUrl: './app.component.html',
  styles: [],
})
export class AppComponent {
  title = 'HospitalApp';
}
