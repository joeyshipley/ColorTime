USE [Color]
GO
/****** Object:  Schema [TEST]    Script Date: 11/03/2011 15:56:50 ******/
CREATE SCHEMA [TEST] AUTHORIZATION [dbo]
GO
/****** Object:  Table [TEST].[GameRounds]    Script Date: 11/03/2011 15:56:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [TEST].[GameRounds](
	[Id] [uniqueidentifier] NOT NULL,
	[PlayerId] [uniqueidentifier] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Answer] [varchar](20) NOT NULL,
	[FailedAttempts] [int] NOT NULL,
	[Score] [int] NULL,
 CONSTRAINT [PK_GameRounds] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [TEST].[Players]    Script Date: 11/03/2011 15:56:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [TEST].[Players](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](30) NOT NULL,
	[Score] [int] NULL,
 CONSTRAINT [PK_Players] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [TEST].[GameRoundsChoices]    Script Date: 11/03/2011 15:56:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [TEST].[GameRoundsChoices](
	[Id] [uniqueidentifier] NOT NULL,
	[GameRoundId] [uniqueidentifier] NOT NULL,
	[Choice] [varchar](20) NOT NULL,
 CONSTRAINT [PK_GameRoundsChoices] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  ForeignKey [FK_GameRounds_GameRoundsChoices]    Script Date: 11/03/2011 15:56:49 ******/
ALTER TABLE [TEST].[GameRoundsChoices]  WITH CHECK ADD  CONSTRAINT [FK_GameRounds_GameRoundsChoices] FOREIGN KEY([GameRoundId])
REFERENCES [TEST].[GameRounds] ([Id])
GO
ALTER TABLE [TEST].[GameRoundsChoices] CHECK CONSTRAINT [FK_GameRounds_GameRoundsChoices]
GO
