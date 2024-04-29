import { Guid } from 'guid-typescript';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BACKLOGITEM_URL } from 'app/constants';
import { BacklogItemBI } from 'app/models/backlogItem';
import { Observable } from 'rxjs';
import { BacklogItemDTO } from 'app/models/backlogItemDTO';
import { BacklogServiceService } from './backlog-service.service';
import { BacklogItemUpdateDTO } from 'app/models/backlogItemUpdateDTO';

@Injectable({
  providedIn: 'root'
})
export class BacklogItemServicesService {

  constructor(private httpClient: HttpClient) { }

  public getAllBacklogItem(): Observable<any> {
    return this.httpClient.get(`${BACKLOGITEM_URL}`);
  }

  public async addBacklogItem(backlogItem:BacklogItemDTO):Promise<Observable<any>>{
    return await this.httpClient.post(`${BACKLOGITEM_URL}` ,backlogItem);
  }

  public async updateBacklogItem(backlogItem:BacklogItemUpdateDTO):Promise<Observable<any>>{
    return await this.httpClient.put(`${BACKLOGITEM_URL}` ,backlogItem);
  }

  public async deleteBacklogItem(backlogItemId:Guid):Promise<Observable<any>> {
    return await this.httpClient.delete(`${BACKLOGITEM_URL}/${backlogItemId}`);
  }
}