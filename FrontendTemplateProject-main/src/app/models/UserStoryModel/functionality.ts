import { Guid } from 'guid-typescript';
import { UserStoryRoot } from './userStory';

export class Functionality {
    functionalityId!: Guid;
    textFunctionality!: string;
    userStoryRoot: UserStoryRoot;
    sprintId: Guid;
}