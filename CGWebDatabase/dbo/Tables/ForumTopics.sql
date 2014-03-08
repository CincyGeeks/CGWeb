CREATE TABLE [dbo].[ForumTopic]
(
	[TopicId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
	[ForumId] UNIQUEIDENTIFIER NOT NULL, 
    [TopicTitle] NVARCHAR(128) NOT NULL, 
    [TopicDescription] TEXT NOT NULL, 
    [IsPublic] BIT NOT NULL DEFAULT 0, 
    [CreatedBy] INT NOT NULL, 
    [CreatedOn] DATETIME NOT NULL DEFAULT GETDATE(), 
    [IsDeleted] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_ForumTopic_Forum] FOREIGN KEY ([ForumId]) REFERENCES [dbo].[Forum]([ForumId]), 
    CONSTRAINT [FK_ForumTopic_UserProfile] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[UserProfile]([UserId])
)
