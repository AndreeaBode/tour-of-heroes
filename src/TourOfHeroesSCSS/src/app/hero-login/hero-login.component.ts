import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router, RouterLink } from '@angular/router';


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
  selectedRole = '';
  emailError: boolean = false;
  passwordError: boolean = false;
  passwordConfirmedError: boolean = false;
  

  constructor(private http: HttpClient, 
    private router: Router) { }

  ngOnInit(): void {
  }
  

  onLogin() {
    this.emailError = false;
  this.passwordError = false;
    if (this.authType === 'login') {

      if(!this.email && !this.password){
        this.passwordError = true;
        this.emailError = true; 
      }

      if (this.authType === 'login') {
        if (!this.email) {
          this.emailError = true;
          return;
        }
    
        if (!this.password) {
          this.passwordError = true;
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
            this.router.navigateByUrl('/heroes');
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
  }

  onSignup() {

    if (this.authType === 'signup') {

      if(!this.email && !this.password){
        this.passwordError = true;
        this.emailError = true; 
      }

      if (!this.email) {
        this.emailError = true;
        this.passwordError = false; 
      } 
  
      if (!this.password) {
        this.passwordError = true;
        this.emailError = false; 
        return;
      }
  
      if (this.password !== this.confirmedPassword) {
        this.passwordConfirmedError = true;
        this.emailError = false; 
        return;
      }

      
      const signupData = {
        email: this.email,
        password: this.password,
        confirmedPassword: this.confirmedPassword,
        role: this.selectedRole,
      };

      console.log("roleee")
      console.log(signupData.role);
      console.log(this.selectedRole);
      console.log(this.confirmedPassword);

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
          console.error('Signup error', error);
        }
      );
    }
  }

  
  onLogout() {
    localStorage.removeItem('jwtToken');
    this.router.navigate(['/heroes']);
    
  }
  setAuthType(type: 'login' | 'signup') {
    //window.location.reload();
    this.authType = type;
  }
}