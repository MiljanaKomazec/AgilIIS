import { Guid } from 'guid-typescript';

export class BacklogItemDTO {
    
    backlogItemId!: Guid;
    timeAddedBacklogItem: string;
    backlogId!: Guid;
    sprintId!: Guid;
    pobiId!: Guid;
    
    constructor(
        backlogItemId: Guid,
        timeAddedBacklogItem: string,
        backlogId: Guid,
        sprintId: Guid,
        pobiId: Guid
    ) {
        this.backlogItemId = backlogItemId;
        this.timeAddedBacklogItem = timeAddedBacklogItem;
        this.backlogId = backlogId;
        this.sprintId = sprintId;
        this.pobiId = pobiId;
    }
    
}