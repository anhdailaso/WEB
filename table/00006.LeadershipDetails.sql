
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES Where Table_Schema = 'dbo'  AND Table_Name ='LeadershipDetails')
BEGIN
CREATE TABLE [dbo].[LeadershipDetails]
(
[IDLeadership] [int] NOT NULL,
[UserName] [nvarchar] (30)  NOT NULL,
[DateCreate] [datetime] NOT NULL,
[Yes] [bit] NULL,
[No] [bit] NULL,
[NA] [bit] NULL
) ON [PRIMARY]
ALTER TABLE [dbo].[LeadershipDetails] ADD CONSTRAINT [PK_LeadershipDetails] PRIMARY KEY CLUSTERED ([IDLeadership], [UserName], [DateCreate]) ON [PRIMARY]
END
