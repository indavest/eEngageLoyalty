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