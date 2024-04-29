import { Guid } from 'guid-typescript';
import { Functionality } from './functionality';

export class Task {
    taskId!: Guid;
    textTask!: string;
    functionality!: Functionality;
    sprintId: Guid;
}