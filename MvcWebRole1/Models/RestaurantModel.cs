using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace LoyaltyAdministration.Models
{
    public class RestaurantModel
    {
        string restaurantName;
        int restaurantId;
        int loyaltyTypeId;

        [Required]
        public string RestaurantName
        {
            get { return restaurantName; }
            set { restaurantName = value; }
        }

        [Required]
        public int RestaurantId
        {
            get { return restaurantId; }
            set { restaurantId = value; }
        }

        [Required]
        public int LoyaltyTypeId
        {
            get { return loyaltyTypeId; }
            set { loyaltyTypeId = value; }
        }
    }
}
