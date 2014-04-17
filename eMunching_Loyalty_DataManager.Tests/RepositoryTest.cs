using eMunching_Loyalty_DataManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;

namespace eMunching_Loyalty_DataManager.Tests
{
    
    
    /// <summary>
    ///This is a test class for RepositoryTest and is intended
    ///to contain all RepositoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RepositoryTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for AddCouponCodesToDatabase
        ///</summary>
        [TestMethod()]
        [DeploymentItem("eMunching_Loyalty_DataManager.dll")]
        public void AddCouponCodesToDatabaseTest()
        {
            Repository_Accessor target = new Repository_Accessor(); // TODO: Initialize to an appropriate value
            Restaurant r = null; // TODO: Initialize to an appropriate value
            SettlementInfo s = null; // TODO: Initialize to an appropriate value
            Reward reward = null; // TODO: Initialize to an appropriate value
            int numberOfCouponsToGenerate = 0; // TODO: Initialize to an appropriate value
            target.AddCouponCodesToDatabase(r, s, reward, numberOfCouponsToGenerate);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddOwnershipToSettlementInfo
        ///</summary>
        [TestMethod()]
        public void AddOwnershipToSettlementInfoTest()
        {
            Repository target = new Repository(); // TODO: Initialize to an appropriate value
            target.AddOwnershipToSettlementInfo();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for GenerateCouponCodes
        ///</summary>
        [TestMethod()]
        public void GenerateCouponCodesTest()
        {
            Repository target = new Repository(); // TODO: Initialize to an appropriate value
            target.GenerateCouponCodes();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for GetEmailAddressFromUniqueCode_UserMapping
        ///</summary>
        [TestMethod()]
        public void GetEmailAddressFromUniqueCode_UserMappingTest()
        {
            Repository target = new Repository(); // TODO: Initialize to an appropriate value
            string uniqueCode = string.Empty; // TODO: Initialize to an appropriate value
            int restaurantId = 0; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.GetEmailAddressFromUniqueCode_UserMapping(uniqueCode, restaurantId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetRestaurants
        ///</summary>
        [TestMethod()]
        public void GetRestaurantsTest()
        {
            Repository target = new Repository(); // TODO: Initialize to an appropriate value
            int expected = 1; // TODO: Initialize to an appropriate value
            List<Restaurant> actual;
            actual = target.GetRestaurants();
            Assert.AreEqual(expected, actual.Count);
        }

        /// <summary>
        ///A test for GetRunningCount
        ///</summary>
        [TestMethod()]
        [DeploymentItem("eMunching_Loyalty_DataManager.dll")]
        public void GetRunningCountTest()
        {
            Repository_Accessor target = new Repository_Accessor(); // TODO: Initialize to an appropriate value
            int restaurantId = 0; // TODO: Initialize to an appropriate value
            string emailAddress = string.Empty; // TODO: Initialize to an appropriate value
            int rewardId = 0; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.GetRunningCount(restaurantId, emailAddress, rewardId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetSettlementInfoWithNullEmail
        ///</summary>
        [TestMethod()]
        public void GetSettlementInfoWithNullEmailTest()
        {
            Repository target = new Repository(); // TODO: Initialize to an appropriate value
            int restaurantId = 0; // TODO: Initialize to an appropriate value
            List<SettlementInfo> expected = null; // TODO: Initialize to an appropriate value
            List<SettlementInfo> actual;
            actual = target.GetSettlementInfoWithNullEmail(restaurantId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsEligible
        ///</summary>
        [TestMethod()]
        [DeploymentItem("eMunching_Loyalty_DataManager.dll")]
        public void IsEligibleTest()
        {
            Repository_Accessor target = new Repository_Accessor(); // TODO: Initialize to an appropriate value
            OrderDetail o = null; // TODO: Initialize to an appropriate value
            Reward r = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.IsEligible(o, r);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsGenerateUniqueCodes
        ///</summary>
        [TestMethod()]
        public void IsGenerateUniqueCodesTest()
        {
            Repository target = new Repository(); // TODO: Initialize to an appropriate value
            int restaurantId = 0; // TODO: Initialize to an appropriate value
            int numberOfCodesToCreate = 0; // TODO: Initialize to an appropriate value
            int numberOfCodesToCreateExpected = 0; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.IsGenerateUniqueCodes(restaurantId, out numberOfCodesToCreate);
            Assert.AreEqual(numberOfCodesToCreateExpected, numberOfCodesToCreate);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SetRunningCount
        ///</summary>
        [TestMethod()]
        [DeploymentItem("eMunching_Loyalty_DataManager.dll")]
        public void SetRunningCountTest()
        {
            Repository_Accessor target = new Repository_Accessor(); // TODO: Initialize to an appropriate value
            int restaurantId = 0; // TODO: Initialize to an appropriate value
            int rewardId = 0; // TODO: Initialize to an appropriate value
            string emailAddress = string.Empty; // TODO: Initialize to an appropriate value
            int runningCount = 0; // TODO: Initialize to an appropriate value
            target.SetRunningCount(restaurantId, rewardId, emailAddress, runningCount);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for UniqueCode_BulkInsert
        ///</summary>
        [TestMethod()]
        public void UniqueCode_BulkInsertTest()
        {
            Repository target = new Repository(); // TODO: Initialize to an appropriate value
            string[] uniqueCodes = null; // TODO: Initialize to an appropriate value
            int restaurantId = 0; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.UniqueCode_BulkInsert(uniqueCodes, restaurantId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ValidateAndAcceptUniqueCodes
        ///</summary>
        [TestMethod()]
        public void ValidateAndAcceptUniqueCodesTest()
        {
            //to make this test run, ensure that:
            // 1. UniqueCode_UserMapping doesn't have the record
            // 2. Generated_UniqueCode table contains IsValidated = false for this record

            eMunching_LoyaltyEntities dbContext = new eMunching_LoyaltyEntities();
            string uniqueCode = "Un1qu301";
            string emailAddress = "anand@emunching.com";
            int restaurantId = 36; // TODO: Initialize to an appropriate value

            DataCleanup(dbContext, uniqueCode, emailAddress, restaurantId);

            //Test Setup - Add Unique Code
            AddUniqueCode(dbContext, uniqueCode);

            Repository target = new Repository(); // TODO: Initialize to an appropriate value      
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.ValidateAndAcceptUniqueCodes(uniqueCode, emailAddress, restaurantId);
            Assert.AreEqual(expected, actual);

            //TEARDOWN
            DeleteUniqueCodeUserMapping(dbContext, uniqueCode);
            DeleteUniqueCode(dbContext, uniqueCode);
        }

        /// <summary>
        ///A test for GetCouponCodes
        ///</summary>
        [TestMethod()]
        public void GetCouponCodesTest()
        {
            Repository target = new Repository();
            string emailAddress = "abilax@gmail.com";
            int restaurantId = 67; // Toit
            bool isRedeemed = false; //show only unredeemed codes
            int expected = 5; //Run a query to find out this number
            List<string> actual;
            actual = target.GetCouponCodes(emailAddress, restaurantId, isRedeemed);
            Assert.AreEqual(expected, actual.Count);
        }

        /// <summary>
        /// Data Clean up method
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="uniqueCode"></param>
        /// <param name="emailAddress"></param>
        /// <param name="restaurantId"></param>
        private static void DataCleanup(eMunching_LoyaltyEntities dbContext, string uniqueCode, string emailAddress, int restaurantId)
        {
            //clear out the Running counts 
            if (dbContext.RunningCounts.Where(i => i.RestaurantId == restaurantId && i.EmailAddress == emailAddress).ToList().Count != 0)
            {
                DeleteRunningCount(dbContext, restaurantId, emailAddress);
            }

            //clear out the coupon codes
            if (dbContext.Generated_CouponCode.Where(i => i.EmailAddress == emailAddress && i.RestaurantId == restaurantId).ToList().Count != 0)
            {
                DeleteCouponCodes(dbContext, restaurantId, emailAddress);
            }

            //clear out any existing mapping for this test data
            if (dbContext.UniqueCode_UserMapping.Where(i => i.UniqueCode == uniqueCode && i.EmailAddress == emailAddress).ToList().Count != 0)
            {
                DeleteUniqueCodeUserMapping(dbContext, uniqueCode);
            }

            //clear out out test data
            if (dbContext.Generated_UniqueCode.Where(i => i.UniqueCode == uniqueCode).ToList().Count != 0)
            {
                DeleteUniqueCode(dbContext, uniqueCode);
            }

        }

        #region Helper methods
        /// <summary>
        /// Deletes Coupon Codes
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="restaurantId"></param>
        /// <param name="emailAddress"></param>
        private static void DeleteCouponCodes(eMunching_LoyaltyEntities dbContext, int restaurantId, string emailAddress)
        {
            dbContext.Generated_CouponCode.DeleteObject(dbContext.Generated_CouponCode.Single(i => i.RestaurantId == restaurantId && i.EmailAddress == emailAddress));
        }

        /// <summary>
        /// Deletes Running Counts
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="restaurantId"></param>
        /// <param name="emailAddress"></param>
        private static void DeleteRunningCount(eMunching_LoyaltyEntities dbContext, int restaurantId, string emailAddress)
        {
            dbContext.RunningCounts.DeleteObject(dbContext.RunningCounts.Single(i => i.RestaurantId == restaurantId && i.EmailAddress == emailAddress));
        }

        /// <summary>
        /// Add Unique
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="uniqueCode"></param>
        /// <param name="emailAddress"></param>
        private static void AddUniqueCodeUserMapping(eMunching_LoyaltyEntities dbContext, string uniqueCode, string emailAddress)
        {
            UniqueCode_UserMapping uu = new UniqueCode_UserMapping();
            uu.UniqueCode = uniqueCode;
            uu.EmailAddress = emailAddress;
            uu.RestaurantId = 36;
            uu.DateCreated = DateTime.Now;

            dbContext.UniqueCode_UserMapping.AddObject(uu);
            dbContext.SaveChanges();
        }

        /// <summary>
        /// Deletes the mapping - This is a Test TEARDOWN method
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="uniqueCode"></param>
        private static void DeleteUniqueCodeUserMapping(eMunching_LoyaltyEntities dbContext, string uniqueCode)
        {
            dbContext.UniqueCode_UserMapping.DeleteObject(dbContext.UniqueCode_UserMapping.Single(i => i.UniqueCode == uniqueCode));
        }

        /// <summary>
        /// Add a Unique Code - This is a Test SETUP method
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="uniqueCode"></param>
        private static void AddUniqueCode(eMunching_LoyaltyEntities dbContext, string uniqueCode)
        {
            Generated_UniqueCode uc = new Generated_UniqueCode();
            uc.UniqueCode = uniqueCode;
            uc.DateCreated = DateTime.Now;
            uc.RestaurantId = 36;
            uc.IsValidated = false;

            //add the unique code and ensure that it is not validated
            dbContext.Generated_UniqueCode.AddObject(uc);
            dbContext.SaveChanges();
        }

        /// <summary>
        /// Delete the UniqueCode - This is a Test TEARDOWN method
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="uniqueCode"></param>
        private static void DeleteUniqueCode(eMunching_LoyaltyEntities dbContext, string uniqueCode)
        {
            //Get the record to be deleted
            Generated_UniqueCode uc = dbContext.Generated_UniqueCode.Single(i => i.UniqueCode == uniqueCode);

            //delete the record
            dbContext.DeleteObject(uc);
            dbContext.SaveChanges();
        }

        /// <summary>
        /// Add Order details - This is a Test Setup method
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="billNumber"></param>
        private static void AddOrderDetails(eMunching_LoyaltyEntities dbContext, string billNumber)
        {
            //Step 4. Add some order details
            OrderDetail od1 = new OrderDetail();
            od1.BillNumber = billNumber;
            od1.ItemCode = 101;
            od1.RestaurantId = 36;
            od1.Quantity = 1;

            OrderDetail od2 = new OrderDetail();
            od2.BillNumber = billNumber;
            od2.ItemCode = 102;
            od2.RestaurantId = 36;
            od2.Quantity = 4;

            OrderDetail od3 = new OrderDetail();
            od3.BillNumber = billNumber;
            od3.ItemCode = 104;
            od3.RestaurantId = 36;
            od3.Quantity = 3;

            OrderDetail od4 = new OrderDetail();
            od4.BillNumber = billNumber;
            od4.ItemCode = 105;
            od4.RestaurantId = 36;
            od4.Quantity = 1;

            OrderDetail od5 = new OrderDetail();
            od5.BillNumber = billNumber;
            od5.ItemCode = 106;
            od5.RestaurantId = 36;
            od5.Quantity = 1;

            OrderDetail od6 = new OrderDetail();
            od6.BillNumber = billNumber;
            od6.ItemCode = 107;
            od6.RestaurantId = 36;
            od6.Quantity = 2;

            OrderDetail od7 = new OrderDetail();
            od7.BillNumber = billNumber;
            od7.ItemCode = 108;
            od7.RestaurantId = 36;
            od7.Quantity = 2;

            OrderDetail od8 = new OrderDetail();
            od8.BillNumber = billNumber;
            od8.ItemCode = 109;
            od8.RestaurantId = 36;
            od8.Quantity = 2;

            dbContext.OrderDetails.AddObject(od1);
            dbContext.OrderDetails.AddObject(od2);
            dbContext.OrderDetails.AddObject(od3);
            dbContext.OrderDetails.AddObject(od4);
            dbContext.OrderDetails.AddObject(od5);
            dbContext.OrderDetails.AddObject(od6);
            dbContext.OrderDetails.AddObject(od7);
            dbContext.OrderDetails.AddObject(od8);

            dbContext.SaveChanges();

        }

        /// <summary>
        /// Delete all the Order Details - This is a Test TEARDOWN method
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="billNumber"></param>
        private static void DeleteOrderDetails(eMunching_LoyaltyEntities dbContext, string billNumber)
        {
            //get the order details
            IList<OrderDetail> orderDetails = dbContext.OrderDetails.Where(i => i.BillNumber == billNumber).ToList();

            //delete each one
            foreach (OrderDetail o in orderDetails)
            {
                dbContext.DeleteObject(o);
            }

            dbContext.SaveChanges();
        }

        /// <summary>
        /// Add Settlement Information - This is a Test SETUP method
        /// </summary>
        /// <param name="billNumber"></param>
        /// <param name="dbContext"></param>
        /// <param name="uniqueCode"></param>
        private static void AddSettlementInformation(string billNumber, eMunching_LoyaltyEntities dbContext, string uniqueCode)
        {
            SettlementInfo si = new SettlementInfo();
            si.BillNumber = billNumber;
            si.RestaurantId = 36;
            si.UniqueCode = uniqueCode;
            si.EmailAddress = null;
            si.IsServiced = false;
            si.DateCreated = DateTime.Now;
            si.DateModified = DateTime.Now;
            si.SettlementDate = DateTime.Today.AddDays(-1);

            dbContext.SettlementInfoes.AddObject(si);
            dbContext.SaveChanges();
        }

        /// <summary>
        /// Delete SettlementInfo data - This is a Test TEARDOWN method
        /// </summary>
        /// <param name="billNumber"></param>
        /// <param name="dbContext"></param>
        private static void DeleteSettlementInformation(string billNumber, eMunching_LoyaltyEntities dbContext)
        {
            //identity the record to delete
            IList<SettlementInfo> settlementInfoes = dbContext.SettlementInfoes.Where(i => i.BillNumber == billNumber).ToList();

            foreach (SettlementInfo si in settlementInfoes)
            {
                dbContext.DeleteObject(si);
            }

            dbContext.SaveChanges();
        }

        #endregion Helper methods

        /// <summary>
        ///A test for GetRestaurantId
        ///</summary>
        [TestMethod()]
        public void GetRestaurantIdTest()
        {
            Repository target = new Repository(); // TODO: Initialize to an appropriate value
            string userName = "anand@emunching.com"; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.GetRestaurantId(userName);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AddPreApprovedUser
        ///</summary>
        [TestMethod()]
        public void AddPreApprovedUserTest()
        {
            Repository target = new Repository(); 
            string emailAddress = "testuser1";
            int restaurantId = 67; 
            int roleId = 2;
            string message = string.Empty;
            string messageExpected = "User has been Pre-Approved";
            int expected = 1;
            int actual;
            actual = target.AddPreApprovedUser(emailAddress, restaurantId, roleId, out message);
            Assert.AreEqual(messageExpected, message);
            Assert.AreEqual(expected, actual);
        }
    }
}
