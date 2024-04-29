import { Guid } from 'guid-typescript';
import { Functionality } from './functionality';

export class TaskUpdateDto {
    
    taskId!: Guid;
    textTask!: string;
    functionalityId!: Guid;
    functionality!: Functionality;
    sprintId: Guid;
    
    constructor(
        taskId: Guid,
        textTask: string,
        functionalityId: Guid,
        functionality: Functionality,
        sprintId: Guid
    ) {
        this.taskId = taskId;
        this.textTask = textTask;
        this.functionalityId = functionalityId;
        this.functionality = functionality;
        this.sprintId = sprintId;
    }
    
}