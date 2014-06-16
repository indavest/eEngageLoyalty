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