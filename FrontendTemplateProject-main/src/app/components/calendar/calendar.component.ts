import { Component, OnInit } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { CalendarDialogComponent } from "app/dialogs/calendar-dialog/calendar-dialog.component";
import { Calendar } from "app/models/Calendar-Tim/calendar";
import { EventType } from "app/models/Calendar-Tim/eventType";
import { CalendarService } from "app/services/Calendar-Tim/calendar-service.service";
import { EventService } from "app/services/Calendar-Tim/event-service.service";
import { EventTypeService } from "app/services/Calendar-Tim/eventType-service.service";
import { Guid } from "guid-typescript";
import { Subscription } from "rxjs";

@Component({
  selector: "calendar",
  templateUrl: "./calendar.component.html",
  styleUrls: ["./calendar.component.css"],
})
export class CalendarComponent implements OnInit {
  calendars: Calendar[];
  events: Event[];
  eventTypes: EventType[];

  subscription!: Subscription;

  constructor(
    private calendarService: CalendarService,
    private eventService: EventService,
    private eventTypeService: EventTypeService,
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
    this.calendarService.getAllCalendars().subscribe((res) => {
      console.log(res);
      this.calendars = res;
      console.log(this.calendars);
    });
    this.eventService.getAllEvents().subscribe((res) => {
      console.log(res);
      this.events = res;
      console.log(this.events);
    })
    this.eventTypeService.getAllEventTypes().subscribe((res) => {
      console.log(res);
      this.eventTypes = res;
      console.log(this.eventTypes);
    })
  }

  public openDialog(
    flag: number,
    calendarId?: Guid,
    calendarName?: string,
    numberOfDaysCalendar?: Number,
    yearCalendar?: Number,
    monthCalendar?: number
  ): void {
    console.log("Flag value:", flag);
    const dialogRef = this.dialog.open(CalendarDialogComponent, {
      data: { calendarId, calendarName, numberOfDaysCalendar, yearCalendar, monthCalendar },
    });
    dialogRef.componentInstance.flag = flag;
    dialogRef.afterClosed().subscribe((result) => {
      if (result == 1) {
        this.startSubscription();
      }
    });
  }
  public openDialogEvent(
    flag: number,
    eventId?: Guid,
    eventName?: string,
    eventDate?: Number,
    eventTime?: Number,
    eventDescription?: string,
    eventType?: EventType,
    calendar?: Calendar,
  ): void {
    console.log("Flag value:", flag);
    const dialogRef = this.dialog.open(CalendarDialogComponent, {
      data: { eventId, eventName, eventDate, eventDescription, eventTime, eventType, calendar },
    });
    dialogRef.componentInstance.flag = flag;
    dialogRef.afterClosed().subscribe((result) => {
      if (result == 1) {
        this.startSubscription();
      }
    });
  }
  public openDialogEventType(
    flag: number,
    eventTypeId?: Guid,
    eventTypeName?: string,
  ): void {
    console.log("Flag value:", flag);
    const dialogRef = this.dialog.open(CalendarDialogComponent, {
      data: { eventTypeId, eventTypeName },
    });
    dialogRef.componentInstance.flag = flag;
    dialogRef.afterClosed().subscribe((result) => {
      if (result == 1) {
        this.startSubscription();
      }
    });
  }
}
