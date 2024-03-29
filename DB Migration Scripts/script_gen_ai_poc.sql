USE [gen_ai_poc]
GO
ALTER TABLE [dbo].[TransactionSummary] DROP CONSTRAINT [FK_TransactionSummary_UserAccount]
GO
ALTER TABLE [dbo].[SchemeInfo] DROP CONSTRAINT [FK_SchemaInfo_UserAccount]
GO
ALTER TABLE [dbo].[HoldingSummary] DROP CONSTRAINT [FK_HoldingSummary_UserAccount]
GO
ALTER TABLE [dbo].[BankInfo] DROP CONSTRAINT [FK_BankInfo_UserAccount]
GO
/****** Object:  Table [dbo].[UserAuth]    Script Date: 2/19/2024 11:12:45 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserAuth]') AND type in (N'U'))
DROP TABLE [dbo].[UserAuth]
GO
/****** Object:  Table [dbo].[UserAccount]    Script Date: 2/19/2024 11:12:45 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserAccount]') AND type in (N'U'))
DROP TABLE [dbo].[UserAccount]
GO
/****** Object:  Table [dbo].[TransactionSummary]    Script Date: 2/19/2024 11:12:45 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TransactionSummary]') AND type in (N'U'))
DROP TABLE [dbo].[TransactionSummary]
GO
/****** Object:  Table [dbo].[SchemeInfo]    Script Date: 2/19/2024 11:12:45 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchemeInfo]') AND type in (N'U'))
DROP TABLE [dbo].[SchemeInfo]
GO
/****** Object:  Table [dbo].[HoldingSummary]    Script Date: 2/19/2024 11:12:45 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HoldingSummary]') AND type in (N'U'))
DROP TABLE [dbo].[HoldingSummary]
GO
/****** Object:  Table [dbo].[BankInfo]    Script Date: 2/19/2024 11:12:45 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BankInfo]') AND type in (N'U'))
DROP TABLE [dbo].[BankInfo]
GO
/****** Object:  Table [dbo].[BankInfo]    Script Date: 2/19/2024 11:12:45 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HoldingSummary]    Script Date: 2/19/2024 11:12:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoldingSummary](
	[HoldingSummaryId] [int] IDENTITY(1,1) NOT NULL,
	[UniqueId] [int] NOT NULL,
	[HoldingSchemeName] [nvarchar](50) NULL,
	[TotalUnits] [decimal](16, 2) NULL,
	[Nav] [decimal](16, 2) NULL,
	[Amount] [decimal](16, 2) NULL,
	[TransactionDate] [date] NULL,
	[CreatedDate] [date] NULL,
	[ExitDate] [date] NULL,
 CONSTRAINT [PK_HoldingSummary_UniqueId] PRIMARY KEY CLUSTERED 
(
	[HoldingSummaryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SchemeInfo]    Script Date: 2/19/2024 11:12:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SchemeInfo](
	[schemeId] [int] NOT NULL,
	[schemeName] [nchar](10) NOT NULL,
	[fundManagerName] [nchar](50) NULL,
	[percantageContribution] [int] NOT NULL,
	[uniqueId] [int] NOT NULL,
	[status] [nchar](10) NULL,
	[CreatedDate] [date] NULL,
	[ExitDate] [date] NULL,
	[IsPreferred] [bit] NULL,
 CONSTRAINT [PK_SchemaInfo] PRIMARY KEY CLUSTERED 
(
	[schemeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransactionSummary]    Script Date: 2/19/2024 11:12:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransactionSummary](
	[TransactionSummaryId] [int] IDENTITY(1,1) NOT NULL,
	[UniqueId] [int] NOT NULL,
	[TransactionDate] [date] NULL,
	[Description] [nvarchar](255) NULL,
	[Amount] [decimal](16, 2) NULL,
	[TransactionType] [char](2) NULL,
 CONSTRAINT [PK_TransactionSummary_UniqueId] PRIMARY KEY CLUSTERED 
(
	[TransactionSummaryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserAccount]    Script Date: 2/19/2024 11:12:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAccount](
	[uniqueId] [int] NOT NULL,
	[fullName] [nchar](100) NOT NULL,
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
/****** Object:  Table [dbo].[UserAuth]    Script Date: 2/19/2024 11:12:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAuth](
	[UserName] [nchar](10) NULL,
	[Password] [nchar](10) NULL
) ON [PRIMARY]
GO
INSERT [dbo].[BankInfo] ([accountNumber], [bankBranch], [bankName], [address], [ifscCode], [uniqueId]) VALUES (1234, N'branch1   ', N'hdfc      ', N'xyz lane, Bangalore                                                                                                                                                                                     ', 123456, 1)
GO
SET IDENTITY_INSERT [dbo].[HoldingSummary] ON 
GO
INSERT [dbo].[HoldingSummary] ([HoldingSummaryId], [UniqueId], [HoldingSchemeName], [TotalUnits], [Nav], [Amount], [TransactionDate], [CreatedDate], [ExitDate]) VALUES (1, 1, N'schema1', CAST(28.00 AS Decimal(16, 2)), CAST(25.00 AS Decimal(16, 2)), CAST(280.00 AS Decimal(16, 2)), CAST(N'2024-01-11' AS Date), CAST(N'2024-01-11' AS Date), CAST(N'2024-01-15' AS Date))
GO
INSERT [dbo].[HoldingSummary] ([HoldingSummaryId], [UniqueId], [HoldingSchemeName], [TotalUnits], [Nav], [Amount], [TransactionDate], [CreatedDate], [ExitDate]) VALUES (2, 1, N'schema2', CAST(8.00 AS Decimal(16, 2)), CAST(25.00 AS Decimal(16, 2)), CAST(80.00 AS Decimal(16, 2)), CAST(N'2024-01-11' AS Date), CAST(N'2024-01-11' AS Date), NULL)
GO
SET IDENTITY_INSERT [dbo].[HoldingSummary] OFF
GO
INSERT [dbo].[SchemeInfo] ([schemeId], [schemeName], [fundManagerName], [percantageContribution], [uniqueId], [status], [CreatedDate], [ExitDate], [IsPreferred]) VALUES (1, N'schema1   ', N'Mr. xyz                                           ', 70, 1, N'Active    ', NULL, NULL, 1)
GO
INSERT [dbo].[SchemeInfo] ([schemeId], [schemeName], [fundManagerName], [percantageContribution], [uniqueId], [status], [CreatedDate], [ExitDate], [IsPreferred]) VALUES (2, N'schema2   ', N'Mr. xyz                                           ', 90, 1, N'Active    ', NULL, NULL, 0)
GO
SET IDENTITY_INSERT [dbo].[TransactionSummary] ON 
GO
INSERT [dbo].[TransactionSummary] ([TransactionSummaryId], [UniqueId], [TransactionDate], [Description], [Amount], [TransactionType]) VALUES (1, 1, CAST(N'2024-01-11' AS Date), N'Withdrew 0%', CAST(0.00 AS Decimal(16, 2)), N'W ')
GO
INSERT [dbo].[TransactionSummary] ([TransactionSummaryId], [UniqueId], [TransactionDate], [Description], [Amount], [TransactionType]) VALUES (2, 1, CAST(N'2024-01-11' AS Date), N'Withdrew 10%', CAST(1.00 AS Decimal(16, 2)), N'W ')
GO
INSERT [dbo].[TransactionSummary] ([TransactionSummaryId], [UniqueId], [TransactionDate], [Description], [Amount], [TransactionType]) VALUES (3, 1, CAST(N'2024-01-15' AS Date), N'Contrubuted units: 10', CAST(100.00 AS Decimal(16, 2)), N'C ')
GO
INSERT [dbo].[TransactionSummary] ([TransactionSummaryId], [UniqueId], [TransactionDate], [Description], [Amount], [TransactionType]) VALUES (4, 1, CAST(N'2024-01-15' AS Date), N'Contrubuted units: 10', CAST(100.00 AS Decimal(16, 2)), N'C ')
GO
INSERT [dbo].[TransactionSummary] ([TransactionSummaryId], [UniqueId], [TransactionDate], [Description], [Amount], [TransactionType]) VALUES (5, 1, CAST(N'2024-01-15' AS Date), N'Withdrew 0%', CAST(0.00 AS Decimal(16, 2)), N'W ')
GO
INSERT [dbo].[TransactionSummary] ([TransactionSummaryId], [UniqueId], [TransactionDate], [Description], [Amount], [TransactionType]) VALUES (6, 1, CAST(N'2024-01-15' AS Date), N'Withdrew 0%', CAST(0.00 AS Decimal(16, 2)), N'W ')
GO
INSERT [dbo].[TransactionSummary] ([TransactionSummaryId], [UniqueId], [TransactionDate], [Description], [Amount], [TransactionType]) VALUES (7, 1, CAST(N'2024-01-15' AS Date), N'Withdrew 10%', CAST(2.90 AS Decimal(16, 2)), N'W ')
GO
INSERT [dbo].[TransactionSummary] ([TransactionSummaryId], [UniqueId], [TransactionDate], [Description], [Amount], [TransactionType]) VALUES (8, 1, CAST(N'2024-01-16' AS Date), N'Contrubuted units: 2', CAST(20.00 AS Decimal(16, 2)), N'C ')
GO
INSERT [dbo].[TransactionSummary] ([TransactionSummaryId], [UniqueId], [TransactionDate], [Description], [Amount], [TransactionType]) VALUES (9, 1, CAST(N'2024-01-16' AS Date), N'Withdrew 12%', CAST(1.20 AS Decimal(16, 2)), N'W ')
GO
SET IDENTITY_INSERT [dbo].[TransactionSummary] OFF
GO
INSERT [dbo].[UserAccount] ([uniqueId], [fullName], [fathersName], [mothersName], [dateOfBirth], [gender], [nationality], [isKycDone], [address1], [address2], [city], [pinCode], [mobile]) VALUES (1, N'Aritro                                                                                              ', N'Ashis                                                                                               ', N'Ranu                                                                                                ', CAST(N'2023-12-31T15:03:08.403' AS DateTime), N'Male      ', N'Indina                                            ', 1, N'Sai Kunj                                                                                                                                                                                                ', N'Siddhinath Chatterjee Rd.                                                                                                                                                                               ', N'Kolkata   ', 700034, N'98765          ')
GO
INSERT [dbo].[UserAuth] ([UserName], [Password]) VALUES (N'admin     ', N'abcd      ')
GO
ALTER TABLE [dbo].[BankInfo]  WITH CHECK ADD  CONSTRAINT [FK_BankInfo_UserAccount] FOREIGN KEY([uniqueId])
REFERENCES [dbo].[UserAccount] ([uniqueId])
GO
ALTER TABLE [dbo].[BankInfo] CHECK CONSTRAINT [FK_BankInfo_UserAccount]
GO
ALTER TABLE [dbo].[HoldingSummary]  WITH CHECK ADD  CONSTRAINT [FK_HoldingSummary_UserAccount] FOREIGN KEY([UniqueId])
REFERENCES [dbo].[UserAccount] ([uniqueId])
GO
ALTER TABLE [dbo].[HoldingSummary] CHECK CONSTRAINT [FK_HoldingSummary_UserAccount]
GO
ALTER TABLE [dbo].[SchemeInfo]  WITH CHECK ADD  CONSTRAINT [FK_SchemaInfo_UserAccount] FOREIGN KEY([uniqueId])
REFERENCES [dbo].[UserAccount] ([uniqueId])
GO
ALTER TABLE [dbo].[SchemeInfo] CHECK CONSTRAINT [FK_SchemaInfo_UserAccount]
GO
ALTER TABLE [dbo].[TransactionSummary]  WITH CHECK ADD  CONSTRAINT [FK_TransactionSummary_UserAccount] FOREIGN KEY([UniqueId])
REFERENCES [dbo].[UserAccount] ([uniqueId])
GO
ALTER TABLE [dbo].[TransactionSummary] CHECK CONSTRAINT [FK_TransactionSummary_UserAccount]
GO
