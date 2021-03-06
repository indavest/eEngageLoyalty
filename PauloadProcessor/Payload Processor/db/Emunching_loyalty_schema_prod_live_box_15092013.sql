USE [master]
GO
/****** Object:  Database [eMunching_Loyalty]    Script Date: 09/15/2013 16:55:10 ******/
CREATE DATABASE [eMunching_Loyalty]
GO
ALTER DATABASE [eMunching_Loyalty] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [eMunching_Loyalty].[dbo].[sp_fulltext_database] @action = 'disable'
end
GO
ALTER DATABASE [eMunching_Loyalty] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [eMunching_Loyalty] SET ANSI_NULLS OFF
GO
ALTER DATABASE [eMunching_Loyalty] SET ANSI_PADDING OFF
GO
ALTER DATABASE [eMunching_Loyalty] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [eMunching_Loyalty] SET ARITHABORT OFF
GO
ALTER DATABASE [eMunching_Loyalty] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [eMunching_Loyalty] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [eMunching_Loyalty] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [eMunching_Loyalty] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [eMunching_Loyalty] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [eMunching_Loyalty] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [eMunching_Loyalty] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [eMunching_Loyalty] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [eMunching_Loyalty] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [eMunching_Loyalty] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [eMunching_Loyalty] SET  DISABLE_BROKER
GO
ALTER DATABASE [eMunching_Loyalty] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [eMunching_Loyalty] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [eMunching_Loyalty] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [eMunching_Loyalty] SET ALLOW_SNAPSHOT_ISOLATION ON
GO
ALTER DATABASE [eMunching_Loyalty] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [eMunching_Loyalty] SET READ_COMMITTED_SNAPSHOT ON
GO
ALTER DATABASE [eMunching_Loyalty] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [eMunching_Loyalty] SET  READ_WRITE
GO
ALTER DATABASE [eMunching_Loyalty] SET RECOVERY FULL
GO
ALTER DATABASE [eMunching_Loyalty] SET  MULTI_USER
GO
ALTER DATABASE [eMunching_Loyalty] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [eMunching_Loyalty] SET DB_CHAINING OFF
GO
USE [eMunching_Loyalty]
GO
/****** Object:  UserDefinedTableType [dbo].[SettlementInfoesType]    Script Date: 09/15/2013 16:55:14 ******/
CREATE TYPE [dbo].[SettlementInfoesType] AS TABLE(
	[BillNumber] [nvarchar](50) NOT NULL,
	[RestaurantId] [int] NOT NULL,
	[UniqueCode] [nvarchar](20) NOT NULL,
	[EmailAddress] [nvarchar](50) NULL,
	[IsServiced] [bit] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
	[SettlementDate] [datetime] NOT NULL
)
GO
/****** Object:  Table [dbo].[webpages_UsersInRoles]    Script Date: 09/15/2013 16:55:17 ******/
CREATE TABLE [dbo].[webpages_UsersInRoles](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
GO
/****** Object:  Table [dbo].[webpages_Roles]    Script Date: 09/15/2013 16:55:20 ******/
CREATE TABLE [dbo].[webpages_Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](256) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
UNIQUE NONCLUSTERED 
(
	[RoleName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
GO
/****** Object:  Table [dbo].[webpages_OAuthMembership]    Script Date: 09/15/2013 16:55:22 ******/
CREATE TABLE [dbo].[webpages_OAuthMembership](
	[Provider] [nvarchar](30) NOT NULL,
	[ProviderUserId] [nvarchar](100) NOT NULL,
	[UserId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Provider] ASC,
	[ProviderUserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
GO
/****** Object:  Table [dbo].[webpages_Membership]    Script Date: 09/15/2013 16:55:29 ******/
CREATE TABLE [dbo].[webpages_Membership](
	[UserId] [int] NOT NULL,
	[CreateDate] [datetime] NULL,
	[ConfirmationToken] [nvarchar](128) NULL,
	[IsConfirmed] [bit] NULL,
	[LastPasswordFailureDate] [datetime] NULL,
	[PasswordFailuresSinceLastSuccess] [int] NOT NULL,
	[Password] [nvarchar](128) NOT NULL,
	[PasswordChangedDate] [datetime] NULL,
	[PasswordSalt] [nvarchar](128) NOT NULL,
	[PasswordVerificationToken] [nvarchar](128) NULL,
	[PasswordVerificationTokenExpirationDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
GO
/****** Object:  Table [dbo].[UserProfile]    Script Date: 09/15/2013 16:55:32 ******/
CREATE TABLE [dbo].[UserProfile](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](56) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
GO
/****** Object:  Table [dbo].[LoyaltyTypes]    Script Date: 09/15/2013 16:55:34 ******/
CREATE TABLE [dbo].[LoyaltyTypes](
	[ID] [int] NOT NULL,
	[LoyaltyType1] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_LoyaltyTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
GO
/****** Object:  UserDefinedTableType [dbo].[ItemCodeType]    Script Date: 09/15/2013 16:55:38 ******/
CREATE TYPE [dbo].[ItemCodeType] AS TABLE(
	[ItemCode1] [int] NOT NULL,
	[RestaurantId] [int] NOT NULL,
	[ItemName] [nvarchar](256) NOT NULL,
	[LoyaltyEnabled] [bit] NOT NULL,
	[LoyaltyPoints] [int] NOT NULL,
	[LoyaltyMultiplier] [int] NOT NULL,
	[BonusPoints] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL
)
GO
/****** Object:  Table [dbo].[CouponCode_UniqueCodeMapping]    Script Date: 09/15/2013 16:55:41 ******/
CREATE TABLE [dbo].[CouponCode_UniqueCodeMapping](
	[ID] [uniqueidentifier] NOT NULL,
	[CouponCode] [nvarchar](20) NOT NULL,
	[UniqueCode] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_CouponCode_UniqueCodeMapping] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
GO
/****** Object:  Table [dbo].[Restaurants]    Script Date: 09/15/2013 16:55:43 ******/
CREATE TABLE [dbo].[Restaurants](
	[ID] [int] NOT NULL,
	[RestaurantName] [nvarchar](50) NOT NULL,
	[LoyaltyID] [int] NULL,
 CONSTRAINT [PK_Restaurants] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
GO
CREATE NONCLUSTERED INDEX [IX_FK_Restaurant_ToLoyaltyType] ON [dbo].[Restaurants] 
(
	[LoyaltyID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
/****** Object:  Table [dbo].[Rewards]    Script Date: 09/15/2013 16:55:49 ******/
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Rewards](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[NumberOfItems] [int] NOT NULL,
	[EligibleSKUs] [nvarchar](max) NOT NULL,
	[RewardSKUs] [nvarchar](max) NOT NULL,
	[RestaurantID] [int] NOT NULL,
	[Image] [varchar](200) NULL,
	[Validity] [int] NULL,
	[Multiplier] [int] NOT NULL,
 CONSTRAINT [PK_Rewards] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [IX_FK_RestaurantReward] ON [dbo].[Rewards] 
(
	[RestaurantID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
/****** Object:  Table [dbo].[RestaurantUsers]    Script Date: 09/15/2013 16:55:53 ******/
CREATE TABLE [dbo].[RestaurantUsers](
	[RestaurantId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_RestaurantUsers] PRIMARY KEY CLUSTERED 
(
	[RestaurantId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
GO
CREATE NONCLUSTERED INDEX [IX_FK_UserProfileRestaurantUser] ON [dbo].[RestaurantUsers] 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
/****** Object:  Table [dbo].[Payload]    Script Date: 09/15/2013 16:55:57 ******/
CREATE TABLE [dbo].[Payload](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LastReadFile] [nvarchar](max) NOT NULL,
	[RestaurantID] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_Payload] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
GO
/****** Object:  Table [dbo].[ItemCodes]    Script Date: 09/15/2013 16:56:04 ******/
CREATE TABLE [dbo].[ItemCodes](
	[ItemCode1] [int] NOT NULL,
	[RestaurantId] [int] NOT NULL,
	[ItemName] [nvarchar](256) NOT NULL,
	[LoyaltyEnabled] [bit] NOT NULL,
	[LoyaltyPoints] [int] NOT NULL,
	[LoyaltyMultiplier] [int] NOT NULL,
	[BonusPoints] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
 CONSTRAINT [PK_ItemCodes] PRIMARY KEY CLUSTERED 
(
	[ItemCode1] ASC,
	[RestaurantId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
GO
CREATE NONCLUSTERED INDEX [IX_FK_ItemCode_ToTableRestaurant] ON [dbo].[ItemCodes] 
(
	[RestaurantId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
/****** Object:  Table [dbo].[Generated_UniqueCode]    Script Date: 09/15/2013 16:56:09 ******/
CREATE TABLE [dbo].[Generated_UniqueCode](
	[UniqueCode] [nvarchar](20) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[RestaurantId] [int] NOT NULL,
	[IsValidated] [bit] NOT NULL,
	[DateValidated] [datetime] NULL,
	[IsAssigned] [bit] NOT NULL,
 CONSTRAINT [PK_Generated_UniqueCode] PRIMARY KEY CLUSTERED 
(
	[UniqueCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
GO
CREATE NONCLUSTERED INDEX [IX_FK_Generated_UniqueCodes_ToTableRestaurant] ON [dbo].[Generated_UniqueCode] 
(
	[RestaurantId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
/****** Object:  Table [dbo].[CreateNotifier_UniqueCodes]    Script Date: 09/15/2013 16:56:13 ******/
CREATE TABLE [dbo].[CreateNotifier_UniqueCodes](
	[Id] [int] NOT NULL,
	[RestaurantId] [int] NOT NULL,
	[IsCreateNewCodes] [bit] NOT NULL,
	[NumberofCodesToGenerate] [int] NOT NULL,
	[IsServiced] [bit] NOT NULL,
 CONSTRAINT [PK_CreateNotifier_UniqueCodes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
GO
CREATE NONCLUSTERED INDEX [IX_FK_CreateNotifier_UniqueCodes_ToRestaurant] ON [dbo].[CreateNotifier_UniqueCodes] 
(
	[RestaurantId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
/****** Object:  Table [dbo].[UniqueCode_UserMapping]    Script Date: 09/15/2013 16:56:18 ******/
CREATE TABLE [dbo].[UniqueCode_UserMapping](
	[UniqueCode] [nvarchar](20) NOT NULL,
	[EmailAddress] [nvarchar](50) NOT NULL,
	[RestaurantId] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_UniqueCode_UserMapping] PRIMARY KEY CLUSTERED 
(
	[UniqueCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
GO
CREATE NONCLUSTERED INDEX [IX_FK_UniqueCode_UserMapping_ToTableRestaurant] ON [dbo].[UniqueCode_UserMapping] 
(
	[RestaurantId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
/****** Object:  Table [dbo].[UniqueCode_Archive]    Script Date: 09/15/2013 16:56:21 ******/
CREATE TABLE [dbo].[UniqueCode_Archive](
	[UniqueCode] [nvarchar](20) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[RestaurantId] [int] NOT NULL,
	[DateValidated] [datetime] NULL,
 CONSTRAINT [PK_UniqueCode_Archive] PRIMARY KEY CLUSTERED 
(
	[UniqueCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
GO
CREATE NONCLUSTERED INDEX [IX_FK_UniqueCode_Archive_ToTableRestaurant] ON [dbo].[UniqueCode_Archive] 
(
	[RestaurantId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
/****** Object:  Table [dbo].[TempUsers]    Script Date: 09/15/2013 16:56:26 ******/
CREATE TABLE [dbo].[TempUsers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmailAddress] [nvarchar](max) NOT NULL,
	[RestaurantID] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_TempUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
GO
CREATE NONCLUSTERED INDEX [IX_FK_RestaurantTempUser] ON [dbo].[TempUsers] 
(
	[RestaurantID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
CREATE NONCLUSTERED INDEX [IX_FK_webpages_RolesTempUser] ON [dbo].[TempUsers] 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
/****** Object:  Table [dbo].[SettlementInfoes]    Script Date: 09/15/2013 16:56:31 ******/
CREATE TABLE [dbo].[SettlementInfoes](
	[BillNumber] [nvarchar](50) NOT NULL,
	[RestaurantId] [int] NOT NULL,
	[UniqueCode] [nvarchar](20) NOT NULL,
	[EmailAddress] [nvarchar](50) NULL,
	[IsServiced] [bit] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
	[SettlementDate] [datetime] NOT NULL,
 CONSTRAINT [PK_SettlementInfoes] PRIMARY KEY CLUSTERED 
(
	[BillNumber] ASC,
	[RestaurantId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
GO
CREATE NONCLUSTERED INDEX [IX_FK_SettlementInfo_ToTableRestaurant] ON [dbo].[SettlementInfoes] 
(
	[RestaurantId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
/****** Object:  Table [dbo].[SettlementInfo_Archive]    Script Date: 09/15/2013 16:56:37 ******/
CREATE TABLE [dbo].[SettlementInfo_Archive](
	[BillNumber] [nvarchar](50) NOT NULL,
	[RestaurantId] [int] NOT NULL,
	[UniqueCode] [nvarchar](20) NOT NULL,
	[EmailAddress] [nvarchar](50) NOT NULL,
	[IsServiced] [bit] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
	[SettlementDate] [datetime] NOT NULL,
 CONSTRAINT [PK_SettlementInfo_Archive] PRIMARY KEY CLUSTERED 
(
	[BillNumber] ASC,
	[RestaurantId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
GO
CREATE NONCLUSTERED INDEX [IX_FK_SettlementInfo_Archive_ToGenerated_UniqueCode] ON [dbo].[SettlementInfo_Archive] 
(
	[UniqueCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
CREATE NONCLUSTERED INDEX [IX_FK_SettlementInfo_Archive_ToRestaurant] ON [dbo].[SettlementInfo_Archive] 
(
	[RestaurantId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
/****** Object:  Table [dbo].[RunningCounts]    Script Date: 09/15/2013 16:56:43 ******/
CREATE TABLE [dbo].[RunningCounts](
	[EmailAddress] [nvarchar](50) NOT NULL,
	[RestaurantId] [int] NOT NULL,
	[RewardId] [int] NOT NULL,
	[RunningCount1] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
	[UpdateCount] [int] NOT NULL,
	[LastRunningCount] [int] NOT NULL,
 CONSTRAINT [PK_RunningCounts] PRIMARY KEY CLUSTERED 
(
	[EmailAddress] ASC,
	[RestaurantId] ASC,
	[RewardId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
GO
CREATE NONCLUSTERED INDEX [IX_FK_RunningCount_ToTableRestaurant] ON [dbo].[RunningCounts] 
(
	[RestaurantId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
CREATE NONCLUSTERED INDEX [IX_FK_RunningCount_ToTableRewards] ON [dbo].[RunningCounts] 
(
	[RewardId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
/****** Object:  StoredProcedure [dbo].[process_updateSettlementInfoes]    Script Date: 09/15/2013 16:56:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[process_updateSettlementInfoes](@TVP AS SettlementInfoesType READONLY)
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO [eMunching_Loyalty].[dbo].[settlementInfoes]
           ([BillNumber]
           ,[RestaurantId]
           ,[UniqueCode]
           ,[EmailAddress]
           ,[IsServiced]
           ,[DateCreated]
           ,[DateModified],
           [SettlementDate])
		  SELECT
			distinct(TVP.BillNumber)
				   ,TVP.RestaurantId
				   ,TVP.UniqueCode
				   ,TVP.EmailAddress
				   ,TVP.IsServiced
				   ,TVP.DateCreated
				   ,TVP.DateModified
				   ,TVP.SettlementDate
		  FROM
			@TVP AS TVP
		  WHERE
			NOT EXISTS (SELECT * FROM dbo.settlementInfoes AS I WHERE TVP.BillNumber = I.BillNumber)
		RETURN @@ROWCOUNT
END
GO
/****** Object:  StoredProcedure [dbo].[process_updateItemCodes]    Script Date: 09/15/2013 16:56:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[process_updateItemCodes](@TVP AS ItemCodeType READONLY)
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO [eMunching_Loyalty].[dbo].[ItemCodes]
           ([ItemCode1]
           ,[RestaurantId]
           ,[ItemName]
           ,[LoyaltyEnabled]
           ,[LoyaltyPoints]
           ,[LoyaltyMultiplier]
           ,[BonusPoints]
           ,[DateCreated]
           ,[DateModified])
		  SELECT
			distinct(TVP.ItemCode1)
				   ,TVP.RestaurantId
				   ,TVP.ItemName
				   ,TVP.LoyaltyEnabled
				   ,TVP.LoyaltyPoints
				   ,TVP.LoyaltyMultiplier
				   ,TVP.BonusPoints
				   ,TVP.DateCreated
				   ,TVP.DateModified
		  FROM
			@TVP AS TVP
		  WHERE
			NOT EXISTS (SELECT * FROM dbo.ItemCodes AS I WHERE TVP.ItemCode1 = I.ItemCode1)
		RETURN @@ROWCOUNT
END
GO
/****** Object:  Table [dbo].[Generated_CouponCode]    Script Date: 09/15/2013 16:56:51 ******/
CREATE TABLE [dbo].[Generated_CouponCode](
	[CouponCode] [nvarchar](20) NOT NULL,
	[RestaurantId] [int] NOT NULL,
	[RewardId] [int] NOT NULL,
	[EmailAddress] [nvarchar](50) NOT NULL,
	[IsAssigned] [bit] NOT NULL,
	[IsRedeemed] [bit] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateRedeemed] [datetime] NULL,
	[ExpirationDate] [datetime] NULL,
 CONSTRAINT [PK_Generated_CouponCode] PRIMARY KEY CLUSTERED 
(
	[CouponCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
GO
CREATE NONCLUSTERED INDEX [IX_FK_Generated_CouponCode_ToTableRestaurant] ON [dbo].[Generated_CouponCode] 
(
	[RestaurantId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
CREATE NONCLUSTERED INDEX [IX_FK_Generated_CouponCode_ToTableRewards] ON [dbo].[Generated_CouponCode] 
(
	[RewardId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 09/15/2013 16:56:57 ******/
CREATE TABLE [dbo].[OrderDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BillNumber] [nvarchar](50) NOT NULL,
	[ItemCode] [int] NOT NULL,
	[RestaurantId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
GO
CREATE NONCLUSTERED INDEX [IX_FK_OrderDetail_ToItemCode] ON [dbo].[OrderDetails] 
(
	[ItemCode] ASC,
	[RestaurantId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
CREATE NONCLUSTERED INDEX [IX_FK_OrderDetail_ToRestaurant] ON [dbo].[OrderDetails] 
(
	[RestaurantId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
CREATE NONCLUSTERED INDEX [IX_FK_OrderDetail_ToSettlementInfo] ON [dbo].[OrderDetails] 
(
	[BillNumber] ASC,
	[RestaurantId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
/****** Object:  Table [dbo].[OrderDetail_Archive]    Script Date: 09/15/2013 16:57:03 ******/
CREATE TABLE [dbo].[OrderDetail_Archive](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BillNumber] [nvarchar](50) NOT NULL,
	[ItemCode] [int] NOT NULL,
	[RestaurantId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_OrderDetail_Archive] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
GO
CREATE NONCLUSTERED INDEX [IX_FK_OrderDetail_Archive_ToItemCode] ON [dbo].[OrderDetail_Archive] 
(
	[ItemCode] ASC,
	[RestaurantId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
CREATE NONCLUSTERED INDEX [IX_FK_OrderDetail_Archive_ToRestaurant] ON [dbo].[OrderDetail_Archive] 
(
	[RestaurantId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
CREATE NONCLUSTERED INDEX [IX_FK_OrderDetail_Archive_ToSettlementInfo] ON [dbo].[OrderDetail_Archive] 
(
	[BillNumber] ASC,
	[RestaurantId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
/****** Object:  Table [dbo].[Admin_CouponCode_Mapping]    Script Date: 09/15/2013 16:57:08 ******/
CREATE TABLE [dbo].[Admin_CouponCode_Mapping](
	[CouponCode] [nvarchar](20) NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Admin_CouponCode_Mapping] PRIMARY KEY CLUSTERED 
(
	[CouponCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
GO
CREATE NONCLUSTERED INDEX [IX_FK_Admin_CouponCode_Mapping_ToTableGenerated_CouponCode] ON [dbo].[Admin_CouponCode_Mapping] 
(
	[CouponCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
CREATE NONCLUSTERED INDEX [IX_FK_Admin_CouponCode_Mapping_ToTableUserProfile] ON [dbo].[Admin_CouponCode_Mapping] 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
/****** Object:  Default [DF__webpages___IsCon__5535A963]    Script Date: 09/15/2013 16:55:29 ******/
ALTER TABLE [dbo].[webpages_Membership] ADD  DEFAULT ((0)) FOR [IsConfirmed]
GO
/****** Object:  Default [DF__webpages___Passw__5629CD9C]    Script Date: 09/15/2013 16:55:30 ******/
ALTER TABLE [dbo].[webpages_Membership] ADD  DEFAULT ((0)) FOR [PasswordFailuresSinceLastSuccess]
GO
/****** Object:  Default [DF__Rewards__Multipl__7A672E12]    Script Date: 09/15/2013 16:55:50 ******/
ALTER TABLE [dbo].[Rewards] ADD  DEFAULT ((1)) FOR [Multiplier]
GO
/****** Object:  ForeignKey [FK_Restaurant_ToLoyaltyType]    Script Date: 09/15/2013 16:55:44 ******/
ALTER TABLE [dbo].[Restaurants]  WITH CHECK ADD  CONSTRAINT [FK_Restaurant_ToLoyaltyType] FOREIGN KEY([LoyaltyID])
REFERENCES [dbo].[LoyaltyTypes] ([ID])
GO
ALTER TABLE [dbo].[Restaurants] CHECK CONSTRAINT [FK_Restaurant_ToLoyaltyType]
GO
/****** Object:  ForeignKey [FK_RestaurantReward]    Script Date: 09/15/2013 16:55:50 ******/
ALTER TABLE [dbo].[Rewards]  WITH CHECK ADD  CONSTRAINT [FK_RestaurantReward] FOREIGN KEY([RestaurantID])
REFERENCES [dbo].[Restaurants] ([ID])
GO
ALTER TABLE [dbo].[Rewards] CHECK CONSTRAINT [FK_RestaurantReward]
GO
/****** Object:  ForeignKey [FK_RestaurantUsers_ToRestaurants]    Script Date: 09/15/2013 16:55:53 ******/
ALTER TABLE [dbo].[RestaurantUsers]  WITH CHECK ADD  CONSTRAINT [FK_RestaurantUsers_ToRestaurants] FOREIGN KEY([RestaurantId])
REFERENCES [dbo].[Restaurants] ([ID])
GO
ALTER TABLE [dbo].[RestaurantUsers] CHECK CONSTRAINT [FK_RestaurantUsers_ToRestaurants]
GO
/****** Object:  ForeignKey [FK_UserProfileRestaurantUser]    Script Date: 09/15/2013 16:55:54 ******/
ALTER TABLE [dbo].[RestaurantUsers]  WITH CHECK ADD  CONSTRAINT [FK_UserProfileRestaurantUser] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserProfile] ([UserId])
GO
ALTER TABLE [dbo].[RestaurantUsers] CHECK CONSTRAINT [FK_UserProfileRestaurantUser]
GO
/****** Object:  ForeignKey [FK_RestaurantPayload]    Script Date: 09/15/2013 16:55:58 ******/
ALTER TABLE [dbo].[Payload]  WITH CHECK ADD  CONSTRAINT [FK_RestaurantPayload] FOREIGN KEY([RestaurantID])
REFERENCES [dbo].[Restaurants] ([ID])
GO
ALTER TABLE [dbo].[Payload] CHECK CONSTRAINT [FK_RestaurantPayload]
GO
/****** Object:  ForeignKey [FK_ItemCode_ToTableRestaurant]    Script Date: 09/15/2013 16:56:05 ******/
ALTER TABLE [dbo].[ItemCodes]  WITH CHECK ADD  CONSTRAINT [FK_ItemCode_ToTableRestaurant] FOREIGN KEY([RestaurantId])
REFERENCES [dbo].[Restaurants] ([ID])
GO
ALTER TABLE [dbo].[ItemCodes] CHECK CONSTRAINT [FK_ItemCode_ToTableRestaurant]
GO
/****** Object:  ForeignKey [FK_Generated_UniqueCodes_ToTableRestaurant]    Script Date: 09/15/2013 16:56:09 ******/
ALTER TABLE [dbo].[Generated_UniqueCode]  WITH NOCHECK ADD  CONSTRAINT [FK_Generated_UniqueCodes_ToTableRestaurant] FOREIGN KEY([RestaurantId])
REFERENCES [dbo].[Restaurants] ([ID])
GO
ALTER TABLE [dbo].[Generated_UniqueCode] CHECK CONSTRAINT [FK_Generated_UniqueCodes_ToTableRestaurant]
GO
/****** Object:  ForeignKey [FK_CreateNotifier_UniqueCodes_ToRestaurant]    Script Date: 09/15/2013 16:56:14 ******/
ALTER TABLE [dbo].[CreateNotifier_UniqueCodes]  WITH CHECK ADD  CONSTRAINT [FK_CreateNotifier_UniqueCodes_ToRestaurant] FOREIGN KEY([RestaurantId])
REFERENCES [dbo].[Restaurants] ([ID])
GO
ALTER TABLE [dbo].[CreateNotifier_UniqueCodes] CHECK CONSTRAINT [FK_CreateNotifier_UniqueCodes_ToRestaurant]
GO
/****** Object:  ForeignKey [FK_UniqueCode_UserMapping_ToTableRestaurant]    Script Date: 09/15/2013 16:56:18 ******/
ALTER TABLE [dbo].[UniqueCode_UserMapping]  WITH CHECK ADD  CONSTRAINT [FK_UniqueCode_UserMapping_ToTableRestaurant] FOREIGN KEY([RestaurantId])
REFERENCES [dbo].[Restaurants] ([ID])
GO
ALTER TABLE [dbo].[UniqueCode_UserMapping] CHECK CONSTRAINT [FK_UniqueCode_UserMapping_ToTableRestaurant]
GO
/****** Object:  ForeignKey [FK_UniqueCode_Archive_ToTableRestaurant]    Script Date: 09/15/2013 16:56:22 ******/
ALTER TABLE [dbo].[UniqueCode_Archive]  WITH CHECK ADD  CONSTRAINT [FK_UniqueCode_Archive_ToTableRestaurant] FOREIGN KEY([RestaurantId])
REFERENCES [dbo].[Restaurants] ([ID])
GO
ALTER TABLE [dbo].[UniqueCode_Archive] CHECK CONSTRAINT [FK_UniqueCode_Archive_ToTableRestaurant]
GO
/****** Object:  ForeignKey [FK_RestaurantTempUser]    Script Date: 09/15/2013 16:56:26 ******/
ALTER TABLE [dbo].[TempUsers]  WITH CHECK ADD  CONSTRAINT [FK_RestaurantTempUser] FOREIGN KEY([RestaurantID])
REFERENCES [dbo].[Restaurants] ([ID])
GO
ALTER TABLE [dbo].[TempUsers] CHECK CONSTRAINT [FK_RestaurantTempUser]
GO
/****** Object:  ForeignKey [FK_webpages_RolesTempUser]    Script Date: 09/15/2013 16:56:27 ******/
ALTER TABLE [dbo].[TempUsers]  WITH CHECK ADD  CONSTRAINT [FK_webpages_RolesTempUser] FOREIGN KEY([RoleId])
REFERENCES [dbo].[webpages_Roles] ([RoleId])
GO
ALTER TABLE [dbo].[TempUsers] CHECK CONSTRAINT [FK_webpages_RolesTempUser]
GO
/****** Object:  ForeignKey [FK_SettlementInfo_ToTableRestaurant]    Script Date: 09/15/2013 16:56:32 ******/
ALTER TABLE [dbo].[SettlementInfoes]  WITH CHECK ADD  CONSTRAINT [FK_SettlementInfo_ToTableRestaurant] FOREIGN KEY([RestaurantId])
REFERENCES [dbo].[Restaurants] ([ID])
GO
ALTER TABLE [dbo].[SettlementInfoes] CHECK CONSTRAINT [FK_SettlementInfo_ToTableRestaurant]
GO
/****** Object:  ForeignKey [FK_SettlementInfo_Archive_ToGenerated_UniqueCode]    Script Date: 09/15/2013 16:56:38 ******/
ALTER TABLE [dbo].[SettlementInfo_Archive]  WITH CHECK ADD  CONSTRAINT [FK_SettlementInfo_Archive_ToGenerated_UniqueCode] FOREIGN KEY([UniqueCode])
REFERENCES [dbo].[Generated_UniqueCode] ([UniqueCode])
GO
ALTER TABLE [dbo].[SettlementInfo_Archive] CHECK CONSTRAINT [FK_SettlementInfo_Archive_ToGenerated_UniqueCode]
GO
/****** Object:  ForeignKey [FK_SettlementInfo_Archive_ToRestaurant]    Script Date: 09/15/2013 16:56:38 ******/
ALTER TABLE [dbo].[SettlementInfo_Archive]  WITH CHECK ADD  CONSTRAINT [FK_SettlementInfo_Archive_ToRestaurant] FOREIGN KEY([RestaurantId])
REFERENCES [dbo].[Restaurants] ([ID])
GO
ALTER TABLE [dbo].[SettlementInfo_Archive] CHECK CONSTRAINT [FK_SettlementInfo_Archive_ToRestaurant]
GO
/****** Object:  ForeignKey [FK_RunningCount_ToTableRestaurant]    Script Date: 09/15/2013 16:56:44 ******/
ALTER TABLE [dbo].[RunningCounts]  WITH CHECK ADD  CONSTRAINT [FK_RunningCount_ToTableRestaurant] FOREIGN KEY([RestaurantId])
REFERENCES [dbo].[Restaurants] ([ID])
GO
ALTER TABLE [dbo].[RunningCounts] CHECK CONSTRAINT [FK_RunningCount_ToTableRestaurant]
GO
/****** Object:  ForeignKey [FK_RunningCount_ToTableRewards]    Script Date: 09/15/2013 16:56:44 ******/
ALTER TABLE [dbo].[RunningCounts]  WITH CHECK ADD  CONSTRAINT [FK_RunningCount_ToTableRewards] FOREIGN KEY([RewardId])
REFERENCES [dbo].[Rewards] ([Id])
GO
ALTER TABLE [dbo].[RunningCounts] CHECK CONSTRAINT [FK_RunningCount_ToTableRewards]
GO
/****** Object:  ForeignKey [FK_Generated_CouponCode_ToTableRestaurant]    Script Date: 09/15/2013 16:56:51 ******/
ALTER TABLE [dbo].[Generated_CouponCode]  WITH CHECK ADD  CONSTRAINT [FK_Generated_CouponCode_ToTableRestaurant] FOREIGN KEY([RestaurantId])
REFERENCES [dbo].[Restaurants] ([ID])
GO
ALTER TABLE [dbo].[Generated_CouponCode] CHECK CONSTRAINT [FK_Generated_CouponCode_ToTableRestaurant]
GO
/****** Object:  ForeignKey [FK_Generated_CouponCode_ToTableRewards]    Script Date: 09/15/2013 16:56:52 ******/
ALTER TABLE [dbo].[Generated_CouponCode]  WITH CHECK ADD  CONSTRAINT [FK_Generated_CouponCode_ToTableRewards] FOREIGN KEY([RewardId])
REFERENCES [dbo].[Rewards] ([Id])
GO
ALTER TABLE [dbo].[Generated_CouponCode] CHECK CONSTRAINT [FK_Generated_CouponCode_ToTableRewards]
GO
/****** Object:  ForeignKey [FK_OrderDetail_ToItemCode]    Script Date: 09/15/2013 16:56:57 ******/
ALTER TABLE [dbo].[OrderDetails]  WITH NOCHECK ADD  CONSTRAINT [FK_OrderDetail_ToItemCode] FOREIGN KEY([ItemCode], [RestaurantId])
REFERENCES [dbo].[ItemCodes] ([ItemCode1], [RestaurantId])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetail_ToItemCode]
GO
/****** Object:  ForeignKey [FK_OrderDetail_ToRestaurant]    Script Date: 09/15/2013 16:56:58 ******/
ALTER TABLE [dbo].[OrderDetails]  WITH NOCHECK ADD  CONSTRAINT [FK_OrderDetail_ToRestaurant] FOREIGN KEY([RestaurantId])
REFERENCES [dbo].[Restaurants] ([ID])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetail_ToRestaurant]
GO
/****** Object:  ForeignKey [FK_OrderDetail_ToSettlementInfo]    Script Date: 09/15/2013 16:56:59 ******/
ALTER TABLE [dbo].[OrderDetails]  WITH NOCHECK ADD  CONSTRAINT [FK_OrderDetail_ToSettlementInfo] FOREIGN KEY([BillNumber], [RestaurantId])
REFERENCES [dbo].[SettlementInfoes] ([BillNumber], [RestaurantId])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetail_ToSettlementInfo]
GO
/****** Object:  ForeignKey [FK_OrderDetail_Archive_ToItemCode]    Script Date: 09/15/2013 16:57:04 ******/
ALTER TABLE [dbo].[OrderDetail_Archive]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Archive_ToItemCode] FOREIGN KEY([ItemCode], [RestaurantId])
REFERENCES [dbo].[ItemCodes] ([ItemCode1], [RestaurantId])
GO
ALTER TABLE [dbo].[OrderDetail_Archive] CHECK CONSTRAINT [FK_OrderDetail_Archive_ToItemCode]
GO
/****** Object:  ForeignKey [FK_OrderDetail_Archive_ToRestaurant]    Script Date: 09/15/2013 16:57:04 ******/
ALTER TABLE [dbo].[OrderDetail_Archive]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Archive_ToRestaurant] FOREIGN KEY([RestaurantId])
REFERENCES [dbo].[Restaurants] ([ID])
GO
ALTER TABLE [dbo].[OrderDetail_Archive] CHECK CONSTRAINT [FK_OrderDetail_Archive_ToRestaurant]
GO
/****** Object:  ForeignKey [FK_OrderDetail_Archive_ToSettlementInfo]    Script Date: 09/15/2013 16:57:05 ******/
ALTER TABLE [dbo].[OrderDetail_Archive]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Archive_ToSettlementInfo] FOREIGN KEY([BillNumber], [RestaurantId])
REFERENCES [dbo].[SettlementInfoes] ([BillNumber], [RestaurantId])
GO
ALTER TABLE [dbo].[OrderDetail_Archive] CHECK CONSTRAINT [FK_OrderDetail_Archive_ToSettlementInfo]
GO
/****** Object:  ForeignKey [FK_Admin_CouponCode_Mapping_ToTableGenerated_CouponCode]    Script Date: 09/15/2013 16:57:08 ******/
ALTER TABLE [dbo].[Admin_CouponCode_Mapping]  WITH CHECK ADD  CONSTRAINT [FK_Admin_CouponCode_Mapping_ToTableGenerated_CouponCode] FOREIGN KEY([CouponCode])
REFERENCES [dbo].[Generated_CouponCode] ([CouponCode])
GO
ALTER TABLE [dbo].[Admin_CouponCode_Mapping] CHECK CONSTRAINT [FK_Admin_CouponCode_Mapping_ToTableGenerated_CouponCode]
GO
/****** Object:  ForeignKey [FK_Admin_CouponCode_Mapping_ToTableUserProfile]    Script Date: 09/15/2013 16:57:09 ******/
ALTER TABLE [dbo].[Admin_CouponCode_Mapping]  WITH CHECK ADD  CONSTRAINT [FK_Admin_CouponCode_Mapping_ToTableUserProfile] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserProfile] ([UserId])
GO
ALTER TABLE [dbo].[Admin_CouponCode_Mapping] CHECK CONSTRAINT [FK_Admin_CouponCode_Mapping_ToTableUserProfile]
GO
