import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Functionality } from 'app/models/UserStoryModel/functionality';
import { Task } from 'app/models/UserStoryModel/task';
import { TaskCreationDto } from 'app/models/UserStoryModel/taskCreationDto';
import { TaskDto } from 'app/models/UserStoryModel/taskDto';
import { TaskUpdateDto } from 'app/models/UserStoryModel/taskUpdateDto';
import { FunctionalityService } from 'app/services/UserStory/functionality.service';
import { TaskService } from 'app/services/UserStory/task.service';

@Component({
  selector: 'app-task-dialog',
  templateUrl: './task-dialog.component.html',
  styleUrls: ['./task-dialog.component.css']
})
export class TaskDialogComponent implements OnInit {

  flag!:number;
  functionalities: Functionality[];

  constructor(public snackBar: MatSnackBar,
    public dialogRef: MatDialogRef<Task>,
    @Inject(MAT_DIALOG_DATA) public data: Task,
    public taskService: TaskService,
    public fService: FunctionalityService
    ) { }

  ngOnInit(): void {
    this.fService.getAllFunctionality().subscribe(
      data => {
        this.functionalities = data;
      }
    )
  }

  public compare(a:any,b:any){
    return a.id = b.id;
  }

  public async add():Promise<void>{
    
    console.log(this.data.functionality.functionalityId);
     (await this.taskService.addTask(new TaskCreationDto(this.data.textTask,
                                    this.data.functionality.functionalityId))).subscribe(
      () => {
        this.snackBar.open('Task with this text: ' + this.data.textTask + ' succesfully created',
          'ok', {duration:3500})

      }
    ),
    (error:Error) => {
      console.log(error.name + ' ' + error.message);
      this.snackBar.open('Error while creating task.',
      'ok', {duration:3500});
    }
  }

  public async update(): Promise<void> {
    (await this.taskService.updateTask(new TaskUpdateDto(this.data.taskId, this.data.textTask,
      this.data.functionality.functionalityId, this.data.functionality, this.data.sprintId))).subscribe(() => {
      this.snackBar.open('Task with this text: ' + this.data.textTask + ' succesfully created', 'OK', {
        duration: 2500
      })
    }),
      (error: Error) => {
        console.log(error.name + ' ' + error.message)
        this.snackBar.open('Error while updating task.', 'Zatvori', {
          duration: 2500
        })
      };

  }

  public delete(): void {
    this.taskService.deleteTask(this.data.taskId).subscribe(() => {
      this.snackBar.open('Task succefully deleted:' + this.data.taskId, 'OK', {
        duration: 2500
      })
    }),
      (error: Error) => {
        console.log(error.name + ' ' + error.message)
        this.snackBar.open('Error while deleting task', 'Close', {
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
