import { Guid } from "guid-typescript";

export class Team {
  teamId!: Guid;
  teamName!: String;
  teamDescription!: String;
  userId!: Guid;
  calendarId!: Guid;
}
