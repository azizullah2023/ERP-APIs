using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inspire.Erp.Domain.Entities
{
    public partial class CustomerMaster
    {
        public int CustomerMasterCustomerNo { get; set; }
        public string CustomerMasterCustomerTitle { get; set; }
        public string CustomerMasterCustomerName { get; set; }
        public string CustomerMasterCustomerType { get; set; }
        public string CustomerMasterCustomerContactPerson { get; set; }
        public int? CustomerMasterCustomerCountryId { get; set; }
        public int? CustomerMasterCustomerCityId { get; set; }
        public string CustomerMasterCustomerPoBox { get; set; }
        public string CustomerMasterCustomerTel1 { get; set; }
        public string CustomerMasterCustomerTel2 { get; set; }
        public string CustomerMasterCustomerMobile { get; set; }
        public string CustomerMasterCustomerFax { get; set; }
        public string CustomerMasterCustomerEmail { get; set; }
        public string CustomerMasterCustomerWebSite { get; set; }
        public string CustomerMasterCustomerLocation { get; set; }
        public string CustomerMasterCustomerAddress { get; set; }
        public string CustomerMasterCustomerWhatsAppNo { get; set; }
        public string CustomerMasterCustomerRemarks { get; set; }
        public string CustomerMasterCustomerReffAcNo { get; set; }
        public int? CustomerMasterCustomerUserId { get; set; }
        public int? CustomerMasterCustomerCurrencyId { get; set; }
        public double? CustomerMasterCustomerCreditLimit { get; set; }
        public int? CustomerMasterCustomerCreditDays { get; set; }
        public bool? CustomerMasterCustomerBlackList { get; set; }
        public int? CustomerMasterCustomerStatus { get; set; }
        public bool? CustomerMasterCustomerDeleteStatus { get; set; }
        public DateTime? CustomerMasterCustomerJoinDate { get; set; }
        public bool? CustomerMasterCustomerStatusValue { get; set; }
        public bool? CustomerMasterCustomerCreateAccount { get; set; }
        public string CustomerMasterCustomerPriceLevel { get; set; }
        public int? CustomerMasterCustomerPriceLevelId { get; set; }
        public string CustomerMasterCustomerCustType { get; set; }
        public string CustomerMasterCustomerContactPerson2 { get; set; }
        public string CustomerMasterCustomerContactPerson3 { get; set; }
        public string CustomerMasterCustomerVatNo { get; set; }
        public int? CustomerMasterCustomerLoyaltyId { get; set; }
        public double? CustomerMasterCustomerEarnPoints { get; set; }
        public double? CustomerMasterCustomerTotalPoints { get; set; }
        public double? CustomerMasterCustomerTotalValue { get; set; }
        public double? CustomerMasterCustomerRedeemEarnPoints { get; set; }
        public string CustomerMasterCustomerArabicName { get; set; }
        public string CustomerMasterCustomerGroupAccNo { get; set; }
        public int? CustomerMasterCustomerCtTypeId { get; set; }
        //[NotMapped]
        //public int? CustomerMasterCustomerRouteID { get; set; }
        public bool? CustomerMasterCustomerDelStatus { get; set; }
        [NotMapped]
        public bool IsCreateAccount { get; set; } = true;  
    }
}
