import { Guid } from 'guid-typescript';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { POBI_URL } from 'app/constants';
import { PhaseOfBacklogItem } from 'app/models/phaseOfBacklogItem';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PhaseOfBacklogItemServiceService {

  constructor(private httpClient: HttpClient) { }

  public getAllPhaseOfBacklogItem(): Observable<any> {
    return this.httpClient.get(`${POBI_URL}`);
  }

  public addPhaseOfBacklogItem(pobi:PhaseOfBacklogItem):Observable<any>{
    return this.httpClient.post(`${POBI_URL}` ,pobi);
  }

  public updatePhaseOfBacklogItem(pobi:PhaseOfBacklogItem):Observable<any>{
    return this.httpClient.put(`${POBI_URL}` ,pobi);
  }

  public deletePhaseOfBacklogItem(pobiId:Guid):Observable<any> {
    return this.httpClient.delete(`${POBI_URL}/${pobiId}`);
  }
}
