import { Guid } from "guid-typescript";
import { EventType } from "./eventType";
import { Calendar } from "./calendar";

export class eventUpdateDTO{
    eventId!: Guid;
  eventName!: String;
  eventDate!: Date;
  eventTime!: Date;
  eventDescription!: String;
  eventTypeId!: Guid;
  eventType!: EventType;
  calendarId!: Guid;
  calendar!: Calendar;


    constructor(
        eventId: Guid,
  eventName: String,
  eventDate: Date,
  eventTime: Date,
  eventDescription: String,
  eventTypeId: Guid,
  eventType: EventType,
  calendarId: Guid,
  calendar: Calendar

    ) {
        this.eventId = eventId;
        this.eventName = eventName;
        this.eventDate = eventDate;
        this.eventTime = eventTime;
        this.eventDescription = eventDescription;
        this.eventTypeId = eventTypeId;
        this.eventType = eventType;
        this.calendarId = calendarId;
        this.calendar = calendar;
    }

}