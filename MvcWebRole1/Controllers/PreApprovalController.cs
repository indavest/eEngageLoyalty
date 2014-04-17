using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoyaltyAdministration.Models;
using eMunching_Loyalty_DataManager;


namespace LoyaltyAdministration.Controllers
{
    public class PreApprovalController : Controller
    {
        Repository repo = new Repository();

        //
        // GET: /ProgramManager/
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult Index()
        {
            SelectList list = new SelectList(repo.GetRoles(), "RoleId", "RoleName", 3);
            ViewData["Roles"] = list;

            SelectList restaurantList = new SelectList(repo.GetRestaurants(), "ID", "RestaurantName");
            ViewData["Restaurants"] = restaurantList;

            return View();
        }

        [HttpPost]
        public ActionResult Index(PreApprovedUserModel model)
        {
            string emailAddress = model.EmailAddress;
            int restaurantId = model.RestaurantId;
            int roleId = model.RoleId;

            string message;

            switch (repo.AddPreApprovedUser(emailAddress, restaurantId, roleId, out message))
            {
                case 0:
                    //user already exists case
                    ViewData["PreApprovalStatus"] = "USER EXISTS: ";
                    ViewData["PreApprovalMessage"] = message;
                    return View();
                case 1:
                    //success case
                    ViewData["PreApprovalStatus"] = "SUCCESS: ";
                    ViewData["PreApprovalMessage"] = message;
                    return View();
                case -1:
                    //error case
                    ViewData["PreApprovalStatus"] = "ERROR: ";
                    ViewData["PreApprovalMessage"] = message;
                    return View();
                default:
                    ViewData["PreApprovalStatus"] = "ERROR: ";
                    ViewData["PreApprovalMessage"] = "Unable to add Pre-Approved User! Please contact technical support at support@emunching.com with detailed message.";
                    return View();
            }
        }
    }
}
