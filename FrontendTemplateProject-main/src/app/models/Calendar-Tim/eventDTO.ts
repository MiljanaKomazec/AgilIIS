import { Guid } from 'guid-typescript';

export class eventDTO {
    
    eventId!: Guid;
  eventName!: String;
  eventDate!: Date;
  eventTime!: Date;
  eventDescription!: String;
  eventTypeId!: Guid;
  calendarId!: Guid;
    
    constructor(
        eventId: Guid,
        eventName: String,
        eventDate: Date,
        eventTime: Date,
        eventDescription: String,
        eventTypeId: Guid,
        calendarId: Guid,
    ) {
        this.eventId = eventId;
        this.eventName = eventName;
        this.eventDate = eventDate;
        this.eventTime = eventTime;
        this.eventDescription = eventDescription;
        this.eventTypeId = eventTypeId;
        this.calendarId = calendarId;
    }
    
}