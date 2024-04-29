import { Component, Inject, OnInit } from "@angular/core";
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";
import { MatSnackBar } from "@angular/material/snack-bar";
import { User } from "app/models/user";
import { UserService } from "app/services/UserService/user.service";

@Component({
  selector: "user-dialog",
  templateUrl: "./user-dialog.component.html",
  styleUrls: ["./user-dialog.component.css"],
})
export class UserDialogComponent implements OnInit {
  flag!: number;
  users!: User[];

  constructor(
    public snackBar: MatSnackBar,
    public dialogRef: MatDialogRef<User>,
    @Inject(MAT_DIALOG_DATA) public data: User,
    public userService: UserService
  ) {}

  ngOnInit(): void {
    this.userService.getAllUsers().subscribe((data) => {
      this.users = data;
    });
  }
  public compare(a: any, b: any) {
    return (a.id = b.id);
  }

  public add(): void {
    this.userService.addUser(this.data).subscribe(() => {
      this.snackBar.open(
        "User with name: " + this.data.nameUser + " succesfully created",
        "ok",
        { duration: 3500 }
      );
    }),
      (error: Error) => {
        console.log(error.name + " " + error.message);
        this.snackBar.open("Error while creating user.", "ok", {
          duration: 3500,
        });
      };
  }

  public update(): void {
    this.userService.updateUser(this.data).subscribe(() => {
      this.snackBar.open(
        "User with ID: " + this.data.idUser + " succesfully updated",
        "ok",
        { duration: 3500 }
      );
    }),
      (error: Error) => {
        console.log(error.name + " " + error.message);
        this.snackBar.open("Error while updating user.", "ok", {
          duration: 3500,
        });
      };
  }

  public delete(): void {
    this.userService.deleteUser(this.data.idUser).subscribe(
      () => {
      this.snackBar.open("User succefully deleted", "ok", { duration: 3500 });
    },
      (error: Error) => {
        console.log(error.name + " " + error.message);
        this.snackBar.open("Error while deleting user.", "ok", {
          duration: 3500,
        });
      }
    );
  }
  public cancel(): void {
    this.dialogRef.close();
    this.snackBar.open("You have given up on editing", "ok", {
      duration: 3500,
    });
  }
}
