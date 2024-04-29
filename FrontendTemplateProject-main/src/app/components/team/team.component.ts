import { Subscription } from "rxjs";
import { Component, OnInit } from "@angular/core";
import { Team } from "app/models/Calendar-Tim/team";
import { TeamServicesService } from "app/services/Calendar-Tim/team-service.service";
import { MatDialog } from "@angular/material/dialog";
import { Guid } from "guid-typescript";
import { TeamDialogComponent } from "app/dialogs/team-dialog/team-dialog.component";

@Component({
  selector: "app-team",
  templateUrl: "./team.component.html",
  styleUrls: ["./team.component.css"],
})
export class TeamComponent implements OnInit {
  teams: Team[];

  subscription!: Subscription;

  constructor(
    private teamService: TeamServicesService,
    public dialog: MatDialog
  ) {}

  ngOnInit() {
    this.startSubscription();
  }
  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
  //pokrece subscribe na tok podataka i puni niz sa Timovima iz baze
  startSubscription() {
    this.teamService.getAllTeams().subscribe((res) => {
      console.log(res);
      this.teams = res;
      console.log(this.teams);
    });
  }

  public openDialog(
    flag: number,
    teamId?: Guid,
    teamName?: string,
    teamDescription?: String,
    userId?: Guid,
    calendarId?: Guid
  ): void {
    console.log("Flag value:", flag);
    const dialogRef = this.dialog.open(TeamDialogComponent, {
      data: { teamId, teamName, teamDescription, userId, calendarId },
    });
    dialogRef.componentInstance.flag = flag;
    dialogRef.afterClosed().subscribe((result) => {
      if (result == 1) {
        this.startSubscription();
      }
    });
  }
}
