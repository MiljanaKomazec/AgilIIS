import { Guid } from 'guid-typescript';

export class FunctionalityDto {
    
    functionalityId!: Guid;
    textFunctionality!: string;
    userStoryRootId!: Guid;
    
    constructor(
        functionalityId: Guid,
        textFunctionality: string,
        userStoryRootId: Guid
    ) {
        this.functionalityId = functionalityId;
        this.textFunctionality = textFunctionality;
        this.userStoryRootId = userStoryRootId;
    }
    
}