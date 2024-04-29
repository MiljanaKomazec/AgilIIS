import { Guid } from 'guid-typescript';
import { UserStoryRoot } from './userStory';

export class FunctionalityUpdateDto {
    
    functionalityId!: Guid;
    textFunctionality!: string;
    userStoryRootId!: Guid;
    userStoryRoot!: UserStoryRoot;
    sprintId: Guid;
    
    constructor(
        functionalityId: Guid,
        textFunctionality: string,
        userStoryRootId: Guid,
        userStoryRoot: UserStoryRoot,
        sprintId: Guid
    ) {
        this.functionalityId = functionalityId;
        this.textFunctionality = textFunctionality;
        this.userStoryRootId = userStoryRootId;
        this.userStoryRoot = userStoryRoot;
        this.sprintId = sprintId;
    }
    
}