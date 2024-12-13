using System;
using System.Collections.Generic;

namespace Inspire.Erp.Web.ViewModels
{
    public partial class PurchaseReturnDetailsViewModel
    {
        public decimal PurchaseReturnDetailsId { get; set; }
        public decimal? PurchaseReturnId { get; set; }
        public string PurchaseReturnDetailsNo { get; set; }
        public decimal? PurchaseReturnDetailsSno { get; set; }
        public int? PurchaseReturnDetailsMatId { get; set; }
        public string PurchaseReturnDetailsItemName { get; set; }
        public int? PurchaseReturnDetailsUnitId { get; set; }
        public string PurchaseReturnDetailsUnitName { get; set; }
        public string PurchaseReturnDetailsBatchCode { get; set; }
        public DateTime? PurchaseReturnDetailsManfDate { get; set; }
        public DateTime? PurchaseReturnDetailsExpDate { get; set; }
        public decimal? PurchaseReturnDetailsQuantity { get; set; }
        public decimal? PurchaseReturnDetailsRate { get; set; }
        public decimal? PurchaseReturnDetailsGrossAmount { get; set; }
        public decimal? PurchaseReturnDetailsDiscAmount { get; set; }
        public decimal? PurchaseReturnDetailsActualAmount { get; set; }
        public decimal? PurchaseReturnDetailsVatPer { get; set; }
        public decimal? PurchaseReturnDetailsVatAmt { get; set; }
        public decimal? PurchaseReturnDetailsNetAmt { get; set; }
        public string PurchaseReturnDetailsRemarks { get; set; }
        public bool? PurchaseReturnDetailsIsEdit { get; set; }
        public int? PurchaseReturnDetailsRfqId { get; set; }
        public int? PurchaseReturnDetailsRfqdId { get; set; }
        public int? PurchaseReturnDetailsPrId { get; set; }
        public int? PurchaseReturnDetailsPrdId { get; set; }
        public int? PurchaseReturnDetailsPoId { get; set; }
        public int? PurchaseReturnDetailsPodId { get; set; }
        public int? PurchaseReturnDetailsQtnId { get; set; }
        public int? PurchaseReturnDetailsQtndId { get; set; }

        public int? PurchaseReturnDetailsPurId { get; set; }
        public int? PurchaseReturnDetailsPurdId { get; set; }

        public decimal? PurchaseReturnDetailsSalesPrice { get; set; }
        public bool? PurchaseReturnDetailsDelStatus { get; set; }

        public decimal? PurchaseOrderPurchaseQTY { get; set; }

        public decimal? PurchaseReturnNetStock { get; set; }

        public decimal? PurchaseReturnQtyTill { get; set; }
        //public virtual PurchaseReturnController PurchaseReturnDetailsNoNavigation { get; set; }
    }
}
