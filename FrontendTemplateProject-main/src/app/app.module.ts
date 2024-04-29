import { TeamComponent } from "./components/team/team.component";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { RouterModule } from "@angular/router";
import { AppRoutingModule } from "./app.routing";
import { ComponentsModule } from "./components/components.module";
import { AppComponent } from "./app.component";
import { AdminLayoutComponent } from "./layouts/admin-layout/admin-layout.component";
import { MatDialogModule } from "@angular/material/dialog";
import { MatDatepickerModule } from "@angular/material/datepicker";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";
import { TableListComponent } from "./table-list/table-list.component";
import { TableListDialogComponent } from "./dialogs/table-list-dialog/table-list-dialog.component";
import { MatButton, MatButtonModule } from "@angular/material/button";
import { MatSnackBar, MatSnackBarModule } from "@angular/material/snack-bar";
import { MatNativeDateModule } from "@angular/material/core";
import { MatSelectModule } from "@angular/material/select";
import { TeamDialogComponent } from "./dialogs/team-dialog/team-dialog.component";
import { UserStoryDialogComponent } from "./dialogs/user-story-dialog/user-story-dialog.component";
import { FunctionalityDialogComponent } from "./dialogs/functionality-dialog/functionality-dialog.component";
import { TaskDialogComponent } from "./dialogs/task-dialog/task-dialog.component";
import { CommentDialogComponentComponent } from "./dialogs/comment-dialog/comment-dialog-component/comment-dialog-component.component";
import { CommentDialogAddComponent } from "./dialogs/comment-dialog/comment-dialog-add/comment-dialog-add.component";
import { UserStorySprintAddDialogComponent } from "./dialogs/sprint-userstory-dialog/user-story-sprint-add-dialog/user-story-sprint-add-dialog.component";
import { FunctionalitySprintAddDialogComponent } from "./dialogs/functionality-sprint-dialog/functionality-sprint-add-dialog.component";
import { TaskSprintAddDialogComponent } from "./dialogs/task-sprint-dialog/task-sprint-add-dialog.component";
import { CalendarComponent } from "./components/calendar/calendar.component";
import { CalendarDialogComponent } from "./dialogs/calendar-dialog/calendar-dialog.component";
import { EventComponent } from "./components/event/event.component";
import { EventDialogComponent } from "./dialogs/event-dialog/event-dialog.component";
import { EventTypeComponent } from "./components/event-type/event-type.component";
import { EventTypeDialogComponent } from "./dialogs/event-type-dialog/event-type-dialog.component";
import { UserComponent } from "./components/user/user.component";
import { UserDialogComponent } from "./dialogs/user-dialog/user-dialog.component";

@NgModule({
  imports: [
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    ComponentsModule,
    RouterModule,
    AppRoutingModule,
    MatDialogModule,
    MatDatepickerModule,
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    MatButtonModule,
    MatSnackBarModule,
    MatNativeDateModule,
    MatSelectModule,
  ],
  declarations: [
    AppComponent,
    AdminLayoutComponent,
    TableListDialogComponent,
    TeamComponent,
    TeamDialogComponent,
    //Dodati i za kalendar
    CalendarComponent,
    CalendarDialogComponent,
    EventComponent,
    EventDialogComponent,
    EventTypeComponent,
    EventTypeDialogComponent,
    //UserStory
    UserStoryDialogComponent,
    FunctionalityDialogComponent,
    TaskDialogComponent,
    UserStorySprintAddDialogComponent,
    FunctionalitySprintAddDialogComponent,
    TaskSprintAddDialogComponent,


    //Comment
    CommentDialogComponentComponent,
    CommentDialogAddComponent,

    //User
    UserComponent,
    UserDialogComponent
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
