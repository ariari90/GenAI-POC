USE [gen_ai_poc]
GO

/****** Object:  Table [dbo].[UserAccount]    Script Date: 12/31/2023 2:27:10 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserAccount](
	[uniqueId] [int] NOT NULL,
	[name] [nchar](100) NOT NULL,
	[fathersName] [nchar](100) NULL,
	[mothersName] [nchar](100) NULL,
	[dateOfBirth] [datetime] NOT NULL,
	[gender] [nchar](10) NOT NULL,
	[nationality] [nchar](50) NOT NULL,
	[isKycDone] [bit] NOT NULL,
	[address1] [nchar](200) NULL,
	[address2] [nchar](200) NULL,
	[city] [nchar](10) NULL,
	[pinCode] [int] NULL,
	[mobile] [nchar](15) NULL,
 CONSTRAINT [PK_UserAccount] PRIMARY KEY CLUSTERED 
(
	[uniqueId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** Object:  Table [dbo].[BankInfo]    Script Date: 12/31/2023 2:27:34 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[BankInfo](
	[accountNumber] [int] NOT NULL,
	[bankBranch] [nchar](10) NOT NULL,
	[bankName] [nchar](10) NOT NULL,
	[address] [nchar](200) NOT NULL,
	[ifscCode] [int] NOT NULL,
	[uniqueId] [int] NOT NULL,
 CONSTRAINT [PK_BankInfo] PRIMARY KEY CLUSTERED 
(
	[accountNumber] ASC
)WITH (PAD_INDEX = OFF,

 STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[BankInfo]  WITH CHECK ADD  CONSTRAINT [FK_BankInfo_UserAccount] FOREIGN KEY([uniqueId])
REFERENCES [dbo].[UserAccount] ([uniqueId])
GO

ALTER TABLE [dbo].[BankInfo] CHECK CONSTRAINT [FK_BankInfo_UserAccount]
GO


/****** Object:  Table [dbo].[SchemaInfo]    Script Date: 12/31/2023 2:39:55 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SchemaInfo](
	[schemaId] [int] NOT NULL,
	[schemaName] [nchar](10) NOT NULL,
	[fundManagerName] [nchar](50) NULL,
	[percantageContribution] [int] NOT NULL,
	[uniqueId] [int] NOT NULL,
 CONSTRAINT [PK_SchemaInfo] PRIMARY KEY CLUSTERED 
(
	[schemeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[SchemaInfo]  WITH CHECK ADD  CONSTRAINT [FK_SchemaInfo_UserAccount] FOREIGN KEY([uniqueId])
REFERENCES [dbo].[UserAccount] ([uniqueId])
GO

ALTER TABLE [dbo].[SchemaInfo] CHECK CONSTRAINT [FK_SchemaInfo_UserAccount]
GO



insert into UserAccount values(1, 'Aritro', 'Ashis', 'Ranu', GetDate(), 'Male', 'Indina', 1, 'Sai Kunj', 'Behala Tramdipo', 'Kolkata', 700034, '98312 59144')

insert into bankInfo values(1234, 'branch1', 'hdfc', 'xyz lane, Bangalore', 123456, 1)

insert into SchemaInfo values(1, 'schema1', 'Ashutosh', 70, 1)

insert into SchemaInfo values(2, 'schema2', 'Vijay', 90, 1)
