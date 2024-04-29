import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { TEAM_URL } from "app/constants";
import { Team } from "app/models/Calendar-Tim/team";
import { Guid } from "guid-typescript";
import { Observable } from "rxjs";

@Injectable({
  providedIn: "root",
})
export class TeamServicesService {
  constructor(private httpClient: HttpClient) {}

  //dodato zbog deleta posto vraca string i onda da bi on znao da je string
  public headers = new HttpHeaders({
    "Content-Type": "application/json",
    Accept: "application/json",
    responseType: "text", // to da je string mu ovde govorim
  });

  //Zasto imamo promise?

  public getAllTeams(): Observable<any[]> {
    return this.httpClient.get<Team[]>(`${TEAM_URL}`);
  }
  public getTeamById(teamId: Guid): Observable<any> {
    return this.httpClient.get<Team>(`${TEAM_URL}/${teamId}`);
  }

  public addTeam(team: Team): Observable<any> {
    return this.httpClient.post<Team>(`${TEAM_URL}`, team);
  }

  public updateTeam(team: Team): Observable<any> {
    return this.httpClient.put<Team>(`${TEAM_URL}`, team);
  }

  public deleteTeam(teamId: Guid): Observable<any> {
    return this.httpClient.delete<void>(`${TEAM_URL}/${teamId}`, {
      headers: this.headers,
    });
  }
}
