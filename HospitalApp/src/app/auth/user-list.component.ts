import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '../auth/auth.service';

@Component({
    selector: 'app-user-list',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './user-list.component.html',
})
export class UserListComponent implements OnInit {
    users: any[] = [];
    errorMessage: string | null = null;

    constructor(private authService: AuthService) { }

    async ngOnInit(): Promise<void> {
        const token = localStorage.getItem('token'); // Get token from local storage

        if (!token) {
            this.errorMessage = 'No token found. Please log in first.';
            return;
        }

        try {
            this.users = await this.authService.getUsers(token);
        } catch (error) {
            console.error('Failed to fetch users:', error);
            this.errorMessage = 'Failed to fetch users. Please try again.';
        }
    }
}
