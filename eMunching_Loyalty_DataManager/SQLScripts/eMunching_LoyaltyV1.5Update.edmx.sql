------------------------------------------------------------------------
----------------- [RunningCounts] Updates -----------------------
------------------------------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_RunningCount_ToTableRestaurant]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RunningCounts] DROP CONSTRAINT [FK_RunningCount_ToTableRestaurant];
GO

IF OBJECT_ID(N'[dbo].[RunningCounts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RunningCounts];
GO

-- Creating table 'RunningCounts'
CREATE TABLE [dbo].[RunningCounts] (
    [EmailAddress] nvarchar(50)  NOT NULL,
    [RestaurantId] int  NOT NULL,
	[RewardId] int  NOT NULL,
    [RunningCount1] int  NOT NULL,
    [DateCreated] datetime  NOT NULL,
    [DateModified] datetime  NOT NULL,
    [UpdateCount] int  NOT NULL,
    [LastRunningCount] int  NOT NULL
);
GO

-- Creating primary key on [EmailAddress], [RestaurantId] in table 'RunningCounts'
ALTER TABLE [dbo].[RunningCounts]
ADD CONSTRAINT [PK_RunningCounts]
    PRIMARY KEY CLUSTERED ([EmailAddress], [RestaurantId], [RewardId] ASC);
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

-- Creating foreign key on [RestaurantId] in table 'RunningCounts'
ALTER TABLE [dbo].[RunningCounts]
ADD CONSTRAINT [FK_RunningCount_ToTableRewards]
    FOREIGN KEY ([RewardId])
    REFERENCES [dbo].[Rewards]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
	
-- Creating non-clustered index for FOREIGN KEY 'FK_RunningCount_ToTableRestaurant'
CREATE INDEX [IX_FK_RunningCount_ToTableRewards]
ON [dbo].[RunningCounts]
    ([RewardId]);
GO

------------------------------------------------------------------------
----------------- [Generated_CouponCode] Updates -----------------------
------------------------------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Generated_CouponCode_ToTableRestaurant]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Generated_CouponCode] DROP CONSTRAINT [FK_Generated_CouponCode_ToTableRestaurant];
GO

IF OBJECT_ID(N'[dbo].[Generated_CouponCode]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Generated_CouponCode];
GO

-- Creating table 'Generated_CouponCode'
CREATE TABLE [dbo].[Generated_CouponCode] (
    [CouponCode] nvarchar(20)  NOT NULL,
    [RestaurantId] int  NOT NULL,
    [RewardId] int NOT NULL,
    [EmailAddress] nvarchar(50)  NOT NULL,
    [IsAssigned] bit  NOT NULL,
    [IsRedeemed] bit  NOT NULL,
    [DateCreated] datetime  NOT NULL,
    [DateRedeemed] datetime  NULL
);
GO

-- Creating primary key on [CouponCode] in table 'Generated_CouponCode'
ALTER TABLE [dbo].[Generated_CouponCode]
ADD CONSTRAINT [PK_Generated_CouponCode]
    PRIMARY KEY CLUSTERED ([CouponCode] ASC);
GO

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

-- Creating foreign key on [RewardId] in table 'Generated_CouponCode'
ALTER TABLE [dbo].[Generated_CouponCode]
ADD CONSTRAINT [FK_Generated_CouponCode_ToTableRewards]
    FOREIGN KEY ([RewardId])
    REFERENCES [dbo].[Rewards]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Generated_CouponCode_ToTableRewards'
CREATE INDEX [IX_FK_Generated_CouponCode_ToTableRewards]
ON [dbo].[Generated_CouponCode]
    ([RewardId]);
GO
