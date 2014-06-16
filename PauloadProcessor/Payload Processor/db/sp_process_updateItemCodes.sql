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