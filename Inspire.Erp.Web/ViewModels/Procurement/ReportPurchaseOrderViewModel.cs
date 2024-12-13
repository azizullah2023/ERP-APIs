using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels.Procurement
{
    public class ReportPurchaseOrderViewModel
    {
        public decimal PurchaseOrderId { get; set; }
        public string PurchaseOrderNo { get; set; }
        public DateTime? PurchaseOrderDate { get; set; }
        public string PurchaseOrderType { get; set; }
        public decimal? PurchaseOrderPartyId { get; set; }
        public string PurchaseOrderPartyName { get; set; }
        public string PurchaseOrderPartyAddress { get; set; }
        public string PurchaseOrderPartyVatNo { get; set; }
        public string PurchaseOrderRefNo { get; set; }
        public string PurchaseOrderSupInvNo { get; set; }
        public string PurchaseOrderGrno { get; set; }
        public DateTime? PurchaseOrderGrdate { get; set; }
        public string PurchaseOrderLpono { get; set; }
        public DateTime? PurchaseOrderLpodate { get; set; }
        public string PurchaseOrderQtnNo { get; set; }
        public DateTime? PurchaseOrderQtnDate { get; set; }
        public string PurchaseOrderDescription { get; set; }
        public bool? PurchaseOrderExcludeVat { get; set; }
        public string PurchaseOrderPono { get; set; }
        public string PurchaseOrderBatchCode { get; set; }
        public string PurchaseOrderDayBookNo { get; set; }
        public int? PurchaseOrderLocationId { get; set; }
        public long? PurchaseOrderUserId { get; set; }
        public int? PurchaseOrderCurrencyId { get; set; }
        public int? PurchaseOrderCompanyId { get; set; }
        public long? PurchaseOrderJobId { get; set; }
        public decimal? PurchaseOrderFsno { get; set; }
        public decimal? PurchaseOrderFcRate { get; set; }
        public string PurchaseOrderStatus { get; set; }
        public decimal? PurchaseOrderTotalGrossAmount { get; set; }
        public decimal? PurchaseOrderTotalItemDisAmount { get; set; }
        public decimal? PurchaseOrderTotalActualAmount { get; set; }
        public decimal? PurchaseOrderTotalDisPer { get; set; }
        public decimal? PurchaseOrderTotalDisAmount { get; set; }
        public decimal? PurchaseOrderVatAmt { get; set; }
        public decimal? PurchaseOrderVatPer { get; set; }
        public string PurchaseOrderVatRoundSign { get; set; }
        public decimal? PurchaseOrderVatRountAmt { get; set; }
        public decimal? PurchaseOrderNetDisAmount { get; set; }
        public decimal? PurchaseOrderNetAmount { get; set; }
        public decimal? PurchaseOrderTransportCost { get; set; }
        public decimal? PurchaseOrderHandlingcharges { get; set; }
        public string PurchaseOrderIssueId { get; set; }
        public bool? PurchaseOrderJobDirectPur { get; set; }
        public bool? PurchaseOrderDelStatus { get; set; }
        public string PurchaseOrderIndentNo { get; set; }
        public string PurchaseOrderContPerson { get; set; }
        public string PurchaseOrderPreparedBy { get; set; }
        public string PurchaseOrderRecommendedBy { get; set; }
        public bool? PurchaseOrderApproveEnable { get; set; }
        public DateTime? PurchaseOrderApproveDate { get; set; }
        public string PurchaseOrderApproveStatus { get; set; }
        public long? PurchaseOrderApproveUserId { get; set; }
        public string PurchaseOrderHeader { get; set; }
        public string PurchaseOrderDelivery { get; set; }
        public string PurchaseOrderNotes { get; set; }
        public string PurchaseOrderFooter { get; set; }
        public string PurchaseOrderPaymentTerms { get; set; }
        public string PurchaseOrderSubject { get; set; }
        public string PurchaseOrderContent { get; set; }
        public string PurchaseOrderRemarks1 { get; set; }
        public string PurchaseOrderRemarks2 { get; set; }
        public string PurchaseOrderRemarks3 { get; set; }
        public DateTime? PurchaseOrderDelivFromDate { get; set; }
        public DateTime? PurchaseOrderDelivToDate { get; set; }
        public string PurchaseOrderDelivStatus { get; set; }
        public string PurchaseOrderDelivPlace { get; set; }
        public string SuppliersMasterSupplierName { get; set; }
        public string JobMasterJobName { get; set; }
        public string Partno { get; set; }
        public string MatDesc { get; set; }
        public long? ItemMasterItemId { get; set; }
        public decimal? PurchaseOrderDetailsQuantity { get; set; }
        public decimal? PurchaseOrderDetailsRate { get; set; }
        public decimal? PurchaseOrderDetailsDiscAmount { get; set; }
        public decimal? PurchaseOrderDetailsNetAmt { get; set; }
        public string? LocationName { get; set; }
        public string? UnitName { get; set; }
        public string? CurrencyName { get; set; }
    }
}
