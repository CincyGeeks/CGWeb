CREATE TABLE [dbo].[Service]
(
	[ServiceId] [uniqueidentifier] NOT NULL DEFAULT (NEWID()),
	[ServiceName] [nvarchar](50) NOT NULL,
	CONSTRAINT [PK_Service] PRIMARY KEY CLUSTERED ([ServiceId])
)
