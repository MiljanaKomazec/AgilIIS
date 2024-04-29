import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { UserDialogComponent } from 'app/dialogs/user-dialog/user-dialog.component';
import { User } from 'app/models/user';
import { UserService } from 'app/services/UserService/user.service';
import { Guid } from 'guid-typescript';

import { Subscription } from 'rxjs';


@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  constructor(private userService: UserService, public dialog: MatDialog) { }

  users: User[] = [];

  subscription!: Subscription;

  ngOnInit() {
    this.startSubscription();
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  startSubscription() {
    this.userService.getAllUsers().subscribe((res) => {
        console.log(res);
        this.users = res;
        console.log(this.users);
  });

  }

  public openDialog(
    flag: number,
    idUser?: Guid,
    nameUser?: string,
    surnameUser?: string,
    emailUser?: string,
    teamId?: Guid,
    nameRole?:string
  ): void {
    console.log("Flag value:", flag);
    const dialogRef = this.dialog.open(UserDialogComponent, {
      data: { idUser, nameUser, surnameUser, emailUser, teamId, nameRole },
    });
    dialogRef.componentInstance.flag = flag;
    dialogRef.afterClosed().subscribe((result) => {
      if (result == 1) {
        this.startSubscription();
      }
    });
  }

}

  

