using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eMunching_Loyalty_DataManager;
using LoyaltyAdministration.Models;
using eMunching_Loyalty_DataManager.eMunchingServices;

namespace LoyaltyAdministration.Controllers
{
    public class RedemptionController : Controller
    {
        Repository repo = new Repository();
        private eMunching_LoyaltyEntities _context = new eMunching_LoyaltyEntities();
        /// <summary>
        /// This is the default end point for redemption
        /// This is where we display a form to enter redemption information
        /// </summary>
        /// <returns></returns>
        //[Authorize(Roles = "SuperAdmin, Administrator, Redeemer")]
        //public ActionResult Index(int restaurantId)
        //{
        //    return View();
        //}

        /// <summary>
        /// This is the default end point for redemption
        /// This is where we display a form to enter redemption information
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "SuperAdmin, Administrator, Redeemer")]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// This controller method runs when we submit a couponCode on this page
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize(Roles = "SuperAdmin, Administrator, Redeemer")]
        [HttpPost]
        public ActionResult Index(RedemptionModel model)
        {
            try
            {
                string couponCode = model.CouponCode;
                //int restaurantId = model.RestaurantId;
                //int restaurantId = (int)Session["RestaurantId"];
                int restaurantId = 67;
                string rewardName = String.Empty;
                string userName = String.Empty;
                string description = string.Empty;
                Generated_CouponCode couponCodeObject = _context.Generated_CouponCode.FirstOrDefault(c => c.CouponCode == couponCode);
                switch (repo.ValidateCouponCodeAndCheckIfRedeemed(couponCode, restaurantId))
                {
                    case 0: //no such code exists
                        ViewData["Status"] = "ERROR: ";
                        ViewData["Message"] = "Invalid Coupon Code " + "{" + couponCode + "}" + ". Please check for typos!";
                        return View();
                    case 1: //valid, unused coupon code. REDEEM IT
                        ViewData["Status"] = "Success";
                        ViewData["Message"] = "This Coupon Code " + "{" + couponCode + "}" + " is ready to redeem.";
                        ViewData["username"] = couponCodeObject.EmailAddress;
                        ViewData["Description"] = couponCodeObject.Reward.Description;
                        ViewData["IsReady"] = 1;
                        return View();
                    case 2: //valid code, but OOPS, ITS BEEN USED
                        DateTime dateRedeemed = TimeZone.CurrentTimeZone.ToLocalTime((DateTime)couponCodeObject.DateRedeemed);;
                        ViewData["Status"] = "ERROR: ";
                        ViewData["Message"] = "This Coupon Code " + "{" + couponCode + "}" + " has already been used on "+dateRedeemed.ToString("f")+".";
                        ViewData["username"] = couponCodeObject.EmailAddress;
                        ViewData["Description"] = couponCodeObject.Reward.Description;

                        return View();
                    case 3: //valid code, but it has been expirede
                        ViewData["Status"] = "ERROR: ";
                        ViewData["Message"] = "This Coupon Code " + "{" + couponCode + "}" + " is expired on " + couponCodeObject.ExpirationDate;
                        ViewData["RewardName"] = rewardName;
                        ViewData["username"] = userName;
                        return View();
                    default: //we should never get here. 0, 1, 2 and 3 are the only correct responses
                        ViewData["Status"] = "ERROR: ";
                        ViewData["Message"] = "Unknown Error occurred. Please retry!";
                        ViewData["RewardName"] = rewardName;
                        ViewData["username"] = userName;

                        return View();
                }
            }
            catch (Exception ex)
            {
                repo.LogError(ex.Message + ex.InnerException + "CouponCode="+model.CouponCode, "redemption");
                ViewData["Status"] = "Exception: ";
                ViewData["Message"] = "Please report this to admin";
                return View();
            }
        }

        [Authorize(Roles = "SuperAdmin, Administrator, Redeemer")]
        public ActionResult Redeem()
        {
            return View("Index");
        }

        [HttpPost]
        public ActionResult Redeem(RedemptionModel model)
        {
            string couponCode = model.CouponCode;
            //int restaurantId = model.RestaurantId;
            //int restaurantId = (int)Session["RestaurantId"];
            int restaurantId = 67;
            string rewardName = String.Empty;
            string userName = String.Empty;
            Generated_CouponCode couponObject = repo.GetCouponCodeByCode(couponCode, restaurantId);
            if (repo.RedeemCouponCode(couponCode, restaurantId, out rewardName, out userName))
            {
                DateTime dateRedeemed = TimeZone.CurrentTimeZone.ToLocalTime((DateTime)couponObject.DateRedeemed);
                ViewData["Status"] = "SUCCESS: ";
                ViewData["Message"] = "This Coupon Code " + "{" + couponCode + "}" + " has been redeemed on " + dateRedeemed.ToString("f") + ".";
                ViewData["RewardName"] = rewardName;
                ViewData["username"] = userName;
                return View("Index");
            }
            else
            {
                ViewData["Status"] = "ERROR: ";
                ViewData["Message"] = "Unknown Error occurred. Please retry!";
                ViewData["RewardName"] = rewardName;
                ViewData["username"] = userName;

                return View("Index");
            }
        }
    }
}
