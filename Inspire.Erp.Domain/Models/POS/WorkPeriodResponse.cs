using Inspire.Erp.Domain.Modals.AccountStatement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals
{
    public class WorkPeriodResponse
    {
        public long Id { get; set; }
        public int PeriodId { get; set; }
        public string WorkPeriodName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? Enddate { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Status { get; set; }
        public int? StationId { get; set; }
        public string StationName { get; set; }
        public int? UserId { get; set; }
        public int? UserEnd { get; set; }
        public decimal? OpeningCash { get; set; }
        public decimal? ClosingCash { get; set; }
        public string LoginSystemIP { get; set; }
        public string LoginComputerName { get; set; }
        public decimal? ClosingCashUser { get; set; }
        public int? UserClosebal { get; set; }
        public decimal? DifferenceAmount { get; set; }
        public decimal? OpeningCashUser { get; set; }
        public int? UserOpeningBal { get; set; }
    }

    public class SettlementDetailsRequest
    {
        // public int Quantity { get; set; }

        public int? StationId { get; set; }
        public decimal Total { get; set; }
        //public decimal? NetAmount { get; set; }
        public decimal? VAT { get; set; }
        public decimal? ItemDiscount { get; set; }
        public decimal? CashAmount { get; set; }
        public decimal? BillDiscount { get; set; }
        public decimal? BillDiscountPerc { get; set; }
        public decimal? SalesTax { get; set; }
        public int? VATRound { get; set; }
        public decimal? VATRoundAmount { get; set; }
        public decimal? DiscountVAT { get; set; }
        public string VATRoundSign { get; set; }
        public int UserId { get; set; }


    }

    public class VoidRequest
    {
        public decimal? ItemDiscount { get; set; }
        public bool BillDisp { get; set; }
        public int UserId { get; set; }
    }
    public class CardAmountRequest
    {

        public decimal? Cash { get; set; }

        public long? BillnoLoad { get; set; }
        public string VoucherNo { get; set; }
        public int UserId { get; set; }
        public int? StationId { get; set; }

    }

    public class SettlementDetailsResponse
    {
        public string TransactionDesc { get; set; }
        public int SettlementDetailsId { get; set; }
        public decimal? Amount { get; set; }
        public long? SalesVoucherId { get; set; }
        public DateTime? SettlementDate { get; set; }
        public int? UserId { get; set; }
        public string TransactionCode { get; set; }
        public int? SortOrder { get; set; }
        public bool? ShowinInvoice { get; set; }
        public string Status { get; set; }

        public long? BillnoLoad { get; set; }
        public string VoucherNo { get; set; }
    }
    public class CardAmountResponse
    {
        public decimal? NetTotal { get; set; }
        public decimal? CashAmount { get; set; }
        public decimal? Balance { get; set; }
        public decimal? LastBalance { get; set; }

        public string CardMessage { get; set; }

        public string VoucherNo { get; set; }

        public bool IsFromCash { get; set; }

        public bool IsProcessed { get; set; }
    }


    public class CardRequest
    {
        public decimal? CardAmount { get; set; }
        public decimal? NetTotal { get; set; }
        public decimal? CashAmount { get; set; }
        public decimal? Cash { get; set; }
        public decimal? GrandTotal { get; set; }
        public string RefereceNo { get; set; }

        public long? BillnoLoad { get; set; }
        public string VoucherNo { get; set; }
        public string CardType { get; set; }
        public int? UserId { get; set; }
        public int WorkPeriodId { get; set; }
        public int BillNoSetDet { get; set; }
        
        public decimal? BillAmount { get; set; }
        public decimal? ItemDiscount { get; set; }
        public decimal? BillDiscount { get; set; }
        public decimal? BillDiscountPerc { get; set; }
        public decimal? VAT { get; set; }
        public int VATRound { get; set; }
        public decimal? VATRoundAmount { get; set; }
        public decimal? DiscountVAT { get; set; }
        public string VATRoundSign { get; set; }
        public string PayMode { get; set; }
        public decimal? SalesTax { get; set; }
        public bool? SalesRet { get; set; }
        public string StationName { get; set; }
        public string CreditCardNumber { get; set; }
        public int? CustomerId { get; set; }
        public int? SalesManId { get; set; }
        public int? LocationId { get; set; }
        public int? StationId { get; set; }
        public List<ItemDetail> ItemDetails { get; set; }
    }

    public class CardResettlementRequest
    {
       
        public decimal? TotalAmount { get; set; }
        public decimal? TransCardAmt { get; set; }
        public string VoucherNo { get; set; }
        public string CardNo { get; set; }
        public string CardType { get; set; }
        public string CardMode { get; set; }

        public string TransCheck { get; set; }
        public int? UserId { get; set; }
        public int? LocationId { get; set; }
        public int? StationId { get; set; }
        public int WorkPeriodId { get; set; }
    }


    public class SaleVoucherTempResponse
    {
        public string VoucherNo { get; set; }
        public string CustomerName { get; set; }
        public decimal? GrossAmount { get; set; }
        public decimal? Discount { get; set; }
        public decimal? VAT { get; set; }
        public decimal? NetAmount { get; set; }
        public List<SaleVoucherDetailsTempResponse> SaleVoucherDetails { get; set; }
    }

    public class SaleVoucherDetailsTempResponse
    {
        public string VoucherNo { get; set; }
        public string Description { get; set; }
        public string UnitName { get; set; }
        public decimal? Discount { get; set; }
        public int Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? VAT { get; set; }
        public decimal? NetAmount { get; set; }
        public int? UnitDetailId { get; set; }

    }

    public class SaleVoucherResponse
    {
        public string VoucherNo { get; set; }
        public string CustomerName { get; set; }
        public decimal? GrossAmount { get; set; }
        public decimal? Discount { get; set; }
        public decimal? VAT { get; set; }
        public decimal? NetAmount { get; set; }
        public string PaymentMode { get; set; }
        public string RefNo { get; set; }
        public DateTime? VoucherDate { get; set; }
        public string VoucherType { get; set; }
        public List<SaleVoucherDetailsResponse> SaleVoucherDetails { get; set; }
    }

    public class SaleVoucherDetailsResponse
    {
        public string VoucherNo { get; set; }
        public string Description { get; set; }
        public string UnitName { get; set; }
        public int Quantity { get; set; }
     //   public decimal? Discount { get; set; }
      
        public decimal? UnitPrice { get; set; }
        public decimal? VAT { get; set; }
        public decimal? NetAmount { get; set; }
        //public int? UnitDetailId { get; set; }

    }






    public class BillRecallResponse
    {
        public List<SettDetailsResponse> SettlementDetails { get; set; }
        public List<BillRecallItemResponse> ItemsDetails { get; set; }
    }

    public class SettDetailsResponse
    {
        public decimal SettlementDetId { get; set; }
        public string TransDesc { get; set; }
        public decimal? Amount { get; set; }
        public long? OrderId { get; set; }
        public string Status { get; set; }
        public DateTime? SettlementDate { get; set; }
        public int? UserId { get; set; }
        public string TransCode { get; set; }
        public int? SortOrder { get; set; }
      
        public bool? ShowInInvoice { get; set; }
    
    }

    public class VoidResponse
    {
    
        public string Message { get; set; }
      

    }
    public class BillRecallItemResponse
    {
        public long SDet_ID { get; set; }
        public string VoucherNo { get; set; }
        public long ItemId { get; set; }
        public string Description { get; set; }
        public decimal? UnitId { get; set; }
        public decimal? Sold_Qty { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? CostPrice { get; set; }
        public decimal? GrossAmt { get; set; }
        public decimal? Discount { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? VatAmount { get; set; }
        public decimal? VatableAmt { get; set; }
        public int? UnitDetailsId { get; set; }

        public string BarCode { get; set; }

        public string UnitName { get; set; }
    }
    public class CardType
    {
        public int Id { get; set; }
        public string TransactionCode { get; set; }
        public string TransactionDescription { get; set; }
        public bool? InHouse { get; set; }

    }

    public class ItemDetail
    {
        public int ItemId { get; set; }
        public string BarCode { get; set; }
        public string ItemName { get; set; }
        public string UnitName { get; set; }
        public int UnitId { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Price { get; set; }

        public decimal? VatAmount { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Total { get; set; }
        public decimal? NetTotal { get; set; }

        public int UnitDetailId { get; set; }

    }


    public class ProcessCashCardResponse
    {
        public string VoucherNo { get; set; }
    }

    public class SalesTransactionWithWP
    {
        public string CreditAccountNumber { get; set; }
        public string PayMode { get; set; }
        public string InvoiceNo { get; set; }
        public string TransDescription { get; set; }
        public string CRDR { get; set; }
        public string InvoiceCode { get; set; }
        public string AccountCode { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string Status { get; set; }
        public bool? PosttoAccounts { get; set; }
        public long WorkPeriodId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string WorkIdStatus { get; set; }
    }

}
