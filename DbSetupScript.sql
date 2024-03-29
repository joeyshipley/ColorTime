USE [Color]
GO
/****** Object:  Schema [TEST]    Script Date: 11/03/2011 21:09:26 ******/
CREATE SCHEMA [TEST] AUTHORIZATION [dbo]
GO
/****** Object:  Table [TEST].[Players]    Script Date: 11/03/2011 21:09:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [TEST].[Players](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](30) NOT NULL,
 CONSTRAINT [PK_Players] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Players]    Script Date: 11/03/2011 21:09:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Players](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](30) NOT NULL,
 CONSTRAINT [PK_Players] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [TEST].[GameRounds]    Script Date: 11/03/2011 21:09:26 ******/
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
/****** Object:  Table [dbo].[GameRounds]    Script Date: 11/03/2011 21:09:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GameRounds](
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
/****** Object:  Table [TEST].[GameRoundsChoices]    Script Date: 11/03/2011 21:09:26 ******/
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
/****** Object:  Table [dbo].[GameRoundsChoices]    Script Date: 11/03/2011 21:09:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GameRoundsChoices](
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
/****** Object:  ForeignKey [FK_GameRounds_Players]    Script Date: 11/03/2011 21:09:26 ******/
ALTER TABLE [dbo].[GameRounds]  WITH CHECK ADD  CONSTRAINT [FK_GameRounds_Players] FOREIGN KEY([PlayerId])
REFERENCES [dbo].[Players] ([Id])
GO
ALTER TABLE [dbo].[GameRounds] CHECK CONSTRAINT [FK_GameRounds_Players]
GO
/****** Object:  ForeignKey [FK_GameRounds_GameRoundsChoices]    Script Date: 11/03/2011 21:09:26 ******/
ALTER TABLE [dbo].[GameRoundsChoices]  WITH CHECK ADD  CONSTRAINT [FK_GameRounds_GameRoundsChoices] FOREIGN KEY([GameRoundId])
REFERENCES [dbo].[GameRounds] ([Id])
GO
ALTER TABLE [dbo].[GameRoundsChoices] CHECK CONSTRAINT [FK_GameRounds_GameRoundsChoices]
GO
/****** Object:  ForeignKey [FK_GameRounds_Players]    Script Date: 11/03/2011 21:09:26 ******/
ALTER TABLE [TEST].[GameRounds]  WITH CHECK ADD  CONSTRAINT [FK_GameRounds_Players] FOREIGN KEY([PlayerId])
REFERENCES [TEST].[Players] ([Id])
GO
ALTER TABLE [TEST].[GameRounds] CHECK CONSTRAINT [FK_GameRounds_Players]
GO
/****** Object:  ForeignKey [FK_GameRounds_GameRoundsChoices]    Script Date: 11/03/2011 21:09:26 ******/
ALTER TABLE [TEST].[GameRoundsChoices]  WITH CHECK ADD  CONSTRAINT [FK_GameRounds_GameRoundsChoices] FOREIGN KEY([GameRoundId])
REFERENCES [TEST].[GameRounds] ([Id])
GO
ALTER TABLE [TEST].[GameRoundsChoices] CHECK CONSTRAINT [FK_GameRounds_GameRoundsChoices]
GO
