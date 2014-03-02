CREATE TABLE [dbo].[Service_Roles]
(
	[ServiceRoleId] [uniqueidentifier] NOT NULL DEFAULT NEWID(),
	[ServiceId] [uniqueidentifier] NOT NULL,
	[RoleId] [int] NOT NULL,
	CONSTRAINT [PK_Service_Roles] PRIMARY KEY CLUSTERED ([ServiceRoleId]),
	CONSTRAINT [FK_Service_Roles_webpages_Roles] FOREIGN KEY([RoleId]) REFERENCES [dbo].[webpages_Roles] ([RoleId]),
	CONSTRAINT [FK_Service_Roles_Services] FOREIGN KEY([ServiceId]) REFERENCES [dbo].[Service] ([ServiceId])
)
