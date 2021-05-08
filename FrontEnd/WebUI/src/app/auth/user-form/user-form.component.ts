import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { LoginDto } from 'src/app/models/user/login-dto';
import { SuccLoginDto } from 'src/app/models/user/succ-login-dto';

@Component({
  selector: 'app-user-form',
  templateUrl: './user-form.component.html',
  styleUrls: ['./user-form.component.css']
})
export class UserFormComponent implements OnInit {
  userForm: FormGroup = new FormGroup({
    username: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required)
  });

  @Output()
  userData: EventEmitter<LoginDto> = new EventEmitter();

  @Input()
  title: string;
  
  constructor() { }

  ngOnInit(): void {
  }

  submit() {
    var userDto = new LoginDto(this.userForm.get('username').value, this.userForm.get('password').value);

    this.userData.emit(userDto);
  }

}
