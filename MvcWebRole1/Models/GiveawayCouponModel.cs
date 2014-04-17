using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace LoyaltyAdministration.Models
{
    public class GiveawayCouponModel
    {
        string couponCode;
        string rewardName;
        int rewardId;
        string emailAddress;
        int adminUserId;
        string adminUserName;
        bool isRedeemed;
        DateTime dateCreated;
        DateTime expirationDate;
        Nullable<DateTime> dateRedeemed;

        public string CouponCode
        {
            get { return couponCode; }
            set { couponCode = value; }
        }

        public string RewardName
        {
            get { return rewardName; }
            set { rewardName = value; }
        }

        [Required]
        public int RewardId
        {
            get { return rewardId; }
            set { rewardId = value; }
        }

        [Required]
        public string EmailAddress
        {
            get { return emailAddress; }
            set { emailAddress = value; }
        }

        public int AdminUserId
        {
            get { return adminUserId; }
            set { adminUserId = value; }
        }

        public string AdminUserName
        {
            get { return adminUserName; }
            set { adminUserName = value; }
        }

        public bool IsRedeemed
        {
            get { return isRedeemed; }
            set { isRedeemed = value; }
        }

        public DateTime DateCreated
        {
            get { return dateCreated; }
            set { dateCreated = value; }
        }

        public Nullable<DateTime> DateRedeemed
        {
            get { return dateRedeemed; }
            set { dateRedeemed = value; }
        }

        public DateTime ExpirationDate
        {
            get { return expirationDate; }
            set { expirationDate = value; }
        }
    }
}
