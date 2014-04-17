using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoyaltyAdministration.Models;
using eMunching_Loyalty_DataManager;
using System.Diagnostics;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using System.Threading;

namespace LoyaltyAdministration.Controllers
{
    public class CouponController : Controller
    {
        //
        // GET: /CouponGiveaway/

        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Index(GiveawayCouponModel model)
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public JsonResult CouponList()
        {
            int restaurantId = (int)Session["RestaurantId"];
            string adminUserName = User.Identity.Name;

            try
            {
                Repository repo = new Repository();
                int adminUserId = repo.GetUserId(adminUserName);
                IList<Generated_CouponCode> coupons = repo.GetAllCouponsCreateByAdmin(restaurantId, adminUserId);
                
                List<GiveawayCouponModel> retVal = new List<GiveawayCouponModel>();

                foreach (Generated_CouponCode coupon in coupons)
                {
                    retVal.Add(new GiveawayCouponModel
                    {
                        CouponCode = coupon.CouponCode,
                        RewardName = coupon.Reward.Name,
                        RewardId = coupon.RewardId,
                        EmailAddress = coupon.EmailAddress,
                        AdminUserId = adminUserId,
                        AdminUserName = adminUserName,
                        IsRedeemed = coupon.IsRedeemed,
                        DateCreated = coupon.DateCreated,
                        DateRedeemed = coupon.DateRedeemed,
                        ExpirationDate = (DateTime)coupon.ExpirationDate
                    });
                }

                return Json(new { Result = "OK", Records = retVal }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public JsonResult CreateCoupon(int rewardId, string emailAddress, DateTime expirationDate)
        {
            int restaurantId = (int)Session["RestaurantId"];
            string adminUserName = User.Identity.Name;

            Repository repo = new Repository();
            int adminUserId = repo.GetUserId(adminUserName);

            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new
                    {
                        Result = "ERROR",
                        Message = "Form is not valid! " +
                        "Please correct it and try again."
                    });
                }
                Generated_SimpleCouponCode simpleCouponCode = repo.GetLatestSimpleCouponCode(restaurantId);
                if (repo.SetSimpleCouponCodeIsAssined(restaurantId, simpleCouponCode.UniqueCode))
                {
                    Generated_CouponCode addedCouponCode = (Generated_CouponCode)repo.AssignCouponCodeToUser(restaurantId, simpleCouponCode.UniqueCode, rewardId, emailAddress, adminUserId, expirationDate);
                    //var sendEmailThread = new Thread(x => LoyaltyHelper.sendEmail(emailAddress, "", "New Coupon Code", "New Coupon Code " + addedCouponCode.CouponCode, "Toit Loyalty", "Toit Loyalty"));
                    //sendEmailThread.Start();
                    var retVal = new
                    {
                        CouponCode = addedCouponCode.CouponCode,
                        RewardName = addedCouponCode.Reward.Name,
                        EmailAddress = addedCouponCode.EmailAddress,
                        AdminUserName = addedCouponCode.UserProfile.UserName,
                        IsRedeemed = addedCouponCode.IsRedeemed,
                        DateCreated = addedCouponCode.DateCreated,
                        DateRedeemed = addedCouponCode.DateRedeemed,
                        ExpirationDate = addedCouponCode.ExpirationDate
                    };
                    return Json(new { Result = "OK", Record = retVal });
                }
                else
                {
                    return Json(new { Result = "ERROR", Message = "Error while assigning coupon" });
                }
                
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpGet]
        public ActionResult Generate()
        {
            Repository repo = new Repository();
            int restaurantId = (int)Session["RestaurantId"];
            List<Reward> rewardList = repo.GetAllRewardsAndGiftCards(restaurantId);
            ViewBag.data = rewardList;
            return View("Generate");
        }

        [HttpPost]
        public ActionResult Generate(int coupon_number, int reward, string expiry_date)
        {
            Repository repo = new Repository();
            List<string> couponCodes = new List<string>();
            Generated_SimpleCouponCode simpleCouponCode;
            string adminUserName = User.Identity.Name;
            DateTime expirationDate = DateTime.Parse(expiry_date);
            int restaurantId = (int)Session["RestaurantId"];
            try
            {
                int adminUserId = repo.GetUserId(adminUserName);
                for (int i = 0; i < coupon_number; i++)
                {
                    simpleCouponCode = repo.GetLatestSimpleCouponCode(67);
                    couponCodes.Add(simpleCouponCode.UniqueCode);
                    repo.SetSimpleCouponCodeIsAssined(restaurantId, simpleCouponCode.UniqueCode);
                    repo.AssignCouponCodeToUser(restaurantId, simpleCouponCode.UniqueCode, reward, "toitblr@toit.in", adminUserId, expirationDate);
                }

                //Generate excel for download
                var couponsTable = new System.Data.DataTable("Coupons");
                couponsTable.Columns.Add("CouponCode", typeof(string));
                foreach (string couponCode in couponCodes)
                {
                    couponsTable.Rows.Add(couponCode);
                }

                var grid = new GridView();
                grid.DataSource = couponsTable;
                grid.DataBind();

                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename="+coupon_number+"_Coupons_for_"+reward+".xls");
                Response.ContentType = "application/ms-excel";

                Response.Charset = "";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                grid.RenderControl(htw);

                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();

            }
            catch (Exception ex)
            {
            }
            return View("Generate");
        }
    }
}
