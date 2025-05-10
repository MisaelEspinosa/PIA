import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  imports: [RouterModule, FormsModule] // Importa los módulos necesarios
})
export class LoginComponent {
  username: string = '';
  password: string = '';

  constructor(private router: Router) {}

  login() {
    if (this.username === 'cliente' && this.password === '123') {
      this.router.navigate(['/cliente']);
    } else if (this.username === 'admin' && this.password === 'admin') {
      this.router.navigate(['/admin']);
    } else {
      alert('Usuario o contraseña incorrectos');
    }
  }
}
