import { Guid } from "guid-typescript";
import { EventType } from "./eventType";
import { Calendar } from "./calendar";

export class Event {
  eventId!: Guid;
  eventName!: String;
  eventDate!: Date;
  eventTime!: Date;
  eventDescription!: String;
  eventType!: EventType;
  calendar!: Calendar;
}
