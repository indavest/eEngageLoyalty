using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data.Objects;

namespace UniqueCodeGenerator
{
    public class Repository
    {
        private loyaltyUniqueCodes _context = new loyaltyUniqueCodes();
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

                    //if ((count % 100) == 0)
                    //{
                        //commit changes to database
                        //this.Save();
                    //}
                }

                //commit changes to database
                //this.Save();
                _context.SaveChanges();
                

                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Commit changes to databse
        /// </summary>
        private void Save()
        {
            _context.SaveChanges();
        }


        public Generated_CouponCode GetCouponCode(string couponCode)
        {
            try
            {
                var generated_CouponCode = _context.Generated_CouponCode.FirstOrDefault(c => c.CouponCode == couponCode);
                return generated_CouponCode;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}