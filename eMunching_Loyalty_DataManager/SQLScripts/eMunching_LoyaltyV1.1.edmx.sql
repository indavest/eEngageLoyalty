
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 01/29/2013 02:33:00
-- Generated from EDMX file: D:\Code\eMunchingDev\trunk\eEngageLoyalty\eMunching_Loyalty_DataManager\eMunching_Loyalty.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [eMunching_Loyalty];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CreateNotifier_UniqueCodes_ToRestaurant]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CreateNotifier_UniqueCodes] DROP CONSTRAINT [FK_CreateNotifier_UniqueCodes_ToRestaurant];
GO
IF OBJECT_ID(N'[dbo].[FK_Generated_CouponCode_ToTableRestaurant]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Generated_CouponCode] DROP CONSTRAINT [FK_Generated_CouponCode_ToTableRestaurant];
GO
IF OBJECT_ID(N'[dbo].[FK_Generated_UniqueCodes_ToTableRestaurant]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Generated_UniqueCode] DROP CONSTRAINT [FK_Generated_UniqueCodes_ToTableRestaurant];
GO
IF OBJECT_ID(N'[dbo].[FK_ItemCode_ToTableRestaurant]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ItemCode] DROP CONSTRAINT [FK_ItemCode_ToTableRestaurant];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderDetail_Archive_ToItemCode]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderDetail_Archive] DROP CONSTRAINT [FK_OrderDetail_Archive_ToItemCode];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderDetail_Archive_ToRestaurant]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderDetail_Archive] DROP CONSTRAINT [FK_OrderDetail_Archive_ToRestaurant];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderDetail_Archive_ToSettlementInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderDetail_Archive] DROP CONSTRAINT [FK_OrderDetail_Archive_ToSettlementInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderDetail_ToItemCode]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderDetail] DROP CONSTRAINT [FK_OrderDetail_ToItemCode];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderDetail_ToRestaurant]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderDetail] DROP CONSTRAINT [FK_OrderDetail_ToRestaurant];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderDetail_ToSettlementInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderDetail] DROP CONSTRAINT [FK_OrderDetail_ToSettlementInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_Restaurant_ToLoyaltyType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Restaurant] DROP CONSTRAINT [FK_Restaurant_ToLoyaltyType];
GO
IF OBJECT_ID(N'[dbo].[FK_RestaurantUsers_ToRestaurants]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RestaurantUsers] DROP CONSTRAINT [FK_RestaurantUsers_ToRestaurants];
GO
IF OBJECT_ID(N'[dbo].[FK_RunningCount_ToTableRestaurant]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RunningCount] DROP CONSTRAINT [FK_RunningCount_ToTableRestaurant];
GO
IF OBJECT_ID(N'[dbo].[FK_SettlementInfo_Archive_ToGenerated_UniqueCode]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SettlementInfo_Archive] DROP CONSTRAINT [FK_SettlementInfo_Archive_ToGenerated_UniqueCode];
GO
IF OBJECT_ID(N'[dbo].[FK_SettlementInfo_Archive_ToRestaurant]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SettlementInfo_Archive] DROP CONSTRAINT [FK_SettlementInfo_Archive_ToRestaurant];
GO
IF OBJECT_ID(N'[dbo].[FK_SettlementInfo_ToTableRestaurant]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SettlementInfo] DROP CONSTRAINT [FK_SettlementInfo_ToTableRestaurant];
GO
IF OBJECT_ID(N'[dbo].[FK_UniqueCode_Archive_ToTableRestaurant]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UniqueCode_Archive] DROP CONSTRAINT [FK_UniqueCode_Archive_ToTableRestaurant];
GO
IF OBJECT_ID(N'[dbo].[FK_UniqueCode_UserMapping_ToTableRestaurant]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UniqueCode_UserMapping] DROP CONSTRAINT [FK_UniqueCode_UserMapping_ToTableRestaurant];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[CouponCode_UniqueCodeMapping]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CouponCode_UniqueCodeMapping];
GO
IF OBJECT_ID(N'[dbo].[CreateNotifier_UniqueCodes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CreateNotifier_UniqueCodes];
GO
IF OBJECT_ID(N'[dbo].[Generated_CouponCode]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Generated_CouponCode];
GO
IF OBJECT_ID(N'[dbo].[Generated_UniqueCode]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Generated_UniqueCode];
GO
IF OBJECT_ID(N'[dbo].[ItemCode]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ItemCode];
GO
IF OBJECT_ID(N'[dbo].[LoyaltyType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LoyaltyType];
GO
IF OBJECT_ID(N'[dbo].[OrderDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderDetail];
GO
IF OBJECT_ID(N'[dbo].[OrderDetail_Archive]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderDetail_Archive];
GO
IF OBJECT_ID(N'[dbo].[Restaurant]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Restaurant];
GO
IF OBJECT_ID(N'[dbo].[RestaurantUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RestaurantUsers];
GO
IF OBJECT_ID(N'[dbo].[RunningCount]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RunningCount];
GO
IF OBJECT_ID(N'[dbo].[SettlementInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SettlementInfo];
GO
IF OBJECT_ID(N'[dbo].[SettlementInfo_Archive]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SettlementInfo_Archive];
GO
IF OBJECT_ID(N'[dbo].[UniqueCode_Archive]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UniqueCode_Archive];
GO
IF OBJECT_ID(N'[dbo].[UniqueCode_UserMapping]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UniqueCode_UserMapping];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'CouponCode_UniqueCodeMapping'
CREATE TABLE [dbo].[CouponCode_UniqueCodeMapping] (
    [ID] uniqueidentifier  NOT NULL,
    [CouponCode] nvarchar(20)  NOT NULL,
    [UniqueCode] nvarchar(20)  NOT NULL
);
GO

-- Creating table 'Generated_CouponCode'
CREATE TABLE [dbo].[Generated_CouponCode] (
    [CouponCode] nvarchar(20)  NOT NULL,
    [RestaurantId] int  NOT NULL,
    [EmailAddress] nvarchar(50)  NOT NULL,
    [IsAssigned] bit  NOT NULL,
    [IsRedeemed] bit  NOT NULL,
    [DateCreated] datetime  NOT NULL,
    [DateRedeemed] datetime  NULL
);
GO

-- Creating table 'Generated_UniqueCode'
CREATE TABLE [dbo].[Generated_UniqueCode] (
    [UniqueCode] nvarchar(20)  NOT NULL,
    [DateCreated] datetime  NOT NULL,
    [RestaurantId] int  NOT NULL,
    [IsValidated] bit  NOT NULL,
    [DateValidated] datetime  NULL,
    [IsAssigned] bit  NOT NULL
);
GO

-- Creating table 'ItemCodes'
CREATE TABLE [dbo].[ItemCodes] (
    [ItemCode1] int  NOT NULL,
    [RestaurantId] int  NOT NULL,
    [ItemName] nvarchar(256)  NOT NULL,
    [LoyaltyEnabled] bit  NOT NULL,
    [LoyaltyPoints] int  NOT NULL,
    [LoyaltyMultiplier] int  NOT NULL,
    [BonusPoints] int  NOT NULL,
    [DateCreated] datetime  NOT NULL,
    [DateModified] datetime  NOT NULL
);
GO

-- Creating table 'LoyaltyTypes'
CREATE TABLE [dbo].[LoyaltyTypes] (
    [ID] int  NOT NULL,
    [LoyaltyType1] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'OrderDetails'
CREATE TABLE [dbo].[OrderDetails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [BillNumber] nvarchar(50)  NOT NULL,
    [ItemCode] int  NOT NULL,
    [RestaurantId] int  NOT NULL,
    [Quantity] int  NOT NULL
);
GO

-- Creating table 'OrderDetail_Archive'
CREATE TABLE [dbo].[OrderDetail_Archive] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [BillNumber] nvarchar(50)  NOT NULL,
    [ItemCode] int  NOT NULL,
    [RestaurantId] int  NOT NULL,
    [Quantity] int  NOT NULL
);
GO

-- Creating table 'Restaurants'
CREATE TABLE [dbo].[Restaurants] (
    [ID] int  NOT NULL,
    [RestaurantName] nvarchar(50)  NOT NULL,
    [LoyaltyID] int  NULL
);
GO

-- Creating table 'RunningCounts'
CREATE TABLE [dbo].[RunningCounts] (
    [EmailAddress] nvarchar(50)  NOT NULL,
    [RestaurantId] int  NOT NULL,
    [RunningCount1] int  NOT NULL,
    [DateCreated] datetime  NOT NULL,
    [DateModified] datetime  NOT NULL,
    [UpdateCount] int  NOT NULL,
    [LastRunningCount] int  NOT NULL
);
GO

-- Creating table 'SettlementInfoes'
CREATE TABLE [dbo].[SettlementInfoes] (
    [BillNumber] nvarchar(50)  NOT NULL,
    [RestaurantId] int  NOT NULL,
    [UniqueCode] nvarchar(20)  NOT NULL,
    [EmailAddress] nvarchar(50)  NULL,
    [IsServiced] bit  NOT NULL,
    [DateCreated] datetime  NOT NULL,
    [DateModified] datetime  NOT NULL,
    [SettlementDate] datetime  NOT NULL
);
GO

-- Creating table 'SettlementInfo_Archive'
CREATE TABLE [dbo].[SettlementInfo_Archive] (
    [BillNumber] nvarchar(50)  NOT NULL,
    [RestaurantId] int  NOT NULL,
    [UniqueCode] nvarchar(20)  NOT NULL,
    [EmailAddress] nvarchar(50)  NOT NULL,
    [IsServiced] bit  NOT NULL,
    [DateCreated] datetime  NOT NULL,
    [DateModified] datetime  NOT NULL,
    [SettlementDate] datetime  NOT NULL
);
GO

-- Creating table 'UniqueCode_UserMapping'
CREATE TABLE [dbo].[UniqueCode_UserMapping] (
    [UniqueCode] nvarchar(20)  NOT NULL,
    [EmailAddress] nvarchar(50)  NOT NULL,
    [RestaurantId] int  NOT NULL,
    [DateCreated] datetime  NOT NULL
);
GO

-- Creating table 'CreateNotifier_UniqueCodes'
CREATE TABLE [dbo].[CreateNotifier_UniqueCodes] (
    [Id] int  NOT NULL,
    [RestaurantId] int  NOT NULL,
    [IsCreateNewCodes] bit  NOT NULL,
    [NumberofCodesToGenerate] int  NOT NULL,
    [IsServiced] bit  NOT NULL
);
GO

-- Creating table 'UniqueCode_Archive'
CREATE TABLE [dbo].[UniqueCode_Archive] (
    [UniqueCode] nvarchar(20)  NOT NULL,
    [DateCreated] datetime  NOT NULL,
    [RestaurantId] int  NOT NULL,
    [DateValidated] datetime  NULL
);
GO

-- Creating table 'RestaurantUsers'
CREATE TABLE [dbo].[RestaurantUsers] (
    [RestaurantId] int  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'CouponCode_UniqueCodeMapping'
ALTER TABLE [dbo].[CouponCode_UniqueCodeMapping]
ADD CONSTRAINT [PK_CouponCode_UniqueCodeMapping]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [CouponCode] in table 'Generated_CouponCode'
ALTER TABLE [dbo].[Generated_CouponCode]
ADD CONSTRAINT [PK_Generated_CouponCode]
    PRIMARY KEY CLUSTERED ([CouponCode] ASC);
GO

-- Creating primary key on [UniqueCode] in table 'Generated_UniqueCode'
ALTER TABLE [dbo].[Generated_UniqueCode]
ADD CONSTRAINT [PK_Generated_UniqueCode]
    PRIMARY KEY CLUSTERED ([UniqueCode] ASC);
GO

-- Creating primary key on [ItemCode1], [RestaurantId] in table 'ItemCodes'
ALTER TABLE [dbo].[ItemCodes]
ADD CONSTRAINT [PK_ItemCodes]
    PRIMARY KEY CLUSTERED ([ItemCode1], [RestaurantId] ASC);
GO

-- Creating primary key on [ID] in table 'LoyaltyTypes'
ALTER TABLE [dbo].[LoyaltyTypes]
ADD CONSTRAINT [PK_LoyaltyTypes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Id] in table 'OrderDetails'
ALTER TABLE [dbo].[OrderDetails]
ADD CONSTRAINT [PK_OrderDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrderDetail_Archive'
ALTER TABLE [dbo].[OrderDetail_Archive]
ADD CONSTRAINT [PK_OrderDetail_Archive]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ID] in table 'Restaurants'
ALTER TABLE [dbo].[Restaurants]
ADD CONSTRAINT [PK_Restaurants]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [EmailAddress], [RestaurantId] in table 'RunningCounts'
ALTER TABLE [dbo].[RunningCounts]
ADD CONSTRAINT [PK_RunningCounts]
    PRIMARY KEY CLUSTERED ([EmailAddress], [RestaurantId] ASC);
GO

-- Creating primary key on [BillNumber], [RestaurantId] in table 'SettlementInfoes'
ALTER TABLE [dbo].[SettlementInfoes]
ADD CONSTRAINT [PK_SettlementInfoes]
    PRIMARY KEY CLUSTERED ([BillNumber], [RestaurantId] ASC);
GO

-- Creating primary key on [BillNumber], [RestaurantId] in table 'SettlementInfo_Archive'
ALTER TABLE [dbo].[SettlementInfo_Archive]
ADD CONSTRAINT [PK_SettlementInfo_Archive]
    PRIMARY KEY CLUSTERED ([BillNumber], [RestaurantId] ASC);
GO

-- Creating primary key on [UniqueCode] in table 'UniqueCode_UserMapping'
ALTER TABLE [dbo].[UniqueCode_UserMapping]
ADD CONSTRAINT [PK_UniqueCode_UserMapping]
    PRIMARY KEY CLUSTERED ([UniqueCode] ASC);
GO

-- Creating primary key on [Id] in table 'CreateNotifier_UniqueCodes'
ALTER TABLE [dbo].[CreateNotifier_UniqueCodes]
ADD CONSTRAINT [PK_CreateNotifier_UniqueCodes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [UniqueCode] in table 'UniqueCode_Archive'
ALTER TABLE [dbo].[UniqueCode_Archive]
ADD CONSTRAINT [PK_UniqueCode_Archive]
    PRIMARY KEY CLUSTERED ([UniqueCode] ASC);
GO

-- Creating primary key on [RestaurantId], [UserId] in table 'RestaurantUsers'
ALTER TABLE [dbo].[RestaurantUsers]
ADD CONSTRAINT [PK_RestaurantUsers]
    PRIMARY KEY NONCLUSTERED ([RestaurantId], [UserId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [RestaurantId] in table 'Generated_CouponCode'
ALTER TABLE [dbo].[Generated_CouponCode]
ADD CONSTRAINT [FK_Generated_CouponCode_ToTableRestaurant]
    FOREIGN KEY ([RestaurantId])
    REFERENCES [dbo].[Restaurants]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Generated_CouponCode_ToTableRestaurant'
CREATE INDEX [IX_FK_Generated_CouponCode_ToTableRestaurant]
ON [dbo].[Generated_CouponCode]
    ([RestaurantId]);
GO

-- Creating foreign key on [RestaurantId] in table 'Generated_UniqueCode'
ALTER TABLE [dbo].[Generated_UniqueCode]
ADD CONSTRAINT [FK_Generated_UniqueCodes_ToTableRestaurant]
    FOREIGN KEY ([RestaurantId])
    REFERENCES [dbo].[Restaurants]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Generated_UniqueCodes_ToTableRestaurant'
CREATE INDEX [IX_FK_Generated_UniqueCodes_ToTableRestaurant]
ON [dbo].[Generated_UniqueCode]
    ([RestaurantId]);
GO

-- Creating foreign key on [UniqueCode] in table 'SettlementInfo_Archive'
ALTER TABLE [dbo].[SettlementInfo_Archive]
ADD CONSTRAINT [FK_SettlementInfo_Archive_ToGenerated_UniqueCode]
    FOREIGN KEY ([UniqueCode])
    REFERENCES [dbo].[Generated_UniqueCode]
        ([UniqueCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SettlementInfo_Archive_ToGenerated_UniqueCode'
CREATE INDEX [IX_FK_SettlementInfo_Archive_ToGenerated_UniqueCode]
ON [dbo].[SettlementInfo_Archive]
    ([UniqueCode]);
GO

-- Creating foreign key on [RestaurantId] in table 'ItemCodes'
ALTER TABLE [dbo].[ItemCodes]
ADD CONSTRAINT [FK_ItemCode_ToTableRestaurant]
    FOREIGN KEY ([RestaurantId])
    REFERENCES [dbo].[Restaurants]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ItemCode_ToTableRestaurant'
CREATE INDEX [IX_FK_ItemCode_ToTableRestaurant]
ON [dbo].[ItemCodes]
    ([RestaurantId]);
GO

-- Creating foreign key on [ItemCode], [RestaurantId] in table 'OrderDetail_Archive'
ALTER TABLE [dbo].[OrderDetail_Archive]
ADD CONSTRAINT [FK_OrderDetail_Archive_ToItemCode]
    FOREIGN KEY ([ItemCode], [RestaurantId])
    REFERENCES [dbo].[ItemCodes]
        ([ItemCode1], [RestaurantId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderDetail_Archive_ToItemCode'
CREATE INDEX [IX_FK_OrderDetail_Archive_ToItemCode]
ON [dbo].[OrderDetail_Archive]
    ([ItemCode], [RestaurantId]);
GO

-- Creating foreign key on [ItemCode], [RestaurantId] in table 'OrderDetails'
ALTER TABLE [dbo].[OrderDetails]
ADD CONSTRAINT [FK_OrderDetail_ToItemCode]
    FOREIGN KEY ([ItemCode], [RestaurantId])
    REFERENCES [dbo].[ItemCodes]
        ([ItemCode1], [RestaurantId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderDetail_ToItemCode'
CREATE INDEX [IX_FK_OrderDetail_ToItemCode]
ON [dbo].[OrderDetails]
    ([ItemCode], [RestaurantId]);
GO

-- Creating foreign key on [LoyaltyID] in table 'Restaurants'
ALTER TABLE [dbo].[Restaurants]
ADD CONSTRAINT [FK_Restaurant_ToLoyaltyType]
    FOREIGN KEY ([LoyaltyID])
    REFERENCES [dbo].[LoyaltyTypes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Restaurant_ToLoyaltyType'
CREATE INDEX [IX_FK_Restaurant_ToLoyaltyType]
ON [dbo].[Restaurants]
    ([LoyaltyID]);
GO

-- Creating foreign key on [RestaurantId] in table 'OrderDetails'
ALTER TABLE [dbo].[OrderDetails]
ADD CONSTRAINT [FK_OrderDetail_ToRestaurant]
    FOREIGN KEY ([RestaurantId])
    REFERENCES [dbo].[Restaurants]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderDetail_ToRestaurant'
CREATE INDEX [IX_FK_OrderDetail_ToRestaurant]
ON [dbo].[OrderDetails]
    ([RestaurantId]);
GO

-- Creating foreign key on [BillNumber], [RestaurantId] in table 'OrderDetails'
ALTER TABLE [dbo].[OrderDetails]
ADD CONSTRAINT [FK_OrderDetail_ToSettlementInfo]
    FOREIGN KEY ([BillNumber], [RestaurantId])
    REFERENCES [dbo].[SettlementInfoes]
        ([BillNumber], [RestaurantId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderDetail_ToSettlementInfo'
CREATE INDEX [IX_FK_OrderDetail_ToSettlementInfo]
ON [dbo].[OrderDetails]
    ([BillNumber], [RestaurantId]);
GO

-- Creating foreign key on [RestaurantId] in table 'OrderDetail_Archive'
ALTER TABLE [dbo].[OrderDetail_Archive]
ADD CONSTRAINT [FK_OrderDetail_Archive_ToRestaurant]
    FOREIGN KEY ([RestaurantId])
    REFERENCES [dbo].[Restaurants]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderDetail_Archive_ToRestaurant'
CREATE INDEX [IX_FK_OrderDetail_Archive_ToRestaurant]
ON [dbo].[OrderDetail_Archive]
    ([RestaurantId]);
GO

-- Creating foreign key on [BillNumber], [RestaurantId] in table 'OrderDetail_Archive'
ALTER TABLE [dbo].[OrderDetail_Archive]
ADD CONSTRAINT [FK_OrderDetail_Archive_ToSettlementInfo]
    FOREIGN KEY ([BillNumber], [RestaurantId])
    REFERENCES [dbo].[SettlementInfoes]
        ([BillNumber], [RestaurantId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderDetail_Archive_ToSettlementInfo'
CREATE INDEX [IX_FK_OrderDetail_Archive_ToSettlementInfo]
ON [dbo].[OrderDetail_Archive]
    ([BillNumber], [RestaurantId]);
GO

-- Creating foreign key on [RestaurantId] in table 'RunningCounts'
ALTER TABLE [dbo].[RunningCounts]
ADD CONSTRAINT [FK_RunningCount_ToTableRestaurant]
    FOREIGN KEY ([RestaurantId])
    REFERENCES [dbo].[Restaurants]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RunningCount_ToTableRestaurant'
CREATE INDEX [IX_FK_RunningCount_ToTableRestaurant]
ON [dbo].[RunningCounts]
    ([RestaurantId]);
GO

-- Creating foreign key on [RestaurantId] in table 'SettlementInfo_Archive'
ALTER TABLE [dbo].[SettlementInfo_Archive]
ADD CONSTRAINT [FK_SettlementInfo_Archive_ToRestaurant]
    FOREIGN KEY ([RestaurantId])
    REFERENCES [dbo].[Restaurants]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SettlementInfo_Archive_ToRestaurant'
CREATE INDEX [IX_FK_SettlementInfo_Archive_ToRestaurant]
ON [dbo].[SettlementInfo_Archive]
    ([RestaurantId]);
GO

-- Creating foreign key on [RestaurantId] in table 'SettlementInfoes'
ALTER TABLE [dbo].[SettlementInfoes]
ADD CONSTRAINT [FK_SettlementInfo_ToTableRestaurant]
    FOREIGN KEY ([RestaurantId])
    REFERENCES [dbo].[Restaurants]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SettlementInfo_ToTableRestaurant'
CREATE INDEX [IX_FK_SettlementInfo_ToTableRestaurant]
ON [dbo].[SettlementInfoes]
    ([RestaurantId]);
GO

-- Creating foreign key on [RestaurantId] in table 'UniqueCode_UserMapping'
ALTER TABLE [dbo].[UniqueCode_UserMapping]
ADD CONSTRAINT [FK_UniqueCode_UserMapping_ToTableRestaurant]
    FOREIGN KEY ([RestaurantId])
    REFERENCES [dbo].[Restaurants]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UniqueCode_UserMapping_ToTableRestaurant'
CREATE INDEX [IX_FK_UniqueCode_UserMapping_ToTableRestaurant]
ON [dbo].[UniqueCode_UserMapping]
    ([RestaurantId]);
GO

-- Creating foreign key on [RestaurantId] in table 'CreateNotifier_UniqueCodes'
ALTER TABLE [dbo].[CreateNotifier_UniqueCodes]
ADD CONSTRAINT [FK_CreateNotifier_UniqueCodes_ToRestaurant]
    FOREIGN KEY ([RestaurantId])
    REFERENCES [dbo].[Restaurants]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CreateNotifier_UniqueCodes_ToRestaurant'
CREATE INDEX [IX_FK_CreateNotifier_UniqueCodes_ToRestaurant]
ON [dbo].[CreateNotifier_UniqueCodes]
    ([RestaurantId]);
GO

-- Creating foreign key on [RestaurantId] in table 'UniqueCode_Archive'
ALTER TABLE [dbo].[UniqueCode_Archive]
ADD CONSTRAINT [FK_UniqueCode_Archive_ToTableRestaurant]
    FOREIGN KEY ([RestaurantId])
    REFERENCES [dbo].[Restaurants]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UniqueCode_Archive_ToTableRestaurant'
CREATE INDEX [IX_FK_UniqueCode_Archive_ToTableRestaurant]
ON [dbo].[UniqueCode_Archive]
    ([RestaurantId]);
GO

-- Creating foreign key on [RestaurantId] in table 'RestaurantUsers'
ALTER TABLE [dbo].[RestaurantUsers]
ADD CONSTRAINT [FK_RestaurantUsers_ToRestaurants]
    FOREIGN KEY ([RestaurantId])
    REFERENCES [dbo].[Restaurants]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------