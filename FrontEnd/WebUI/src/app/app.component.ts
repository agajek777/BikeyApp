import { Component } from '@angular/core';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'BikeyApp';

  constructor(private auth: AuthService)
  { }

  getUsername() {
    return this.auth.getUsername();
  }

  isLogged() {
    return this.auth.isLogged();
  }
}
