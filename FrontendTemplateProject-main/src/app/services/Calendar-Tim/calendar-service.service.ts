import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { CALENDAR_URL } from "app/constants";
import { Calendar } from "app/models/Calendar-Tim/calendar";
import { Guid } from "guid-typescript";
import { Observable } from "rxjs";

@Injectable({
  providedIn: "root",
})
export class CalendarService {
  constructor(private httpClient: HttpClient) {}

  //dodato zbog deleta posto vraca string i onda da bi on znao da je string
  public headers = new HttpHeaders({
    "Content-Type": "application/json",
    Accept: "application/json",
    responseType: "text", // to da je string mu ovde govorim
  });

  //Zasto imamo promise?

  public getAllCalendars(): Observable<any[]> {
    return this.httpClient.get<Calendar[]>(`${CALENDAR_URL}`);
  }
  public getCalendarById(calendarId: Guid): Observable<any> {
    return this.httpClient.get<Calendar>(`${CALENDAR_URL}/${calendarId}`);
  }

  public addCalendar(calendar: Calendar): Observable<any> {
    return this.httpClient.post<Calendar>(`${CALENDAR_URL}`, calendar);
  }

  public updateCalendar(calendar: Calendar): Observable<any> {
    return this.httpClient.put<Calendar>(`${CALENDAR_URL}`, calendar);
  }

  public deleteCalendar(calendarId: Guid): Observable<any> {
    return this.httpClient.delete<void>(`${CALENDAR_URL}/${calendarId}`, {
      headers: this.headers,
    });
  }
}
