using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Models.Procurement.PurchaseOrderTracking
{
   public class POTrackingDetails : POTrackingCommon
    {
        public int? PODetailsId { get; set; }
        public string ItemId { get; set; }
        public string PartNO { get; set; }
        public string ItemName { get; set; }
        public decimal? POQuantity { get; set; }
        public decimal? DeliveredQty { get; set; }
        public decimal? BalanceQty{ get; set; }
        public decimal? IssuedQty{ get; set; }
        public decimal? ItemRate { get; set; }
        public decimal? CurrencyRate { get; set; }
        public string? PurchaseOrderPartyId { get; set; }



    }
  public class UserTrackingDetails
    {
        public string? VP_Action { get; set; }
        public string UserName { get; set; }
        public string? VP_Type { get; set; }
        public DateTime? Change_Dt { get; set; }
        public DateTime? Change_Time { get; set; }
        public string? User_VP_NO { get; set; }      

    }
    public class DropdownUserVPType
    {
        public string VPType { get; set; }
    }
}
