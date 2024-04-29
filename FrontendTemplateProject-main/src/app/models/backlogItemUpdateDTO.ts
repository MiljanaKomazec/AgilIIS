import { Guid } from "guid-typescript";
import { BacklogB } from "./backlog";
import { SprintS } from "./sprint";
import { PhaseOfBacklogItem } from "./phaseOfBacklogItem";

export class BacklogItemUpdateDTO{
    backlogItemId!: Guid;
    timeAddedBacklogItem: string;
    backlogId!: Guid;
    backlog!: BacklogB;
    sprintId!: Guid;
    sprint!: SprintS;
    pobiId!: Guid;
    pobi!: PhaseOfBacklogItem;


    constructor(
        backlogItemId: Guid,
        timeAddedBacklogItem: string,
        backlogId: Guid,
        backlog: BacklogB,
        sprintId: Guid,
        sprint: SprintS,
        pobiId: Guid,
        pobi: PhaseOfBacklogItem

    ) {
        this.backlogItemId = backlogItemId;
        this.timeAddedBacklogItem = timeAddedBacklogItem;
        this.backlogId = backlogId;
        this.backlog = backlog;
        this.sprintId = sprintId;
        this.sprint = sprint;
        this.pobiId = pobiId;
        this.pobi = pobi;
    }

}