import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AuthGuard } from './auth/auth.guard';

@Component({
  selector: 'app-root',
  standalone: true,
  providers: [AuthGuard],
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'frontEnd';
}
