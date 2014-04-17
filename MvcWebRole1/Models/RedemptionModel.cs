using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace LoyaltyAdministration.Models
{
    public class RedemptionModel
    {
        string couponCode;
        //int restaurantId;

        [Required]
        public string CouponCode
        {
            get { return couponCode; }
            set { couponCode = value; }
        }

        //[Required]
        //public int RestaurantId
        //{
        //    get { return restaurantId; }
        //    set { restaurantId = value; }
        //}

    }
}
