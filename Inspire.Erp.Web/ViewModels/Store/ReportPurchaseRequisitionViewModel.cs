using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public partial class ReportPurchaseRequisitionViewModel
    {
        public long? ItemGroupId { get; set; }
        public string ItemGroupName { get; set; }
        public long? ItemBrandId { get; set; }
        public string ItemBrandName { get; set; }
        public decimal PurchaseRequisitionId { get; set; }
        public string PurchaseRequisitionNo { get; set; }
        public DateTime? PurchaseRequisitionDate { get; set; }
        public string PurchaseRequisitionType { get; set; }
        public decimal? PurchaseRequisitionPartyId { get; set; }
        public string PurchaseRequisitionPartyName { get; set; }
        public string PurchaseRequisitionPartyAddress { get; set; }
        public string PurchaseRequisitionPartyVatNo { get; set; }
        public string PurchaseRequisitionRefNo { get; set; }
        public string PurchaseRequisitionSupInvNo { get; set; }
        public string PurchaseRequisitionGrno { get; set; }
        public DateTime? PurchaseRequisitionGrdate { get; set; }
        public string PurchaseRequisitionLpono { get; set; }
        public DateTime? PurchaseRequisitionLpodate { get; set; }
        public string PurchaseRequisitionQtnNo { get; set; }
        public DateTime? PurchaseRequisitionQtnDate { get; set; }
        public string PurchaseRequisitionDescription { get; set; }
        public bool? PurchaseRequisitionExcludeVat { get; set; }
        public string PurchaseRequisitionPono { get; set; }
        public string PurchaseRequisitionBatchCode { get; set; }
        public string PurchaseRequisitionDayBookNo { get; set; }
        public int? PurchaseRequisitionLocationId { get; set; }
        public long? PurchaseRequisitionUserId { get; set; }
        public int? PurchaseRequisitionCurrencyId { get; set; }
        public int? PurchaseRequisitionCompanyId { get; set; }
        public long? PurchaseRequisitionJobId { get; set; }
        public decimal? PurchaseRequisitionFsno { get; set; }
        public decimal? PurchaseRequisitionFcRate { get; set; }
        public string PurchaseRequisitionStatus { get; set; }
        public decimal? PurchaseRequisitionTotalGrossAmount { get; set; }
        public decimal? PurchaseRequisitionTotalItemDisAmount { get; set; }
        public decimal? PurchaseRequisitionTotalActualAmount { get; set; }
        public decimal? PurchaseRequisitionTotalDisPer { get; set; }
        public decimal? PurchaseRequisitionTotalDisAmount { get; set; }
        public decimal? PurchaseRequisitionVatAmt { get; set; }
        public decimal? PurchaseRequisitionVatPer { get; set; }
        public string PurchaseRequisitionVatRoundSign { get; set; }
        public decimal? PurchaseRequisitionVatRountAmt { get; set; }
        public decimal? PurchaseRequisitionNetDisAmount { get; set; }
        public decimal? PurchaseRequisitionNetAmount { get; set; }
        public decimal? PurchaseRequisitionTransportCost { get; set; }
        public decimal? PurchaseRequisitionHandlingcharges { get; set; }
        public string PurchaseRequisitionIssueId { get; set; }
        public bool? PurchaseRequisitionJobDirectPur { get; set; }
        public bool? PurchaseRequisitionDelStatus { get; set; }
        public string CurrencyMasterCurrencyName { get; set; }
        public string SuppliersMasterSupplierName { get; set; }
        public string SuppliersMasterSupplierContactPerson { get; set; }
        public int? SuppliersMasterSupplierCountryId { get; set; }
        public int? SuppliersMasterSupplierCityId { get; set; }
        public string SuppliersMasterSupplierMobile { get; set; }
        public string SuppliersMasterSupplierRemarks { get; set; }
        public string SuppliersMasterSupplierVatNo { get; set; }
        public string CountryMasterCountryName { get; set; }
        public int? CityMasterCityId { get; set; }
        public string UnitMasterUnitShortName { get; set; }
        public string ItemMasterPartNo { get; set; }
        public string ItemMasterItemName { get; set; }
        public decimal? Expr1 { get; set; }
        public string PurchaseRequisitionDetailsNo { get; set; }
        public decimal? PurchaseRequisitionDetailsSno { get; set; }
        public decimal? PurchaseRequisitionDetailsMatId { get; set; }
        public string PurchaseRequisitionDetailsItemName { get; set; }
        public decimal? PurchaseRequisitionDetailsUnitId { get; set; }
        public string PurchaseRequisitionDetailsUnitName { get; set; }
        public string PurchaseRequisitionDetailsBatchCode { get; set; }
        public bool? PurchaseRequisitionDetailsDelStatus { get; set; }
        public decimal? PurchaseRequisitionDetailsSalesPrice { get; set; }
        public int? PurchaseRequisitionDetailsQtndId { get; set; }
        public int? PurchaseRequisitionDetailsQtnId { get; set; }
        public int? PurchaseRequisitionDetailsPodId { get; set; }
        public int? PurchaseRequisitionDetailsPoId { get; set; }
        public int? PurchaseRequisitionDetailsPrdId { get; set; }
        public int? PurchaseRequisitionDetailsPrId { get; set; }
        public int? PurchaseRequisitionDetailsRfqdId { get; set; }
        public int? PurchaseRequisitionDetailsRfqId { get; set; }
        public bool? PurchaseRequisitionDetailsIsEdit { get; set; }
        public string PurchaseRequisitionDetailsRemarks { get; set; }
        public decimal? PurchaseRequisitionDetailsNetAmt { get; set; }
        public decimal? PurchaseRequisitionDetailsVatAmt { get; set; }
        public decimal? PurchaseRequisitionDetailsVatPer { get; set; }
        public decimal? PurchaseRequisitionDetailsActualAmount { get; set; }
        public DateTime? PurchaseRequisitionDetailsReqDate { get; set; }
        public string PurchaseRequisitionDetailsReqStatus { get; set; }
        public decimal? PurchaseRequisitionDetailsDiscAmount { get; set; }
        public decimal? PurchaseRequisitionDetailsGrossAmount { get; set; }
        public decimal? PurchaseRequisitionDetailsRate { get; set; }
        public decimal? PurchaseRequisitionDetailsQuantity { get; set; }
        public DateTime? PurchaseRequisitionDetailsExpDate { get; set; }
        public DateTime? PurchaseRequisitionDetailsManfDate { get; set; }
        public string SuppliersMasterSupplierTel1 { get; set; }
        public string SuppliersMasterSupplierFax { get; set; }
        public string LocationMasterLocationName { get; set; }
        public string JobMasterJobName { get; set; }
    }
}
