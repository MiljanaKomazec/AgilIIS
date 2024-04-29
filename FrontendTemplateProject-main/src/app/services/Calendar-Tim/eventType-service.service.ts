import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { EVENTTYPE_URL } from "app/constants";
import { EventType } from "app/models/Calendar-Tim/eventType";
import { Guid } from "guid-typescript";
import { Observable } from "rxjs";

@Injectable({
  providedIn: "root",
})
export class EventTypeService {
  constructor(private httpClient: HttpClient) {}

  //dodato zbog deleta posto vraca string i onda da bi on znao da je string
  public headers = new HttpHeaders({
    "Content-Type": "application/json",
    Accept: "application/json",
    responseType: "text", // to da je string mu ovde govorim
  });

  //Zasto imamo promise?

  public getAllEventTypes(): Observable<any[]> {
    return this.httpClient.get<EventType[]>(`${EVENTTYPE_URL}`);
  }
  public getEventTypeById(eventTypeId: Guid): Observable<any> {
    return this.httpClient.get<EventType>(`${EVENTTYPE_URL}/${eventTypeId}`);
  }

  public addEventType(eventType: EventType): Observable<any> {
    return this.httpClient.post<EventType>(`${EVENTTYPE_URL}`, eventType);
  }

  public updateEventType(eventType: EventType): Observable<any> {
    return this.httpClient.put<EventType>(`${EVENTTYPE_URL}`, eventType);
  }

  public deleteEventType(eventTypeId: Guid): Observable<any> {
    return this.httpClient.delete<void>(`${EVENTTYPE_URL}/${eventTypeId}`, {
      headers: this.headers,
    });
  }
}
