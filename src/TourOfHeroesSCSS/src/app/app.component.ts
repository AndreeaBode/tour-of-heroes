import { Component } from '@angular/core';
import { AuthService } from './auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  constructor(public authService: AuthService,
    private router: Router){}

  onLogout() {
    localStorage.removeItem('jwtToken');
    this.router.navigate(['/heroes']);
    
  }
  title = 'Tour of Heroes';
}
