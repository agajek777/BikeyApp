import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { MessageDialogComponent } from 'src/app/dialogs/message-dialog/message-dialog.component';
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
  constructor(private authService: AuthService, private router: Router, private dialog: MatDialog) { }

  submit(loginDto: LoginDto) {
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
        console.log('1234');
        
        var outcome = error as HttpErrorResponse;
        const dialogRef = this.dialog.open(MessageDialogComponent, 
          {
            data: { title: 'Error!', message: outcome.error }
          });
        console.log(dialogRef);
        
      }
    );
  }

  ngOnInit(): void {
  }

}
