using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Diagnostics;
using System.Data.SqlClient;
using eMunching_Loyalty_DataManager.eMunchingServices;
using KeyManager;
using System.Data.Objects;


namespace eMunching_Loyalty_DataManager
{
    public class Repository
    {
        private eMunching_LoyaltyEntities _context = new eMunching_LoyaltyEntities();

        #region Private Methods

        /// <summary>
        /// Archive Unique Codes
        /// </summary>
        /// <param name="uniqueCode"></param>
        /// <param name="dateCreated"></param>
        /// <param name="restaurantId"></param>
        /// <param name="dateValidated"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private bool ArchiveUniqueCode(string uniqueCode, DateTime dateCreated, int restaurantId, DateTime dateValidated, eMunching_LoyaltyEntities context)
        {
            try
            {
                var recordToDelete = context.Generated_UniqueCode.Single(i => i.UniqueCode == uniqueCode);
                
                UniqueCode_Archive uc = new UniqueCode_Archive();
                uc.UniqueCode = uniqueCode;
                uc.DateCreated = dateCreated;
                uc.RestaurantId = restaurantId;
                uc.DateValidated = dateValidated;

                //add to archive table, delete from working table
                context.AddToUniqueCode_Archive(uc);
                context.Generated_UniqueCode.DeleteObject(recordToDelete);

                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Insert into UniqueCode_UserMapping table
        /// </summary>
        /// <param name="uniqueCode"></param>
        /// <param name="emailAddress"></param>
        /// <param name="restaurantId"></param>
        /// <param name="dateCreated"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private bool InsertCustomerInfo(string uniqueCode, string emailAddress, int restaurantId, DateTime dateCreated, eMunching_LoyaltyEntities context)
        {
            try
            {
                UniqueCode_UserMapping custInfo = new UniqueCode_UserMapping();
                custInfo.UniqueCode = uniqueCode;
                custInfo.EmailAddress = emailAddress;
                custInfo.RestaurantId = restaurantId;
                custInfo.DateCreated = dateCreated;

                context.AddToUniqueCode_UserMapping(custInfo);

                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Validate the Unique Code provided
        /// </summary>
        /// <param name="uniqueCode"></param>
        /// <returns></returns>
        private bool ValidateUniqueCode(string uniqueCode, int restaurantId, out DateTime dateCreated, eMunching_LoyaltyEntities context)
        {
            var record = context.Generated_UniqueCode.FirstOrDefault(i => i.UniqueCode == uniqueCode && i.RestaurantId == restaurantId);

            //if a record is found, mark it as validated and return true
            if (record != null)
            {
                dateCreated = record.DateCreated;
                record.IsValidated = true;
                return true;
            }
            else
            {
                dateCreated = DateTime.MinValue;
                return false;
            }
        }

        /// <summary>
        /// Adds Coupon Codes to the databse
        /// </summary>
        /// <param name="r">Restaurant</param>
        /// <param name="s">SettlementInfo</param>
        /// <param name="numberOfCouponsToGenerate">Number of Coupons To Generate</param>
        private void AddCouponCodesToDatabase(Restaurant r, SettlementInfo s, Reward reward, int numberOfCouponsToGenerate)
        {
            //Generate Coupons
            List<Guid> guids = new List<Guid>();
            for (int i = 0; i < numberOfCouponsToGenerate; i++)
            {
                guids.Add(Guid.NewGuid());
            }

            foreach (Guid guid in guids)
            {
                Generated_CouponCode cc = new Generated_CouponCode();
                UniqueKeyGenerator keyGen = new UniqueKeyGenerator();
                cc.CouponCode = keyGen.GenerateUniqueKey();
                cc.RestaurantId = r.ID;
                cc.EmailAddress = s.EmailAddress;
                cc.IsAssigned = false;
                cc.IsRedeemed = false;
                cc.DateCreated = DateTime.Now;
                cc.ExpirationDate = DateTime.Now.AddDays(Double.Parse(reward.Validity.ToString()));
                cc.RewardId = reward.Id;

                try
                {
                    _context.AddToGenerated_CouponCode(cc);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message);
                    throw;
                }
            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        /// <param name="s"></param>
        /// <param name="reward"></param>
        /// <param name="numberOfCouponsToGenerate"></param>
        private void AddSimpleCouponCodesToDatabase(Restaurant r, SettlementInfo s, Reward reward, int numberOfCouponsToGenerate)
        {
            try
            {
                for (int i = 0; i < numberOfCouponsToGenerate; i++)
                {
                    Generated_SimpleCouponCode simpleCouponCode = this.GetLatestSimpleCouponCode(r.ID);
                    if (this.SetSimpleCouponCodeIsAssined(r.ID, simpleCouponCode.UniqueCode))
                    {
                        Generated_CouponCode cc = new Generated_CouponCode();
                        cc.CouponCode = simpleCouponCode.UniqueCode;
                        cc.RestaurantId = r.ID;
                        cc.EmailAddress = s.EmailAddress;
                        cc.IsAssigned = false;
                        cc.IsRedeemed = false;
                        cc.DateCreated = DateTime.Now;
                        cc.ExpirationDate = DateTime.Now.AddDays(Double.Parse(reward.Validity.ToString()));
                        cc.RewardId = reward.Id;
                        _context.AddToGenerated_CouponCode(cc);
                        _context.SaveChanges();
                    }

                }
            }
            catch (Exception ex)
            {
                //Trace.WriteLine(ex.Message);
                this.LogError(ex.Message + ex.InnerException, "Assigning Coupons");
                throw;
            }
        }

        /// <summary>
        /// Gets the Running Count for a given user of a particular restaurant
        /// </summary>
        /// <param name="restaurantId">Restaurant Id</param>
        /// <param name="emailAddress">Email Address</param>
        /// <returns></returns>
        public int GetRunningCount(int restaurantId, string emailAddress, int rewardId)
        {
            var runningCountRecord = _context.RunningCounts.FirstOrDefault(r => r.RestaurantId == restaurantId && r.EmailAddress == emailAddress && r.RewardId == rewardId);

            if (null != runningCountRecord)
            {
                return runningCountRecord.RunningCount1;
            }
            else
            {
                return 0;
            }

        }

        /// <summary>
        /// Set the Running Count
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <param name="emailAddress"></param>
        /// <param name="runningCount"></param>
        private void SetRunningCount(int restaurantId, int rewardId, string emailAddress, int runningCount)
        {
            var runningCountRecord = _context.RunningCounts.FirstOrDefault(r => r.RestaurantId == restaurantId && r.RewardId == rewardId && r.EmailAddress == emailAddress);
                                  
            //if running count exists for this restaurant/email address combination, update it
            if (null != runningCountRecord)
            {
                runningCountRecord.LastRunningCount = runningCountRecord.RunningCount1;
                runningCountRecord.RunningCount1 = runningCount;
                runningCountRecord.DateModified = DateTime.Now;
            }
            else //if running count doesn't exist, create it
            {
                RunningCount rc = new RunningCount();
                rc.RestaurantId = restaurantId;
                rc.RewardId = rewardId;
                rc.EmailAddress = emailAddress;
                rc.DateCreated = DateTime.Now;
                rc.DateModified = DateTime.Now;
                rc.UpdateCount = 0;
                rc.LastRunningCount = 0;
                rc.RunningCount1 = runningCount;

                _context.AddToRunningCounts(rc);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Check with an item is eligible for Loyalty
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private bool IsEligible(OrderDetail o, Reward r)
        {
            var item = _context.ItemCodes.FirstOrDefault(i => i.ItemCode1 == o.ItemCode && i.RestaurantId == o.RestaurantId);

            //check to see if the item is loyalty enabled and if it is one of the reward's eligible SKUs
            if (item.LoyaltyEnabled && r.EligibleSKUs.Contains(item.ItemCode1.ToString()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion Private Methods

        #region COUPON CODE LOGIC
        /// <summary>
        /// Gets a list of Coupon Codes for a particular user of a restaurant
        /// </summary>
        /// <param name="emailAddress">EmailAddress of the registered user</param>
        /// <param name="restaurantId">Restaurant Id</param>
        /// <returns>List of strings representing the Coupon Codes or NULL if nothing exists</returns>
        public List<string> GetCouponCodes(string emailAddress, int restaurantId, bool isRedeemed)
        {
            //This is the list of strings to be returned.
            List<string> retVal = null;

            //Get the records from the Generated_CouponCode table for the specified email and restaurant
            var couponCodes = from alpha in _context.Generated_CouponCode
                              where alpha.EmailAddress == emailAddress && alpha.RestaurantId == restaurantId && alpha.IsRedeemed == isRedeemed orderby alpha.DateCreated descending
                              select alpha;

            //if records exist populate the retVal List with CouponCodes
            if (couponCodes.Count() != 0)
            {
                retVal = new List<string>();

                foreach (Generated_CouponCode code in couponCodes)
                {
                    retVal.Add(code.CouponCode);
                }
            }

            return retVal;
        }

        /// <summary>
        /// Gets a list of Coupon Codes for a particular user of a restaurant
        /// </summary>
        /// <param name="emailAddress">EmailAddress of the registered user</param>
        /// <param name="restaurantId">Restaurant Id</param>
        /// <returns>List of strings representing the Coupon Codes or NULL if nothing exists</returns>
        public List<Generated_CouponCode> GetCouponCodesByReward(string emailAddress, int restaurantId, bool isRedeemed, int rewardId)
        {
            //This is the list of strings to be returned.
            List<Generated_CouponCode> retVal = null;

            //Get the records from the Generated_CouponCode table for the specified email and restaurant
            var couponCodes = from alpha in _context.Generated_CouponCode
                              where alpha.EmailAddress == emailAddress && alpha.RestaurantId == restaurantId && alpha.IsRedeemed == isRedeemed && alpha.RewardId == rewardId && alpha.ExpirationDate >= DateTime.Now
                              orderby alpha.DateCreated descending
                              select alpha;

            //if records exist populate the retVal List with CouponCodes
            if (couponCodes.Count() != 0)
            {
                retVal = new List<Generated_CouponCode>();

                foreach (Generated_CouponCode code in couponCodes)
                {
                    retVal.Add(code);
                }
            }

            return retVal;
        }

        /// <summary>
        /// Generate coupon codes
        /// </summary>
        public void GenerateCouponCodes()
        {
            IList<Restaurant> restaurants = this.GetRestaurants();

            foreach (Restaurant r in restaurants)
            {
                //Get settlement records that are not serviced
                IList<SettlementInfo> settlementRecords = _context.SettlementInfoes.Where(alpha => alpha.RestaurantId == r.ID && !alpha.IsServiced && alpha.EmailAddress != null).ToList();

                //Get the Reward Count
                int rewardCount = r.Rewards.Count( re => re.RewardType == 1);

                foreach (Reward reward in r.Rewards.Where( re => re.RewardType == 1))
                {
                    //For each settlement record get the Order Details and figure out total eligible product
                    foreach (SettlementInfo s in settlementRecords)
                    {
                        IList<OrderDetail> orderDetails = _context.OrderDetails.Where(beta => beta.BillNumber == s.BillNumber && beta.RestaurantId == r.ID).ToList();

                        //get the running count so far
                        int runningCount = GetRunningCount(r.ID, s.EmailAddress, reward.Id);
                        int totalCount = runningCount;
                        int numberOfCouponsToGenerate = 0;

                        foreach (OrderDetail o in orderDetails)
                        {
                            //check if the ordered item is eligible for loyalty points
                            if (IsEligible(o, reward))
                            {
                                //compute total count
                                totalCount += o.Quantity;
                            }
                        }

                        //check if total count is greater than magic number
                        if (totalCount >= reward.NumberOfItems)
                        {
                            //add some fault tolerant logic. If for whatever reason the multiplier is <= 0, assume multiplier = 1
                            if (reward.Multiplier <= 0)
                            {
                                //compute number of coupons to generate
                                numberOfCouponsToGenerate = (totalCount / reward.NumberOfItems) * 1;
                            }
                            else
                            {
                                //compute number of coupons to generate
                                numberOfCouponsToGenerate = (totalCount / reward.NumberOfItems) * reward.Multiplier;
                            }

                            //compute balance
                            int updatedRunningCount = totalCount % reward.NumberOfItems;

                            //this.AddCouponCodesToDatabase(r, s, reward, numberOfCouponsToGenerate);
                            this.AddSimpleCouponCodesToDatabase(r, s, reward, numberOfCouponsToGenerate);

                            try
                            {
                                eMunchingWebServicesSoapClient eMunchingWSClient = new eMunchingWebServicesSoapClient();
                                eMunchingWSClient.RegisterLoyaltyNotification("eMunch", "idnlgeah11", r.ID, s.EmailAddress, numberOfCouponsToGenerate, reward.Id);
                            }
                            catch (Exception ex)
                            {
                                Trace.WriteLine(ex.Message);
                            }

                            //set the new running count
                            this.SetRunningCount(r.ID, reward.Id, s.EmailAddress, updatedRunningCount);
                        }
                        else
                        {
                            //set the new running count
                            this.SetRunningCount(r.ID, reward.Id, s.EmailAddress, totalCount);
                        }

                        //Decrement the reward count
                        rewardCount--;

                        //reset the totalCount
                        totalCount = 0;

                        //mark the settlementinfo record as serviced
                        if(rewardCount <= 0)
                            s.IsServiced = true;

                        _context.SaveChanges();
                    }
                }
            }
        }

        /// <summary>
        /// Validates a Coupon Code and lets us know if it has been Redeemed
        /// </summary>
        /// <param name="couponCode"></param>
        /// <param name="emailAddress"></param>
        /// <param name="restaurantId"></param>
        /// <returns></returns>
        public int ValidateCouponCodeAndCheckIfRedeemed(string couponCode, int restaurantId)
        {
            var record = _context.Generated_CouponCode.SingleOrDefault(i => i.CouponCode == couponCode && i.RestaurantId == restaurantId);

            //if the record was not found, return false
            if (record == null)
            {
                return 0; //no such coupon code exists
            }
            else
            {
                //if the record was found, check the IsRedeemd property
                if (record.ExpirationDate < DateTime.Now)
                {
                    return 3; //coupon code is valid but is has been expired
                }
                else if (!record.IsRedeemed)
                {
                    //if it is not redeemed, return true
                    return 1; //coupon code is valid and it is not redeemed
                }
                else
                {
                    return 2; //coupon code is valid but it has been redeemed
                }
            }
        }

        /// <summary>
        /// Redeems a coupon Code
        /// </summary>
        /// <param name="couponCode"></param>
        /// <param name="emailAddress"></param>
        /// <param name="restaurantId"></param>
        /// <returns></returns>
        public bool RedeemCouponCode(string couponCode, int restaurantId, out string rewardName, out string userName)
        {
            var record = _context.Generated_CouponCode.SingleOrDefault(i => i.CouponCode == couponCode && i.RestaurantId == restaurantId);

            if (record == null)
            {
                rewardName = String.Empty;
                userName = String.Empty;

                //the record wasn't found
                return false;
            }
            else
            {
                //if the record was found, check the IsRedeemd property
                if (!record.IsRedeemed)
                {
                    //if it is not redeemed, redeem it and return true
                    record.IsRedeemed = true;
                    record.DateRedeemed = this.GetUTCTimeNow();
                    _context.SaveChanges();

                    rewardName = record.Reward.Name;
                    userName = record.EmailAddress;

                    return true;
                }
                else
                {
                    rewardName = String.Empty;
                    userName = String.Empty;

                    return false;
                }
            }
        }
        #endregion #region COUPON CODE LOGIC

        #region UNIQUE CODE LOGIC
        /// <summary>
        /// Validate and Accept Unique Codes
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="uniqueCode"></param>
        /// <param name="emailAddress"></param>
        /// <param name="restaurantId"></param>
        /// <returns></returns>
        public bool ValidateAndAcceptUniqueCodes(string uniqueCode, string emailAddress, int restaurantId)
        {
            DateTime dateCreated;

            //make sure the user has not already inserted this
            var record = _context.UniqueCode_UserMapping.FirstOrDefault(i => i.UniqueCode == uniqueCode &&
                i.EmailAddress == emailAddress && i.RestaurantId == restaurantId);

            //no record exists
            if (null == record)
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    eMunching_LoyaltyEntities context1 = new eMunching_LoyaltyEntities();

                    //validate against generated unique code table
                    if (true == this.ValidateUniqueCode(uniqueCode, restaurantId, out dateCreated, context1))
                    {
                        context1.SaveChanges(SaveOptions.DetectChangesBeforeSave);

                        //VALIDATED - GOOD TO GO
                        eMunching_LoyaltyEntities context2 = new eMunching_LoyaltyEntities();

                        //insert into UniqueCode_UserMapping
                        if (true == this.InsertCustomerInfo(uniqueCode, emailAddress, restaurantId, DateTime.Now, context2))
                        {
                            context2.SaveChanges(SaveOptions.DetectChangesBeforeSave);
                        }
                        else
                        {
                            //unable to insert customer info
                            return false;
                        }

                        scope.Complete();

                        context1.AcceptAllChanges();
                        context2.AcceptAllChanges();

                        return true;
                    }
                    else
                    {
                        //NOT VALIDATED - THIS IS A NO GO
                        return false;
                    }
                }
            }
            else
            {
                //User Mapping already exists
                return true;
            }
        }

        /// <summary>
        /// True tells us to create new codes, False says don't
        /// </summary>
        /// <param name="restaurantId">Id of the restaurant</param>
        /// <param name="numberOfCodesToCreate">Number of Codes to generate</param>
        /// <returns></returns>
        public bool IsGenerateUniqueCodes(int restaurantId, out int numberOfCodesToCreate)
        {
            var record = from alpha in _context.CreateNotifier_UniqueCodes
                         where alpha.RestaurantId == restaurantId 
                            && alpha.IsServiced == false 
                            && alpha.IsCreateNewCodes == true
                         select alpha;

            //if any records exist
            if (record.Any())
            {
                //get the number of codes to be created
                numberOfCodesToCreate = ((CreateNotifier_UniqueCodes)record.FirstOrDefault()).NumberofCodesToGenerate;

                //mark the record as serviced
                ((CreateNotifier_UniqueCodes)record.FirstOrDefault()).IsServiced = true;
                _context.SaveChanges();

                return true;
            }
            else
            {

                numberOfCodesToCreate = 0;
                return false;
            }
        }

        /// <summary>
        /// Insert Unique Codes
        /// </summary>
        /// <param name="uniqueCodes"></param>
        /// <param name="restaurant"></param>
        /// <returns></returns>
        public bool UniqueCode_BulkInsert(string[] uniqueCodes, int restaurantId)
        {
            List<Generated_UniqueCode> UniqueCodeList = new List<Generated_UniqueCode>();

            foreach (string code in uniqueCodes)
            {
                Generated_UniqueCode generated_UniqueCode = new Generated_UniqueCode();
                generated_UniqueCode.UniqueCode = code;
                generated_UniqueCode.DateCreated = DateTime.Today;
                generated_UniqueCode.RestaurantId = restaurantId;
                generated_UniqueCode.IsValidated = false;
                generated_UniqueCode.DateValidated = null;

                UniqueCodeList.Add(generated_UniqueCode);
            }

            try
            {
                int count = 0;
                foreach (var code in UniqueCodeList)
                {
                    _context.Generated_UniqueCode.AddObject(code);
                    count++;

                    if ((count % 100) == 0)
                    {
                        //commit changes to database
                        this.Save();
                    }
                }

                //commit changes to database
                this.Save();

                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                return false;
            }
        }

        public string[] GetUnusedUniqueCodes(int restaurantId, int numberOfCodes)
        {
            var uniqueCodes = (from alpha in _context.Generated_UniqueCode
                              where alpha.RestaurantId == restaurantId && alpha.IsAssigned == false
                              select alpha.UniqueCode).Take(numberOfCodes);

            return uniqueCodes.ToArray();

        }

        /// <summary>
        /// Generates Unique Codes based on the number of 
        /// </summary>
        public void UniqueCodeGenerator()
        {
            //get a list of all restaurants
            IList<Restaurant> restaurants = this.GetRestaurants();

            //for each restaurant check to see if new codes have to be generated
            foreach (Restaurant r in restaurants)
            {
                //this is the number of codes that need to generated
                int numberOfCodes;

                bool isCreateNewCodes = this.IsGenerateUniqueCodes(r.ID, out numberOfCodes);

                if (isCreateNewCodes)
                {
                    UniqueKeyGenerator ucGen = new UniqueKeyGenerator();
                    string[] uniqueCodes = ucGen.GenerateUniqueKeys(numberOfCodes);

                    this.UniqueCode_BulkInsert(uniqueCodes, r.ID);
                }
            }
        }
        #endregion UNIQUE CODE LOGIC

        /// <summary>
        /// Gets a list of all restaurants
        /// </summary>
        /// <returns>List of Restaurant objects</returns>                   mk;k'
        public List<Restaurant> GetRestaurants()
        {
            return _context.Restaurants.ToList();
        }

        /// <summary>
        /// Get a list of all SettlementInfo records for a particular restuarant where email id is NULL
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public List<SettlementInfo> GetSettlementInfoWithNullEmail(int restaurantId)
        {
            var records = from alpha in _context.SettlementInfoes
                         where alpha.EmailAddress == null && alpha.RestaurantId == restaurantId
                         select alpha;

            return (List<SettlementInfo>)records.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <returns></returns>
        public List<SettlementInfo> GetSettlementInfoWithNullEmailAndLoggedInUniqueCode(int restaurantId, List<string> UniqueCodes)
        {
            var records = from alpha in _context.SettlementInfoes
                          where alpha.EmailAddress == null && alpha.RestaurantId == restaurantId && UniqueCodes.Contains(alpha.UniqueCode)
                          select alpha;

            return (List<SettlementInfo>)records.ToList();
        }

        /// <summary>
        /// Get Looged in unique codes
        /// </summary>
        /// <returns></returns>
        public List<string> GetLoggedInUniqueCodes()
        {
            var records = from alpha in _context.UniqueCode_UserMapping select alpha.UniqueCode;
            return records.ToList();
        }

        /// <summary>
        /// If an app user has entered a uniquecode, let's match that up to the Settlement records they own
        /// </summary>
        /// <param name="uniqueCode">string UniqueCode</param>
        /// <param name="restaurantId">int RestaurantId</param>
        /// <returns></returns>
        public string GetEmailAddressFromUniqueCode_UserMapping(string uniqueCode, int restaurantId)
        {
            IQueryable<string> emailAddresses = from alpha in _context.UniqueCode_UserMapping
                                where alpha.UniqueCode == uniqueCode && alpha.RestaurantId == restaurantId
                                select alpha.EmailAddress;

            return emailAddresses.FirstOrDefault();
        }

        /// <summary>
        /// Customer Email Addresses are added to the SettlementInfo records
        /// </summary>
        /// <param name="repo"></param>
        public void AddOwnershipToSettlementInfo()
        {
            //Get a list of restaurants
            IList<Restaurant> restaurants = this.GetRestaurants();

            //this flag is updated if any changes need to be committed to the database
            bool isDatabaseUpdated = false;

            foreach (Restaurant r in restaurants)
            {
                List<string> UniqueCodes = this.GetLoggedInUniqueCodes();
                //Check SettlementInfo table and see if there are new records where EmailAddress is missing
                //IList<SettlementInfo> settlementRecords = this.GetSettlementInfoWithNullEmail(r.ID);
                IList<SettlementInfo> settlementRecords = this.GetSettlementInfoWithNullEmailAndLoggedInUniqueCode(r.ID, UniqueCodes);

                //Check UniqueCode_UserMapping table to see if there's an EmailAddress for each UniqueCode in the information retrieved above
                foreach (SettlementInfo record in settlementRecords)
                {
                    string emailAddress = this.GetEmailAddressFromUniqueCode_UserMapping(record.UniqueCode, r.ID);

                    //ensure that the emailAddress is NOT null or empty
                    if ((null != emailAddress) && (string.Empty != emailAddress) && ("" != emailAddress))
                    {
                        record.EmailAddress = emailAddress;
                        record.DateModified = DateTime.Now;
                        isDatabaseUpdated = true;
                    }
                }
            }

            //commit the database
            if (isDatabaseUpdated)
            {
                this.Save();

                //reset the database commit flag
                isDatabaseUpdated = false;
            }
        }

        /// <summary>
        /// Commit changes to databse
        /// </summary>
        private void Save()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uniqueCodes"></param>
        public void MarkUniqueCodeAsAssigned(string[] uniqueCodes)
        {
            foreach (string uc in uniqueCodes)
            {
                Generated_UniqueCode recordToUpdate = _context.Generated_UniqueCode.Single(i => i.UniqueCode == uc);

                recordToUpdate.IsAssigned = true;
            }

            _context.SaveChanges();
        }

        /// <summary>
        /// Gets the RestaurantId for which the user belongs to
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public int GetRestaurantId(string userName)
        {
            try
            {
                UserProfile userProfile = _context.UserProfiles.Single(i => i.UserName == userName);

                int restaurantId = _context.RestaurantUsers.Single(i => i.UserId == userProfile.UserId).RestaurantId;

                return restaurantId;

            }
            catch (InvalidOperationException ex)
            {
                Trace.WriteLine(ex.Message);

                TempUser tempUser = _context.TempUsers.Single(i => i.EmailAddress == userName);
                return tempUser.RestaurantID;
            }

        }

        /// <summary>
        /// Gets the name of the restaurant
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string GetRestaurantName(string userName)
        {
            try
            {
                //Get the restaurant Id
                int restaurantId = this.GetRestaurantId(userName);

                //Get the restaurant Name
                Restaurant restaurant = _context.Restaurants.Single(i => i.ID == restaurantId);

                return restaurant.RestaurantName;
            }
            catch (InvalidOperationException ex)
            {
                Trace.WriteLine(ex.Message);
            }

            return "Copa Cabana";
        }

        /// <summary>
        /// Sets the user role
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userRole"></param>
        /// <returns></returns>
        public bool SetRole(string userName, int userRole)
        {
            int userId = this.GetUserId(userName);

            return this.SetRole(userId, userRole);
        }

        public int AddUserAndSetRole(string userName, int userRole, int restaurantId, out string message)
        {
            int userId = this.GetUserId(userName);

            if (userId < 0) //user hasn't signed in for the first time
            {
                message = "User doesn't exist. Please have the user register by signing in via Google or Facebook and entering the registration email address.";
                return 0;
            }
            else //user has signed in and user exists in UserProfiles
            {
                if (this.IsUserAlreadyBelongingToBusiness(userId, restaurantId)) //user is part of business
                {
                    if (!this.SetRole(userId, userRole))
                    {
                        message = "Unable to set role";
                        return 3;
                    }
                    else
                    {
                        message = "Role successfully set";
                        return 2;
                    }
                }
                else //user is not part of business
                {
                    if (this.DoWorkOfAddingUserAndSettingRole(userId, userRole, restaurantId))
                    {
                        message = "User added to business and role has been set.";
                        return 1;
                    }
                    else
                    {
                        message = "Unable to add user or set role.";
                        return 4;
                    }
                }
            }

        }

        /// <summary>
        /// Add User and Set Role
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userRole"></param>
        /// <param name="restaurantId"></param>
        /// <returns></returns>
        private bool DoWorkOfAddingUserAndSettingRole(int userId, int userRole, int restaurantId)
        {
            try
            {

                RestaurantUser restaurantUser = new RestaurantUser();
                restaurantUser.UserId = userId;
                restaurantUser.RestaurantId = restaurantId;

                _context.RestaurantUsers.AddObject(restaurantUser);
                _context.SaveChanges();

                this.SetRole(userId, userRole);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Tries setting the role
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userRole"></param>
        /// <returns></returns>
        private bool SetRole(int userId, int userRole)
        {
            try
            {
                UserProfile userProfile = _context.UserProfiles.Single(i => i.UserId == userId);

                //if role already exists, update it
                if (userProfile.webpages_Roles.Count > 0)
                {
                    userProfile.webpages_Roles.Remove(userProfile.webpages_Roles.Single());

                    webpages_Roles role = _context.webpages_Roles.Single(i => i.RoleId == userRole);
                    userProfile.webpages_Roles.Add(role);

                }
                else //role does not exist; create it.
                {
                    webpages_Roles role = _context.webpages_Roles.Single(i => i.RoleId == userRole);
                    userProfile.webpages_Roles.Add(role);
                }


                //BUGBUG
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        /// <summary>
        /// Check if the user has already been added to the restaurant users
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private bool IsUserAlreadyBelongingToBusiness(int userId, int restaurantId)
        {
            try
            {
                RestaurantUser restaurantUser = _context.RestaurantUsers.Single(i => i.UserId == userId && i.RestaurantId == restaurantId);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the UserId for the user
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public int GetUserId(string userName)
        {
            try
            {
                int userId = _context.UserProfiles.Single(i => i.UserName == userName).UserId;

                return userId;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// Gets all the role records
        /// </summary>
        /// <returns></returns>
        public IEnumerable<webpages_Roles> GetRoles()
        {
            return _context.webpages_Roles;
        }

        /// <summary>
        /// Adds a preapproved user to the TempUser table
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="restaurantId"></param>
        /// <param name="roleId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public int AddPreApprovedUser(string emailAddress, int restaurantId, int roleId, out string message)
        {
            try
            {
                IList<TempUser> tempUsers = _context.TempUsers.Where(i => i.EmailAddress == emailAddress).ToList();
                if (tempUsers.Count > 0)
                {
                    message = "User has already been Pre-Approved.";
                    return -1;
                }

                //check if this emailaddress is already a part of restaurantUsers
                if (this.ExistsRestaurantUser(emailAddress))
                {
                    message = "User is already assigned to a restaurant. Please use a different email address";
                    return 0; // 0 --> user already exists. let's not preApprove him/her.
                }
                else
                {
                    TempUser preApprovedUser = new TempUser();
                    preApprovedUser.EmailAddress = emailAddress;
                    preApprovedUser.RestaurantID = restaurantId;
                    preApprovedUser.RoleId = roleId;

                    _context.TempUsers.AddObject(preApprovedUser);
                    _context.SaveChanges();

                    message = "User has been Pre-Approved";
                    return 1;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// Checks to see if this user is already in preApproved Users
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        private bool ExistsPreApprovedUser(string emailAddress)
        {
            try
            {
                TempUser preApprovedUser = _context.TempUsers.Single(i => i.EmailAddress == emailAddress);

                if (null != preApprovedUser)
                    return true;
                else
                    return false;
            }
            catch (InvalidOperationException ex)
            {
                Trace.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Checks if a given user is already part of any restaurant.
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public bool ExistsRestaurantUser(string emailAddress)
        {
            try
            {
                RestaurantUser restaurantUser = _context.RestaurantUsers.Single(i => i.UserProfile.UserName == emailAddress);

                if (null != restaurantUser)
                    return true;
                else
                    return false;
            }
            catch (InvalidOperationException ex)
            {
                Trace.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Gets the Loyalty Types from the database
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LoyaltyType> GetLoyaltyTypes()
        {
            return _context.LoyaltyTypes;
        }

        /// <summary>
        /// Adds a new restuarant to the database
        /// </summary>
        /// <param name="restaurantName"></param>
        /// <param name="restaurantId"></param>
        /// <param name="loyaltyTypeId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public int AddNewRestaurant(string restaurantName, int restaurantId, int loyaltyTypeId, out string message)
        {
            try
            {
                //check if this emailaddress is already a part of restaurantUsers
                if (this.ExistsRestaurant(restaurantId, restaurantName))
                {
                    message = "Restaurant with same ID or Name exists. Please use unique IDs and Names";
                    return 0; // 0 --> user already exists. let's not preApprove him/her.
                }
                else
                {
                    Restaurant r = new Restaurant();
                    r.ID = restaurantId;
                    r.RestaurantName = restaurantName;
                    r.LoyaltyID = loyaltyTypeId;

                    _context.Restaurants.AddObject(r);
                    _context.SaveChanges();

                    message = restaurantName + " has been added.";
                    return 1;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// Checks for duplicate restaurants
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <param name="restaurantName"></param>
        /// <returns></returns>
        private bool ExistsRestaurant(int restaurantId, string restaurantName)
        {
            try
            {
                IList<Restaurant> restaurants = _context.Restaurants.Where(i => i.RestaurantName == restaurantName || i.ID == restaurantId).ToList();

                if(restaurants.Count > 0)
                    return true;
                else
                    return false;
            }
            catch (InvalidOperationException ex)
            {
                Trace.WriteLine(ex.Message);
                return false;
            }

        }

        /// <summary>
        /// Checks if a user has been pre-approved.
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public bool IsPreApprovedUser(string emailAddress)
        {
            try
            {
                IList<TempUser> preApprovedUsers = _context.TempUsers.Where(i => i.EmailAddress == emailAddress).ToList();

                if (preApprovedUsers.Count > 0)
                    return true;
                else
                    return false;
            }
            catch (InvalidOperationException ex)
            {
                Trace.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Gets the UserRole of the PreApproved User
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public int GetPreApprovedUserRole(string emailAddress)
        {
            try
            {
                var preApprovedUser = _context.TempUsers.Single(i => i.EmailAddress == emailAddress);
                return preApprovedUser.RoleId;
            }
            catch (InvalidOperationException ex)
            {
                Trace.WriteLine(ex.Message);
                return -1;
            }
        }

        /// <summary>
        /// Adds the pre-approved user to the Restaurant Users table
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="restaurantId"></param>
        /// <returns></returns>
        public bool AddToRestaurantUsersAndRemoveFromTempUser(string userName)
        {
            //If the user is already a restaurant user, do nothing and return true
            if (this.ExistsRestaurantUser(userName))
            {
                return true;
            }

            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                try
                {
                    //get the temp user
                    var tempUser = _context.TempUsers.Single(i => i.EmailAddress == userName);

                    //add the restaurant user
                    RestaurantUser restaurantUser = new RestaurantUser();
                    restaurantUser.RestaurantId = tempUser.RestaurantID;
                    restaurantUser.UserId = this.GetUserId(userName);

                    _context.RestaurantUsers.AddObject(restaurantUser);

                    //remove the temp user
                    _context.TempUsers.DeleteObject(tempUser);

                    //save changes
                    _context.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);

                    scope.Complete();

                    return true;

                }
                catch (InvalidOperationException ex)
                {
                    Trace.WriteLine(ex.Message);
                    return false;
                }
            }
        }

        public List<Reward> GetAllRewardsAndGiftCards(int restaurantId)
        {
            var rewards = _context.Rewards.Where(i => i.RestaurantID == restaurantId).ToList();

            return rewards;
        }

        public List<Reward> GetAllRewards(int restaurantId)
        {
            var rewards = _context.Rewards.Where(i => i.RestaurantID == restaurantId && i.RewardType == 1).ToList();

            return rewards;
        }

        public List<Reward> GetAllGiftCards(int restaurantId)
        {
            var giftCardRewards = _context.Rewards.Where(i => i.RestaurantID == restaurantId && i.RewardType == 2).ToList();

            return giftCardRewards;
        }

        /// <summary>
        /// Adds a reward
        /// </summary>
        /// <param name="reward"></param>
        /// <returns></returns>
        public object AddReward(Reward reward)
        {
            _context.Rewards.AddObject(reward);
            _context.SaveChanges();

            return reward;
        }

        /// <summary>
        /// Update a reward
        /// </summary>
        /// <param name="reward"></param>
        public void UpdateReward(Reward reward)
        {
            var rewardToUpdate = _context.Rewards.Single(i => i.Id == reward.Id);
            if (null == rewardToUpdate)
                return;

            //make the updates
            rewardToUpdate.Name = reward.Name;
            rewardToUpdate.Description = reward.Description;
            rewardToUpdate.NumberOfItems = reward.NumberOfItems;
            rewardToUpdate.EligibleSKUs = reward.EligibleSKUs;
            rewardToUpdate.RewardSKUs = reward.RewardSKUs;
            rewardToUpdate.Image = reward.Image;
            rewardToUpdate.Multiplier = reward.Multiplier;

            //commit the changes to the database
            _context.SaveChanges();
        }

        /// <summary>
        /// Deletes a reward
        /// </summary>
        /// <param name="rewardId"></param>
        public void DeleteReward(int rewardId)
        {
            var foundReward = _context.Rewards.Single(i => i.Id == rewardId);

            _context.Rewards.DeleteObject(foundReward);

            //commit the changes to the database
            _context.SaveChanges();
        }

        /// <summary>
        /// Gets all the coupons created by this admin
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <param name="adminUserId"></param>
        /// <returns></returns>
        public IList<Generated_CouponCode> GetAllCouponsCreateByAdmin(int restaurantId, int adminUserId)
        {
            try
            {
                UserProfile userProfile = _context.UserProfiles.Single(i => i.UserId == adminUserId);

                IList<Generated_CouponCode> coupons = (from c in _context.Generated_CouponCode
                                                      join u in _context.UserProfiles on c.UserProfile.UserId equals u.UserId
                                                      where c.RestaurantId == restaurantId 
                                                            && u.UserId == adminUserId
                                                      select c).ToList();

                return coupons;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Adds a coupon code to the database
        /// </summary>
        /// <param name="couponCode"></param>
        /// <returns></returns>
        public Generated_CouponCode AddNewCouponCode(int restaurantId, int rewardId, string emailAddress, int adminUserId)
        {
            try
            {
                UniqueKeyGenerator keyGen = new UniqueKeyGenerator();
                string couponCode = keyGen.GenerateUniqueKey();

                IList<Generated_CouponCode> foundCoupons = _context.Generated_CouponCode.Where(i => i.CouponCode == couponCode).ToList();
                Reward reward = _context.Rewards.Single(r => r.Id == rewardId);

                if (foundCoupons.Count != 0)
                {
                    Trace.WriteLine("Cannot insert duplicate coupon codes. Please try again!");
                    return null;
                }
                else
                {
                    //insert the coupon code
                    Generated_CouponCode couponCodeToAdd = new Generated_CouponCode();
                    couponCodeToAdd.CouponCode = couponCode;
                    couponCodeToAdd.DateCreated = DateTime.Now;
                    couponCodeToAdd.ExpirationDate = DateTime.Now.AddDays(Double.Parse(reward.Validity.ToString()));
                    couponCodeToAdd.DateRedeemed = null;
                    couponCodeToAdd.EmailAddress = emailAddress;
                    couponCodeToAdd.IsAssigned = true;
                    couponCodeToAdd.IsRedeemed = false;
                    couponCodeToAdd.RestaurantId = restaurantId;
                    couponCodeToAdd.RewardId = rewardId;

                    //insert the mapping
                    couponCodeToAdd.UserProfile = _context.UserProfiles.Single(i => i.UserId == adminUserId);

                    _context.Generated_CouponCode.AddObject(couponCodeToAdd);
                    _context.SaveChanges();

                    return couponCodeToAdd;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="couponCode"></param>
        /// <param name="restautantId"></param>
        /// <returns></returns>
        public Generated_CouponCode GetCouponCodeByCode(string couponCode, int restautantId)
        {
            try
            {
                var foundCouponCode = _context.Generated_CouponCode.FirstOrDefault(c => c.CouponCode == couponCode && c.RestaurantId == restautantId);
                if (foundCouponCode != null)
                {
                    return foundCouponCode;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Generated_SimpleCouponCode GetLatestSimpleCouponCode(int restaurantId)
        {
            try
            {
                Generated_SimpleCouponCode simpleCouponCode = _context.Generated_SimpleCouponCode.First(s => s.IsAssigned == false && s.RestaurantId == restaurantId);
                if (simpleCouponCode != null)
                {
                    return simpleCouponCode;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                return null;
            }
            
        }

        public Boolean SetSimpleCouponCodeIsAssined(int restaurantId, string couponCode)
        {
            try
            {
                Generated_SimpleCouponCode simpleCouponCode = _context.Generated_SimpleCouponCode.First(s => s.RestaurantId == restaurantId && s.UniqueCode == couponCode);
                simpleCouponCode.IsAssigned = true;
                simpleCouponCode.DateAssigned = DateTime.Now;
                _context.Generated_SimpleCouponCode.ApplyCurrentValues(simpleCouponCode);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Generated_CouponCode AssignCouponCodeToUser(int restaurantId, string couponCode, int rewardId, string emailAddress, int adminUserId, DateTime expirationDate)
        {
            try
            {
                
                IList<Generated_CouponCode> foundCoupons = _context.Generated_CouponCode.Where(i => i.CouponCode == couponCode).ToList();
                Reward reward = _context.Rewards.Single(r => r.Id == rewardId);
                DateTime validity;
                if (expirationDate == null || DateTime.Compare(expirationDate, DateTime.Now) < 1)
                {
                    validity = DateTime.Now.AddDays(Double.Parse(reward.Validity.ToString()));
                }
                else
                {
                    validity = expirationDate;
                }


                if (foundCoupons.Count != 0)
                {
                    Trace.WriteLine("Cannot insert duplicate coupon codes. Please try again!");
                    return null;
                }
                else
                {
                    //insert the coupon code
                    Generated_CouponCode couponCodeToAdd = new Generated_CouponCode();
                    couponCodeToAdd.CouponCode = couponCode;
                    couponCodeToAdd.DateCreated = DateTime.Now;
                    couponCodeToAdd.ExpirationDate = validity;
                    couponCodeToAdd.DateRedeemed = null;
                    couponCodeToAdd.EmailAddress = emailAddress;
                    couponCodeToAdd.IsAssigned = true;
                    couponCodeToAdd.IsRedeemed = false;
                    couponCodeToAdd.RestaurantId = restaurantId;
                    couponCodeToAdd.RewardId = rewardId;

                    //insert the mapping
                    couponCodeToAdd.UserProfile = _context.UserProfiles.Single(i => i.UserId == adminUserId);

                    _context.Generated_CouponCode.AddObject(couponCodeToAdd);
                    _context.SaveChanges();

                    return couponCodeToAdd;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                return null;
            }
        }

        public Boolean UpdateLastReadPayload(int restaurantId, string fileName)
        {
            try
            {
                var foundPayload = _context.Payloads.FirstOrDefault(p => p.LastReadFile == fileName && p.RestaurantID == restaurantId);
                if (foundPayload == null)
                {
                    Payload payload = new Payload();
                    payload.RestaurantID = restaurantId;
                    payload.LastReadFile = fileName;
                    payload.DateCreated = DateTime.Now;
                    _context.Payloads.AddObject(payload);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public Boolean CheckIfPayloadAlreadyRead(int restaurantId, string fileName)
        {
            try
            {
                var foundPayload = _context.Payloads.FirstOrDefault(p => p.LastReadFile == fileName && p.RestaurantID == restaurantId);
                if (foundPayload == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        #region Logging critical error to DB

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public Boolean LogError(string errorMessage, string action)
        {
            try
            {
                ErrorLogs errorLog = new ErrorLogs();
                errorLog.Error = errorMessage;
                errorLog.DateCreated = DateTime.Now;
                errorLog.Action = action;
                _context.ErrorLogs.AddObject(errorLog);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }

        #endregion

        #region Helper function
        public DateTime GetUTCTimeNow()
        {
            DateTime dt = DateTime.Now.ToUniversalTime();
            //DateTime utcTime = DateTime.SpecifyKind(dt, DateTimeKind.Utc);
            return dt;
        }

        #endregion

    }
}