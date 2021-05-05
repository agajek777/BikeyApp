import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { LoginDto } from 'src/app/models/user/login-dto';
import { SuccLoginDto } from 'src/app/models/user/succ-login-dto';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup = new FormGroup({
    username: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required)
  });

  constructor(private authService: AuthService) { }

  submit() {
    var loginDto = new LoginDto(this.loginForm.get('username').value, this.loginForm.get('password').value);

    console.log(loginDto);
    

    this.authService.login(loginDto).subscribe(
      succ => 
      {
        var outcome = succ as SuccLoginDto;
        console.log(outcome);

        this.authService.setVariables(outcome);
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
