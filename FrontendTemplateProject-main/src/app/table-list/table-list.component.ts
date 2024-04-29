import { Component, OnInit } from "@angular/core";
import { BacklogB } from "app/models/backlog";
import { BacklogItemBI } from "app/models/backlogItem";
import { CommentC } from "app/models/comment";
import { PhaseOfBacklogItem } from "app/models/phaseOfBacklogItem";
import { SprintS } from "app/models/sprint";
import { BacklogItemServicesService } from "app/services/backlog-item-services.service";
import { BacklogServiceService } from "app/services/backlog-service.service";
import { CommentServiceService } from "app/services/comment-service.service";
import { PhaseOfBacklogItemServiceService } from "app/services/phase-of-backlog-item-service.service";
import { SprintServiceService } from "app/services/sprint-service.service";
import { Subscription } from "rxjs";
import { MatDialog } from "@angular/material/dialog";
import { TableListDialogComponent } from "app/dialogs/table-list-dialog/table-list-dialog.component";
import { Guid } from "guid-typescript";

@Component({
  selector: "app-table-list",
  templateUrl: "./table-list.component.html",
  styleUrls: ["./table-list.component.css"],
})
export class TableListComponent implements OnInit {
  sprints: SprintS[] = [];
  pobis: PhaseOfBacklogItem[] = [];
  backlogs: BacklogB[] = [];
  backlogItems: BacklogItemBI[] = [];
  comments: CommentC[] = [];

  subscription!: Subscription;

  constructor(
    private sprintService: SprintServiceService,
    private pobiService: PhaseOfBacklogItemServiceService,
    private backlogService: BacklogServiceService,
    private backlogItemService: BacklogItemServicesService,
    private commentService: CommentServiceService,
    public dialog: MatDialog
  ) {}

  ngOnInit() {
    this.startSubscription();
  }
  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  startSubscription() {
    this.sprintService.getAllSprint().subscribe((res) => {
      console.log(res);
      this.sprints = res;
      console.log(this.sprints);
    });

    this.pobiService.getAllPhaseOfBacklogItem().subscribe((res) => {
      console.log(res);
      this.pobis = res;
      console.log(this.pobis);
    });

    this.backlogService.getAllBacklog().subscribe((res) => {
      console.log(res);
      this.backlogs = res;
      console.log(this.backlogs);
    });

    this.backlogItemService.getAllBacklogItem().subscribe((res) => {
      console.log(res);
      this.backlogItems = res;
      console.log(this.backlogItems);
    });

    this.commentService.getAllComment().subscribe((res) => {
      console.log(res);
      this.comments = res;
      console.log(this.comments);
    });
  }

  public openDialog(
    flag: number,
    sprintId?: Guid,
    durationSprint?: string,
    startOfSprint?: Date,
    endOfSprint?: Date
  ): void {
    console.log("Flag value:", flag);
    const dialogRef = this.dialog.open(TableListDialogComponent, {
      data: { sprintId, durationSprint, startOfSprint, endOfSprint },
    });
    dialogRef.componentInstance.flag = flag;
    dialogRef.afterClosed().subscribe((result) => {
      if (result == 1) {
        this.startSubscription();
      }
    });
  }

  public openDialogPOBI(
    flag: number,
    pobiId?: Guid,
    nameOfPOBI?: string
  ): void {
    console.log("Flag value:", flag);
    const dialogRef = this.dialog.open(TableListDialogComponent, {
      data: { pobiId, nameOfPOBI },
    });
    dialogRef.componentInstance.flag = flag;
    dialogRef.afterClosed().subscribe((result) => {
      if (result == 1) {
        this.startSubscription();
      }
    });
  }

  public openDialogBacklog(
    flag: number,
    backlogId?: Guid,
    nameBacklog?: string
  ): void {
    console.log("Flag value:", flag);
    const dialogRef = this.dialog.open(TableListDialogComponent, {
      data: { backlogId, nameBacklog },
    });
    dialogRef.componentInstance.flag = flag;
    dialogRef.afterClosed().subscribe((result) => {
      if (result == 1) {
        this.startSubscription();
      }
    });
  }

  public openDialogBI(
    flag: number,
    backlogItemId?: Guid,
    timeAddedBacklogItem?: string,
    backlog?: BacklogB,
    sprint?: SprintS,
    pobi?: PhaseOfBacklogItem
  ): void {
    console.log("Flag value:", flag);
    const dialogRef = this.dialog.open(TableListDialogComponent, {
      data: { backlogItemId, timeAddedBacklogItem, backlog, sprint, pobi },
    });
    dialogRef.componentInstance.flag = flag;
    dialogRef.afterClosed().subscribe((result) => {
      if (result == 1) {
        this.startSubscription();
      }
    });
  }

  public openDialogC(
    flag: number,
    commentId?: Guid,
    dateComment?: Date,
    textComment?: string,
    userStoryRootId?: Guid,
  ): void {
    console.log("Flag value:", flag);
    const dialogRef = this.dialog.open(TableListDialogComponent, {
      data: { commentId, dateComment, textComment, userStoryRootId},
    });
    dialogRef.componentInstance.flag = flag;
    dialogRef.afterClosed().subscribe((result) => {
      if (result == 1) {
        this.startSubscription();
      }
    });
  }
}
