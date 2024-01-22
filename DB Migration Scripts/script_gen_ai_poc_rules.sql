Use Master
Go
IF EXISTS (SELECT * 
	   FROM   master..sysdatabases 
	   WHERE  name = N'Rules')
	DROP DATABASE Rules
GO
CREATE DATABASE Rules
GO
USE gen_ai_poc_rules
GO

/****** Object:  Table [dbo].[Tenant]    Script Date: 04/10/2013 11:28:30 ******/
IF EXISTS (SELECT * from dbo.sysobjects WHERE id = object_id(N'[dbo].[Tenant]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[Tenant]
GO
CREATE TABLE [dbo].[Tenant](
	[TenantId] [int] NOT NULL,
	[TenantName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Tenant] PRIMARY KEY CLUSTERED 
(
	[TenantId] ASC
)ON [PRIMARY]
) ON [PRIMARY]

GO


/****** Object:  Table [dbo].[AssemblyDetails]    Script Date: 04/10/2013 11:12:01 ******/

IF EXISTS (SELECT * from dbo.sysobjects WHERE id = object_id(N'[dbo].[AssemblyDetails]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[AssemblyDetails]
GO

CREATE TABLE [dbo].[AssemblyDetails](
	[AssemblyId] [int] NOT NULL,
	[AssemblyName] [nvarchar](50) NULL,
 CONSTRAINT [PK_AssemblyDetails] PRIMARY KEY CLUSTERED 
(
	[AssemblyId] ASC
)ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[ClassDetails]    Script Date: 04/10/2013 11:24:17 ******/
IF EXISTS (SELECT * from dbo.sysobjects WHERE id = object_id(N'[dbo].[ClassDetails]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[ClassDetails]
GO

CREATE TABLE [dbo].[ClassDetails](
	[ClassId] [int] IDENTITY(1,1) NOT NULL,
	[ClassName] [nvarchar](50) NULL,
 CONSTRAINT [PK_ClassDetails] PRIMARY KEY CLUSTERED 
(
	[ClassId] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[NamespaceDetails]    Script Date: 04/10/2013 11:26:48 ******/
IF EXISTS (SELECT * from dbo.sysobjects WHERE id = object_id(N'[dbo].[NamespaceDetails]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[NamespaceDetails]
GO

CREATE TABLE [dbo].[NamespaceDetails](
	[NamespaceId] [int] NOT NULL,
	[Namespace] [nvarchar](50) NULL,
 CONSTRAINT [PK_NamespaceDetails] PRIMARY KEY CLUSTERED 
(
	[NamespaceId] ASC
)ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[AssemblyNamespaceMap]    Script Date: 04/10/2013 11:19:23 ******/
IF EXISTS (SELECT * from dbo.sysobjects WHERE id = object_id(N'[dbo].[AssemblyNamespaceMap]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[AssemblyNamespaceMap]
GO

CREATE TABLE [dbo].[AssemblyNamespaceMap](
	[AssemblyNamespaceMapId] [int] IDENTITY(1,1) NOT NULL,
	[AssemblyId] [int] NOT NULL,
	[NamespaceId] [int] NOT NULL,
 CONSTRAINT [PK_AssemblyNamespaceMap] PRIMARY KEY CLUSTERED 
(
	[AssemblyNamespaceMapId] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[AssemblyNamespaceMap]  WITH CHECK ADD  CONSTRAINT [FK_AssemblyNamespaceMap_AssemblyDetails] FOREIGN KEY([AssemblyId])
REFERENCES [dbo].[AssemblyDetails] ([AssemblyId])
GO

ALTER TABLE [dbo].[AssemblyNamespaceMap] CHECK CONSTRAINT [FK_AssemblyNamespaceMap_AssemblyDetails]
GO

ALTER TABLE [dbo].[AssemblyNamespaceMap]  WITH CHECK ADD  CONSTRAINT [FK_AssemblyNamespaceMap_NamespaceDetails] FOREIGN KEY([NamespaceId])
REFERENCES [dbo].[NamespaceDetails] ([NamespaceId])
GO

ALTER TABLE [dbo].[AssemblyNamespaceMap] CHECK CONSTRAINT [FK_AssemblyNamespaceMap_NamespaceDetails]
GO




/****** Object:  Table [dbo].[NamespaceClassMap]    Script Date: 04/10/2013 11:25:18 ******/
IF EXISTS (SELECT * from dbo.sysobjects WHERE id = object_id(N'[dbo].[NamespaceClassMap]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[NamespaceClassMap]
GO

CREATE TABLE [dbo].[NamespaceClassMap](
	[NamespaceMapId] [int] IDENTITY(1,1) NOT NULL,
	[NamespaceId] [int] NULL,
	[ClassId] [int] NULL,
 CONSTRAINT [PK_NamespaceClassMap] PRIMARY KEY CLUSTERED 
(
	[NamespaceMapId] ASC
)ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[NamespaceClassMap]  WITH CHECK ADD  CONSTRAINT [FK_NamespaceClassMap_ClassDetails] FOREIGN KEY([ClassId])
REFERENCES [dbo].[ClassDetails] ([ClassId])
GO

ALTER TABLE [dbo].[NamespaceClassMap] CHECK CONSTRAINT [FK_NamespaceClassMap_ClassDetails]
GO

ALTER TABLE [dbo].[NamespaceClassMap]  WITH CHECK ADD  CONSTRAINT [FK_NamespaceClassMap_NamespaceDetails] FOREIGN KEY([NamespaceId])
REFERENCES [dbo].[NamespaceDetails] ([NamespaceId])
GO

ALTER TABLE [dbo].[NamespaceClassMap] CHECK CONSTRAINT [FK_NamespaceClassMap_NamespaceDetails]
GO




/****** Object:  Table [dbo].[RuleSet]    Script Date: 04/10/2013 11:27:38 ******/
IF EXISTS (SELECT * from dbo.sysobjects WHERE id = object_id(N'[dbo].[RuleSet]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[RuleSet]
GO

CREATE TABLE [dbo].[RuleSet](
	[Name] [nvarchar](128) NOT NULL,
	[MajorVersion] [int] NOT NULL,
	[MinorVersion] [int] NOT NULL,
	[RuleSet] [ntext] NOT NULL,
	[Status] [smallint] NULL,
	[AssemblyPath] [nvarchar](256) NULL,
	[ActivityName] [nvarchar](128) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_RuleSet_1] PRIMARY KEY CLUSTERED 
(
	[Name] ASC,
	[MajorVersion] ASC,
	[MinorVersion] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[RuleSet]  WITH CHECK ADD  CONSTRAINT [FK_RuleSet_Tenant] FOREIGN KEY([MajorVersion])
REFERENCES [dbo].[Tenant] ([TenantId])
GO

ALTER TABLE [dbo].[RuleSet] CHECK CONSTRAINT [FK_RuleSet_Tenant]
GO




/****** Object:  Table [dbo].[TenantAssemblyMap]    Script Date: 04/10/2013 11:29:07 ******/
IF EXISTS (SELECT * from dbo.sysobjects WHERE id = object_id(N'[dbo].[TenantAssemblyMap]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[TenantAssemblyMap]
GO
CREATE TABLE [dbo].[TenantAssemblyMap](
	[TenantId] [int] NOT NULL,
	[AssemblyId] [int] NOT NULL
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[TenantAssemblyMap]  WITH CHECK ADD  CONSTRAINT [FK_Table_1_AssemblyDetails] FOREIGN KEY([AssemblyId])
REFERENCES [dbo].[AssemblyDetails] ([AssemblyId])
GO

ALTER TABLE [dbo].[TenantAssemblyMap] CHECK CONSTRAINT [FK_Table_1_AssemblyDetails]
GO

ALTER TABLE [dbo].[TenantAssemblyMap]  WITH CHECK ADD  CONSTRAINT [FK_TenantAssemblyMap_Tenant] FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenant] ([TenantId])
GO

ALTER TABLE [dbo].[TenantAssemblyMap] CHECK CONSTRAINT [FK_TenantAssemblyMap_Tenant]
GO

/*************************************************STORED PROCEDURES*****************************************/
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetRuleSetInfo] 
	-- Add the parameters for the stored procedure here
	@name NVARCHAR(50),
	@MajorVersion INT,
	@MinorVersion INT
	
AS
BEGIN
    -- Insert statements for procedure here
	IF EXISTS(SELECT TOP 1 * FROM RuleSet WHERE Name=@name AND MajorVersion=@MajorVersion 
	AND MinorVersion=@MinorVersion ORDER BY MajorVersion DESC, MinorVersion DESC)
	BEGIN
	SELECT TOP 1 * FROM RuleSet WHERE Name=@name AND MajorVersion=@MajorVersion 
	AND MinorVersion=@MinorVersion ORDER BY MajorVersion DESC, MinorVersion DESC 
	END
	ELSE
	BEGIN
	SELECT TOP 1 * FROM RuleSet WHERE Name=@name AND MajorVersion=0 
	AND MinorVersion=0 ORDER BY MajorVersion DESC, MinorVersion DESC
	END 
END
GO
/****** Object:  StoredProcedure [dbo].[GetAssemblyNamespaceClassDetails]    Script Date: 04/10/2013 11:59:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

--exec GetAssemblyNamespaceClassDetails 3,'CreateFacility'
CREATE PROCEDURE  [dbo].[GetAssemblyNamespaceClassDetails]
	@tenantId AS INT,
	@className AS NVARCHAR(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    Select ad.AssemblyName from AssemblyDetails ad 
    inner join TenantAssemblyMap tam on tam.AssemblyId = ad.AssemblyId
	inner join AssemblyNamespaceMap  Ans on Ans.AssemblyId = ad.AssemblyId
	inner join NamespaceClassMap NsCs on NsCs.NamespaceId = Ans.NamespaceId
	inner join ClassDetails cd on cd.ClassId = NsCs.ClassId
    where tam.TenantId = @tenantId and cd.ClassName = @className
END
GO

/*************************************DATA SCRIPTS**********************************************************************/

/*Data Scripts - Tenant*/
INSERT INTO [Tenant]([TenantId],[TenantName])VALUES(0,'Core')
INSERT INTO [Tenant]([TenantId],[TenantName])VALUES(1,'Tenant1')
INSERT INTO [Tenant]([TenantId],[TenantName])VALUES(2,'Tenant2')
INSERT INTO [Tenant]([TenantId],[TenantName])VALUES(3,'Tenant3')

INSERT INTO [AssemblyDetails]([AssemblyId],[AssemblyName])VALUES (1,'WorkflowTenant1')
INSERT INTO [AssemblyDetails]([AssemblyId],[AssemblyName])VALUES (2,'WorkflowTenant2')
INSERT INTO [AssemblyDetails]([AssemblyId],[AssemblyName])VALUES (3,'WorkflowTenant3')

INSERT INTO [ClassDetails]([ClassName])VALUES('CreateFacility')
INSERT INTO [ClassDetails]([ClassName])VALUES('CreateSecurity')
INSERT INTO [ClassDetails]([ClassName])VALUES('UpdateSecurityStatus')

INSERT INTO [NamespaceDetails]([NamespaceId],[Namespace])VALUES (1,'WorkflowTenant1')
INSERT INTO [NamespaceDetails]([NamespaceId],[Namespace])VALUES (2,'WorkflowTenant2')
INSERT INTO [NamespaceDetails]([NamespaceId],[Namespace])VALUES (3,'WorkflowTenant3')

INSERT INTO [AssemblyNamespaceMap] ([AssemblyId],[NamespaceId])VALUES(1,1)
INSERT INTO [AssemblyNamespaceMap] ([AssemblyId],[NamespaceId])VALUES(2,2)
INSERT INTO [AssemblyNamespaceMap] ([AssemblyId],[NamespaceId])VALUES(3,3)

INSERT INTO [NamespaceClassMap]([NamespaceId],[ClassId])VALUES(1,1)
INSERT INTO [NamespaceClassMap]([NamespaceId],[ClassId])VALUES(1,2)
INSERT INTO [NamespaceClassMap]([NamespaceId],[ClassId])VALUES(1,3)
INSERT INTO [NamespaceClassMap]([NamespaceId],[ClassId])VALUES(2,1)
INSERT INTO [NamespaceClassMap]([NamespaceId],[ClassId])VALUES(3,3)

INSERT INTO [TenantAssemblyMap]([TenantId],[AssemblyId])VALUES(1,1)
INSERT INTO [TenantAssemblyMap]([TenantId],[AssemblyId])VALUES(2,2)
INSERT INTO [TenantAssemblyMap]([TenantId],[AssemblyId])VALUES(3,3)



INSERT INTO [RuleSet]([Name],[MajorVersion],[MinorVersion],[RuleSet],[Status],[AssemblyPath],[ActivityName],[ModifiedDate])
VALUES ('ApplyClientRules',0,0,'<RuleSet Description="{p1:Null}" Name="ApplyClientRules" ChainingBehavior="Full" xmlns:p1="http://schemas.microsoft.com/winfx/2006/xaml" xmlns="http://schemas.microsoft.com/winfx/2006/xaml/workflow">
	<RuleSet.Rules>
		<Rule ReevaluationBehavior="Always" Priority="0" Description="{p1:Null}" Active="True" Name="Rule1">
			<Rule.ThenActions>
				<RuleStatementAction>
					<RuleStatementAction.CodeDomStatement>
						<ns0:CodeAssignStatement LinePragma="{p1:Null}" xmlns:ns0="clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">
							<ns0:CodeAssignStatement.Left>
								<ns0:CodePropertyReferenceExpression PropertyName="Salary">
									<ns0:CodePropertyReferenceExpression.TargetObject>
										<ns0:CodeThisReferenceExpression />
									</ns0:CodePropertyReferenceExpression.TargetObject>
								</ns0:CodePropertyReferenceExpression>
							</ns0:CodeAssignStatement.Left>
							<ns0:CodeAssignStatement.Right>
								<ns0:CodePrimitiveExpression>
									<ns0:CodePrimitiveExpression.Value>
										<ns1:Int32 xmlns:ns1="clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">20000</ns1:Int32>
									</ns0:CodePrimitiveExpression.Value>
								</ns0:CodePrimitiveExpression>
							</ns0:CodeAssignStatement.Right>
						</ns0:CodeAssignStatement>
					</RuleStatementAction.CodeDomStatement>
				</RuleStatementAction>
			</Rule.ThenActions>
			<Rule.Condition>
				<RuleExpressionCondition Name="{p1:Null}">
					<RuleExpressionCondition.Expression>
						<ns0:CodeBinaryOperatorExpression Operator="BooleanAnd" xmlns:ns0="clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">
							<ns0:CodeBinaryOperatorExpression.Right>
								<ns0:CodeBinaryOperatorExpression Operator="LessThan">
									<ns0:CodeBinaryOperatorExpression.Right>
										<ns0:CodePrimitiveExpression>
											<ns0:CodePrimitiveExpression.Value>
												<ns1:Int32 xmlns:ns1="clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">2000</ns1:Int32>
											</ns0:CodePrimitiveExpression.Value>
										</ns0:CodePrimitiveExpression>
									</ns0:CodeBinaryOperatorExpression.Right>
									<ns0:CodeBinaryOperatorExpression.Left>
										<ns0:CodePropertyReferenceExpression PropertyName="ClientId">
											<ns0:CodePropertyReferenceExpression.TargetObject>
												<ns0:CodeThisReferenceExpression />
											</ns0:CodePropertyReferenceExpression.TargetObject>
										</ns0:CodePropertyReferenceExpression>
									</ns0:CodeBinaryOperatorExpression.Left>
								</ns0:CodeBinaryOperatorExpression>
							</ns0:CodeBinaryOperatorExpression.Right>
							<ns0:CodeBinaryOperatorExpression.Left>
								<ns0:CodeBinaryOperatorExpression Operator="GreaterThanOrEqual">
									<ns0:CodeBinaryOperatorExpression.Right>
										<ns0:CodePrimitiveExpression>
											<ns0:CodePrimitiveExpression.Value>
												<ns1:Int32 xmlns:ns1="clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">1000</ns1:Int32>
											</ns0:CodePrimitiveExpression.Value>
										</ns0:CodePrimitiveExpression>
									</ns0:CodeBinaryOperatorExpression.Right>
									<ns0:CodeBinaryOperatorExpression.Left>
										<ns0:CodePropertyReferenceExpression PropertyName="ClientId">
											<ns0:CodePropertyReferenceExpression.TargetObject>
												<ns0:CodeThisReferenceExpression />
											</ns0:CodePropertyReferenceExpression.TargetObject>
										</ns0:CodePropertyReferenceExpression>
									</ns0:CodeBinaryOperatorExpression.Left>
								</ns0:CodeBinaryOperatorExpression>
							</ns0:CodeBinaryOperatorExpression.Left>
						</ns0:CodeBinaryOperatorExpression>
					</RuleExpressionCondition.Expression>
				</RuleExpressionCondition>
			</Rule.Condition>
		</Rule>
		<Rule ReevaluationBehavior="Always" Priority="0" Description="{p1:Null}" Active="True" Name="Rule2">
			<Rule.ThenActions>
				<RuleStatementAction>
					<RuleStatementAction.CodeDomStatement>
						<ns0:CodeAssignStatement LinePragma="{p1:Null}" xmlns:ns0="clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">
							<ns0:CodeAssignStatement.Left>
								<ns0:CodePropertyReferenceExpression PropertyName="Salary">
									<ns0:CodePropertyReferenceExpression.TargetObject>
										<ns0:CodeThisReferenceExpression />
									</ns0:CodePropertyReferenceExpression.TargetObject>
								</ns0:CodePropertyReferenceExpression>
							</ns0:CodeAssignStatement.Left>
							<ns0:CodeAssignStatement.Right>
								<ns0:CodePrimitiveExpression>
									<ns0:CodePrimitiveExpression.Value>
										<ns1:Int32 xmlns:ns1="clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">30000</ns1:Int32>
									</ns0:CodePrimitiveExpression.Value>
								</ns0:CodePrimitiveExpression>
							</ns0:CodeAssignStatement.Right>
						</ns0:CodeAssignStatement>
					</RuleStatementAction.CodeDomStatement>
				</RuleStatementAction>
			</Rule.ThenActions>
			<Rule.Condition>
				<RuleExpressionCondition Name="{p1:Null}">
					<RuleExpressionCondition.Expression>
						<ns0:CodeBinaryOperatorExpression Operator="BooleanAnd" xmlns:ns0="clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">
							<ns0:CodeBinaryOperatorExpression.Right>
								<ns0:CodeBinaryOperatorExpression Operator="LessThan">
									<ns0:CodeBinaryOperatorExpression.Right>
										<ns0:CodePrimitiveExpression>
											<ns0:CodePrimitiveExpression.Value>
												<ns1:Int32 xmlns:ns1="clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">3000</ns1:Int32>
											</ns0:CodePrimitiveExpression.Value>
										</ns0:CodePrimitiveExpression>
									</ns0:CodeBinaryOperatorExpression.Right>
									<ns0:CodeBinaryOperatorExpression.Left>
										<ns0:CodePropertyReferenceExpression PropertyName="ClientId">
											<ns0:CodePropertyReferenceExpression.TargetObject>
												<ns0:CodeThisReferenceExpression />
											</ns0:CodePropertyReferenceExpression.TargetObject>
										</ns0:CodePropertyReferenceExpression>
									</ns0:CodeBinaryOperatorExpression.Left>
								</ns0:CodeBinaryOperatorExpression>
							</ns0:CodeBinaryOperatorExpression.Right>
							<ns0:CodeBinaryOperatorExpression.Left>
								<ns0:CodeBinaryOperatorExpression Operator="GreaterThanOrEqual">
									<ns0:CodeBinaryOperatorExpression.Right>
										<ns0:CodePrimitiveExpression>
											<ns0:CodePrimitiveExpression.Value>
												<ns1:Int32 xmlns:ns1="clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">2000</ns1:Int32>
											</ns0:CodePrimitiveExpression.Value>
										</ns0:CodePrimitiveExpression>
									</ns0:CodeBinaryOperatorExpression.Right>
									<ns0:CodeBinaryOperatorExpression.Left>
										<ns0:CodePropertyReferenceExpression PropertyName="ClientId">
											<ns0:CodePropertyReferenceExpression.TargetObject>
												<ns0:CodeThisReferenceExpression />
											</ns0:CodePropertyReferenceExpression.TargetObject>
										</ns0:CodePropertyReferenceExpression>
									</ns0:CodeBinaryOperatorExpression.Left>
								</ns0:CodeBinaryOperatorExpression>
							</ns0:CodeBinaryOperatorExpression.Left>
						</ns0:CodeBinaryOperatorExpression>
					</RuleExpressionCondition.Expression>
				</RuleExpressionCondition>
			</Rule.Condition>
		</Rule>
		<Rule ReevaluationBehavior="Always" Priority="0" Description="{p1:Null}" Active="True" Name="Rule3">
			<Rule.ThenActions>
				<RuleStatementAction>
					<RuleStatementAction.CodeDomStatement>
						<ns0:CodeAssignStatement LinePragma="{p1:Null}" xmlns:ns0="clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">
							<ns0:CodeAssignStatement.Left>
								<ns0:CodePropertyReferenceExpression PropertyName="Salary">
									<ns0:CodePropertyReferenceExpression.TargetObject>
										<ns0:CodeThisReferenceExpression />
									</ns0:CodePropertyReferenceExpression.TargetObject>
								</ns0:CodePropertyReferenceExpression>
							</ns0:CodeAssignStatement.Left>
							<ns0:CodeAssignStatement.Right>
								<ns0:CodePrimitiveExpression>
									<ns0:CodePrimitiveExpression.Value>
										<ns1:Int32 xmlns:ns1="clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">40000</ns1:Int32>
									</ns0:CodePrimitiveExpression.Value>
								</ns0:CodePrimitiveExpression>
							</ns0:CodeAssignStatement.Right>
						</ns0:CodeAssignStatement>
					</RuleStatementAction.CodeDomStatement>
				</RuleStatementAction>
			</Rule.ThenActions>
			<Rule.Condition>
				<RuleExpressionCondition Name="{p1:Null}">
					<RuleExpressionCondition.Expression>
						<ns0:CodeBinaryOperatorExpression Operator="BooleanAnd" xmlns:ns0="clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">
							<ns0:CodeBinaryOperatorExpression.Right>
								<ns0:CodeBinaryOperatorExpression Operator="LessThan">
									<ns0:CodeBinaryOperatorExpression.Right>
										<ns0:CodePrimitiveExpression>
											<ns0:CodePrimitiveExpression.Value>
												<ns1:Int32 xmlns:ns1="clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">4000</ns1:Int32>
											</ns0:CodePrimitiveExpression.Value>
										</ns0:CodePrimitiveExpression>
									</ns0:CodeBinaryOperatorExpression.Right>
									<ns0:CodeBinaryOperatorExpression.Left>
										<ns0:CodePropertyReferenceExpression PropertyName="ClientId">
											<ns0:CodePropertyReferenceExpression.TargetObject>
												<ns0:CodeThisReferenceExpression />
											</ns0:CodePropertyReferenceExpression.TargetObject>
										</ns0:CodePropertyReferenceExpression>
									</ns0:CodeBinaryOperatorExpression.Left>
								</ns0:CodeBinaryOperatorExpression>
							</ns0:CodeBinaryOperatorExpression.Right>
							<ns0:CodeBinaryOperatorExpression.Left>
								<ns0:CodeBinaryOperatorExpression Operator="GreaterThanOrEqual">
									<ns0:CodeBinaryOperatorExpression.Right>
										<ns0:CodePrimitiveExpression>
											<ns0:CodePrimitiveExpression.Value>
												<ns1:Int32 xmlns:ns1="clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">3000</ns1:Int32>
											</ns0:CodePrimitiveExpression.Value>
										</ns0:CodePrimitiveExpression>
									</ns0:CodeBinaryOperatorExpression.Right>
									<ns0:CodeBinaryOperatorExpression.Left>
										<ns0:CodePropertyReferenceExpression PropertyName="ClientId">
											<ns0:CodePropertyReferenceExpression.TargetObject>
												<ns0:CodeThisReferenceExpression />
											</ns0:CodePropertyReferenceExpression.TargetObject>
										</ns0:CodePropertyReferenceExpression>
									</ns0:CodeBinaryOperatorExpression.Left>
								</ns0:CodeBinaryOperatorExpression>
							</ns0:CodeBinaryOperatorExpression.Left>
						</ns0:CodeBinaryOperatorExpression>
					</RuleExpressionCondition.Expression>
				</RuleExpressionCondition>
			</Rule.Condition>
		</Rule>
		<Rule ReevaluationBehavior="Always" Priority="0" Description="{p1:Null}" Active="True" Name="Rule4">
			<Rule.ThenActions>
				<RuleStatementAction>
					<RuleStatementAction.CodeDomStatement>
						<ns0:CodeAssignStatement LinePragma="{p1:Null}" xmlns:ns0="clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">
							<ns0:CodeAssignStatement.Left>
								<ns0:CodePropertyReferenceExpression PropertyName="Salary">
									<ns0:CodePropertyReferenceExpression.TargetObject>
										<ns0:CodeThisReferenceExpression />
									</ns0:CodePropertyReferenceExpression.TargetObject>
								</ns0:CodePropertyReferenceExpression>
							</ns0:CodeAssignStatement.Left>
							<ns0:CodeAssignStatement.Right>
								<ns0:CodePrimitiveExpression>
									<ns0:CodePrimitiveExpression.Value>
										<ns1:Int32 xmlns:ns1="clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">50000</ns1:Int32>
									</ns0:CodePrimitiveExpression.Value>
								</ns0:CodePrimitiveExpression>
							</ns0:CodeAssignStatement.Right>
						</ns0:CodeAssignStatement>
					</RuleStatementAction.CodeDomStatement>
				</RuleStatementAction>
			</Rule.ThenActions>
			<Rule.Condition>
				<RuleExpressionCondition Name="{p1:Null}">
					<RuleExpressionCondition.Expression>
						<ns0:CodeBinaryOperatorExpression Operator="BooleanAnd" xmlns:ns0="clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">
							<ns0:CodeBinaryOperatorExpression.Right>
								<ns0:CodeBinaryOperatorExpression Operator="LessThan">
									<ns0:CodeBinaryOperatorExpression.Right>
										<ns0:CodePrimitiveExpression>
											<ns0:CodePrimitiveExpression.Value>
												<ns1:Int32 xmlns:ns1="clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">5000</ns1:Int32>
											</ns0:CodePrimitiveExpression.Value>
										</ns0:CodePrimitiveExpression>
									</ns0:CodeBinaryOperatorExpression.Right>
									<ns0:CodeBinaryOperatorExpression.Left>
										<ns0:CodePropertyReferenceExpression PropertyName="ClientId">
											<ns0:CodePropertyReferenceExpression.TargetObject>
												<ns0:CodeThisReferenceExpression />
											</ns0:CodePropertyReferenceExpression.TargetObject>
										</ns0:CodePropertyReferenceExpression>
									</ns0:CodeBinaryOperatorExpression.Left>
								</ns0:CodeBinaryOperatorExpression>
							</ns0:CodeBinaryOperatorExpression.Right>
							<ns0:CodeBinaryOperatorExpression.Left>
								<ns0:CodeBinaryOperatorExpression Operator="GreaterThanOrEqual">
									<ns0:CodeBinaryOperatorExpression.Right>
										<ns0:CodePrimitiveExpression>
											<ns0:CodePrimitiveExpression.Value>
												<ns1:Int32 xmlns:ns1="clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">4000</ns1:Int32>
											</ns0:CodePrimitiveExpression.Value>
										</ns0:CodePrimitiveExpression>
									</ns0:CodeBinaryOperatorExpression.Right>
									<ns0:CodeBinaryOperatorExpression.Left>
										<ns0:CodePropertyReferenceExpression PropertyName="ClientId">
											<ns0:CodePropertyReferenceExpression.TargetObject>
												<ns0:CodeThisReferenceExpression />
											</ns0:CodePropertyReferenceExpression.TargetObject>
										</ns0:CodePropertyReferenceExpression>
									</ns0:CodeBinaryOperatorExpression.Left>
								</ns0:CodeBinaryOperatorExpression>
							</ns0:CodeBinaryOperatorExpression.Left>
						</ns0:CodeBinaryOperatorExpression>
					</RuleExpressionCondition.Expression>
				</RuleExpressionCondition>
			</Rule.Condition>
		</Rule>
		<Rule ReevaluationBehavior="Always" Priority="0" Description="{p1:Null}" Active="True" Name="Rule5">
			<Rule.ThenActions>
				<RuleStatementAction>
					<RuleStatementAction.CodeDomStatement>
						<ns0:CodeAssignStatement LinePragma="{p1:Null}" xmlns:ns0="clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">
							<ns0:CodeAssignStatement.Left>
								<ns0:CodePropertyReferenceExpression PropertyName="Salary">
									<ns0:CodePropertyReferenceExpression.TargetObject>
										<ns0:CodeThisReferenceExpression />
									</ns0:CodePropertyReferenceExpression.TargetObject>
								</ns0:CodePropertyReferenceExpression>
							</ns0:CodeAssignStatement.Left>
							<ns0:CodeAssignStatement.Right>
								<ns0:CodePrimitiveExpression>
									<ns0:CodePrimitiveExpression.Value>
										<ns1:Int32 xmlns:ns1="clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">60000</ns1:Int32>
									</ns0:CodePrimitiveExpression.Value>
								</ns0:CodePrimitiveExpression>
							</ns0:CodeAssignStatement.Right>
						</ns0:CodeAssignStatement>
					</RuleStatementAction.CodeDomStatement>
				</RuleStatementAction>
			</Rule.ThenActions>
			<Rule.Condition>
				<RuleExpressionCondition Name="{p1:Null}">
					<RuleExpressionCondition.Expression>
						<ns0:CodeBinaryOperatorExpression Operator="BooleanAnd" xmlns:ns0="clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">
							<ns0:CodeBinaryOperatorExpression.Right>
								<ns0:CodeBinaryOperatorExpression Operator="LessThan">
									<ns0:CodeBinaryOperatorExpression.Right>
										<ns0:CodePrimitiveExpression>
											<ns0:CodePrimitiveExpression.Value>
												<ns1:Int32 xmlns:ns1="clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">6000</ns1:Int32>
											</ns0:CodePrimitiveExpression.Value>
										</ns0:CodePrimitiveExpression>
									</ns0:CodeBinaryOperatorExpression.Right>
									<ns0:CodeBinaryOperatorExpression.Left>
										<ns0:CodePropertyReferenceExpression PropertyName="ClientId">
											<ns0:CodePropertyReferenceExpression.TargetObject>
												<ns0:CodeThisReferenceExpression />
											</ns0:CodePropertyReferenceExpression.TargetObject>
										</ns0:CodePropertyReferenceExpression>
									</ns0:CodeBinaryOperatorExpression.Left>
								</ns0:CodeBinaryOperatorExpression>
							</ns0:CodeBinaryOperatorExpression.Right>
							<ns0:CodeBinaryOperatorExpression.Left>
								<ns0:CodeBinaryOperatorExpression Operator="GreaterThanOrEqual">
									<ns0:CodeBinaryOperatorExpression.Right>
										<ns0:CodePrimitiveExpression>
											<ns0:CodePrimitiveExpression.Value>
												<ns1:Int32 xmlns:ns1="clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">5000</ns1:Int32>
											</ns0:CodePrimitiveExpression.Value>
										</ns0:CodePrimitiveExpression>
									</ns0:CodeBinaryOperatorExpression.Right>
									<ns0:CodeBinaryOperatorExpression.Left>
										<ns0:CodePropertyReferenceExpression PropertyName="ClientId">
											<ns0:CodePropertyReferenceExpression.TargetObject>
												<ns0:CodeThisReferenceExpression />
											</ns0:CodePropertyReferenceExpression.TargetObject>
										</ns0:CodePropertyReferenceExpression>
									</ns0:CodeBinaryOperatorExpression.Left>
								</ns0:CodeBinaryOperatorExpression>
							</ns0:CodeBinaryOperatorExpression.Left>
						</ns0:CodeBinaryOperatorExpression>
					</RuleExpressionCondition.Expression>
				</RuleExpressionCondition>
			</Rule.Condition>
		</Rule>
	</RuleSet.Rules>
</RuleSet>',
0,'','Rules.RulesLibrary.Client',GETDATE())


INSERT INTO [RuleSet]([Name],[MajorVersion],[MinorVersion],[RuleSet],[Status],[AssemblyPath],[ActivityName],[ModifiedDate])
VALUES ('ApplyClientRules',1,0,'<RuleSet Description="{p1:Null}" Name="ApplyClientRules" ChainingBehavior="Full" xmlns:p1="http://schemas.microsoft.com/winfx/2006/xaml" xmlns="http://schemas.microsoft.com/winfx/2006/xaml/workflow">
	<RuleSet.Rules>
		<Rule ReevaluationBehavior="Always" Priority="0" Description="{p1:Null}" Active="True" Name="Rule1">
			<Rule.ThenActions>
				<RuleStatementAction>
					<RuleStatementAction.CodeDomStatement>
						<ns0:CodeAssignStatement LinePragma="{p1:Null}" xmlns:ns0="clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">
							<ns0:CodeAssignStatement.Left>
								<ns0:CodePropertyReferenceExpression PropertyName="Salary">
									<ns0:CodePropertyReferenceExpression.TargetObject>
										<ns0:CodeThisReferenceExpression />
									</ns0:CodePropertyReferenceExpression.TargetObject>
								</ns0:CodePropertyReferenceExpression>
							</ns0:CodeAssignStatement.Left>
							<ns0:CodeAssignStatement.Right>
								<ns0:CodePrimitiveExpression>
									<ns0:CodePrimitiveExpression.Value>
										<ns1:Int32 xmlns:ns1="clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">10000</ns1:Int32>
									</ns0:CodePrimitiveExpression.Value>
								</ns0:CodePrimitiveExpression>
							</ns0:CodeAssignStatement.Right>
						</ns0:CodeAssignStatement>
					</RuleStatementAction.CodeDomStatement>
				</RuleStatementAction>
			</Rule.ThenActions>
			<Rule.Condition>
				<RuleExpressionCondition Name="{p1:Null}">
					<RuleExpressionCondition.Expression>
						<ns0:CodeBinaryOperatorExpression Operator="BooleanAnd" xmlns:ns0="clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">
							<ns0:CodeBinaryOperatorExpression.Right>
								<ns0:CodeBinaryOperatorExpression Operator="LessThan">
									<ns0:CodeBinaryOperatorExpression.Right>
										<ns0:CodePrimitiveExpression>
											<ns0:CodePrimitiveExpression.Value>
												<ns1:Int32 xmlns:ns1="clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">2000</ns1:Int32>
											</ns0:CodePrimitiveExpression.Value>
										</ns0:CodePrimitiveExpression>
									</ns0:CodeBinaryOperatorExpression.Right>
									<ns0:CodeBinaryOperatorExpression.Left>
										<ns0:CodePropertyReferenceExpression PropertyName="ClientId">
											<ns0:CodePropertyReferenceExpression.TargetObject>
												<ns0:CodeThisReferenceExpression />
											</ns0:CodePropertyReferenceExpression.TargetObject>
										</ns0:CodePropertyReferenceExpression>
									</ns0:CodeBinaryOperatorExpression.Left>
								</ns0:CodeBinaryOperatorExpression>
							</ns0:CodeBinaryOperatorExpression.Right>
							<ns0:CodeBinaryOperatorExpression.Left>
								<ns0:CodeBinaryOperatorExpression Operator="GreaterThanOrEqual">
									<ns0:CodeBinaryOperatorExpression.Right>
										<ns0:CodePrimitiveExpression>
											<ns0:CodePrimitiveExpression.Value>
												<ns1:Int32 xmlns:ns1="clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">1000</ns1:Int32>
											</ns0:CodePrimitiveExpression.Value>
										</ns0:CodePrimitiveExpression>
									</ns0:CodeBinaryOperatorExpression.Right>
									<ns0:CodeBinaryOperatorExpression.Left>
										<ns0:CodePropertyReferenceExpression PropertyName="ClientId">
											<ns0:CodePropertyReferenceExpression.TargetObject>
												<ns0:CodeThisReferenceExpression />
											</ns0:CodePropertyReferenceExpression.TargetObject>
										</ns0:CodePropertyReferenceExpression>
									</ns0:CodeBinaryOperatorExpression.Left>
								</ns0:CodeBinaryOperatorExpression>
							</ns0:CodeBinaryOperatorExpression.Left>
						</ns0:CodeBinaryOperatorExpression>
					</RuleExpressionCondition.Expression>
				</RuleExpressionCondition>
			</Rule.Condition>
		</Rule>
		<Rule ReevaluationBehavior="Always" Priority="0" Description="{p1:Null}" Active="True" Name="Rule2">
			<Rule.ThenActions>
				<RuleStatementAction>
					<RuleStatementAction.CodeDomStatement>
						<ns0:CodeAssignStatement LinePragma="{p1:Null}" xmlns:ns0="clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">
							<ns0:CodeAssignStatement.Left>
								<ns0:CodePropertyReferenceExpression PropertyName="Salary">
									<ns0:CodePropertyReferenceExpression.TargetObject>
										<ns0:CodeThisReferenceExpression />
									</ns0:CodePropertyReferenceExpression.TargetObject>
								</ns0:CodePropertyReferenceExpression>
							</ns0:CodeAssignStatement.Left>
							<ns0:CodeAssignStatement.Right>
								<ns0:CodePrimitiveExpression>
									<ns0:CodePrimitiveExpression.Value>
										<ns1:Int32 xmlns:ns1="clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">20000</ns1:Int32>
									</ns0:CodePrimitiveExpression.Value>
								</ns0:CodePrimitiveExpression>
							</ns0:CodeAssignStatement.Right>
						</ns0:CodeAssignStatement>
					</RuleStatementAction.CodeDomStatement>
				</RuleStatementAction>
			</Rule.ThenActions>
			<Rule.Condition>
				<RuleExpressionCondition Name="{p1:Null}">
					<RuleExpressionCondition.Expression>
						<ns0:CodeBinaryOperatorExpression Operator="BooleanAnd" xmlns:ns0="clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">
							<ns0:CodeBinaryOperatorExpression.Right>
								<ns0:CodeBinaryOperatorExpression Operator="LessThan">
									<ns0:CodeBinaryOperatorExpression.Right>
										<ns0:CodePrimitiveExpression>
											<ns0:CodePrimitiveExpression.Value>
												<ns1:Int32 xmlns:ns1="clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">3000</ns1:Int32>
											</ns0:CodePrimitiveExpression.Value>
										</ns0:CodePrimitiveExpression>
									</ns0:CodeBinaryOperatorExpression.Right>
									<ns0:CodeBinaryOperatorExpression.Left>
										<ns0:CodePropertyReferenceExpression PropertyName="ClientId">
											<ns0:CodePropertyReferenceExpression.TargetObject>
												<ns0:CodeThisReferenceExpression />
											</ns0:CodePropertyReferenceExpression.TargetObject>
										</ns0:CodePropertyReferenceExpression>
									</ns0:CodeBinaryOperatorExpression.Left>
								</ns0:CodeBinaryOperatorExpression>
							</ns0:CodeBinaryOperatorExpression.Right>
							<ns0:CodeBinaryOperatorExpression.Left>
								<ns0:CodeBinaryOperatorExpression Operator="GreaterThanOrEqual">
									<ns0:CodeBinaryOperatorExpression.Right>
										<ns0:CodePrimitiveExpression>
											<ns0:CodePrimitiveExpression.Value>
												<ns1:Int32 xmlns:ns1="clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">2000</ns1:Int32>
											</ns0:CodePrimitiveExpression.Value>
										</ns0:CodePrimitiveExpression>
									</ns0:CodeBinaryOperatorExpression.Right>
									<ns0:CodeBinaryOperatorExpression.Left>
										<ns0:CodePropertyReferenceExpression PropertyName="ClientId">
											<ns0:CodePropertyReferenceExpression.TargetObject>
												<ns0:CodeThisReferenceExpression />
											</ns0:CodePropertyReferenceExpression.TargetObject>
										</ns0:CodePropertyReferenceExpression>
									</ns0:CodeBinaryOperatorExpression.Left>
								</ns0:CodeBinaryOperatorExpression>
							</ns0:CodeBinaryOperatorExpression.Left>
						</ns0:CodeBinaryOperatorExpression>
					</RuleExpressionCondition.Expression>
				</RuleExpressionCondition>
			</Rule.Condition>
		</Rule>
		<Rule ReevaluationBehavior="Always" Priority="0" Description="{p1:Null}" Active="True" Name="Rule3">
			<Rule.ThenActions>
				<RuleStatementAction>
					<RuleStatementAction.CodeDomStatement>
						<ns0:CodeAssignStatement LinePragma="{p1:Null}" xmlns:ns0="clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">
							<ns0:CodeAssignStatement.Left>
								<ns0:CodePropertyReferenceExpression PropertyName="Salary">
									<ns0:CodePropertyReferenceExpression.TargetObject>
										<ns0:CodeThisReferenceExpression />
									</ns0:CodePropertyReferenceExpression.TargetObject>
								</ns0:CodePropertyReferenceExpression>
							</ns0:CodeAssignStatement.Left>
							<ns0:CodeAssignStatement.Right>
								<ns0:CodePrimitiveExpression>
									<ns0:CodePrimitiveExpression.Value>
										<ns1:Int32 xmlns:ns1="clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">30000</ns1:Int32>
									</ns0:CodePrimitiveExpression.Value>
								</ns0:CodePrimitiveExpression>
							</ns0:CodeAssignStatement.Right>
						</ns0:CodeAssignStatement>
					</RuleStatementAction.CodeDomStatement>
				</RuleStatementAction>
			</Rule.ThenActions>
			<Rule.Condition>
				<RuleExpressionCondition Name="{p1:Null}">
					<RuleExpressionCondition.Expression>
						<ns0:CodeBinaryOperatorExpression Operator="BooleanAnd" xmlns:ns0="clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">
							<ns0:CodeBinaryOperatorExpression.Right>
								<ns0:CodeBinaryOperatorExpression Operator="LessThan">
									<ns0:CodeBinaryOperatorExpression.Right>
										<ns0:CodePrimitiveExpression>
											<ns0:CodePrimitiveExpression.Value>
												<ns1:Int32 xmlns:ns1="clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">4000</ns1:Int32>
											</ns0:CodePrimitiveExpression.Value>
										</ns0:CodePrimitiveExpression>
									</ns0:CodeBinaryOperatorExpression.Right>
									<ns0:CodeBinaryOperatorExpression.Left>
										<ns0:CodePropertyReferenceExpression PropertyName="ClientId">
											<ns0:CodePropertyReferenceExpression.TargetObject>
												<ns0:CodeThisReferenceExpression />
											</ns0:CodePropertyReferenceExpression.TargetObject>
										</ns0:CodePropertyReferenceExpression>
									</ns0:CodeBinaryOperatorExpression.Left>
								</ns0:CodeBinaryOperatorExpression>
							</ns0:CodeBinaryOperatorExpression.Right>
							<ns0:CodeBinaryOperatorExpression.Left>
								<ns0:CodeBinaryOperatorExpression Operator="GreaterThanOrEqual">
									<ns0:CodeBinaryOperatorExpression.Right>
										<ns0:CodePrimitiveExpression>
											<ns0:CodePrimitiveExpression.Value>
												<ns1:Int32 xmlns:ns1="clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">3000</ns1:Int32>
											</ns0:CodePrimitiveExpression.Value>
										</ns0:CodePrimitiveExpression>
									</ns0:CodeBinaryOperatorExpression.Right>
									<ns0:CodeBinaryOperatorExpression.Left>
										<ns0:CodePropertyReferenceExpression PropertyName="ClientId">
											<ns0:CodePropertyReferenceExpression.TargetObject>
												<ns0:CodeThisReferenceExpression />
											</ns0:CodePropertyReferenceExpression.TargetObject>
										</ns0:CodePropertyReferenceExpression>
									</ns0:CodeBinaryOperatorExpression.Left>
								</ns0:CodeBinaryOperatorExpression>
							</ns0:CodeBinaryOperatorExpression.Left>
						</ns0:CodeBinaryOperatorExpression>
					</RuleExpressionCondition.Expression>
				</RuleExpressionCondition>
			</Rule.Condition>
		</Rule>
		<Rule ReevaluationBehavior="Always" Priority="0" Description="{p1:Null}" Active="True" Name="Rule4">
			<Rule.ThenActions>
				<RuleStatementAction>
					<RuleStatementAction.CodeDomStatement>
						<ns0:CodeAssignStatement LinePragma="{p1:Null}" xmlns:ns0="clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">
							<ns0:CodeAssignStatement.Left>
								<ns0:CodePropertyReferenceExpression PropertyName="Salary">
									<ns0:CodePropertyReferenceExpression.TargetObject>
										<ns0:CodeThisReferenceExpression />
									</ns0:CodePropertyReferenceExpression.TargetObject>
								</ns0:CodePropertyReferenceExpression>
							</ns0:CodeAssignStatement.Left>
							<ns0:CodeAssignStatement.Right>
								<ns0:CodePrimitiveExpression>
									<ns0:CodePrimitiveExpression.Value>
										<ns1:Int32 xmlns:ns1="clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">40000</ns1:Int32>
									</ns0:CodePrimitiveExpression.Value>
								</ns0:CodePrimitiveExpression>
							</ns0:CodeAssignStatement.Right>
						</ns0:CodeAssignStatement>
					</RuleStatementAction.CodeDomStatement>
				</RuleStatementAction>
			</Rule.ThenActions>
			<Rule.Condition>
				<RuleExpressionCondition Name="{p1:Null}">
					<RuleExpressionCondition.Expression>
						<ns0:CodeBinaryOperatorExpression Operator="BooleanAnd" xmlns:ns0="clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">
							<ns0:CodeBinaryOperatorExpression.Right>
								<ns0:CodeBinaryOperatorExpression Operator="LessThan">
									<ns0:CodeBinaryOperatorExpression.Right>
										<ns0:CodePrimitiveExpression>
											<ns0:CodePrimitiveExpression.Value>
												<ns1:Int32 xmlns:ns1="clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">5000</ns1:Int32>
											</ns0:CodePrimitiveExpression.Value>
										</ns0:CodePrimitiveExpression>
									</ns0:CodeBinaryOperatorExpression.Right>
									<ns0:CodeBinaryOperatorExpression.Left>
										<ns0:CodePropertyReferenceExpression PropertyName="ClientId">
											<ns0:CodePropertyReferenceExpression.TargetObject>
												<ns0:CodeThisReferenceExpression />
											</ns0:CodePropertyReferenceExpression.TargetObject>
										</ns0:CodePropertyReferenceExpression>
									</ns0:CodeBinaryOperatorExpression.Left>
								</ns0:CodeBinaryOperatorExpression>
							</ns0:CodeBinaryOperatorExpression.Right>
							<ns0:CodeBinaryOperatorExpression.Left>
								<ns0:CodeBinaryOperatorExpression Operator="GreaterThanOrEqual">
									<ns0:CodeBinaryOperatorExpression.Right>
										<ns0:CodePrimitiveExpression>
											<ns0:CodePrimitiveExpression.Value>
												<ns1:Int32 xmlns:ns1="clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">4000</ns1:Int32>
											</ns0:CodePrimitiveExpression.Value>
										</ns0:CodePrimitiveExpression>
									</ns0:CodeBinaryOperatorExpression.Right>
									<ns0:CodeBinaryOperatorExpression.Left>
										<ns0:CodePropertyReferenceExpression PropertyName="ClientId">
											<ns0:CodePropertyReferenceExpression.TargetObject>
												<ns0:CodeThisReferenceExpression />
											</ns0:CodePropertyReferenceExpression.TargetObject>
										</ns0:CodePropertyReferenceExpression>
									</ns0:CodeBinaryOperatorExpression.Left>
								</ns0:CodeBinaryOperatorExpression>
							</ns0:CodeBinaryOperatorExpression.Left>
						</ns0:CodeBinaryOperatorExpression>
					</RuleExpressionCondition.Expression>
				</RuleExpressionCondition>
			</Rule.Condition>
		</Rule>
		<Rule ReevaluationBehavior="Always" Priority="0" Description="{p1:Null}" Active="True" Name="Rule5">
			<Rule.ThenActions>
				<RuleStatementAction>
					<RuleStatementAction.CodeDomStatement>
						<ns0:CodeAssignStatement LinePragma="{p1:Null}" xmlns:ns0="clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">
							<ns0:CodeAssignStatement.Left>
								<ns0:CodePropertyReferenceExpression PropertyName="Salary">
									<ns0:CodePropertyReferenceExpression.TargetObject>
										<ns0:CodeThisReferenceExpression />
									</ns0:CodePropertyReferenceExpression.TargetObject>
								</ns0:CodePropertyReferenceExpression>
							</ns0:CodeAssignStatement.Left>
							<ns0:CodeAssignStatement.Right>
								<ns0:CodePrimitiveExpression>
									<ns0:CodePrimitiveExpression.Value>
										<ns1:Int32 xmlns:ns1="clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">50000</ns1:Int32>
									</ns0:CodePrimitiveExpression.Value>
								</ns0:CodePrimitiveExpression>
							</ns0:CodeAssignStatement.Right>
						</ns0:CodeAssignStatement>
					</RuleStatementAction.CodeDomStatement>
				</RuleStatementAction>
			</Rule.ThenActions>
			<Rule.Condition>
				<RuleExpressionCondition Name="{p1:Null}">
					<RuleExpressionCondition.Expression>
						<ns0:CodeBinaryOperatorExpression Operator="BooleanAnd" xmlns:ns0="clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">
							<ns0:CodeBinaryOperatorExpression.Right>
								<ns0:CodeBinaryOperatorExpression Operator="LessThan">
									<ns0:CodeBinaryOperatorExpression.Right>
										<ns0:CodePrimitiveExpression>
											<ns0:CodePrimitiveExpression.Value>
												<ns1:Int32 xmlns:ns1="clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">6000</ns1:Int32>
											</ns0:CodePrimitiveExpression.Value>
										</ns0:CodePrimitiveExpression>
									</ns0:CodeBinaryOperatorExpression.Right>
									<ns0:CodeBinaryOperatorExpression.Left>
										<ns0:CodePropertyReferenceExpression PropertyName="ClientId">
											<ns0:CodePropertyReferenceExpression.TargetObject>
												<ns0:CodeThisReferenceExpression />
											</ns0:CodePropertyReferenceExpression.TargetObject>
										</ns0:CodePropertyReferenceExpression>
									</ns0:CodeBinaryOperatorExpression.Left>
								</ns0:CodeBinaryOperatorExpression>
							</ns0:CodeBinaryOperatorExpression.Right>
							<ns0:CodeBinaryOperatorExpression.Left>
								<ns0:CodeBinaryOperatorExpression Operator="GreaterThanOrEqual">
									<ns0:CodeBinaryOperatorExpression.Right>
										<ns0:CodePrimitiveExpression>
											<ns0:CodePrimitiveExpression.Value>
												<ns1:Int32 xmlns:ns1="clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">5000</ns1:Int32>
											</ns0:CodePrimitiveExpression.Value>
										</ns0:CodePrimitiveExpression>
									</ns0:CodeBinaryOperatorExpression.Right>
									<ns0:CodeBinaryOperatorExpression.Left>
										<ns0:CodePropertyReferenceExpression PropertyName="ClientId">
											<ns0:CodePropertyReferenceExpression.TargetObject>
												<ns0:CodeThisReferenceExpression />
											</ns0:CodePropertyReferenceExpression.TargetObject>
										</ns0:CodePropertyReferenceExpression>
									</ns0:CodeBinaryOperatorExpression.Left>
								</ns0:CodeBinaryOperatorExpression>
							</ns0:CodeBinaryOperatorExpression.Left>
						</ns0:CodeBinaryOperatorExpression>
					</RuleExpressionCondition.Expression>
				</RuleExpressionCondition>
			</Rule.Condition>
		</Rule>
	</RuleSet.Rules>
</RuleSet>',
0,'','Rules.RulesLibrary.Client',GETDATE())

GO






