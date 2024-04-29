import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Guid } from 'guid-typescript';
import { FUNCTIONALITYBYUSERSTORY_URL, FUNCTIONALITY_BY_SPRINTID, FUNCTIONALITY_URL } from 'app/constants';
import { Functionality } from 'app/models/UserStoryModel/functionality';
import { FunctionalityUpdateDto } from 'app/models/UserStoryModel/functionalityUpdateDto';
import { FunctionalityDto } from 'app/models/UserStoryModel/functionalityDto';
import { FunctionalityCreationDto } from 'app/models/UserStoryModel/functionalityCreationDto';

@Injectable({
  providedIn: 'root'
})
export class FunctionalityService {

  constructor(private httpClient: HttpClient) { }

  public getAllFunctionality(): Observable<any> {
    return this.httpClient.get(`${FUNCTIONALITY_URL}`);
  }

  public async addFunctionality(functionality: FunctionalityCreationDto): Promise<Observable<any>> {
    return await this.httpClient.post(`${FUNCTIONALITY_URL}`, functionality);
  }

  public deleteFunctionality(id: Guid): Observable<any> {
    return this.httpClient.delete(`${FUNCTIONALITY_URL}`  + "/" + id);
  }

  public async updateFunctionality(functionality: FunctionalityUpdateDto) : Promise<Observable<any>>{
    return await this.httpClient.put(`${FUNCTIONALITY_URL}` , functionality);  
  }
  
  public getFunctionalityByUserStory(userStoryId: Guid): Observable<Functionality[]> {
    return this.httpClient.get<Functionality[]>(`${FUNCTIONALITYBYUSERSTORY_URL}` +'/' + userStoryId);
  }

  public getFunctionalitiesBySprintId(sprintId: Guid): Observable<Functionality[]> {
    return this.httpClient.get<Functionality[]>(`${FUNCTIONALITY_BY_SPRINTID}/${sprintId}`);
  }

}