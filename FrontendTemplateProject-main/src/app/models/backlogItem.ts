import { BacklogB } from "./backlog";
import { PhaseOfBacklogItem } from "./phaseOfBacklogItem";
import { SprintS } from "./sprint";
import { Guid } from 'guid-typescript';

export class BacklogItemBI {
    backlogItemId!: Guid;
    timeAddedBacklogItem: string;
    backlog!: BacklogB;
    sprint!: SprintS;
    pobi!: PhaseOfBacklogItem;

}