using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace LoyaltyAdministration.Models
{
    public class NewUserModel
    {
        string userName;
        int roleId;

        [Required]
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        [Required]
        public int RoleId
        {
            get { return roleId; }
            set { roleId = value; }
        }        
    }
}
