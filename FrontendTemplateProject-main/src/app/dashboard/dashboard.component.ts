import { Functionality } from './../models/UserStoryModel/functionality';
import { Component, OnInit } from '@angular/core';
import { BacklogB } from 'app/models/backlog';
import { SprintS } from 'app/models/sprint';
import { BacklogServiceService } from 'app/services/backlog-service.service';
import { SprintServiceService } from 'app/services/sprint-service.service';
import { NgModel } from '@angular/forms';
import { Guid } from 'guid-typescript';
import { UserStoryRoot } from 'app/models/UserStoryModel/userStory';
import { Task } from 'app/models/UserStoryModel/task';
import { Subscription } from 'rxjs';
import { UserStoryService } from 'app/services/UserStory/userstory.service';
import { FunctionalityService } from 'app/services/UserStory/functionality.service';
import { TaskService } from 'app/services/UserStory/task.service';
import { MatDialog } from '@angular/material/dialog';
import { PrioritetizationParameter } from 'app/models/UserStoryModel/prioritetizationParameter';
import { UserStoryDialogComponent } from 'app/dialogs/user-story-dialog/user-story-dialog.component';
import { FunctionalityDialogComponent } from 'app/dialogs/functionality-dialog/functionality-dialog.component';
import { TaskDialogComponent } from 'app/dialogs/task-dialog/task-dialog.component';
import { CommentDialogComponentComponent } from 'app/dialogs/comment-dialog/comment-dialog-component/comment-dialog-component.component';
import { CommentDialogAddComponent } from 'app/dialogs/comment-dialog/comment-dialog-add/comment-dialog-add.component';
import { UserStorySprintAddDialogComponent } from 'app/dialogs/sprint-userstory-dialog/user-story-sprint-add-dialog/user-story-sprint-add-dialog.component';
import { FunctionalitySprintAddDialogComponent } from 'app/dialogs/functionality-sprint-dialog/functionality-sprint-add-dialog.component';
import { TaskSprintAddDialogComponent } from 'app/dialogs/task-sprint-dialog/task-sprint-add-dialog.component';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
  providers: [NgModel]
})
export class DashboardComponent implements OnInit {

  sprints: SprintS[] = [];
  selectedSprint: SprintS;

  selectedUserStoryId: Guid;

  backlogs: BacklogB[] = [];
  selectedBacklog: BacklogB;
  //selectedBacklogName: string = '';

  //UserStory
  stories: UserStoryRoot[] = [];
  functionalities: Functionality[] = [];
  tasks: Task[] = [];

  showFunctionalities: boolean = false;
  showFunctionalitiesSprint: boolean = false;
  showTasks: boolean = false;
  showTasksSprint: boolean = false;
  showUserStories: boolean = false;
  showUserStoriesSprint: boolean = false;

  subscription!: Subscription;

  constructor(private sprintService: SprintServiceService,
    private backlogService: BacklogServiceService,
    private userStoryService: UserStoryService,
    private functionalityService: FunctionalityService,
    private taskService: TaskService,
    private dialog: MatDialog) {
      this.subscription = new Subscription(); // Inicijalizacija subscription
}

