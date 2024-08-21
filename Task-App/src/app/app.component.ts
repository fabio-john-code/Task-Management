import { HttpClient, HttpClientModule, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Observable } from 'rxjs/internal/Observable';
import { TaskItem } from '../models/TaskItem.model';
import { AsyncPipe } from '@angular/common';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HttpClientModule, AsyncPipe, FormsModule, ReactiveFormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Task Management';
  http = inject(HttpClient);

  taskForm = new FormGroup(
    {
      description: new FormControl<string>('')
    });

  taskList$ = this.GetTaskList();

  onFormSubmit()
  {
    console.log(this.taskForm.value.description);
    
    const createTaskRequest = {
      description: this.taskForm.value.description
    };

    this.http.post('https://localhost:51193/TaskManagement', createTaskRequest)
    .subscribe(
    {
      next: (value) => {
        console.log(value);
        this.taskList$ = this.GetTaskList();
        this.taskForm.reset();
      }
    });
  }

  private GetTaskList(): Observable<TaskItem[]>
  { 
    return this.http.get<TaskItem[]>('https://localhost:51193/TaskManagement');
  }

  updateTaskList(id: string, status: number)
  {
    const updateTaskRequest = {
      id: id,
      status: 3
    };
    console.log(updateTaskRequest);

    this.http.patch('https://localhost:51193/TaskManagement', updateTaskRequest)    
    .subscribe(
      {
        next: (value) => {
          console.log(value);
          this.taskList$ = this.GetTaskList();
        }
      });
  
  }
}
