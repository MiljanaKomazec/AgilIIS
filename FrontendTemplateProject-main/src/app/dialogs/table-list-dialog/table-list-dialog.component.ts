import { BacklogItemUpdateDTO } from './../../models/backlogItemUpdateDTO';

import { Component, Inject, NgModule, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogContent, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { SprintS } from 'app/models/sprint';
import { SprintServiceService } from 'app/services/sprint-service.service';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { PhaseOfBacklogItemServiceService } from 'app/services/phase-of-backlog-item-service.service';
import { PhaseOfBacklogItem } from 'app/models/phaseOfBacklogItem';
import { BacklogB } from 'app/models/backlog';
import { BacklogServiceService } from 'app/services/backlog-service.service';
import { BacklogItemBI } from 'app/models/backlogItem';
import { BacklogItemServicesService } from 'app/services/backlog-item-services.service';
import { BacklogItemDTO } from 'app/models/backlogItemDTO';
import { CommentC } from 'app/models/comment';
import { CommentServiceService } from 'app/services/comment-service.service';
//import {BacklogItemDTO} from 'app/models/backlogItemDTO'


@Component({
  selector: 'table-list-dialog',
  templateUrl: './table-list-dialog.component.html',
  styleUrls: ['./table-list-dialog.component.scss']
})
export class TableListDialogComponent implements OnInit {
  flag!:number;
  sprints!: SprintS[];
  pobis!: PhaseOfBacklogItem[];
  backlogs!: BacklogB[];
  comments!: CommentC[];

  constructor(public snackBar: MatSnackBar,
    public dialogRef: MatDialogRef<SprintS>,
    @Inject(MAT_DIALOG_DATA) public data: SprintS,
    @Inject(MAT_DIALOG_DATA) public dataPOBI: PhaseOfBacklogItem,
    @Inject(MAT_DIALOG_DATA) public dataBacklog: BacklogB,
    @Inject(MAT_DIALOG_DATA) public dataBI: BacklogItemBI,
    @Inject(MAT_DIALOG_DATA) public dataC: CommentC,
    public sprintService:SprintServiceService,
    public pobiService:PhaseOfBacklogItemServiceService,
    public backlogService:BacklogServiceService,
    public biService:BacklogItemServicesService,
    public commentService: CommentServiceService){

  }
  ngOnInit(): void {
    this.sprintService.getAllSprint().subscribe(
      dataBI => {
        this.sprints = dataBI;
      }
    )
    this.pobiService.getAllPhaseOfBacklogItem().subscribe(
      dataBI => {
        this.pobis = dataBI;
      }
    )

    this.backlogService.getAllBacklog().subscribe(
      dataBI => {
        this.backlogs = dataBI;
      }
    )
  }

  public compare(a:any,b:any){
    return a.id = b.id;
  }

  public add():void{
    this.sprintService.addSprint(this.data).subscribe(
      () => {
        this.snackBar.open('Sprint with duration: ' + this.data.durationSprint + ' succesfully created',
          'ok', {duration:3500})

      }
    ),
    (error:Error) => {
      console.log(error.name + ' ' + error.message);
      this.snackBar.open('Error while creating sprint.',
      'ok', {duration:3500});
    }
  }

  public update():void{
    this.sprintService.updateSprint(this.data).subscribe(
      () => {
        this.snackBar.open('Sprint with ID: ' + this.data.sprintId + ' succesfully updated',
          'ok', {duration:3500})

      }
    ),
    (error:Error) => {
      console.log(error.name + ' ' + error.message);
      this.snackBar.open('Error while updating sprint.',
      'ok', {duration:3500});
    }
  }
  
  public delete():void{
    this.sprintService.deleteSprint(this.data.sprintId).subscribe(
      () => {
        this.snackBar.open('Sprint succefully deleted',
          'ok', {duration:3500})

      }
    ),
    (error:Error) => {
      console.log(error.name + ' ' + error.message);
      this.snackBar.open('Error while deleting sprint.',
      'ok', {duration:3500});
    }
  }

  public addPOBI():void{
    this.pobiService.addPhaseOfBacklogItem(this.dataPOBI).subscribe(
      () => {
        this.snackBar.open('Phase of backlog item with name: ' + this.dataPOBI.nameOfPOBI + ' succesfully created',
          'ok', {duration:3500})

      }
    ),
    (error:Error) => {
      console.log(error.name + ' ' + error.message);
      this.snackBar.open('Error while creating Phase of backlog item.',
      'ok', {duration:3500});
    }
  }

  public updatePOBI():void{
    this.pobiService.updatePhaseOfBacklogItem(this.dataPOBI).subscribe(
      () => {
        this.snackBar.open('Phase of backlog item with ID: ' + this.dataPOBI.pobiId + ' succesfully updated',
          'ok', {duration:3500})

      }
    ),
    (error:Error) => {
      console.log(error.name + ' ' + error.message);
      this.snackBar.open('Error while updating Phase of backlog item.',
      'ok', {duration:3500});
    }
  }
  
  public deletePOBI():void{
    this.pobiService.deletePhaseOfBacklogItem(this.dataPOBI.pobiId).subscribe(
      () => {
        this.snackBar.open('Phase of backlog item succefully deleted',
          'ok', {duration:3500})

      }
    ),
    (error:Error) => {
      console.log(error.name + ' ' + error.message);
      this.snackBar.open('Error while deleting Phase of backlog item.',
      'ok', {duration:3500});
    }
  }

  public addBacklog():void{
    this.backlogService.addBacklog(this.dataBacklog).subscribe(
      () => {
        this.snackBar.open('Backlog: ' + this.dataBacklog.nameBacklog + ' succesfully created',
          'ok', {duration:3500})

      }
    ),
    (error:Error) => {
      console.log(error.name + ' ' + error.message);
      this.snackBar.open('Error while creating backlog.',
      'ok', {duration:3500});
    }
  }

  public updateBacklog():void{
    this.backlogService.updateBacklog(this.dataBacklog).subscribe(
      () => {
        this.snackBar.open('Backlog with ID: ' + this.dataBacklog.backlogId + ' succesfully updated',
          'ok', {duration:3500})

      }
    ),
    (error:Error) => {
      console.log(error.name + ' ' + error.message);
      this.snackBar.open('Error while updating backlog.',
      'ok', {duration:3500});
    }
  }
  
  public deleteBacklog():void{
    this.backlogService.deleteBacklog(this.dataBacklog.backlogId).subscribe(
      () => {
        this.snackBar.open('Backlog succefully deleted',
          'ok', {duration:3500})

      }
    ),
    (error:Error) => {
      console.log(error.name + ' ' + error.message);
      this.snackBar.open('Error while deleting backlog.',
      'ok', {duration:3500});
    }
  }

  public async addBI():Promise<void>{
      
      
    (await this.biService.addBacklogItem(new BacklogItemDTO(this.dataBI.backlogItemId, this.dataBI.timeAddedBacklogItem,
                                   this.dataBI.backlog.backlogId, this.dataBI.sprint.sprintId, this.dataBI.pobi.pobiId))).subscribe(
     () => {
       this.snackBar.open('Backlog item with this time added: ' + this.dataBI.timeAddedBacklogItem + ' succesfully created',
         'ok', {duration:3500})

     }
   ),
   (error:Error) => {
     console.log(error.name + ' ' + error.message);
     this.snackBar.open('Error while creating user story.',
     'ok', {duration:3500});
   }
 }

  public async updateBI():Promise<void>{
    (await this.biService.updateBacklogItem(new BacklogItemUpdateDTO(this.dataBI.backlogItemId, this.dataBI.timeAddedBacklogItem,
      this.dataBI.backlog.backlogId, this.dataBI.backlog, this.dataBI.sprint.sprintId, this.dataBI.sprint, this.dataBI.pobi.pobiId, this.dataBI.pobi))).subscribe(
      () => {
        this.snackBar.open('Backlog item with ID: ' + this.dataBI.backlogItemId + ' succesfully updated',
          'ok', {duration:3500})

      }
    ),
    (error:Error) => {
      console.log(error.name + ' ' + error.message);
      this.snackBar.open('Error while updating backlog item.',
      'ok', {duration:3500});
    }
  }
  
  public async deleteBI():Promise<void>{
    (await this.biService.deleteBacklogItem(this.dataBI.backlogItemId)).subscribe(
      () => {
        this.snackBar.open('Backlog item succefully deleted',
          'ok', {duration:3500})

      }
    ),
    (error:Error) => {
      console.log(error.name + ' ' + error.message);
      this.snackBar.open('Error while deleting backlog item.',
      'ok', {duration:3500});
    }
  }

  public deleteComment():void{
    this.commentService.deleteComment(this.dataC.commentId).subscribe(
      () => {
        this.snackBar.open('Comment succefully deleted',
          'ok', {duration:3500})

      }
    ),
    (error:Error) => {
      console.log(error.name + ' ' + error.message);
      this.snackBar.open('Error while deleting comment.',
      'ok', {duration:3500});
    }
  }

  public cancel():void{
    this.dialogRef.close();
    this.snackBar.open('You have given up on editing',
      'ok', {duration:3500});
  }
}
