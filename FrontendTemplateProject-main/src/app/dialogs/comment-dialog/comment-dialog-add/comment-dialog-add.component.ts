import { Component, Inject, Pipe, PipeTransform } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CommentC } from 'app/models/comment';
import { CommentServiceService } from 'app/services/comment-service.service';

@Component({
  selector: 'comment-dialog-add',
  templateUrl: './comment-dialog-add.component.html',
  styleUrls: ['./comment-dialog-add.component.scss']

})
export class CommentDialogAddComponent {

  comments!: CommentC[];

  constructor(public snackBar: MatSnackBar,
    public dialogRef: MatDialogRef<CommentC>,
    @Inject(MAT_DIALOG_DATA) public data: CommentC,
    public commentService: CommentServiceService,

  ) {}

  public add():void{
    this.commentService.addComment(this.data).subscribe(
      () => {
        this.snackBar.open('Comment with text: ' + this.data.textComment + ' succesfully created',
          'ok', {duration:3500})

      }
    ),
    (error:Error) => {
      console.log(error.name + ' ' + error.message);
      this.snackBar.open('Error while creating comment.',
      'ok', {duration:3500});
    }
  }

  public cancel():void{
    this.dialogRef.close();
    this.snackBar.open('You have given up on editing',
      'ok', {duration:3500});
  }

  

}
