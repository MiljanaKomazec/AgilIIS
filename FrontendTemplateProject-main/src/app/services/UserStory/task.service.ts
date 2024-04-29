import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Guid } from 'guid-typescript';
import { TASKBYFUNCTIONALITY, TASK_BY_SPRINTID, TASK_URL } from 'app/constants';
import { Task } from 'app/models/UserStoryModel/task';
import { TaskUpdateDto } from 'app/models/UserStoryModel/taskUpdateDto';
import { TaskDto } from 'app/models/UserStoryModel/taskDto';
import { TaskCreationDto } from 'app/models/UserStoryModel/taskCreationDto';

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  constructor(private httpClient: HttpClient) { }

  public getAllTask(): Observable<any> {
    return this.httpClient.get(`${TASK_URL}`);
  }

  public async addTask(task: TaskCreationDto): Promise<Observable<any>> {
    return await this.httpClient.post(`${TASK_URL}`, task);
  }

  public deleteTask(id: Guid): Observable<any> {
    return this.httpClient.delete(`${TASK_URL}`  + "/" + id);
  }

  public async updateTask(task: TaskUpdateDto) : Promise<Observable<any>>{
    return await this.httpClient.put(`${TASK_URL}` ,task);  
  }
  
  public GetTaskByFunctionality(functionalityId: Guid): Observable<Task[]> {
    return this.httpClient.get<Task[]>(`${TASKBYFUNCTIONALITY}`+'/' + functionalityId);
  }

  public getTasksBySprintId(sprintId: Guid): Observable<Task[]> {
    return this.httpClient.get<Task[]>(`${TASK_BY_SPRINTID}/${sprintId}`);
  }
}