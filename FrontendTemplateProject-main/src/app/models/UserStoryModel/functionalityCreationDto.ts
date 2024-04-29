import { Guid } from 'guid-typescript';

export class FunctionalityCreationDto {
    
    textFunctionality!: string;
    userStoryRootId!: Guid;

    constructor(
        textFunctionality: string,
        userStoryRootId: Guid
    ) {
        this.textFunctionality = textFunctionality;
        this.userStoryRootId = userStoryRootId;
    }
    
}