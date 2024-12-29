import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterOutlet } from '@angular/router';
import { AuthService } from './auth/auth.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule, FormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'HospitalApp';
  token: string | null = null;
  username: string | null = null;
  
  constructor(private router: Router, private authService: AuthService) {}

  ngOnInit(): void {
    this.token = localStorage.getItem('token');
    if (!this.token) return;
    this.username = this.authService.getUsernameFromToken(this.token);
  }

  main(): void {
    this.router.navigate(['/']);
  }

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

  profile(): void {
    this.router.navigate(['/profile']);
  }

  login(): void {
    this.router.navigate(['/login']);
  }
}
