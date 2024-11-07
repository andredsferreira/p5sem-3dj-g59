import { Component, inject, OnInit } from '@angular/core';
import { AuthService } from '../auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-main-page',
  standalone: true,
  imports: [],
  templateUrl: './main-page.component.html',
  styleUrl: './main-page.component.css'
})
export class MainPageComponent implements OnInit {
  private authService = inject(AuthService);
  private router = inject(Router);
  ngOnInit(): void {
    let token = localStorage.getItem("token");
    if(token){
      let role = this.authService.getRoleFromToken(token);
      switch (role) {
          case 'Admin':
              this.router.navigate(['/userlist']);
              break;
          case 'Doctor':
              this.router.navigate(['/doctor']);
              break;
      }
    }
  }
}
