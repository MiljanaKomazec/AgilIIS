import { EventTypeService } from './../../services/Calendar-Tim/eventType-service.service';
import { Component, Inject, OnInit } from "@angular/core";
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";
import { MatSnackBar } from "@angular/material/snack-bar";
import { Calendar } from "app/models/Calendar-Tim/calendar";
import { Event } from "app/models/Calendar-Tim/event";
import { eventDTO } from 'app/models/Calendar-Tim/eventDTO';
import { EventType } from "app/models/Calendar-Tim/eventType";
import { eventUpdateDTO } from 'app/models/Calendar-Tim/eventUpdateDTO';
import { CalendarService } from "app/services/Calendar-Tim/calendar-service.service";
import { EventService } from "app/services/Calendar-Tim/event-service.service";

@Component({
  selector: "calendar-dialog",
  templateUrl: "./calendar-dialog.component.html",
  styleUrls: ["./calendar-dialog.component.css"],
})
export class CalendarDialogComponent implements OnInit {
  flag!: number;
  calendars!: Calendar[];
  events!: Event[];
  eventTypes!: EventType[];

  constructor(
    public snackBar: MatSnackBar,
    public dialogRef: MatDialogRef<Calendar>,
    @Inject(MAT_DIALOG_DATA) public data: Calendar,
    @Inject(MAT_DIALOG_DATA) public dataEvent: Event,
    @Inject(MAT_DIALOG_DATA) public dataEventType: EventType,
    public calendarService: CalendarService,
    public eventService: EventService,
    public eventTypeService: EventTypeService
  ) {}

  ngOnInit(): void {
    this.calendarService.getAllCalendars().subscribe((dataBI) => {
      this.calendars = dataBI;
    });
    this.eventService.getAllEvents().subscribe((data) => {
      this.events = data;
    })
    this.eventTypeService.getAllEventTypes().subscribe((dataE) => {
      this.eventTypes = dataE
    })
  }
  public compare(a: any, b: any) {
    return (a.id = b.id);
  }

  public add(): void {
    this.calendarService.addCalendar(this.data).subscribe(() => {
      this.snackBar.open(
        "Calendar with name: " + this.data.calendarName + " succesfully created",
        "ok",
        { duration: 3500 }
      );
    }),
      (error: Error) => {
        console.log(error.name + " " + error.message);
        this.snackBar.open("Error while creating calendar.", "ok", {
          duration: 3500,
        });
      };
  }

  public update(): void {
    this.calendarService.updateCalendar(this.data).subscribe(() => {
      this.snackBar.open(
        "Calendar with ID: " + this.data.calendarId + " succesfully updated",
        "ok",
        { duration: 3500 }
      );
    }),
      (error: Error) => {
        console.log(error.name + " " + error.message);
        this.snackBar.open("Error while updating calendar.", "ok", {
          duration: 3500,
        });
      };
  }

  public delete(): void {
    this.calendarService.deleteCalendar(this.data.calendarId).subscribe(() => {
      this.snackBar.open("Calendar succefully deleted", "ok", { duration: 3500 });
    }),
      (error: Error) => {
        console.log(error.name + " " + error.message);
        this.snackBar.open("Error while deleting calendar.", "ok", {
          duration: 3500,
        });
      };
  }

  public addEvent(): void {
    this.eventService.addEvent(new eventDTO(this.dataEvent.eventId, this.dataEvent.eventName, this.dataEvent.eventDate,
      this.dataEvent.eventTime, this.dataEvent.eventDescription,this.dataEvent.eventType.eventTypeId,this.dataEvent.calendar.calendarId)).subscribe(() => {
      this.snackBar.open(
        "Event with name: " + this.dataEvent.eventName + " succesfully created",
        "ok",
        { duration: 3500 }
      );
    }),
      (error: Error) => {
        console.log(error.name + " " + error.message);
        this.snackBar.open("Error while creating event.", "ok", {
          duration: 3500,
        });
      };
  }

  public updateEvent(): void {
    this.eventService.updateEvent(new eventUpdateDTO(this.dataEvent.eventId, this.dataEvent.eventName, this.dataEvent.eventDate,
      this.dataEvent.eventTime, this.dataEvent.eventDescription, this.dataEvent.eventType.eventTypeId, this.dataEvent.eventType, this.dataEvent.calendar.calendarId, this.dataEvent.calendar)).subscribe(() => {
      this.snackBar.open(
        "Event with ID: " + this.dataEvent.eventId + " succesfully updated",
        "ok",
        { duration: 3500 }
      );
    }),
      (error: Error) => {
        console.log(error.name + " " + error.message);
        this.snackBar.open("Error while updating event.", "ok", {
          duration: 3500,
        });
      };
  }

  public deleteEvent(): void {
    this.eventService.deleteEvent(this.dataEvent.eventId).subscribe(() => {
      this.snackBar.open("Event succefully deleted", "ok", { duration: 3500 });
    }),
      (error: Error) => {
        console.log(error.name + " " + error.message);
        this.snackBar.open("Error while deleting event.", "ok", {
          duration: 3500,
        });
      };
  }

  public addEventType(): void {
    this.eventTypeService.addEventType(this.dataEventType).subscribe(() => {
      this.snackBar.open(
        "EventType with name: " + this.dataEventType.eventTypeName + " succesfully created",
        "ok",
        { duration: 3500 }
      );
    }),
      (error: Error) => {
        console.log(error.name + " " + error.message);
        this.snackBar.open("Error while creating eventType.", "ok", {
          duration: 3500,
        });
      };
  }

  public updateEventType(): void {
    console.log(this.dataEventType.eventTypeId);
    this.eventTypeService.updateEventType(this.dataEventType).subscribe(() => {
      this.snackBar.open(
        "EventType with ID: " + this.dataEventType.eventTypeId + " succesfully updated",
        "ok",
        { duration: 3500 }
      );
    }),
      (error: Error) => {
        console.log(error.name + " " + error.message);
        this.snackBar.open("Error while updating eventType.", "ok", {
          duration: 3500,
        });
      };
  }

  public deleteEventType(): void {
    this.eventTypeService.deleteEventType(this.dataEventType.eventTypeId).subscribe(() => {
      this.snackBar.open("Event Type succefully deleted", "ok", { duration: 3500 });
    }),
      (error: Error) => {
        console.log(error.name + " " + error.message);
        this.snackBar.open("Error while deleting eventType.", "ok", {
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
