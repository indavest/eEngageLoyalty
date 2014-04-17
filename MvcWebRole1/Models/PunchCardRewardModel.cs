﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace LoyaltyAdministration.Models
{
    public class PunchCardRewardModel
    {
        int restaurantId;
        string name;
        string description;
        int numberOfItems;
        int[] skusToTrack;
        int rewardSku;

        public int RestaurantId
        {
            get { return restaurantId; }
            set { restaurantId = value; }
        }

        [Required]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [Required]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        [Required]
        public int NumberOfItems
        {
            get { return numberOfItems; }
            set { numberOfItems = value; }
        }

        [Required]
        public int[] SkusToTrack
        {
            get { return skusToTrack; }
            set { skusToTrack = value; }
        }

        [Required]
        public int RewardSku
        {
            get { return rewardSku; }
            set { rewardSku = value; }
        }
    }
}
