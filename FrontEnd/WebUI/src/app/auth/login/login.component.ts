import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginDto } from 'src/app/models/user/login-dto';
import { SuccLoginDto } from 'src/app/models/user/succ-login-dto';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  title: string = "Sign In";
  constructor(private authService: AuthService, private router: Router) { }

  submit(loginDto: LoginDto) {
    console.log(loginDto);

    this.authService.login(loginDto).subscribe(
      succ => 
      {
        var outcome = succ as SuccLoginDto;
        console.log(outcome);

        this.authService.setVariables(outcome);

        this.router.navigate(['home']);
      },
      error =>
      {
        console.log(error);
      }
    );
  }

  ngOnInit(): void {
  }

}
