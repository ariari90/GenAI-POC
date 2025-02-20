USE [gen_ai_poc]
GO
ALTER TABLE [dbo].[SchemeInfo] DROP CONSTRAINT [FK_SchemaInfo_UserAccount]
GO
ALTER TABLE [dbo].[LoanPayments] DROP CONSTRAINT [FK__LoanPayme__Uniqu__7B5B524B]
GO
ALTER TABLE [dbo].[LoanPayments] DROP CONSTRAINT [FK__LoanPayme__LoanI__7A672E12]
GO
ALTER TABLE [dbo].[BankInfo] DROP CONSTRAINT [FK_BankInfo_UserAccount]
GO
/****** Object:  Table [dbo].[UserAuth]    Script Date: 2/20/2025 8:03:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserAuth]') AND type in (N'U'))
DROP TABLE [dbo].[UserAuth]
GO
/****** Object:  Table [dbo].[UserAccount]    Script Date: 2/20/2025 8:03:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserAccount]') AND type in (N'U'))
DROP TABLE [dbo].[UserAccount]
GO
/****** Object:  Table [dbo].[TransactionSummary]    Script Date: 2/20/2025 8:03:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TransactionSummary]') AND type in (N'U'))
DROP TABLE [dbo].[TransactionSummary]
GO
/****** Object:  Table [dbo].[TaxableEvents]    Script Date: 2/20/2025 8:03:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TaxableEvents]') AND type in (N'U'))
DROP TABLE [dbo].[TaxableEvents]
GO
/****** Object:  Table [dbo].[SchemeInfo]    Script Date: 2/20/2025 8:03:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchemeInfo]') AND type in (N'U'))
DROP TABLE [dbo].[SchemeInfo]
GO
/****** Object:  Table [dbo].[LoanPayments]    Script Date: 2/20/2025 8:03:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LoanPayments]') AND type in (N'U'))
DROP TABLE [dbo].[LoanPayments]
GO
/****** Object:  Table [dbo].[LoanApplications]    Script Date: 2/20/2025 8:03:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LoanApplications]') AND type in (N'U'))
DROP TABLE [dbo].[LoanApplications]
GO
/****** Object:  Table [dbo].[HoldingSummary]    Script Date: 2/20/2025 8:03:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HoldingSummary]') AND type in (N'U'))
DROP TABLE [dbo].[HoldingSummary]
GO
/****** Object:  Table [dbo].[CreditCardApplications]    Script Date: 2/20/2025 8:03:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CreditCardApplications]') AND type in (N'U'))
DROP TABLE [dbo].[CreditCardApplications]
GO
/****** Object:  Table [dbo].[BankInfo]    Script Date: 2/20/2025 8:03:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BankInfo]') AND type in (N'U'))
DROP TABLE [dbo].[BankInfo]
GO
/****** Object:  Table [dbo].[AutomaticSavingsPlan]    Script Date: 2/20/2025 8:03:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AutomaticSavingsPlan]') AND type in (N'U'))
DROP TABLE [dbo].[AutomaticSavingsPlan]
GO
USE [master]
GO
/****** Object:  Database [gen_ai_poc]    Script Date: 2/20/2025 8:03:32 PM ******/
DROP DATABASE [gen_ai_poc]
GO
/****** Object:  Database [gen_ai_poc]    Script Date: 2/20/2025 8:03:32 PM ******/
CREATE DATABASE [gen_ai_poc]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'gen_ai_poc1', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\gen_ai_poc1.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'gen_ai_poc1_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\gen_ai_poc1_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [gen_ai_poc] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [gen_ai_poc].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [gen_ai_poc] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [gen_ai_poc] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [gen_ai_poc] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [gen_ai_poc] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [gen_ai_poc] SET ARITHABORT OFF 
GO
ALTER DATABASE [gen_ai_poc] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [gen_ai_poc] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [gen_ai_poc] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [gen_ai_poc] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [gen_ai_poc] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [gen_ai_poc] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [gen_ai_poc] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [gen_ai_poc] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [gen_ai_poc] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [gen_ai_poc] SET  DISABLE_BROKER 
GO
ALTER DATABASE [gen_ai_poc] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [gen_ai_poc] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [gen_ai_poc] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [gen_ai_poc] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [gen_ai_poc] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [gen_ai_poc] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [gen_ai_poc] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [gen_ai_poc] SET RECOVERY FULL 
GO
ALTER DATABASE [gen_ai_poc] SET  MULTI_USER 
GO
ALTER DATABASE [gen_ai_poc] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [gen_ai_poc] SET DB_CHAINING OFF 
GO
ALTER DATABASE [gen_ai_poc] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [gen_ai_poc] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [gen_ai_poc] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [gen_ai_poc] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'gen_ai_poc', N'ON'
GO
ALTER DATABASE [gen_ai_poc] SET QUERY_STORE = ON
GO
ALTER DATABASE [gen_ai_poc] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [gen_ai_poc]
GO
/****** Object:  Table [dbo].[AutomaticSavingsPlan]    Script Date: 2/20/2025 8:03:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AutomaticSavingsPlan](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UniqueId] [int] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[SavingsAccountNumber] [nvarchar](50) NOT NULL,
	[IntervalDays] [int] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[Status] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BankInfo]    Script Date: 2/20/2025 8:03:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BankInfo](
	[accountNumber] [int] IDENTITY(1,1) NOT NULL,
	[bankBranch] [nchar](50) NOT NULL,
	[bankName] [nchar](50) NOT NULL,
	[address] [nchar](200) NOT NULL,
	[ifscCode] [int] NOT NULL,
	[uniqueId] [int] NOT NULL,
 CONSTRAINT [PK_BankInfo] PRIMARY KEY CLUSTERED 
(
	[accountNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CreditCardApplications]    Script Date: 2/20/2025 8:03:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CreditCardApplications](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UniqueId] [int] NOT NULL,
	[CardType] [nvarchar](50) NOT NULL,
	[CreditLimit] [decimal](18, 2) NOT NULL,
	[ApplicationDate] [datetime] NOT NULL,
	[Status] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HoldingSummary]    Script Date: 2/20/2025 8:03:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoldingSummary](
	[HoldingSummaryId] [int] IDENTITY(1,1) NOT NULL,
	[UniqueId] [int] NOT NULL,
	[UserId] [int] NULL,
	[HoldingSchemeName] [nvarchar](max) NULL,
	[OperationType] [nvarchar](50) NULL,
	[TotalUnits] [decimal](16, 2) NULL,
	[Nav] [decimal](16, 2) NULL,
	[Amount] [decimal](16, 2) NULL,
	[TransactionDate] [date] NULL,
	[CreatedDate] [date] NULL,
	[ExitDate] [date] NULL,
 CONSTRAINT [PK__HoldingS__4FB225A844908AE1] PRIMARY KEY CLUSTERED 
(
	[HoldingSummaryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoanApplications]    Script Date: 2/20/2025 8:03:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoanApplications](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UniqueId] [int] NOT NULL,
	[LoanAmount] [decimal](18, 2) NOT NULL,
	[LoanType] [nvarchar](50) NOT NULL,
	[ApplicationDate] [datetime] NOT NULL,
	[Status] [nvarchar](20) NOT NULL,
	[InterestRate] [decimal](5, 2) NOT NULL,
	[LoanTermMonths] [int] NOT NULL,
	[TotalRepaymentAmount] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK__LoanAppl__3214EC070C8B65AE] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoanPayments]    Script Date: 2/20/2025 8:03:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoanPayments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LoanId] [int] NOT NULL,
	[UniqueId] [int] NOT NULL,
	[PaymentAmount] [decimal](18, 2) NOT NULL,
	[PaymentDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SchemeInfo]    Script Date: 2/20/2025 8:03:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SchemeInfo](
	[schemeId] [int] IDENTITY(1,1) NOT NULL,
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
/****** Object:  Table [dbo].[TaxableEvents]    Script Date: 2/20/2025 8:03:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaxableEvents](
	[EventId] [int] IDENTITY(1,1) NOT NULL,
	[UniqueId] [int] NOT NULL,
	[EventDate] [datetime] NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[TaxType] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransactionSummary]    Script Date: 2/20/2025 8:03:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransactionSummary](
	[TransactionSummaryId] [int] IDENTITY(1,1) NOT NULL,
	[UniqueId] [nvarchar](max) NOT NULL,
	[UserId] [int] NULL,
	[TransactionDate] [date] NULL,
	[Description] [nvarchar](max) NULL,
	[Amount] [decimal](16, 2) NULL,
	[TransactionType] [char](2) NULL,
PRIMARY KEY CLUSTERED 
(
	[TransactionSummaryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserAccount]    Script Date: 2/20/2025 8:03:32 PM ******/
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
/****** Object:  Table [dbo].[UserAuth]    Script Date: 2/20/2025 8:03:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAuth](
	[UserName] [nchar](10) NULL,
	[Password] [nchar](10) NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BankInfo]  WITH CHECK ADD  CONSTRAINT [FK_BankInfo_UserAccount] FOREIGN KEY([uniqueId])
REFERENCES [dbo].[UserAccount] ([uniqueId])
GO
ALTER TABLE [dbo].[BankInfo] CHECK CONSTRAINT [FK_BankInfo_UserAccount]
GO
ALTER TABLE [dbo].[LoanPayments]  WITH CHECK ADD  CONSTRAINT [FK__LoanPayme__LoanI__7A672E12] FOREIGN KEY([LoanId])
REFERENCES [dbo].[LoanApplications] ([Id])
GO
ALTER TABLE [dbo].[LoanPayments] CHECK CONSTRAINT [FK__LoanPayme__LoanI__7A672E12]
GO
ALTER TABLE [dbo].[LoanPayments]  WITH CHECK ADD FOREIGN KEY([UniqueId])
REFERENCES [dbo].[UserAccount] ([uniqueId])
GO
ALTER TABLE [dbo].[SchemeInfo]  WITH CHECK ADD  CONSTRAINT [FK_SchemaInfo_UserAccount] FOREIGN KEY([uniqueId])
REFERENCES [dbo].[UserAccount] ([uniqueId])
GO
ALTER TABLE [dbo].[SchemeInfo] CHECK CONSTRAINT [FK_SchemaInfo_UserAccount]
GO
USE [master]
GO
ALTER DATABASE [gen_ai_poc] SET  READ_WRITE 
GO
