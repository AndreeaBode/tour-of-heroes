import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';




@Component({
  selector: 'app-hero-login',
  templateUrl: './hero-login.component.html',
  styleUrls: ['./hero-login.component.scss']
})

export class HeroLoginComponent implements OnInit {
  authType: 'login' | 'signup' = 'login';
  email: string = '';
  password: string = '';
  confirmedPassword = '';


  constructor(private http: HttpClient, 
    private router: Router) { }

  ngOnInit(): void {
 
  }
  

  onLogin() {
    if (this.authType === 'login') {
      if (!this.email || !this.password) {
        console.error('Email and password are required.');
        return;
      }

      const loginData = {
        email: this.email,
        password: this.password
      };

      this.http.post('https://localhost:44346/login', loginData).subscribe(
        (response: any) => {
          if (response && response.token) {
            localStorage.setItem('jwtToken', response.token);
            console.log('Login successful', response);
          } else {
            console.error('Invalid response from server');
          }
        },
        (error) => {
          console.error('Login error', error);
        }
      );
    }
  }


  onSignup() {
    if (this.authType === 'signup') {
      if (!this.email || !this.password || !this.confirmedPassword) {
        console.error('Email, password, and confirmed password are required.');
        return;
      }

      if (this.password !== this.confirmedPassword) {
        console.error('Password and confirmed password do not match.');
        return;
      }

      const signupData = {
        email: this.email,
        password: this.password,
        confirmedPassword: this.confirmedPassword,
      };

      this.http.post('https://localhost:44346/register', signupData).subscribe(
        (response: any) => {
          if (response && response.tokenResponse && response.tokenResponse.token) {
            const jwtToken = response.tokenResponse.token;
            localStorage.setItem('jwtToken', jwtToken);
            console.log('Signup successful', response);
            this.authType='login';
          } else {
            console.error('Invalid response from server');
          }
        },        
        (error) => {
          console.error('Login error', error);
        }
      );
    }
  }


  
  onLogout() {
    localStorage.removeItem('jwtToken');
    this.router.navigate(['/heroes']);
    
  }
  setAuthType(type: 'login' | 'signup') {
    this.authType = type;
  }
}