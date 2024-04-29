import { Guid } from 'guid-typescript';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SPRINT_URL } from 'app/constants';
import { SprintS } from 'app/models/sprint';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SprintServiceService {

  constructor(private httpClient: HttpClient) { }

  public getAllSprint(): Observable<any> {
    return this.httpClient.get(`${SPRINT_URL}`);
  }

  public addSprint(sprint:SprintS):Observable<any>{
    return this.httpClient.post(`${SPRINT_URL}` ,sprint);
  }

  public updateSprint(sprint:SprintS):Observable<any>{
    return this.httpClient.put(`${SPRINT_URL}` ,sprint);
  }

  public deleteSprint(sprintId:Guid):Observable<any> {
    return this.httpClient.delete(`${SPRINT_URL}/${sprintId}`);
  }
}
