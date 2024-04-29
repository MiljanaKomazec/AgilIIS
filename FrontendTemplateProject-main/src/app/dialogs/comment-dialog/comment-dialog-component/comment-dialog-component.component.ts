import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CommentC } from 'app/models/comment';
import { CommentServiceService } from 'app/services/comment-service.service';

@Component({
  selector: 'comment-dialog-component',
  templateUrl: './comment-dialog-component.component.html',
  styleUrls: ['./comment-dialog-component.component.scss']
})
export class CommentDialogComponentComponent {

  comments!: CommentC[];

  constructor(public snackBar: MatSnackBar,
    public dialogRef: MatDialogRef<CommentC>,
    @Inject(MAT_DIALOG_DATA) public data: CommentC,
    public commentService: CommentServiceService
  ) {}


  ngOnInit(): void {
    this.getComment();
  }

  public getComment():void{
    this.commentService.getCommentByUserStory(this.data.userStoryRootId).subscribe(res =>{
      console.log('Fetched comments:', res);
      this.comments = res;
        this.snackBar.open('Comments wirh user story id: ' + this.data.userStoryRootId + ' succesfully restored',
          'ok', {duration:3500})

      }
    ),
    (error:Error) => {
      console.log(error.name + ' ' + error.message);
      this.snackBar.open('Error while restoring comments.',
      'ok', {duration:3500});
    }
  }

}
