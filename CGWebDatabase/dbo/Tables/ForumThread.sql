CREATE TABLE [dbo].[ForumThread]
(
	[ThreadId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(), 
    [ForumTopic] UNIQUEIDENTIFIER NOT NULL,
	[ThreadTitle] NVARCHAR(128) NOT NULL, 
	[ThreadContent] TEXT NOT NULL,
    [CreatedBy] INT NOT NULL, 
    [CreatedOn] DATETIME NOT NULL DEFAULT GETDATE(), 
	[ModifiedOn] DATETIME NULL,
    [IsSticky] BIT NOT NULL DEFAULT 0, 
    [IsDeleted] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_ForumThread_Forum] FOREIGN KEY ([ForumTopic]) REFERENCES [dbo].[ForumTopic]([TopicId]), 
    CONSTRAINT [FK_ForumThread_UserProfile] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[UserProfile]([UserId])   
)