  ngOnInit() {
    this.startSubscription();
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  startSubscription() {
    this.sprintService.getAllSprint().subscribe(res => {
      this.sprints = res;
      console.log(this.sprints);
    });

    this.backlogService.getAllBacklog().subscribe(res => {
      this.backlogs = res;
      console.log(this.backlogs);
    });

  }

  onSelectBacklog() {
    console.log('Selected backlog:', this.selectedBacklog); 
  }

  getUserStoriesByBacklogId(backlogId: Guid) {
    console.log('Sending request for user stories for backlog with ID:', backlogId); // Provera slanja zahteva

    this.userStoryService.getUserStoriesByBacklogId(backlogId).subscribe(res => {
      console.log('Fetched user stories:', res); // Provera dobijenih korisničkih priča
      this.stories = res;
      this.showUserStories = true;
    }, error => {
      console.error('Error fetching user stories:', error); // Provera greške ako se dogodi
    });
    
  }

  onSprintSelected() {
    console.log('Selected Sprint:', this.selectedSprint);
  }

  getUserStoriesBySprintId(sprintId: Guid) {
    console.log('Sending request for user stories for sprint with ID:', sprintId); // Provera slanja zahteva

    this.userStoryService.getUserStoriesBySprintId(sprintId).subscribe(res => {
      console.log('Fetched user stories:', res); // Provera dobijenih korisničkih priča
      this.stories = res;
      this.showUserStoriesSprint = true;
    }, error => {
      console.error('Error fetching user stories:', error); // Provera greške ako se dogodi
    });
    
  }

  public compare(a: any, b: any) {
    return a.id = b.id;
  }

  getFunctionalityByUserStory(userStoryId: Guid) {
    this.functionalityService.getFunctionalityByUserStory(userStoryId).subscribe(res => {
      console.log('Dohvaćene funkcionalnosti:', res);
      this.functionalities = res;
    });
    this.selectedUserStoryId = userStoryId
    this.showFunctionalities = true;
  }

  getFunctionalityBySprintId(sprintId: Guid) {
    console.log('Sending request for functionalities for sprint with ID:', sprintId); // Provera slanja zahteva

    this.functionalityService.getFunctionalitiesBySprintId(sprintId).subscribe(res => {
      console.log('Fetched functionalities:', res); // Provera dobijenih korisničkih priča
      this.functionalities = res;
      this.showFunctionalitiesSprint = true;
    }, error => {
      console.error('Error fetching user stories:', error); // Provera greške ako se dogodi
    });
    
  }

  getTaskByFunctionality(functionalityId: Guid) {
    this.taskService.GetTaskByFunctionality(functionalityId).subscribe(res => {
      console.log('Dohvaćeni taskovi:', res);
      this.tasks = res;
    });

    this.showTasks = true;
  }


  public openDialog(flag:number, userStoryRootId?:Guid, textUserStory?:string, partOfEpic?:string, prioritetizationParameter?: PrioritetizationParameter, backlogId?: Guid):void{
    // Provera da li je selectedBacklog definisan pre pristupa backlogId
    //const backlogId = selectedBacklog ? selectedBacklog.backlogId : undefined;
    
    console.log('Flag value:', flag);
    console.log('Selected backlogId:', backlogId);

    const dialogRef = this.dialog.open(UserStoryDialogComponent, { data: { userStoryRootId, textUserStory, partOfEpic, prioritetizationParameter, backlogId } });

    dialogRef.componentInstance.flag=flag;
    dialogRef.afterClosed().subscribe(
      result =>{
        if(result==1){
          this.startSubscription();
        }
      }
    )
}

  public openDialogFunctionality(flag: number, functionalityId?: Guid, textFunctionality?: string,  userStoryRootId?:Guid ,userStoryRoot?: UserStoryRoot): void {
    console.log('Flag value:', flag);
    const dialogRef = this.dialog.open(FunctionalityDialogComponent, { data: { functionalityId, textFunctionality, userStoryRootId,userStoryRoot } });
    dialogRef.componentInstance.flag = flag;
    dialogRef.afterClosed().subscribe(
      result => {
        if (result == 1) {
          this.startSubscription();
        }
      }
    )
  }

  public openDialogTask(flag: number, taskId?: Guid, textTask?: string, functionalityId?: Guid  ,functionality?: Functionality): void {
    console.log('Flag value:', flag);
    const dialogRef = this.dialog.open(TaskDialogComponent, { data: { taskId, textTask, functionalityId, functionality } });
    dialogRef.componentInstance.flag = flag;
    dialogRef.afterClosed().subscribe(
      result => {
        if (result == 1) {
          this.startSubscription();
        }
      }
    )
  }

  public openDialogCommentUserStory(userStoryRootId?:Guid): void {
    console.log('UserStory value:', userStoryRootId);
    const dialogRef = this.dialog.open(CommentDialogComponentComponent, { data: { userStoryRootId } });
    dialogRef.afterClosed().subscribe(
      result => {
        if (result == 1) {
          this.startSubscription();
        }
      }
    )
  }

  public openDialogCommentAdd(userStoryRootId?:Guid): void {
    console.log('UserStory value:', userStoryRootId);
    const dialogRef = this.dialog.open(CommentDialogAddComponent, { data: { userStoryRootId } });
    dialogRef.afterClosed().subscribe(
      result => {
        if (result == 1) {
          this.startSubscription();
        }
      }
    )
  }

  public openDialogS(userStoryRootId?:Guid, textUserStory?:string, partOfEpic?:string, prioritetizationParameter?: PrioritetizationParameter, backlogId?: Guid): void {
    console.log('UserStory value:', userStoryRootId);
    const dialogRef = this.dialog.open(UserStorySprintAddDialogComponent, { data: { userStoryRootId, textUserStory, partOfEpic, prioritetizationParameter, backlogId } });
    dialogRef.afterClosed().subscribe(
      result => {
        if (result == 1) {
          this.startSubscription();
        }
      }
    )
  }

  public openDialogSprintFunctionality(functionalityId?:Guid, textFunctionality?:string, userStoryRoot?: UserStoryRoot): void {
    console.log('Functionality value:', functionalityId);
    const dialogRef = this.dialog.open(FunctionalitySprintAddDialogComponent, { data: { functionalityId, textFunctionality, userStoryRoot } });
    dialogRef.afterClosed().subscribe(
      result => {
        if (result == 1) {
          this.startSubscription();
        }
      }
    )
  }

  public openDialogSprintTask(taskId?:Guid, textTask?:string, functionality?: Functionality): void {
    console.log('Task value:', taskId);
    const dialogRef = this.dialog.open(TaskSprintAddDialogComponent, { data: { taskId, textTask, functionality } });
    dialogRef.afterClosed().subscribe(
      result => {
        if (result == 1) {
          this.startSubscription();
        }
      }
    )
  }
}
