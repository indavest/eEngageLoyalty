using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoyaltyAdministration.Models;
using eMunching_Loyalty_DataManager;

namespace LoyaltyAdministration.Controllers
{
    [Authorize(Roles="SuperAdmin, Administrator")]
    public class BusinessManagerController : Controller
    {
        Repository repo = new Repository();

        //
        // GET: /BusinessManager/
        [Authorize(Roles = "SuperAdmin, Administrator")]
        public ActionResult Index()
        {
            SelectList list = new SelectList(repo.GetRoles(), "RoleId", "RoleName", 3);
            ViewData["Roles"] = list;

            return View();
        }

        [Authorize(Roles="SuperAdmin, Administrator")]
        [HttpPost]
        public ActionResult Index(NewUserModel model)
        {
            string userName = model.UserName;
            int userRole = model.RoleId;
            int restaurantId = (int)Session["RestaurantId"];
            string message;

            switch(repo.AddUserAndSetRole(userName, userRole, restaurantId, out message))
            {
                case 0:
                case 3:
                case 4:
                    ViewData["Status"] = "ERROR: ";
                    ViewData["Message"] = message;
                    return View();
                case 1:
                case 2: 
                    ViewData["Status"] = "SUCCESS: ";
                    ViewData["Message"] = message;
                    return View();
                default:
                    ViewData["Status"] = "ERROR: ";
                    ViewData["Message"] = "Unable to add user or set role! Please contact technical support at support@emunching.com with detailed message.";
                    return View();
            }
        }
    }
}
