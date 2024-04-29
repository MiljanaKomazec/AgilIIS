import { Component, Inject, OnInit } from "@angular/core";
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";
import { MatSnackBar } from "@angular/material/snack-bar";
import { Team } from "app/models/Calendar-Tim/team";
import { TeamServicesService } from "app/services/Calendar-Tim/team-service.service";

@Component({
  selector: "team-dialog",
  templateUrl: "./team-dialog.component.html",
  styleUrls: ["./team-dialog.component.css"],
})
export class TeamDialogComponent implements OnInit {
  flag!: number;
  teams!: Team[];

  constructor(
    public snackBar: MatSnackBar,
    public dialogRef: MatDialogRef<Team>,
    @Inject(MAT_DIALOG_DATA) public data: Team,
    public teamService: TeamServicesService
  ) {}

  ngOnInit(): void {
    this.teamService.getAllTeams().subscribe((dataBI) => {
      this.teams = dataBI;
    });
  }
  public compare(a: any, b: any) {
    return (a.id = b.id);
  }

  public add(): void {
    this.teamService.addTeam(this.data).subscribe(() => {
      this.snackBar.open(
        "Team with name: " + this.data.teamName + " succesfully created",
        "ok",
        { duration: 3500 }
      );
    }),
      (error: Error) => {
        console.log(error.name + " " + error.message);
        this.snackBar.open("Error while creating team.", "ok", {
          duration: 3500,
        });
      };
  }

  public update(): void {
    this.teamService.updateTeam(this.data).subscribe(() => {
      this.snackBar.open(
        "Team with ID: " + this.data.teamId + " succesfully updated",
        "ok",
        { duration: 3500 }
      );
    }),
      (error: Error) => {
        console.log(error.name + " " + error.message);
        this.snackBar.open("Error while updating team.", "ok", {
          duration: 3500,
        });
      };
  }

  public delete(): void {
    this.teamService.deleteTeam(this.data.teamId).subscribe(() => {
      this.snackBar.open("Team succefully deleted", "ok", { duration: 3500 });
    }),
      (error: Error) => {
        console.log(error.name + " " + error.message);
        this.snackBar.open("Error while deleting team.", "ok", {
          duration: 3500,
        });
      };
  }
  public cancel(): void {
    this.dialogRef.close();
    this.snackBar.open("You have given up on editing", "ok", {
      duration: 3500,
    });
  }
}
