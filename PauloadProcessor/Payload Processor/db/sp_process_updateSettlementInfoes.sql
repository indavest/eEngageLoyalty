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