import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { AppSettings } from '../app-settings';
import { LoginDto } from '../models/user/login-dto';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  loginUrl = 'identity-service/auth/login'
  constructor(private http: HttpClient) { }

  login(loginDto: LoginDto) {
    return this.http.post(AppSettings.DEV_API_ENDPOINT + this.loginUrl,
        loginDto
      )
  }
}
