import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Functionality } from 'app/models/UserStoryModel/functionality';
import { FunctionalityCreationDto } from 'app/models/UserStoryModel/functionalityCreationDto';
import { FunctionalityDto } from 'app/models/UserStoryModel/functionalityDto';
import { FunctionalityUpdateDto } from 'app/models/UserStoryModel/functionalityUpdateDto';
import { UserStoryRoot } from 'app/models/UserStoryModel/userStory';
import { FunctionalityService } from 'app/services/UserStory/functionality.service';
import { UserStoryService } from 'app/services/UserStory/userstory.service';

@Component({
  selector: 'app-functionality-dialog',
  templateUrl: './functionality-dialog.component.html',
  styleUrls: ['./functionality-dialog.component.css']
})
export class FunctionalityDialogComponent implements OnInit {
  flag!:number;
  stories: UserStoryRoot[];

  constructor(public snackBar: MatSnackBar,
    public dialogRef: MatDialogRef<Functionality>,
    @Inject(MAT_DIALOG_DATA) public data: Functionality,
    public functionalityService: FunctionalityService,
    public usService: UserStoryService
    ) { }

  ngOnInit(): void {
    this.usService.getAllUserStory().subscribe(
      data => {
        this.stories = data;
      }
    )
  }

  public compare(a:any,b:any){
    return a.id = b.id;
  }

  public async add():Promise<void>{
    
    console.log(this.data.userStoryRoot.userStoryRootId);
     (await this.functionalityService.addFunctionality(new FunctionalityCreationDto(this.data.textFunctionality, 
                                    this.data.userStoryRoot.userStoryRootId))).subscribe(
      () => {
        this.snackBar.open('Functionality with this text: ' + this.data.textFunctionality + ' succesfully created',
          'ok', {duration:3500})

      }
    ),
    (error:Error) => {
      console.log(error.name + ' ' + error.message);
      this.snackBar.open('Error while creating functionality.',
      'ok', {duration:3500});
    }
  }

  public async update(): Promise<void> {
    (await this.functionalityService.updateFunctionality(new FunctionalityUpdateDto(this.data.functionalityId, this.data.textFunctionality,
      this.data.userStoryRoot.userStoryRootId, this.data.userStoryRoot, this.data.sprintId))).subscribe(() => {
      this.snackBar.open('Functionality with this text: ' + this.data.textFunctionality + ' succesfully created', 'OK', {
        duration: 2500
      })
    }),
      (error: Error) => {
        console.log(error.name + ' ' + error.message)
        this.snackBar.open('Error while updating functionality.', 'Zatvori', {
          duration: 2500
        })
      };

  }

  public delete(): void {
    this.functionalityService.deleteFunctionality(this.data.functionalityId).subscribe(() => {
      this.snackBar.open('Functionality succefully deleted:' + this.data.functionalityId, 'OK', {
        duration: 2500
      })
    }),
      (error: Error) => {
        console.log(error.name + ' ' + error.message)
        this.snackBar.open('Error while deleting functionality', 'Close', {
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
