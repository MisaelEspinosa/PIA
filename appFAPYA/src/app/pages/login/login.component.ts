import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  imports: [RouterModule, FormsModule]
})
export class LoginComponent {
  username: string = '';
  password: string = '';

  constructor(private router: Router) {}

  login() {
    if (this.username === 'cliente' && this.password === '123') {
      console.log('Redirigiendo a home-cliente');
      this.router.navigate(['/home-cliente']);
    } else if (this.username === 'admin' && this.password === 'admin') {
      console.log('Redirigiendo a admin');
      this.router.navigate(['/admin']);
    } else {
      alert('Usuario o contraseña incorrectos');
      console.log('Intento de login fallido');
    }
  }
}