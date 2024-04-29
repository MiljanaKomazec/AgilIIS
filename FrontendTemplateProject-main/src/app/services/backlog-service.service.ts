import { Guid } from 'guid-typescript';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BACKLOG_URL } from 'app/constants';
import { BacklogB } from 'app/models/backlog';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BacklogServiceService {

  constructor(private httpClient: HttpClient) { }

  public getAllBacklog(): Observable<any> {
    return this.httpClient.get(`${BACKLOG_URL}`);
  }

  public addBacklog(backlog:BacklogB):Observable<any>{
    return this.httpClient.post(`${BACKLOG_URL}` ,backlog);
  }

  public updateBacklog(backlog:BacklogB):Observable<any>{
    return this.httpClient.put(`${BACKLOG_URL}` ,backlog);
  }

  public deleteBacklog(backlogId:Guid):Observable<any> {
    return this.httpClient.delete(`${BACKLOG_URL}/${backlogId}`);
  }

}
