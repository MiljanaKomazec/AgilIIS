import { Injectable } from '@angular/core';
import {HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';
import { UserLoginDto } from 'app/models/userLoginDto';
import { AUTH_URL } from 'app/constants';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  

  constructor(private http: HttpClient) { }

  login(credentials: UserLoginDto): Observable<any> {
    return this.http.post<any>(`${AUTH_URL}`, credentials);
  }
}