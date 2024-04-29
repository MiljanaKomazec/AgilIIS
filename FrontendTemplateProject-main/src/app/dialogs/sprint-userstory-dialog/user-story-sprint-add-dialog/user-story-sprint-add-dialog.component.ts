import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UserStoryRoot } from 'app/models/UserStoryModel/userStory';
import { UserStoryRootUpdateDto } from 'app/models/UserStoryModel/userStoryRootUpdateDto';
import { SprintS } from 'app/models/sprint';
import { UserStoryService } from 'app/services/UserStory/userstory.service';
import { SprintServiceService } from 'app/services/sprint-service.service';

@Component({
  selector: 'user-story-sprint-add-dialog',
  templateUrl: './user-story-sprint-add-dialog.component.html',
  styleUrls: ['./user-story-sprint-add-dialog.component.scss']
})
export class UserStorySprintAddDialogComponent {
  userStories!: UserStoryRoot;
  sprints!: SprintS[];

  constructor(public snackBar: MatSnackBar,
    public dialogRef: MatDialogRef<UserStoryRoot>,
    @Inject(MAT_DIALOG_DATA) public data: UserStoryRoot,
    public userStoryService: UserStoryService,
    public sprintService:SprintServiceService

  ) {}

  ngOnInit(): void {
    this.sprintService.getAllSprint().subscribe(
      data => {
        this.sprints = data;
      }
    )
  }

  public compare(a:any,b:any){
    return a.id = b.id;
  }


  public async update(): Promise<void> {
    (await this.userStoryService.updateUserStory(new UserStoryRootUpdateDto(this.data.userStoryRootId, this.data.textUserStory, this.data.partOfEpic, 
      this.data.prioritetizationParameter.prioritetizationParameterId, this.data.prioritetizationParameter, this.data.backlogId, this.data.sprintId))).subscribe(() => {
      this.snackBar.open('User story with this text: ' + this.data.textUserStory + ' succesfully added to sprint', 'OK', {
        duration: 2500
      })
    }),
      (error: Error) => {
        console.log(error.name + ' ' + error.message)
        this.snackBar.open('Error while adding user story to sprint.', 'Zatvori', {
          duration: 2500
        })
      };

  }

  public cancel():void{
    this.dialogRef.close();
    this.snackBar.open('You have given up on editing',
      'ok', {duration:3500});
  }


}
