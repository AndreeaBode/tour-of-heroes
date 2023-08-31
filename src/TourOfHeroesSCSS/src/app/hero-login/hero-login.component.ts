import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-hero-login',
  templateUrl: './hero-login.component.html',
  styleUrls: ['./hero-login.component.scss'] 
})

export class HeroLoginComponent implements OnInit {
  authType: 'login' | 'signup' = 'login';

  constructor() {}

  ngOnInit(): void {}

  onLogin() {
    // Handle login logic here
  }

  onSignup() {
    // Handle signup logic here
  }

  setAuthType(type: 'login' | 'signup') {
    this.authType = type;
  }
  
}
