import { CommonModule } from '@angular/common';
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
  providers: [LoginService, ReactiveFormsModule],
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent {
  registerForm!: FormGroup;

  constructor(
    private router: Router,
    private loginService: LoginService,
    private toastService: ToastrService
  ) {
    this.registerForm = new FormGroup({
      name: new FormControl(null, [
        Validators.required,
        Validators.minLength(2),
      ]),
      password: new FormControl(null, [Validators.required]),
    });
  }

  onSubmit() {
    if (this.registerForm.valid) {
      this.loginService
        .create(this.registerForm.value.name, this.registerForm.value.password)
        .subscribe({
          next: () => {
            this.toastService.success('Usuário Criado com sucesso');
            this.router.navigate(['/login']);
          },
          error: () => this.toastService.error('Erro ao criar usuário'),
        });
    }
  }

  navigate() {
    this.router.navigate(['/login']);
  }
}
