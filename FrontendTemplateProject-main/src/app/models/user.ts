import { Guid } from "guid-typescript";

export class User {
    idUser!: Guid;
    nameUser!: string;
    surnameUser!: string;
    emailUser!: string;
    teamId!: Guid;
    nameRole!: string;
}