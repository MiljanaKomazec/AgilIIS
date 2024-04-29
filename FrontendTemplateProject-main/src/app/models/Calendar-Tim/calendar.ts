import { Guid } from "guid-typescript";

export class Calendar {
  calendarId!: Guid;
  calendarName!: String;
  numberOfDaysCalendar!: Number;
  yearCalendar!: number;
  monthCalendar!: number;
}
