import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { PrioritetizationParameter } from 'app/models/UserStoryModel/prioritetizationParameter';
import { UserStoryRoot } from 'app/models/UserStoryModel/userStory';
import { UserStoryCreatinDto } from 'app/models/UserStoryModel/userStoryCreationDto';
import { UserStoryRootDto } from 'app/models/UserStoryModel/userStoryDto';
import { UserStoryRootUpdateDto } from 'app/models/UserStoryModel/userStoryRootUpdateDto';
import { BacklogB } from 'app/models/backlog';
import { PrioritetizationParameterService } from 'app/services/UserStory/prioritetizationParameter.service';
import { UserStoryService } from 'app/services/UserStory/userstory.service';
import { Guid } from 'guid-typescript';

@Component({
  selector: 'app-user-story-dialog',
  templateUrl: './user-story-dialog.component.html',
  styleUrls: ['./user-story-dialog.component.css']
})
export class UserStoryDialogComponent implements OnInit {

  flag!:number;
  prioritizationParameters: PrioritetizationParameter[];

  constructor(public snackBar: MatSnackBar,
    public dialogRef: MatDialogRef<UserStoryRoot>,
    @Inject(MAT_DIALOG_DATA) public data: UserStoryRoot,
    public userStoryService: UserStoryService,
    public ppService: PrioritetizationParameterService
    ) {}

    ngOnInit(): void {
      this.ppService.getAllPrioritetizationParameter().subscribe(
        data => {
          this.prioritizationParameters = data;
        }
      )

       // Pristup backlogId parametru iz podataka i sačuvajte ga u komponenti
      const { backlogId } = this.data;
      console.log('BacklogId in dialog component:', backlogId);
    }

    public compare(a:any,b:any){
      return a.id = b.id;
    }

    public async add():Promise<void>{
      
      console.log(this.data.prioritetizationParameter.prioritetizationParameterId);
       (await this.userStoryService.addUserStory(new UserStoryCreatinDto(this.data.textUserStory, this.data.partOfEpic, 
                                      this.data.prioritetizationParameter.prioritetizationParameterId, this.data.backlogId))).subscribe(
        () => {
          this.snackBar.open('User story with this text: ' + this.data.textUserStory + ' succesfully created',
            'ok', {duration:3500})
  
        }
      ),
      (error:Error) => {
        console.log(error.name + ' ' + error.message);
        this.snackBar.open('Error while creating user story.',
        'ok', {duration:3500});
      }
    }

    public async update(): Promise<void> {
      (await this.userStoryService.updateUserStory(new UserStoryRootUpdateDto(this.data.userStoryRootId, this.data.textUserStory, this.data.partOfEpic, 
        this.data.prioritetizationParameter.prioritetizationParameterId, this.data.prioritetizationParameter, this.data.backlogId, this.data.sprintId))).subscribe(() => {
        this.snackBar.open('User story with this text: ' + this.data.textUserStory + ' succesfully created', 'OK', {
          duration: 2500
        })
      }),
        (error: Error) => {
          console.log(error.name + ' ' + error.message)
          this.snackBar.open('Error while updating user story.', 'Zatvori', {
            duration: 2500
          })
        };
  
    }

    public delete(): void {
      this.userStoryService.deleteUserStory(this.data.userStoryRootId).subscribe(() => {
        this.snackBar.open('User Story succefully deleted:' + this.data.userStoryRootId, 'OK', {
          duration: 2500
        })
      }),
        (error: Error) => {
          console.log(error.name + ' ' + error.message)
          this.snackBar.open('Error while deleting user story', 'Close', {
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
