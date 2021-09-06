
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES Where Table_Schema = 'dbo'  AND Table_Name ='Leadership')
BEGIN
CREATE TABLE [dbo].[Leadership]
(
[ID] [int] NOT NULL IDENTITY(1, 1),
[Content] [nvarchar] (max)  NULL,
[UserCreate] [nvarchar] (30)  NULL,
[DateCreate] [datetime] NULL,
[UserEdits] [nvarchar] (30)  NULL,
[DateEdits] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
ALTER TABLE [dbo].[Leadership] ADD CONSTRAINT [PK_Leadership] PRIMARY KEY CLUSTERED ([ID]) ON [PRIMARY]
END
