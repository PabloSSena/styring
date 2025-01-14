import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  ValidationErrors,
  Validators,
} from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { TasksService } from '../../services/tasks.service';
import { taskResponseModel } from '../../types/get-tasks-response.type';
import { updateTaskModel } from '../../types/update-task-model.type';

@Component({
  selector: 'app-task-modal',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './task-modal.component.html',
  styleUrl: './task-modal.component.css',
})
export class TaskModalComponent {
  @Input() task: taskResponseModel | null = null;

  constructor(
    private tasksService: TasksService,
    private toastService: ToastrService
  ) {}

  taskForm: FormGroup = new FormGroup({
    title: new FormControl('', [
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(100),
    ]),
    description: new FormControl(''),
    dueDate: new FormControl('', [Validators.required, this.dateNotPast]),
    status: new FormControl('', [Validators.required]),
  });

  ngOnChanges() {
    if (this.task) {
      const formattedTask = {
        ...this.task,
        dueDate: this.formatDate(this.task.dueDate),
      };
      this.taskForm.patchValue(formattedTask);
    }
  }

  dateNotPast(control: FormControl): ValidationErrors | null {
    if (!control.value) return null;

    const selectedDate = new Date(control.value);
    const today = new Date();
    today.setHours(0, 0, 0, 0);

    return selectedDate < today ? { dateInvalid: true } : null;
  }

  formatDate(date: string): string {
    if (!date) return '';

    const [day, month, year] = date.split('/');
    return `${year}-${month}-${day}`;
  }

  resetForm() {
    this.taskForm.reset();
  }

  deleteTask() {
    this.tasksService.deleteTask(this.task!.id).subscribe({
      next: () => {
        this.toastService.success('Tarefa deletada com sucesso');
        window.location.reload();
      },
      error: () => this.toastService.success('Erro ao deletar'),
    });
  }

  onSubmit() {
    if (this.taskForm.valid) {
      if (!this.task) {
        this.tasksService.createTask(this.taskForm.value).subscribe({
          next: () => {
            this.toastService.success('Tarefa salva com sucesso');
            window.location.reload();
          },
          error: () => this.toastService.success('Erro ao salvar'),
        });
      } else {
        const taskToUpdate: updateTaskModel = {
          ...this.taskForm.value,
          id: this.task.id,
        };
        this.tasksService.updateTask(taskToUpdate).subscribe({
          next: () => {
            window.location.reload();
            this.toastService.success('Tarefa alterada com sucesso');
          },
          error: () => console.log('erro'),
        });
      }
    }
  }
}
