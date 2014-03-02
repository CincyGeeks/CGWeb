CREATE TABLE [dbo].[UserProfile]
(
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](56) NOT NULL,
	[Email] [nvarchar](256) NOT NULL,
	[Timezone] NVARCHAR(56) NOT NULL,
	[PhoneNumber] [nvarchar](50) NULL,
	[SteamHandle] [nvarchar](128) NULL,
	[ListInDirectory] [bit] NOT NULL DEFAULT 0,
	[Signature] [nvarchar](1000) NULL,
	[AvatarFileName] [nvarchar](64) NULL,
	[BanExpireDate] [datetime] NULL,
	CONSTRAINT [PK_UserProfile] PRIMARY KEY CLUSTERED([UserId])
)
