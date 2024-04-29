import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Guid } from 'guid-typescript';
import { PRIORITETIZATIONPARAMETER_URL } from 'app/constants';
import { PrioritetizationParameter } from 'app/models/UserStoryModel/prioritetizationParameter';

@Injectable({
  providedIn: 'root'
})
export class PrioritetizationParameterService {

  constructor(private httpClient: HttpClient) { }

  public getAllPrioritetizationParameter(): Observable<any> {
    return this.httpClient.get(`${PRIORITETIZATIONPARAMETER_URL}`);
  }

  public addPrioritetizationParameter(pp: PrioritetizationParameter): Observable<any> {
    return this.httpClient.post(`${PRIORITETIZATIONPARAMETER_URL}`, pp);
  }

  public deletePrioritetizationParameter(id: Guid): Observable<any> {
    return this.httpClient.delete(`${PRIORITETIZATIONPARAMETER_URL}`  + "/" + id);
  }

  public updatePrioritetizationParameter(pp: PrioritetizationParameter) : Observable<any>{
    return this.httpClient.put(`${PRIORITETIZATIONPARAMETER_URL}` , pp);  
  }
  
}
