import { Guid } from 'guid-typescript';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { COMMENT_URL, COMMENT_URL_BY_USERSTORY } from 'app/constants';
import { CommentC } from 'app/models/comment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CommentServiceService {

  constructor(private httpClient: HttpClient) { }

  public getAllComment(): Observable<any> {
    return this.httpClient.get(`${COMMENT_URL}`);
  }

  public addComment(comment:CommentC):Observable<any>{
    return this.httpClient.post(`${COMMENT_URL}` ,comment);
  }

  public updateComment(comment:CommentC):Observable<any>{
    return this.httpClient.put(`${COMMENT_URL}` ,comment);
  }

  public deleteComment(commentId:Guid):Observable<any> {
    return this.httpClient.delete(`${COMMENT_URL}/${commentId}`);
  }

  public getCommentByUserStory(userStoryRootId: Guid): Observable<CommentC[]> {
    return this.httpClient.get<CommentC[]>(`${COMMENT_URL_BY_USERSTORY}/${userStoryRootId}`);
  }
}
