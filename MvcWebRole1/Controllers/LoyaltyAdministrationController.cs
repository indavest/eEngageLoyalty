using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoyaltyAdministration.Controllers
{
    public class LoyaltyAdministrationController : Controller
    {
        //
        // GET: /LoyaltyAdministration/
        [Authorize(Roles="Administrator, SuperAdmin")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
