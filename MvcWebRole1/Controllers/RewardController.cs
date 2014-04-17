using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoyaltyAdministration.Models;
using eMunching_Loyalty_DataManager;
using System.IO;
using System.Web.Hosting;

namespace LoyaltyAdministration.Controllers
{
    public class RewardController : Controller
    {
        //
        // GET: /Rewards/
        [Authorize(Roles="Administrator")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Index(PunchCardRewardModel model)
        {
            return View();
        }

        /// <summary>
        /// Gets a list of rewards for the particular restuarant.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public JsonResult RewardList()
        {
            int restaurantId = (int)Session["RestaurantId"];

            try
            {
                Repository repo = new Repository();
                List<Reward> rewards = repo.GetAllRewards(restaurantId);

                List<Reward> retVal = new List<Reward>();

                foreach (Reward reward in rewards)
                {
                    string image = (string.IsNullOrEmpty(reward.Image))?"http://www.emunching.com/images/no_image.png":reward.Image;
                    retVal.Add(new Reward
                    {
                        Id = reward.Id,
                        RewardSKUs = reward.RewardSKUs,
                        Name = reward.Name,
                        Description = reward.Description,
                        EligibleSKUs = reward.EligibleSKUs,
                        NumberOfItems = reward.NumberOfItems,
                        RestaurantID = reward.RestaurantID,
                        Image = image,
                        Validity = reward.Validity,
                        Multiplier = reward.Multiplier
                    });
                }
                

                return Json(new { Result = "OK", Records = retVal }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        /// <summary>
        /// Gets a list of rewards for the particular restuarant.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public JsonResult RewardOptions()
        {
            int restaurantId = (int)Session["RestaurantId"];

            try
            {
                Repository repo = new Repository();
                List<Reward> rewards = repo.GetAllRewardsAndGiftCards(restaurantId);

                List<object> retVal = new List<object>();

                foreach (Reward reward in rewards)
                {
                    retVal.Add(new
                    {
                        DisplayText = reward.Name,
                        Value = reward.Id.ToString()
                    });
                }


                return Json(new { Result = "OK", Options = retVal }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        /// <summary>
        /// Creates a new reward
        /// </summary>
        /// <param name="reward"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public JsonResult CreateReward(Reward reward)
        {
            reward.RestaurantID = (int)Session["RestaurantId"];

            Repository repo = new Repository();
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

                reward.RewardType = 1;
                Reward retVal = new Reward();
                Reward addedReward = (Reward)repo.AddReward(reward);

                retVal.Id = addedReward.Id;
                retVal.Name = addedReward.Name;
                retVal.Description = addedReward.Description;
                retVal.EligibleSKUs = addedReward.EligibleSKUs;
                retVal.RewardSKUs = addedReward.RewardSKUs;
                retVal.NumberOfItems = addedReward.NumberOfItems;
                retVal.RestaurantID = addedReward.RestaurantID;
                retVal.Image = addedReward.Image;
                retVal.Validity = addedReward.Validity;
                retVal.Multiplier = addedReward.Multiplier;

                return Json(new { Result = "OK", Record = retVal });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        /// <summary>
        /// Updates and existing reward
        /// </summary>
        /// <param name="reward"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public JsonResult UpdateReward(Reward reward)
        {
            reward.RestaurantID = (int)Session["RestaurantId"];

            Repository repo = new Repository();

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
                repo.UpdateReward(reward);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        /// <summary>
        /// Deletes a reward
        /// </summary>
        /// <param name="rewardId"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public JsonResult DeleteReward(int Id)
        {
            Repository repo = new Repository();

            try
            {
                repo.DeleteReward(Id);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles= "Administrator")]
        public JsonResult GetRewardImages()
        {
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(HostingEnvironment.ApplicationPhysicalPath + "Images/Rewards");
                FileInfo[] tempFiles = directoryInfo.GetFiles("*.*");
                List<string> fileNames = new List<string>();
                foreach (FileInfo fileInfo in tempFiles)
                {
                    if (fileInfo.Name != "no_image.png")
                    {
                        fileNames.Add(fileInfo.Name);
                    }
                }
                return Json(new { Result = "OK", Data = fileNames });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = ex.Message });
            }
        }

        public ActionResult GiftCard()
        {
            return View("GiftCard");
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public JsonResult GiftCardRewardList()
        {
            int restaurantId = (int)Session["RestaurantId"];

            try
            {
                Repository repo = new Repository();
                List<Reward> rewards = repo.GetAllGiftCards(restaurantId);

                List<Reward> retVal = new List<Reward>();

                foreach (Reward reward in rewards)
                {
                    string image = (string.IsNullOrEmpty(reward.Image)) ? "http://www.emunching.com/images/no_image.png" : reward.Image;
                    retVal.Add(new Reward
                    {
                        Id = reward.Id,
                        Name = reward.Name,
                        Description = reward.Description,
                        RestaurantID = reward.RestaurantID
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
        public JsonResult CreateGiftCardReward(Reward reward)
        {
            reward.RestaurantID = (int)Session["RestaurantId"];

            Repository repo = new Repository();
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

                reward.RewardType = 2;
                reward.NumberOfItems = 0;
                reward.EligibleSKUs = "0";
                reward.RewardSKUs = "0";
                reward.Multiplier = 1;
                reward.Validity = 60;
                Reward retVal = new Reward();
                Reward addedReward = (Reward)repo.AddReward(reward);

                retVal.Id = addedReward.Id;
                retVal.Name = addedReward.Name;
                retVal.Description = addedReward.Description;
                

                return Json(new { Result = "OK", Record = retVal });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public JsonResult UpdateGiftCardReward(Reward reward)
        {
            reward.RestaurantID = (int)Session["RestaurantId"];

            Repository repo = new Repository();

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
                reward.RewardType = 2;
                reward.NumberOfItems = 0;
                reward.EligibleSKUs = "0";
                reward.RewardSKUs = "0";
                reward.Multiplier = 1;
                reward.Validity = 60;
                repo.UpdateReward(reward);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public JsonResult DeleteGiftCardReward(int Id)
        {
            Repository repo = new Repository();

            try
            {
                repo.DeleteReward(Id);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}
