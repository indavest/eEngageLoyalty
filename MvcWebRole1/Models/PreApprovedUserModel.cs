using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace LoyaltyAdministration.Models
{
    public class PreApprovedUserModel
    {
        string emailAddress;
        int restaurantId;
        int roleId;

        [Required]
        public string EmailAddress
        {
            get { return emailAddress; }
            set { emailAddress = value; }
        }

        [Required]
        public int RestaurantId
        {
            get { return restaurantId; }
            set { restaurantId = value; }
        }

        [Required]
        public int RoleId
        {
            get { return roleId; }
            set { roleId = value; }
        }

    }
}

