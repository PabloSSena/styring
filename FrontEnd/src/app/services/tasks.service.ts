import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../enviroments/enviroment';
import { taskResponseModel } from '../types/get-tasks-response.type';
import { postTaskModel } from '../types/post-task-model.type';
import { updateTaskModel } from '../types/update-task-model.type';

@Injectable({
  providedIn: 'root',
})
export class TasksService {
  private apiUrl = environment.API_URL;

  constructor(private httpClient: HttpClient) {}
  getUserTasks(status?: string, dueDate?: string, searchValue?: string) {
    const params: any = {};

    if (status && status !== 'all') params.Status = status;
    if (dueDate) params.dueDate = dueDate;
    if (searchValue?.trim()) params.search = searchValue;

    return this.httpClient.get<taskResponseModel[]>(
      `${this.apiUrl}/tasks/${sessionStorage.getItem('user-id')}`,
      {
        params,
      }
    );
  }

  createTask(task: postTaskModel) {
    return this.httpClient.post<postTaskModel>(`${this.apiUrl}/tasks`, {
      ...task,
      userId: Number(sessionStorage.getItem('user-id')),
    });
  }

  updateTask(task: updateTaskModel) {
    return this.httpClient.put<postTaskModel>(
      `${this.apiUrl}/tasks/${task.id}`,
      { ...task }
    );
  }

  deleteTask(taskId: number) {
    return this.httpClient.delete(`${this.apiUrl}/tasks/${taskId}`);
  }
}
