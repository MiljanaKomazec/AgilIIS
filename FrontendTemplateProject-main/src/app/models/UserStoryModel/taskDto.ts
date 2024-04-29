import { Guid } from 'guid-typescript';

export class TaskDto {
    
    taskId!: Guid;
    textTask!: string;
    functionalityId!: Guid;
    
    constructor(
        taskId: Guid,
        textTask: string,
        functionalityId: Guid
    ) {
        this.taskId = taskId;
        this.textTask = textTask;
        this.functionalityId = functionalityId;
    }
    
}