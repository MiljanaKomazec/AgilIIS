
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { EVENT_URL } from "app/constants";
import { Event } from "app/models/Calendar-Tim/event";
import { eventDTO } from "app/models/Calendar-Tim/eventDTO";
import { eventUpdateDTO } from "app/models/Calendar-Tim/eventUpdateDTO";
import { Guid } from "guid-typescript";
import { Observable } from "rxjs";

@Injectable({
  providedIn: "root",
})
export class EventService {
  constructor(private httpClient: HttpClient) {}

  //dodato zbog deleta posto vraca string i onda da bi on znao da je string
  public headers = new HttpHeaders({
    "Content-Type": "application/json",
    Accept: "application/json",
    responseType: "text", // to da je string mu ovde govorim
  });

  //Zasto imamo promise?

  public getAllEvents(): Observable<any[]> {
    return this.httpClient.get<Event[]>(`${EVENT_URL}`);
  }
  public getEventById(eventId: Guid): Observable<any> {
    return this.httpClient.get<Event>(`${EVENT_URL}/${eventId}`);
  }

  public addEvent(event: eventDTO): Observable<any> {
    return this.httpClient.post<eventDTO>(`${EVENT_URL}`, event);
  }

  public updateEvent(event: eventUpdateDTO): Observable<any> {
    return this.httpClient.put<Event>(`${EVENT_URL}`, event);
  }

  public deleteEvent(eventId: Guid): Observable<any> {
    return this.httpClient.delete<void>(`${EVENT_URL}/${eventId}`, {
      headers: this.headers,
    });
  }
}
