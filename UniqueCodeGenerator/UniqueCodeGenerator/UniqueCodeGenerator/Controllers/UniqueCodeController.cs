using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace UniqueCodeGenerator.Controllers
{
    public class UniqueCodeController : Controller
    {

        private Repository repo = new Repository();
        private loyaltyUniqueCodes _context = new loyaltyUniqueCodes();
        //
        // GET: /UniqueCode/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GenerateUniqueCodes(int numberOfKeys)
        {
            string[] keys = new string[numberOfKeys];

            for (int loop = 0; loop < numberOfKeys; loop++)
            {
                keys[loop] = this.RNGCharacterMask();
            }

            repo.UniqueCode_BulkInsert(keys, 67);
            return View("Generate");
        }

        private string RNGCharacterMask()
        {
            int maxSize = 8;

            char[] chars = new char[62];
            string a;

            a = "abcdefghjkmnpqrstuvwxyz23456789";
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
            }

            return result.ToString();
        }

        private string RNGCharacterMaskCouponCode()
        {
            int maxSize = 8;

            char[] chars = new char[62];
            string a;

            a = "1234567890";
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
            }

            return result.ToString();
        }

        public string GenerateCouponCode()
        {
            return this.RNGCharacterMaskCouponCode();
        }


        public void AddCouponsToDatabase()
        {
            try
            {
                List<Guid> guids = new List<Guid>();
                for (int i = 0; i < 20000; i++)
                {
                    string couponCode = this.GenerateCouponCode();
                    Generated_CouponCode ccExist = repo.GetCouponCode(couponCode);
                    if (ccExist != null)
                    {
                        Response.Write(ccExist.CouponCode + "<br>");
                        continue;
                    }
                    Generated_CouponCode cc = new Generated_CouponCode();
                    cc.CouponCode = couponCode;
                    cc.RestaurantId = 67;
                    cc.EmailAddress = "test@test.com";
                    cc.IsAssigned = false;
                    cc.IsRedeemed = false;
                    cc.DateCreated = DateTime.Now;
                    cc.RewardId = 1;

                    _context.AddToGenerated_CouponCode(cc);
                    _context.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            
        }
    }
}
