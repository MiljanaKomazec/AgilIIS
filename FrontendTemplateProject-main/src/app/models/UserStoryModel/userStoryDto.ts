import { Guid } from 'guid-typescript';

export class UserStoryRootDto {
    
    userStoryRootId!: Guid;
    textUserStory!: string;
    partOfEpic!: string;
    prioritetizationParameterId!: Guid;
    
    constructor(
        userStoryRootId: Guid,
        textUserStory: string,
        partOfEpic: string,
        prioritetizationParameterId: Guid
    ) {
        this.userStoryRootId = userStoryRootId;
        this.textUserStory = textUserStory;
        this.partOfEpic = partOfEpic;
        this.prioritetizationParameterId = prioritetizationParameterId;
    }
    
}