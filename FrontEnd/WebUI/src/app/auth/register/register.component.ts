import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { MessageDialogComponent } from 'src/app/dialogs/message-dialog/message-dialog.component';
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
  
  constructor(private authService: AuthService, private router: Router, private dialog: MatDialog) { }
  
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
        
        var outcome = error as HttpErrorResponse;
        const dialogRef = this.dialog.open(MessageDialogComponent, 
          {
            data: { title: 'Error!', message: outcome.error }
          });
        console.log(dialogRef);
      }
    );
  }

}
