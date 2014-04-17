using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eMunching_Loyalty_DataManager;
using System.Security.Cryptography;
using System.Text;

namespace LoyaltyAdministration.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Roles="SuperAdmin, Administrator, Redeemer")]
        public ActionResult Index()
        {
            Repository repo = new Repository();

            if (User.Identity.IsAuthenticated)
            {
                Session["RestaurantId"] = repo.GetRestaurantId(User.Identity.Name);
                Session["RestaurantName"] = repo.GetRestaurantName(User.Identity.Name);
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public string GenerateUniqueCodes()
        {
            int maxSize = 8;

            char[] chars = new char[62];
            string a;

            a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            chars = a.ToCharArray();

            int size = maxSize;
            byte[] data = new byte[1];

            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);

            size = maxSize;
            data = new byte[size];
            crypto.GetNonZeroBytes(data);

            StringBuilder result = new StringBuilder(size);

            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length - 1)]);
                //Response.Write(chars[b % (chars.Length - 1)]);
            }

            return result.ToString();
            //return View("Test");
        }

        public ActionResult UniqueCodeTest()
        {
            int count = 100000;

            for (int loop = 0; loop < count; loop++)
            {
                Response.Write(this.GenerateUniqueCodes()+"<br>");
            }

            //return result.ToString();
            return View("Test");
        }
    }
}
