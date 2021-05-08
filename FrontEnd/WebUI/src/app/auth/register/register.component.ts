import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginDto } from 'src/app/models/user/login-dto';
import { SuccLoginDto } from 'src/app/models/user/succ-login-dto';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  title: string = "New account";
  
  constructor(private authService: AuthService, private router: Router) { }
  
  ngOnInit(): void {
    
  }

  submit(loginDto: LoginDto) {
    console.log(loginDto);

    this.authService.register(loginDto).subscribe(
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

}
