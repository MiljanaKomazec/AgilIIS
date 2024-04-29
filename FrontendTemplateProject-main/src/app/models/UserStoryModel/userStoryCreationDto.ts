import { Guid } from 'guid-typescript';

export class UserStoryCreatinDto {
    
    textUserStory!: string;
    partOfEpic!: string;
    prioritetizationParameterId!: Guid;
    backlogId!: Guid;

    constructor(
        textUserStory: string,
        partOfEpic: string,
        prioritetizationParameterId: Guid,
        backlogId: Guid
    ) {
        this.textUserStory = textUserStory;
        this.partOfEpic = partOfEpic;
        this.prioritetizationParameterId = prioritetizationParameterId;
        this.backlogId = backlogId;
    }
    
}