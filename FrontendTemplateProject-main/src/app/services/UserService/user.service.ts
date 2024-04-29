import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { USER_URL } from 'app/constants';
import { User } from 'app/models/user';
import { Guid } from 'guid-typescript';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class UserService {

  
  constructor(private http: HttpClient) { }

  public getAllUsers(): Observable<any> {
    return this.http.get(`${USER_URL}`);
  }
  public getUserById(userId: Guid): Observable<any> {
    return this.http.get<User>(`${USER_URL}/${userId}`);
  }

  public addUser(user: User): Observable<any> {
    return this.http.post<User>(`${USER_URL}`, user);
  }

  public updateUser(user: User): Observable<any> {
    return this.http.patch<User>(`${USER_URL}`, user);
  }

  public deleteUser(userId: Guid): Observable<any> {
    return this.http.delete<void>(`${USER_URL}/${userId}`);
  }
}
