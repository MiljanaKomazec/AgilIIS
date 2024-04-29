import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Functionality } from 'app/models/UserStoryModel/functionality';
import { FunctionalityUpdateDto } from 'app/models/UserStoryModel/functionalityUpdateDto';
import { UserStoryRoot } from 'app/models/UserStoryModel/userStory';
import { SprintS } from 'app/models/sprint';
import { FunctionalityService } from 'app/services/UserStory/functionality.service';
import { SprintServiceService } from 'app/services/sprint-service.service';

@Component({
  selector: 'functionality-sprint-add-dialog',
  templateUrl: './functionality-sprint-add-dialog.component.html',
  styleUrls: ['./functionality-sprint-add-dialog.component.scss']
})
export class FunctionalitySprintAddDialogComponent {
  functionalities!: Functionality;
  sprints!: SprintS[];

  constructor(public snackBar: MatSnackBar,
    public dialogRef: MatDialogRef<Functionality>,
    @Inject(MAT_DIALOG_DATA) public data: Functionality,
    public functionalityService: FunctionalityService,
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
    (await this.functionalityService.updateFunctionality(new FunctionalityUpdateDto(this.data.functionalityId, this.data.textFunctionality,
      this.data.userStoryRoot.userStoryRootId, this.data.userStoryRoot,this.data.sprintId))).subscribe(() => {
      this.snackBar.open('Functionality with this text: ' + this.data.textFunctionality + ' succesfully updated', 'OK', {
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

  public cancel():void{
    this.dialogRef.close();
    this.snackBar.open('You have given up on editing',
      'ok', {duration:3500});
  }


}
