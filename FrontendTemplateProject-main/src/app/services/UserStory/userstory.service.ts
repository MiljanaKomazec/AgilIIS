import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Guid } from 'guid-typescript';
import { USERSTORYROOT_URL, USERSTORY_BY_BACKLOGID, USERSTORY_BY_SPRINTID } from 'app/constants';
import { UserStoryRoot } from 'app/models/UserStoryModel/userStory';
import { UserStoryRootUpdateDto } from 'app/models/UserStoryModel/userStoryRootUpdateDto';
import { UserStoryRootDto } from 'app/models/UserStoryModel/userStoryDto';
import { UserStoryCreatinDto } from 'app/models/UserStoryModel/userStoryCreationDto';

@Injectable({
  providedIn: 'root'
})
export class UserStoryService {

  constructor(private httpClient: HttpClient) { }

  public getAllUserStory(): Observable<any> {
    return this.httpClient.get(`${USERSTORYROOT_URL}`);
  }

  public async addUserStory(userStory: UserStoryCreatinDto): Promise<Observable<any>> {
    return await this.httpClient.post(`${USERSTORYROOT_URL}`, userStory);
  }

  public deleteUserStory(id: Guid): Observable<any> {
    return this.httpClient.delete(`${USERSTORYROOT_URL}`  + "/" + id);
  }

  public async updateUserStory(userStory: UserStoryRootUpdateDto) : Promise<Observable<any>>{
    return await this.httpClient.put(`${USERSTORYROOT_URL}` , userStory);  
  }
  
  public getUserStoriesByBacklogId(backlogId: Guid): Observable<UserStoryRoot[]> {
    return this.httpClient.get<UserStoryRoot[]>(`${USERSTORY_BY_BACKLOGID}/${backlogId}`);
  }

  public getUserStoriesBySprintId(sprintId: Guid): Observable<UserStoryRoot[]> {
    return this.httpClient.get<UserStoryRoot[]>(`${USERSTORY_BY_SPRINTID}/${sprintId}`);
  }

}
