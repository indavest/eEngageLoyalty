-----------------------------------------------------------------------------------------------------------------
------------ [Admin_CouponCode_Mapping] Update
-----------------------------------------------------------------------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Generated_CouponCode_ToTableUserProfile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Admin_CouponCode_Mapping] DROP CONSTRAINT [FK_Generated_CouponCode_ToTableUserProfile];
GO

IF OBJECT_ID(N'[dbo].[Admin_CouponCode_Mapping]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Admin_CouponCode_Mapping];
GO

-- Creating table 'Admin_CouponCode_Mapping'
CREATE TABLE [dbo].[Admin_CouponCode_Mapping] (
    [CouponCode] nvarchar(20)  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating primary key on [CouponCode] and [UserId] in table 'Admin_CouponCode_Mapping'
ALTER TABLE [dbo].[Admin_CouponCode_Mapping]
ADD CONSTRAINT [PK_Admin_CouponCode_Mapping]
    PRIMARY KEY CLUSTERED ([CouponCode] ASC);
GO

-- Creating foreign key on [CouponCode] in table 'Admin_CouponCode_Mapping'
ALTER TABLE [dbo].[Admin_CouponCode_Mapping]
ADD CONSTRAINT [FK_Admin_CouponCode_Mapping_ToTableGenerated_CouponCode]
    FOREIGN KEY ([CouponCode])
    REFERENCES [dbo].[Generated_CouponCode]
        ([CouponCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Admin_CouponCode_Mapping_ToTableGenerated_CouponCode'
CREATE INDEX [IX_FK_Admin_CouponCode_Mapping_ToTableGenerated_CouponCode]
ON [dbo].[Admin_CouponCode_Mapping]
    ([CouponCode]);
GO

-- Creating foreign key on [UserId] in table 'Admin_CouponCode_Mapping'
ALTER TABLE [dbo].[Admin_CouponCode_Mapping]
ADD CONSTRAINT [FK_Admin_CouponCode_Mapping_ToTableUserProfile]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[UserProfile]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Admin_CouponCode_Mapping_ToTableGenerated_CouponCode'
CREATE INDEX [IX_FK_Admin_CouponCode_Mapping_ToTableUserProfile]
ON [dbo].[Admin_CouponCode_Mapping]
    ([UserId]);
GO
