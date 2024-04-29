import { Guid } from 'guid-typescript';
export class CommentC {
    commentId!: Guid;
    dateComment: Date;
    textComment: string;
    userStoryRootId!: Guid;

}