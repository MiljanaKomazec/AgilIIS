import { Guid } from 'guid-typescript';

export class TaskCreationDto {
    
    textTask!: string;
    functionalityId!: Guid;

    constructor(
        textTask: string,
        functionalityId: Guid
    ) {
        this.textTask = textTask;
        this.functionalityId = functionalityId;
    }
    
}