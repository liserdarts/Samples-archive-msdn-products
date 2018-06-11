USE [TestDB]
GO

/****** Object:  Table [dbo].[CandidateInfo]    Script Date: 05/03/2010 18:44:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CandidateInfo]') AND type in (N'U'))
DROP TABLE [dbo].[CandidateInfo]
GO

USE [TestDB]
GO

/****** Object:  Table [dbo].[CandidateInfo]    Script Date: 05/03/2010 18:44:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CandidateInfo](
	[FirstName] [nvarchar](50) NULL,
	[CandidateId] [int] NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[MiddleName] [nvarchar](50) NULL,
	[PrimaryContactNo] [nvarchar](50) NOT NULL,
	[SecondaryContactNo] [nvarchar](50) NULL,
	[PrimaryEmail] [nvarchar](50) NULL,
	[SecondaryEmail] [nvarchar](50) NULL,
	[DateofInterview] [datetime] NULL,
	[Comments] [nvarchar](max) NULL,
	[InterviewRound] [smallint] NULL,
 CONSTRAINT [PK_CandidateInfo] PRIMARY KEY CLUSTERED 
(
	[CandidateId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


