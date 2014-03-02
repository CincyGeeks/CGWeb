CREATE TABLE [dbo].[ForumReply]
(
	[ReplyId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(), 
    [ParentThreadId] UNIQUEIDENTIFIER NOT NULL, 
    --[ParentReplyId] UNIQUEIDENTIFIER NULL, 
    [ReplyContent] TEXT NOT NULL, 
    [CreatedBy] INT NOT NULL, 
    [CreatedOn] DATETIME NOT NULL DEFAULT GETDATE(), 
    [ModifiedOn] DATETIME NULL, 
    CONSTRAINT [FK_ForumReply_ForumThread] FOREIGN KEY ([ParentThreadId]) REFERENCES [dbo].[ForumThread]([ThreadId]), 
    --CONSTRAINT [FK_ForumReply_ReplyTo] FOREIGN KEY ([ParentReplyId]) REFERENCES [dbo].[ForumReply]([ReplyId]), 
    CONSTRAINT [FK_ForumReply_UserProfile] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[UserProfile]([UserId])
)
