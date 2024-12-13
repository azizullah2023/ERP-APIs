using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Inspire.Erp.Domain.Modals.Common
{
    public class GenericGridViewModel
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 10000000;
        public string Field { get; set; }
        public string Dir { get; set; }
        public string Search { get; set; }
        public string Filter { get; set; }
        public int Total { get; set; } = 0;
        public string Extension { get; set; }
        public string FormatType { get; set; }
        public  bool isDate { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }

    public class ReportFilter
    {
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
        public string Formate { get; set; }
        public int TotalCount { get; set; }
        public string Query { get; set; }
        public string Serach { get; set; }
        public string CostCenter { get; set; }
        public bool HideZeroTransactions { get; set; }
        public string PrintGroupWise { get; set; }
        public string FinancialMindate { get; set; }
        public int SupplierId { get; set; }
        public int JobId { get; set; }
        public string Status { get; set; }
        public int LocationId { get; set; }
        public int ItemId { get; set; }
        public string PONO { get; set; }
        public bool IsDateCheck { get; set; }

    }
    public class DeliveryNoteReportFilter
    {
        public int LocationId { get; set; }
        public long CustomerId { get; set; }
        public string InvioceStatus { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public string Formate { get; set; }

    }

    public class StockAdjustmentReportFilter
    {
        public int? JobId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }

    public class CustomerEnquirySearchFilter
    {
        public bool IsDateChecked { get; set; }
        public string Enquiry_No { get; set; }
        public long? CustomerId { get; set; }
        public long? StatusId { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
    }
    public class CustomerEnquirySearchFilterStatus
    {
        public bool IsDateChecked { get; set; }
        public long? CustomerId { get; set; }
        public long? StatusId { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
    }
    public class PurChaseRequisitionStatusFilterReport
    {
        public int JobId { get; set; }
        public int ItemId { get; set; }
        public string POStatus { get; set; }
        public string PRNO { get; set; }
        public bool ByDate { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
    }

    public class AccountTransactionsFilterReport
    {
        public List<string> accountNos { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
    public class PurChaseRequisitionStatus
    {
        public int issueQty { get; set; }
        public int BalToIssue { get; set; }
        public int stock { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public string PartNo { get; set; }
        public int DelQty { get; set; }
        public string PoStatus { get; set; }
        public DateTime RequisitionDate { get; set; }
    }
    public class OutStandingReprortFilter
    {
        public string accountNo { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public bool isDateRange { get; set; }
    }

    public class JobCostReportSearchFilter
    {
        public long JobNo { get; set; }
        public long CostCenterId { get; set; }
        public bool IsdateSelected { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
    }

    public class ItemBarCodeSearchFilter
    {
        public bool DisableVat { get; set; }
        public string BarCode { get; set; }
        public int? UnitDetailsId { get; set; }
    }
    public class WorkPeriodFilter
    {
        public int? StationId { get; set; }
        public string? Status { get; set; }
        public int? UserId { get; set; }
    }
    public class PurChaseRequisitionFilterReport
    {
        public int? JobId { get; set; }

    }
    public class PurChaseReqFields
    {
        public string PurchaseRequisitionPartyName { get; set; }
        public string purchaseRequisitionType { get; set; }
        public string Item_Name { get; set; }
        public string Unit { get; set; }
        public decimal DelQty { get; set; }
        public decimal? SRDID { get; set; }
        public decimal? Rate { get; set; }
        public decimal? SRID { get; set; }
        public string SRNo { get; set; }
        public DateTime Date { get; set; }
        public decimal? SRQ { get; set; }
        public decimal? BalQty { get; set; }
        public decimal? matId { get; set; }
        public int unitid { get; set; }
        public long? jobid { get; set; }

        public decimal? Stock { get; set; }


    }
    public class UserTrackFilter
    {

        public int? Userid { get; set; }
        [DefaultValue(null)]
        public string VPAction { get; set; } 
        [DefaultValue(null)]
        public string VPType { get; set; } 
        [DefaultValue(null)]
        public DateTime? fromDate { get; set; } 
        [DefaultValue(null)]
        public DateTime? toDate { get; set; }
        [DefaultValue(false)]
        public bool? isCheck { get; set; } 
    }

}
