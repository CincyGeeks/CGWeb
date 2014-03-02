CREATE TABLE [dbo].[Announcement]
(
	[AnnouncementId] INT NOT NULL PRIMARY KEY NONCLUSTERED IDENTITY,
	[CreatedDate] DATETIME NOT NULL,
	[CreatedBy] INT NOT NULL,
	[ModifiedDate] DATETIME NULL,
	[ModifiedBy] INT NULL,
	[Title] NVARCHAR(128) NOT NULL,
	[Content] TEXT NOT NULL,
	[IsPublic] BIT NOT NULL,
	[RestrictToRole] INT NULL
)
GO

CREATE CLUSTERED INDEX [AnnouncementCreatedDate_CI] ON [Announcement]([CreatedDate])
GO
