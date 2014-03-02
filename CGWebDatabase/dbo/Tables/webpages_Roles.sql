CREATE TABLE [dbo].[webpages_Roles]
(
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](256) NOT NULL UNIQUE,
	CONSTRAINT [PK_webpages_Roles] PRIMARY KEY CLUSTERED ([RoleId])
)
