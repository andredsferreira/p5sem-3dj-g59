import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule, FormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'HospitalApp';
  constructor(private router: Router) {}

  privacy(): void {
    this.router.navigate(['/privacy']);
  }

  aboutus(): void {
    this.router.navigate(['/aboutus']);
  }

  contacts(): void {
    this.router.navigate(['/contacts']);
  }

  hospitalfloor(): void {
    this.router.navigate(['/hospitalfloor']);
  }
}
