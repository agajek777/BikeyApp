import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { AppSettings } from '../app-settings';
import { LoginDto } from '../models/user/login-dto';
import { SuccLoginDto } from '../models/user/succ-login-dto';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  loginUrl = 'identity-service/auth/login'
  constructor(private http: HttpClient) { }

  login(loginDto: LoginDto) {
    return this.http.post(AppSettings.DEV_API_ENDPOINT + this.loginUrl,
        loginDto
      );
  }

  isLogged() {
    return sessionStorage.getItem("id") != null;
  }

  getUsername() {
    return sessionStorage.getItem("username");
  }

  setVariables(loginDto: SuccLoginDto) {
    sessionStorage.setItem("access_token", loginDto.accessToken);
    sessionStorage.setItem("username", loginDto.userName);
    sessionStorage.setItem("id", loginDto.id);
  }
}
