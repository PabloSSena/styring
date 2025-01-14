import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { TasksService } from '../../services/tasks.service';
import { taskResponseModel } from '../../types/get-tasks-response.type';
import { TaskModalComponent } from '../task-modal/task-modal.component';

@Component({
  selector: 'app-home',
  standalone: true,
  providers: [TasksService],
  imports: [CommonModule, TaskModalComponent, ReactiveFormsModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent {
  tasks: taskResponseModel[] = [];
  selectedTask!: taskResponseModel | null;
  filteredTasks: taskResponseModel[] = [];

  constructor(
    private tasksService: TasksService,
    private toastService: ToastrService
  ) {}

  ngOnInit() {
    this.loadTasks();
  }

  filterForm: FormGroup = new FormGroup({
    status: new FormControl(''),
    dueDate: new FormControl(''),
    searchValue: new FormControl(''),
  });

  loadTasks() {
    this.tasksService
      .getUserTasks(
        this.filterForm.value.status,
        this.filterForm.value.dueDate,
        this.filterForm.value.searchValue
      )
      .subscribe({
        next: (data) => {
          data.map((task) => {
            task.dueDate = new Date(task.dueDate).toLocaleDateString('pt-br', {
              timeZone: 'UTC',
            });
          });
          this.tasks = data;
        },
        error: (error) => {
          this.toastService.error('Erro ao listar tarefas', error);
        },
      });
  }

  getStatusColor(status: string): string {
    switch (status) {
      case 'Concluído':
        return '#4CAF50';
      case 'Em andamento':
        return '#FF9800';
      case 'Pendente':
        return '#F44336';
      default:
        return '#9E9E9E';
    }
  }

  openTaskModal(task: taskResponseModel) {
    this.selectedTask = task;
  }

  onSearchSubmit() {
    this.loadTasks();
  }

  onModalClose() {
    this.selectedTask = null;
  }
}
