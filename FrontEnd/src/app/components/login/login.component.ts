import { Component } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { LoginService } from '../../services/login.service';

@Component({
  selector: 'app-login',
  standalone: true,
  providers: [LoginService],
  imports: [ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  loginForm!: FormGroup;

  constructor(
    private router: Router,
    private loginService: LoginService,
    private toastService: ToastrService
  ) {
    this.loginForm = new FormGroup({
      name: new FormControl(null, [
        Validators.required,
        Validators.minLength(2),
      ]),
      password: new FormControl(null, [Validators.required]),
    });
  }

  onSubmit() {
    this.loginService
      .login(this.loginForm.value.name, this.loginForm.value.password)
      .subscribe({
        next: () => {
          this.toastService.success('Login efetuado com sucesso');
          this.router.navigate(['/home']);
        },
        error: () => this.toastService.error('Erro ao logar'),
      });
  }

  navigate() {
    this.router.navigate(['/register']);
  }
}
