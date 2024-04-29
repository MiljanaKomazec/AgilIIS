import { Guid } from 'guid-typescript';
import { PrioritetizationParameter } from './prioritetizationParameter';
import { BacklogB } from '../backlog';

export class UserStoryRoot {
    
    userStoryRootId!: Guid;
    textUserStory!: string;
    partOfEpic!: string;
    prioritetizationParameter!: PrioritetizationParameter;
    backlogId!: Guid;
    sprintId: Guid;
}