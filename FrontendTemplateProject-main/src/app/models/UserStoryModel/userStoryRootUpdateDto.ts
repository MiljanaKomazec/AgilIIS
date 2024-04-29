import { Guid } from 'guid-typescript';
import { PrioritetizationParameter } from './prioritetizationParameter';

export class UserStoryRootUpdateDto {
    
    userStoryRootId!: Guid;
    textUserStory!: string;
    partOfEpic!: string;
    prioritetizationParameterId!: Guid;
    prioritetizationParameter!: PrioritetizationParameter;
    backlogId!: Guid;
    sprintId: Guid;
    
    constructor(
        userStoryRootId: Guid,
        textUserStory: string,
        partOfEpic: string,
        prioritetizationParameterId: Guid,
        prioritetizationParameter: PrioritetizationParameter,
        backlogId: Guid,
        sprintId: Guid
    ) {
        this.userStoryRootId = userStoryRootId;
        this.textUserStory = textUserStory;
        this.partOfEpic = partOfEpic;
        this.prioritetizationParameterId = prioritetizationParameterId;
        this.prioritetizationParameter = prioritetizationParameter;
        this.backlogId = backlogId;
        this.sprintId = sprintId;
    }
    
}