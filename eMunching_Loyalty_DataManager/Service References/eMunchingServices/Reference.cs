﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace eMunching_Loyalty_DataManager.eMunchingServices {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://emunching.org/", ConfigurationName="eMunchingServices.eMunchingWebServicesSoap")]
    public interface eMunchingWebServicesSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/GetCouponCodes", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode GetCouponCodes(string UserName, string PassWord, string emailAddress, int restaurantId, bool isRedeemed, string DeviceToken);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/GetRestaurantsXML", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode GetRestaurantsXML(string UserName, string PassWord);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/GetRestaurantsMenuItemGroups_XML", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode GetRestaurantsMenuItemGroups_XML(string UserName, string PassWord, string RestID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/GetRestaurantsMenuItemGroupsByParent_XML", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode GetRestaurantsMenuItemGroupsByParent_XML(string UserName, string PassWord, string RestID, string ParentID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/GetForgottenPassword_XML", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode GetForgottenPassword_XML(string UserName, string PassWord, string UserID, string RestID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/GetRestaurantMenuItems_XML", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode GetRestaurantMenuItems_XML(string UserName, string PassWord, string RestaurantID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/GetRestaurantResvConfig_XML", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode GetRestaurantResvConfig_XML(string UserName, string PassWord, string RestaurantID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/GetMyReviews_XML", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode GetMyReviews_XML(string UserName, string PassWord, string UserID, string RestaurantID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/GetReviews_XML", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode GetReviews_XML(string UserName, string PassWord, string RestaurantID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/GetEvents_XML", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode GetEvents_XML(string UserName, string PassWord, string RestaurantID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/GetDeals_XML", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode GetDeals_XML(string UserName, string PassWord, string RestaurantID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/GetRestaurantAbout_XML", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode GetRestaurantAbout_XML(string UserName, string PassWord, string RestaurantID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/GetRestaurantMenuItemsAll_XML", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode GetRestaurantMenuItemsAll_XML(string UserName, string PassWord, string RestaurantID, string MealType, string DealType, string MenuItemType, string MealCategory);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/GetRestaurantLocations_XML", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode GetRestaurantLocations_XML(string UserName, string PassWord, string RestaurantID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/GetRestaurantLocationsExtended_XML", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode GetRestaurantLocationsExtended_XML(string UserName, string PassWord, string RestaurantID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/UpdateProfile", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        object UpdateProfile(string UserName, string PassWord, string UserId, string UserPassword, string Phone, string Location);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/UpdateProfileByRestaurant", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        object UpdateProfileByRestaurant(string UserName, string PassWord, string UserId, string UserPassword, string Phone, string Location, string RestaurantID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/UpdateProfileExtended", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        object UpdateProfileExtended(string UserName, string PassWord, string UserId, string FirstName, string LastName, string Phone, string Location, string RestaurantID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/UpdateProfileXML", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode UpdateProfileXML(string UserName, string PassWord, string UserId, string FirstName, string LastName, string Phone, string Location, string RestaurantID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/CreateReview", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        object CreateReview(string UserName, string PassWord, string UserId, string Restaurant, string LocaID, string Rating, string Review);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/LoginUser", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode LoginUser(string UserName, string PassWord, string RestaurantID, string UserID, string UserPassword);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/GetUserByUserID", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode GetUserByUserID(string UserName, string PassWord, string UserID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/GetUserByUserIDAndRestaurant", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode GetUserByUserIDAndRestaurant(string UserName, string PassWord, string UserID, string RestaurantID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/AuthenticateUser", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode AuthenticateUser(string UserName, string PassWord, string UserID, string AuthenticationCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/RegisterRestaurantUser", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode RegisterRestaurantUser(string UserName, string PassWord, string RestaurantID, string RestaurantLocaID, string FirstName, string LastName, string Email, string Salt, string RPassword, string Phone);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/CheckUserExists", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode CheckUserExists(string UserName, string PassWord, string RestaurantID, string EmailAddress);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/RegisterRestaurantUserExtended", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode RegisterRestaurantUserExtended(string UserName, string PassWord, string RestaurantID, string RestaurantLocaID, string FirstName, string LastName, string Email, string Salt, string RPassword, string Phone);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/CreateOrder", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        object CreateOrder(string UserName, string PassWord, string OrderName, string RestaurantID, string RestaurantLocaID, string UserId, string MenuItems);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/CreateReservation", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode CreateReservation(string UserName, string PassWord, string ResName, string CallBackNumber, string RestaurantID, string RestaurantLocaID, string UserID, string NumGuests, string TimeSlot);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/CancelReservation", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        object CancelReservation(string UserName, string PassWord, string ResId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/GetRestaurantDisplaySettings_XML", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode GetRestaurantDisplaySettings_XML(string UserName, string PassWord, string ResId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/RegisterDeviceForNotification", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        object RegisterDeviceForNotification(string UserName, string PassWord, string DeviceToken, string RestaurantID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/GetNotificationsSentToAPNS_XML", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode GetNotificationsSentToAPNS_XML(string UserName, string PassWord, string DeviceToken, string RestId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/SetDealViewedByDevice", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        object SetDealViewedByDevice(string UserName, string PassWord, string RestId, string DealId, string DeviceToken);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/SetEventViewedByDevice", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        object SetEventViewedByDevice(string UserName, string PassWord, string RestId, string EventId, string DeviceToken);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/GetDealsExtended_XML", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode GetDealsExtended_XML(string UserName, string PassWord, string RestaurantID, string DeviceToken);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/GetEventsExtended_XML", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode GetEventsExtended_XML(string UserName, string PassWord, string RestaurantID, string DeviceToken);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/LoyaltyBootstrap", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode LoyaltyBootstrap(string UserName, string PassWord, int RestaurantID, string EmailAddress, string DeviceToken);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/LogUniqueCode", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode LogUniqueCode(string UserName, string PassWord, string UniqueCode, string RestaurantID, string EmailAddress);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/RegisterLoyaltyNotification", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        object RegisterLoyaltyNotification(string UserName, string PassWord, int RestaurantID, string Email, int Count, int RewardId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/UpdateLoyaltyNotification", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        object UpdateLoyaltyNotification(string UserName, string PassWord, int RestaurantID, string Email, int Count, int RewardId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/SetLoyaltyNotificaitonRead", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        object SetLoyaltyNotificaitonRead(string UserName, string PassWord, string RestaurantID, string Email, string DeviceToken);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/ValidateCouponCodeAndCheckIfRedeemed", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        int ValidateCouponCodeAndCheckIfRedeemed(string UserName, string PassWord, string CouponCode, int RestaurantID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/RedeemCouponCode", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        object RedeemCouponCode(string UserName, string PassWord, string CouponCode, int RestaurantID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/GetRewardStats", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode GetRewardStats(string UserName, string PassWord, int RestaurantID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/GetParentCategoriesSubCaregoriesAndPriceRanges", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode GetParentCategoriesSubCaregoriesAndPriceRanges(string UserName, string PassWord, string RestaurantID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/SearchMenuItems", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode SearchMenuItems(string UserName, string PassWord, string RestaurantID, string MealType, string DealType, string MenuItemType, string MealCategory, string PriceRange);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/JsonWrapper", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void JsonWrapper(string callback, string input);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/GetEventsExtendedForAndroid_XML", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode GetEventsExtendedForAndroid_XML(string UserName, string PassWord, string RestaurantID, string DeviceToken, string UserId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/RegisterDeviceForAndroidNotification", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode RegisterDeviceForAndroidNotification(string UserName, string PassWord, string RestaurantID, string DeviceToken, string UserId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/SetEventViewedByAndroidDevice", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode SetEventViewedByAndroidDevice(string UserName, string PassWord, string RestId, string EventId, string DeviceToken, string UserId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/LoyaltyBootstrapAndroid", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode LoyaltyBootstrapAndroid(string UserName, string PassWord, int RestaurantID, string EmailAddress, string DeviceToken);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/RegisterAndroidLoyaltyNotification", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        object RegisterAndroidLoyaltyNotification(string UserName, string PassWord, int RestaurantID, string Email, int Count, int RewardId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://emunching.org/UpdateLoyaltyNotificationsAndroid", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        object UpdateLoyaltyNotificationsAndroid(string UserName, string PassWord, int RestaurantID, string Email, int Count, int RewardId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface eMunchingWebServicesSoapChannel : eMunching_Loyalty_DataManager.eMunchingServices.eMunchingWebServicesSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class eMunchingWebServicesSoapClient : System.ServiceModel.ClientBase<eMunching_Loyalty_DataManager.eMunchingServices.eMunchingWebServicesSoap>, eMunching_Loyalty_DataManager.eMunchingServices.eMunchingWebServicesSoap {
        
        public eMunchingWebServicesSoapClient() {
        }
        
        public eMunchingWebServicesSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public eMunchingWebServicesSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public eMunchingWebServicesSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public eMunchingWebServicesSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Xml.XmlNode GetCouponCodes(string UserName, string PassWord, string emailAddress, int restaurantId, bool isRedeemed, string DeviceToken) {
            return base.Channel.GetCouponCodes(UserName, PassWord, emailAddress, restaurantId, isRedeemed, DeviceToken);
        }
        
        public System.Xml.XmlNode GetRestaurantsXML(string UserName, string PassWord) {
            return base.Channel.GetRestaurantsXML(UserName, PassWord);
        }
        
        public System.Xml.XmlNode GetRestaurantsMenuItemGroups_XML(string UserName, string PassWord, string RestID) {
            return base.Channel.GetRestaurantsMenuItemGroups_XML(UserName, PassWord, RestID);
        }
        
        public System.Xml.XmlNode GetRestaurantsMenuItemGroupsByParent_XML(string UserName, string PassWord, string RestID, string ParentID) {
            return base.Channel.GetRestaurantsMenuItemGroupsByParent_XML(UserName, PassWord, RestID, ParentID);
        }
        
        public System.Xml.XmlNode GetForgottenPassword_XML(string UserName, string PassWord, string UserID, string RestID) {
            return base.Channel.GetForgottenPassword_XML(UserName, PassWord, UserID, RestID);
        }
        
        public System.Xml.XmlNode GetRestaurantMenuItems_XML(string UserName, string PassWord, string RestaurantID) {
            return base.Channel.GetRestaurantMenuItems_XML(UserName, PassWord, RestaurantID);
        }
        
        public System.Xml.XmlNode GetRestaurantResvConfig_XML(string UserName, string PassWord, string RestaurantID) {
            return base.Channel.GetRestaurantResvConfig_XML(UserName, PassWord, RestaurantID);
        }
        
        public System.Xml.XmlNode GetMyReviews_XML(string UserName, string PassWord, string UserID, string RestaurantID) {
            return base.Channel.GetMyReviews_XML(UserName, PassWord, UserID, RestaurantID);
        }
        
        public System.Xml.XmlNode GetReviews_XML(string UserName, string PassWord, string RestaurantID) {
            return base.Channel.GetReviews_XML(UserName, PassWord, RestaurantID);
        }
        
        public System.Xml.XmlNode GetEvents_XML(string UserName, string PassWord, string RestaurantID) {
            return base.Channel.GetEvents_XML(UserName, PassWord, RestaurantID);
        }
        
        public System.Xml.XmlNode GetDeals_XML(string UserName, string PassWord, string RestaurantID) {
            return base.Channel.GetDeals_XML(UserName, PassWord, RestaurantID);
        }
        
        public System.Xml.XmlNode GetRestaurantAbout_XML(string UserName, string PassWord, string RestaurantID) {
            return base.Channel.GetRestaurantAbout_XML(UserName, PassWord, RestaurantID);
        }
        
        public System.Xml.XmlNode GetRestaurantMenuItemsAll_XML(string UserName, string PassWord, string RestaurantID, string MealType, string DealType, string MenuItemType, string MealCategory) {
            return base.Channel.GetRestaurantMenuItemsAll_XML(UserName, PassWord, RestaurantID, MealType, DealType, MenuItemType, MealCategory);
        }
        
        public System.Xml.XmlNode GetRestaurantLocations_XML(string UserName, string PassWord, string RestaurantID) {
            return base.Channel.GetRestaurantLocations_XML(UserName, PassWord, RestaurantID);
        }
        
        public System.Xml.XmlNode GetRestaurantLocationsExtended_XML(string UserName, string PassWord, string RestaurantID) {
            return base.Channel.GetRestaurantLocationsExtended_XML(UserName, PassWord, RestaurantID);
        }
        
        public object UpdateProfile(string UserName, string PassWord, string UserId, string UserPassword, string Phone, string Location) {
            return base.Channel.UpdateProfile(UserName, PassWord, UserId, UserPassword, Phone, Location);
        }
        
        public object UpdateProfileByRestaurant(string UserName, string PassWord, string UserId, string UserPassword, string Phone, string Location, string RestaurantID) {
            return base.Channel.UpdateProfileByRestaurant(UserName, PassWord, UserId, UserPassword, Phone, Location, RestaurantID);
        }
        
        public object UpdateProfileExtended(string UserName, string PassWord, string UserId, string FirstName, string LastName, string Phone, string Location, string RestaurantID) {
            return base.Channel.UpdateProfileExtended(UserName, PassWord, UserId, FirstName, LastName, Phone, Location, RestaurantID);
        }
        
        public System.Xml.XmlNode UpdateProfileXML(string UserName, string PassWord, string UserId, string FirstName, string LastName, string Phone, string Location, string RestaurantID) {
            return base.Channel.UpdateProfileXML(UserName, PassWord, UserId, FirstName, LastName, Phone, Location, RestaurantID);
        }
        
        public object CreateReview(string UserName, string PassWord, string UserId, string Restaurant, string LocaID, string Rating, string Review) {
            return base.Channel.CreateReview(UserName, PassWord, UserId, Restaurant, LocaID, Rating, Review);
        }
        
        public System.Xml.XmlNode LoginUser(string UserName, string PassWord, string RestaurantID, string UserID, string UserPassword) {
            return base.Channel.LoginUser(UserName, PassWord, RestaurantID, UserID, UserPassword);
        }
        
        public System.Xml.XmlNode GetUserByUserID(string UserName, string PassWord, string UserID) {
            return base.Channel.GetUserByUserID(UserName, PassWord, UserID);
        }
        
        public System.Xml.XmlNode GetUserByUserIDAndRestaurant(string UserName, string PassWord, string UserID, string RestaurantID) {
            return base.Channel.GetUserByUserIDAndRestaurant(UserName, PassWord, UserID, RestaurantID);
        }
        
        public System.Xml.XmlNode AuthenticateUser(string UserName, string PassWord, string UserID, string AuthenticationCode) {
            return base.Channel.AuthenticateUser(UserName, PassWord, UserID, AuthenticationCode);
        }
        
        public System.Xml.XmlNode RegisterRestaurantUser(string UserName, string PassWord, string RestaurantID, string RestaurantLocaID, string FirstName, string LastName, string Email, string Salt, string RPassword, string Phone) {
            return base.Channel.RegisterRestaurantUser(UserName, PassWord, RestaurantID, RestaurantLocaID, FirstName, LastName, Email, Salt, RPassword, Phone);
        }
        
        public System.Xml.XmlNode CheckUserExists(string UserName, string PassWord, string RestaurantID, string EmailAddress) {
            return base.Channel.CheckUserExists(UserName, PassWord, RestaurantID, EmailAddress);
        }
        
        public System.Xml.XmlNode RegisterRestaurantUserExtended(string UserName, string PassWord, string RestaurantID, string RestaurantLocaID, string FirstName, string LastName, string Email, string Salt, string RPassword, string Phone) {
            return base.Channel.RegisterRestaurantUserExtended(UserName, PassWord, RestaurantID, RestaurantLocaID, FirstName, LastName, Email, Salt, RPassword, Phone);
        }
        
        public object CreateOrder(string UserName, string PassWord, string OrderName, string RestaurantID, string RestaurantLocaID, string UserId, string MenuItems) {
            return base.Channel.CreateOrder(UserName, PassWord, OrderName, RestaurantID, RestaurantLocaID, UserId, MenuItems);
        }
        
        public System.Xml.XmlNode CreateReservation(string UserName, string PassWord, string ResName, string CallBackNumber, string RestaurantID, string RestaurantLocaID, string UserID, string NumGuests, string TimeSlot) {
            return base.Channel.CreateReservation(UserName, PassWord, ResName, CallBackNumber, RestaurantID, RestaurantLocaID, UserID, NumGuests, TimeSlot);
        }
        
        public object CancelReservation(string UserName, string PassWord, string ResId) {
            return base.Channel.CancelReservation(UserName, PassWord, ResId);
        }
        
        public System.Xml.XmlNode GetRestaurantDisplaySettings_XML(string UserName, string PassWord, string ResId) {
            return base.Channel.GetRestaurantDisplaySettings_XML(UserName, PassWord, ResId);
        }
        
        public object RegisterDeviceForNotification(string UserName, string PassWord, string DeviceToken, string RestaurantID) {
            return base.Channel.RegisterDeviceForNotification(UserName, PassWord, DeviceToken, RestaurantID);
        }
        
        public System.Xml.XmlNode GetNotificationsSentToAPNS_XML(string UserName, string PassWord, string DeviceToken, string RestId) {
            return base.Channel.GetNotificationsSentToAPNS_XML(UserName, PassWord, DeviceToken, RestId);
        }
        
        public object SetDealViewedByDevice(string UserName, string PassWord, string RestId, string DealId, string DeviceToken) {
            return base.Channel.SetDealViewedByDevice(UserName, PassWord, RestId, DealId, DeviceToken);
        }
        
        public object SetEventViewedByDevice(string UserName, string PassWord, string RestId, string EventId, string DeviceToken) {
            return base.Channel.SetEventViewedByDevice(UserName, PassWord, RestId, EventId, DeviceToken);
        }
        
        public System.Xml.XmlNode GetDealsExtended_XML(string UserName, string PassWord, string RestaurantID, string DeviceToken) {
            return base.Channel.GetDealsExtended_XML(UserName, PassWord, RestaurantID, DeviceToken);
        }
        
        public System.Xml.XmlNode GetEventsExtended_XML(string UserName, string PassWord, string RestaurantID, string DeviceToken) {
            return base.Channel.GetEventsExtended_XML(UserName, PassWord, RestaurantID, DeviceToken);
        }
        
        public System.Xml.XmlNode LoyaltyBootstrap(string UserName, string PassWord, int RestaurantID, string EmailAddress, string DeviceToken) {
            return base.Channel.LoyaltyBootstrap(UserName, PassWord, RestaurantID, EmailAddress, DeviceToken);
        }
        
        public System.Xml.XmlNode LogUniqueCode(string UserName, string PassWord, string UniqueCode, string RestaurantID, string EmailAddress) {
            return base.Channel.LogUniqueCode(UserName, PassWord, UniqueCode, RestaurantID, EmailAddress);
        }
        
        public object RegisterLoyaltyNotification(string UserName, string PassWord, int RestaurantID, string Email, int Count, int RewardId) {
            return base.Channel.RegisterLoyaltyNotification(UserName, PassWord, RestaurantID, Email, Count, RewardId);
        }
        
        public object UpdateLoyaltyNotification(string UserName, string PassWord, int RestaurantID, string Email, int Count, int RewardId) {
            return base.Channel.UpdateLoyaltyNotification(UserName, PassWord, RestaurantID, Email, Count, RewardId);
        }
        
        public object SetLoyaltyNotificaitonRead(string UserName, string PassWord, string RestaurantID, string Email, string DeviceToken) {
            return base.Channel.SetLoyaltyNotificaitonRead(UserName, PassWord, RestaurantID, Email, DeviceToken);
        }
        
        public int ValidateCouponCodeAndCheckIfRedeemed(string UserName, string PassWord, string CouponCode, int RestaurantID) {
            return base.Channel.ValidateCouponCodeAndCheckIfRedeemed(UserName, PassWord, CouponCode, RestaurantID);
        }
        
        public object RedeemCouponCode(string UserName, string PassWord, string CouponCode, int RestaurantID) {
            return base.Channel.RedeemCouponCode(UserName, PassWord, CouponCode, RestaurantID);
        }
        
        public System.Xml.XmlNode GetRewardStats(string UserName, string PassWord, int RestaurantID) {
            return base.Channel.GetRewardStats(UserName, PassWord, RestaurantID);
        }
        
        public System.Xml.XmlNode GetParentCategoriesSubCaregoriesAndPriceRanges(string UserName, string PassWord, string RestaurantID) {
            return base.Channel.GetParentCategoriesSubCaregoriesAndPriceRanges(UserName, PassWord, RestaurantID);
        }
        
        public System.Xml.XmlNode SearchMenuItems(string UserName, string PassWord, string RestaurantID, string MealType, string DealType, string MenuItemType, string MealCategory, string PriceRange) {
            return base.Channel.SearchMenuItems(UserName, PassWord, RestaurantID, MealType, DealType, MenuItemType, MealCategory, PriceRange);
        }
        
        public void JsonWrapper(string callback, string input) {
            base.Channel.JsonWrapper(callback, input);
        }
        
        public System.Xml.XmlNode GetEventsExtendedForAndroid_XML(string UserName, string PassWord, string RestaurantID, string DeviceToken, string UserId) {
            return base.Channel.GetEventsExtendedForAndroid_XML(UserName, PassWord, RestaurantID, DeviceToken, UserId);
        }
        
        public System.Xml.XmlNode RegisterDeviceForAndroidNotification(string UserName, string PassWord, string RestaurantID, string DeviceToken, string UserId) {
            return base.Channel.RegisterDeviceForAndroidNotification(UserName, PassWord, RestaurantID, DeviceToken, UserId);
        }
        
        public System.Xml.XmlNode SetEventViewedByAndroidDevice(string UserName, string PassWord, string RestId, string EventId, string DeviceToken, string UserId) {
            return base.Channel.SetEventViewedByAndroidDevice(UserName, PassWord, RestId, EventId, DeviceToken, UserId);
        }
        
        public System.Xml.XmlNode LoyaltyBootstrapAndroid(string UserName, string PassWord, int RestaurantID, string EmailAddress, string DeviceToken) {
            return base.Channel.LoyaltyBootstrapAndroid(UserName, PassWord, RestaurantID, EmailAddress, DeviceToken);
        }
        
        public object RegisterAndroidLoyaltyNotification(string UserName, string PassWord, int RestaurantID, string Email, int Count, int RewardId) {
            return base.Channel.RegisterAndroidLoyaltyNotification(UserName, PassWord, RestaurantID, Email, Count, RewardId);
        }
        
        public object UpdateLoyaltyNotificationsAndroid(string UserName, string PassWord, int RestaurantID, string Email, int Count, int RewardId) {
            return base.Channel.UpdateLoyaltyNotificationsAndroid(UserName, PassWord, RestaurantID, Email, Count, RewardId);
        }
    }
}
