using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eMunching_Loyalty_DataManager
{
    public class AnalyticsRepository
    {
        private eMunching_LoyaltyEntities _context = new eMunching_LoyaltyEntities();

        #region Private Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="restaurantId"></param>
        /// <param name="jtStartIndex"></param>
        /// <param name="jtPageSize"></param>
        /// <param name="jtSorting"></param>
        /// <returns></returns>
        public List<BeersPunched> GetBeersPunchedBetweenDatesBySorting(string startDate, string endDate, int restaurantId, int jtStartIndex, int jtPageSize, string jtSorting)
        {
            try
            {
                string eligibleSKUsQuery = "SELECT RewardSKUs FROM Rewards WHERE RestaurantID=" + restaurantId;
                var skuResult = _context.ExecuteStoreQuery<string>(eligibleSKUsQuery);
                string query = "SELECT * FROM";
                query += " (SELECT ROW_NUMBER() OVER (ORDER BY " + jtSorting + ") AS Row, * FROM (SELECT o.BillNumber,o.ItemCode,o.Quantity,s.EmailAddress as UserEmail,i.ItemName,um.DateCreated Created FROM OrderDetails o,SettlementInfoes s,ItemCodes i,UniqueCode_UserMapping um WHERE o.BillNumber IN (SELECT BillNumber FROM SettlementInfoes WHERE UniqueCode IN (SELECT UniqueCode FROM UniqueCode_UserMapping WHERE DateCreated > {1} AND DateCreated < {2} AND RestaurantId={0})) AND o.BillNumber = s.BillNumber AND o.ItemCode IN (" + skuResult.FirstOrDefault() + ") AND o.ItemCode = i.ItemCode1 AND s.UniqueCode = um.UniqueCode) t1)";
                query += " AS StudentsWithRowNumbers";
                query += " WHERE Row > " + jtStartIndex + " AND Row <= " + (jtStartIndex + jtPageSize);
                Object[] parameters = { restaurantId, startDate, endDate };
                List<BeersPunched> resultSet = _context.ExecuteStoreQuery<BeersPunched>(query, parameters).ToList<BeersPunched>();
                return resultSet;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="restaurantId"></param>
        /// <returns></returns>
        public int GetBeersPunchedCount(string startDate, string endDate, int restaurantId)
        {
            try
            {
                string eligibleSKUsQuery = "SELECT RewardSKUs FROM Rewards WHERE RestaurantID=" + restaurantId;
                var skuResult = _context.ExecuteStoreQuery<string>(eligibleSKUsQuery);
                string query = "SELECT count(o.BillNumber) FROM OrderDetails o,SettlementInfoes s,ItemCodes i,UniqueCode_UserMapping um WHERE o.BillNumber IN (SELECT BillNumber FROM SettlementInfoes WHERE UniqueCode IN (SELECT UniqueCode FROM UniqueCode_UserMapping WHERE DateCreated > {1} AND DateCreated < {2} AND RestaurantId={0})) AND o.BillNumber = s.BillNumber AND o.ItemCode IN (" + skuResult.FirstOrDefault() + ") AND o.ItemCode = i.ItemCode1 AND s.UniqueCode = um.UniqueCode";
                Object[] parameters = { restaurantId, startDate, endDate };
                var resultSet = _context.ExecuteStoreQuery<int>(query, parameters);
                return resultSet.FirstOrDefault();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="restaurantId"></param>
        /// <returns></returns>
        public List<BeersPunched> GetAllBeersPunchedBetweenDates(string startDate, string endDate, int restaurantId)
        {
            try
            {
                string eligibleSKUsQuery = "SELECT RewardSKUs FROM Rewards WHERE RestaurantID=" + restaurantId;
                var skuResult = _context.ExecuteStoreQuery<string>(eligibleSKUsQuery);
                string query = "SELECT o.BillNumber,o.ItemCode,o.Quantity,s.EmailAddress as UserEmail,i.ItemName,um.DateCreated Created FROM OrderDetails o,SettlementInfoes s,ItemCodes i,UniqueCode_UserMapping um WHERE o.BillNumber IN (SELECT BillNumber FROM SettlementInfoes WHERE UniqueCode IN (SELECT UniqueCode FROM UniqueCode_UserMapping WHERE DateCreated > {1} AND DateCreated < {2} AND RestaurantId={0})) AND o.BillNumber = s.BillNumber AND o.ItemCode IN ("+skuResult.FirstOrDefault()+") AND o.ItemCode = i.ItemCode1 AND s.UniqueCode = um.UniqueCode";
                Object[] parameters = { restaurantId, startDate, endDate };
                List<BeersPunched> resultSet = _context.ExecuteStoreQuery<BeersPunched>(query, parameters).ToList<BeersPunched>();
                return resultSet;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="restaurantId"></param>
        /// <param name="jtStartIndex"></param>
        /// <param name="jtPageSize"></param>
        /// <param name="jtSorting"></param>
        /// <returns></returns>
        public List<CouponsGenerated> GetAllCouponsGeneratedBetweenDatesBySorting(string startDate, string endDate, int restaurantId, int jtStartIndex, int jtPageSize, string jtSorting)
        {
            try
            {
                string query = "SELECT * FROM";
                query += " (SELECT ROW_NUMBER() OVER (ORDER BY " + jtSorting + ") AS Row, * FROM (SELECT CouponCode, EmailAddress, IsAssigned, IsRedeemed, DateCreated, DateRedeemed FROM Generated_CouponCode WHERE DateCreated > {1} AND DateCreated < {2} AND RestaurantId={0}) t1)";
                query += " AS StudentsWithRowNumbers";
                query += " WHERE Row > " + jtStartIndex + " AND Row <= " + (jtStartIndex + jtPageSize);
                Object[] parameters = { restaurantId, startDate, endDate };
                List<CouponsGenerated> resultSet = _context.ExecuteStoreQuery<CouponsGenerated>(query, parameters).ToList<CouponsGenerated>();
                return resultSet;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="restaurantId"></param>
        /// <returns></returns>
        public int GetCouponsGeneratedCount(string startDate, string endDate, int restaurantId)
        {
            try
            {
                string query = "SELECT Count(CouponCode) FROM Generated_CouponCode WHERE DateCreated > {1} AND DateCreated < {2} AND RestaurantId={0}";
                Object[] parameters = { restaurantId, startDate, endDate };
                var resultSet = _context.ExecuteStoreQuery<int>(query, parameters);
                return resultSet.FirstOrDefault();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="restaurantId"></param>
        /// <returns></returns>
        public List<CouponsGenerated> GetAllCouponsGeneratedBetweenDates(string startDate, string endDate, int restaurantId)
        {
            try
            {
                string query = "SELECT CouponCode, EmailAddress, IsAssigned, IsRedeemed, DateCreated, DateRedeemed FROM Generated_CouponCode WHERE DateCreated > {1} AND DateCreated < {2} AND RestaurantId={0}";
                Object[] parameters = { restaurantId, startDate, endDate };
                List<CouponsGenerated> resultSet = _context.ExecuteStoreQuery<CouponsGenerated>(query, parameters).ToList<CouponsGenerated>();
                return resultSet;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Bills> GetBillsBetweenDatesBySorting(string startDate, string endDate, int restaurantId, int jtStartIndex, int jtPageSize, string jtSorting)
        {
            try
            {
                string eligibleSKUsQuery = "SELECT RewardSKUs FROM Rewards WHERE RestaurantID=" + restaurantId;
                var skuResult = _context.ExecuteStoreQuery<string>(eligibleSKUsQuery).FirstOrDefault();
                string query = "SELECT * FROM (";
                query += " SELECT ROW_NUMBER() OVER (ORDER BY "+jtSorting+") AS Row, * FROM (";
                query += " SELECT r.BillNumber, SUM(r.Quantity) BeerCount, r.UniqueCode, r.EmailAddress UserEmail, ISNULL(round((ba.NetAmount + ba.TaxAmount),2),0) BillAmount, r.DateCreated Created FROM (";
                query += " (SELECT o.BillNumber BillNumber ,SUM(o.Quantity)Quantity,s.UniqueCode ,s.EmailAddress EmailAddress,s.DateCreated FROM SettlementInfoes s FULL OUTER JOIN OrderDetails o ON s.BillNumber=o.BillNumber WHERE s.DateCreated > {1} AND s.DateCreated < {2} AND s.RestaurantId = {0} AND s.IsServiced=1 AND o.ItemCode IN (" + skuResult + ") GROUP BY o.BillNumber,s.DateCreated,s.EmailAddress,s.UniqueCode)";
                query += " UNION ALL";
                query += " (SELECT o.BillNumber BillNumber ,0 Quantity,s.UniqueCode,s.EmailAddress EmailAddress,s.DateCreated FROM SettlementInfoes s FULL OUTER JOIN OrderDetails o ON s.BillNumber=o.BillNumber WHERE s.DateCreated > {1} AND s.DateCreated < {2} AND s.RestaurantId = {0} AND s.IsServiced=1 AND o.ItemCode NOT IN (" + skuResult + ") GROUP BY o.BillNumber,s.DateCreated,s.EmailAddress,s.UniqueCode))";
                query += " AS r LEFT JOIN BillAmount ba ON r.BillNumber = ba.BillNumber GROUP BY r.BillNumber,r.EmailAddress,ba.NetAmount,ba.TaxAmount,r.DateCreated,r.UniqueCode) t1";
                query += " ) AS StudentsWithRowNumbers WHERE Row > " + jtStartIndex + " AND Row <= " + (jtStartIndex + jtPageSize);
                Object[] parameters = { restaurantId, startDate, endDate };
                List<Bills> resultSet = _context.ExecuteStoreQuery<Bills>(query, parameters).ToList<Bills>();
                return resultSet;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="restaurantId"></param>
        /// <returns></returns>
        public int GetBillsCount(string startDate, string endDate, int restaurantId)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM SettlementInfoes WHERE DateCreated > {1} AND DateCreated < {2} AND IsServiced=1 AND RestaurantId={0}";
                Object[] parameters = { restaurantId, startDate, endDate };
                var resultSet = _context.ExecuteStoreQuery<int>(query, parameters);
                return resultSet.FirstOrDefault();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public List<Bills> GetBillBetweenDates(string startDate, string endDate, int restaurantId)
        {
            try
            {
                string eligibleSKUsQuery = "SELECT RewardSKUs FROM Rewards WHERE RestaurantID=" + restaurantId;
                var skuResult = _context.ExecuteStoreQuery<string>(eligibleSKUsQuery).FirstOrDefault();
                string query = "SELECT * FROM (";
                query += " SELECT r.BillNumber, SUM(r.Quantity) BeerCount,r.UniqueCode, r.EmailAddress UserEmail, ISNULL(round((ba.NetAmount + ba.TaxAmount),2),0) BillAmount, r.DateCreated Created FROM (";
                query += " (SELECT o.BillNumber BillNumber ,SUM(o.Quantity)Quantity,s.UniqueCode ,s.EmailAddress EmailAddress,s.DateCreated FROM SettlementInfoes s FULL OUTER JOIN OrderDetails o ON s.BillNumber=o.BillNumber WHERE s.DateCreated > {1} AND s.DateCreated < {2} AND s.RestaurantId = {0} AND s.IsServiced=1 AND o.ItemCode IN (" + skuResult + ") GROUP BY o.BillNumber,s.DateCreated,s.EmailAddress,s.UniqueCode)";
                query += " UNION ALL";
                query += " (SELECT o.BillNumber BillNumber ,0 Quantity,s.UniqueCode,s.EmailAddress EmailAddress,s.DateCreated FROM SettlementInfoes s FULL OUTER JOIN OrderDetails o ON s.BillNumber=o.BillNumber WHERE s.DateCreated > {1} AND s.DateCreated < {2} AND s.RestaurantId = {0} AND s.IsServiced=1 AND o.ItemCode NOT IN (" + skuResult + ") GROUP BY o.BillNumber,s.DateCreated,s.EmailAddress,s.UniqueCode))";
                query += " AS r LEFT JOIN BillAmount ba ON r.BillNumber = ba.BillNumber GROUP BY r.BillNumber,r.EmailAddress,ba.NetAmount,ba.TaxAmount,r.DateCreated,r.UniqueCode) t1 ORDER BY t1.Created DESC";
                Object[] parameters = { restaurantId, startDate, endDate };
                List<Bills> resultSet = _context.ExecuteStoreQuery<Bills>(query, parameters).ToList<Bills>();
                return resultSet;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

    }
}
