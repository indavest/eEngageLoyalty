using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace LoyaltyAdministration.Models
{
    public class UserRoleMappingModel
    {
        int userId;
        int roleId;

        [Required]
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        [Required]
        public int RoleId
        {
            get { return roleId; }
            set { roleId = value; }
        }

    }
}
