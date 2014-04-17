using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoyaltyAdministration.Models;
using eMunching_Loyalty_DataManager;


namespace LoyaltyAdministration.Controllers
{
    public class ProvisionRestaurantController : Controller
    {
        Repository repo = new Repository();

        //
        // GET: /ProvisionRestaurant/

        [Authorize(Roles = "SuperAdmin")]
        public ActionResult Index()
        {
            SelectList list = new SelectList(repo.GetLoyaltyTypes(), "ID", "LoyaltyType1");
            ViewData["LoyaltyTypes"] = list;

            return View();
        }

        [HttpPost]
        public ActionResult Index(RestaurantModel model)
        {
            string restaurantName = model.RestaurantName;
            int restaurantId = model.RestaurantId;
            int loyaltyTypeId = model.LoyaltyTypeId;

            string message;

            switch (repo.AddNewRestaurant(restaurantName, restaurantId, loyaltyTypeId, out message))
            {
                case 0:
                    //user already exists case
                    ViewData["ProvisionRestaurantStatus"] = "USER EXISTS: ";
                    ViewData["ProvisionRestaurantMessage"] = message;
                    return View();
                case 1:
                    //success case
                    ViewData["ProvisionRestaurantStatus"] = "SUCCESS: ";
                    ViewData["ProvisionRestaurantMessage"] = message;
                    return View();
                case -1:
                    //error case
                    ViewData["ProvisionRestaurantStatus"] = "ERROR: ";
                    ViewData["ProvisionRestaurantMessage"] = message;
                    return View();
                default:
                    ViewData["ProvisionRestaurantStatus"] = "ERROR: ";
                    ViewData["ProvisionRestaurantMessage"] = "Unable to add Restaurant! Please contact technical support at support@emunching.com with detailed message.";
                    return View();
            }
        }
    }
}

