import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { FunctionalityUpdateDto } from 'app/models/UserStoryModel/functionalityUpdateDto';
import { Task } from 'app/models/UserStoryModel/task';
import { TaskUpdateDto } from 'app/models/UserStoryModel/taskUpdateDto';
import { SprintS } from 'app/models/sprint';
import { TaskService } from 'app/services/UserStory/task.service';
import { SprintServiceService } from 'app/services/sprint-service.service';

@Component({
  selector: 'task-sprint-add-dialog',
  templateUrl: './task-sprint-add-dialog.component.html',
  styleUrls: ['./task-sprint-add-dialog.component.scss']

})
export class TaskSprintAddDialogComponent {
  tasks!: Task;
  sprints!: SprintS[];

  constructor(public snackBar: MatSnackBar,
    public dialogRef: MatDialogRef<Task>,
    @Inject(MAT_DIALOG_DATA) public data: Task,
    public taskService: TaskService,
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
    (await this.taskService.updateTask(new TaskUpdateDto(this.data.taskId, this.data.textTask,
      this.data.functionality.functionalityId, this.data.functionality, this.data.sprintId))).subscribe(() => {
      this.snackBar.open('Task with this text: ' + this.data.textTask + ' succesfully updated', 'OK', {
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

  public cancel():void{
    this.dialogRef.close();
    this.snackBar.open('You have given up on editing',
      'ok', {duration:3500});
  }


}
