using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inspire.Erp.Domain.Entities
{
    public class POS_SalesTransactionDetails
    {
        public long Id { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public int? WorkPeriodId { get; set; }
        public string PaymentMode { get; set; }
        public decimal? Amount { get; set; }
        public string CounterName { get; set; }
        public string Status { get; set; }
        public int? UserId { get; set; }
        public string Description { get; set; }
        public string BarCode { get; set; }
        public string CreditAccountNo { get; set; }

    }
    public class POS_SummaryDateWiseReport
    {

        public string VoucherNo { get; set; }
        public string PaymentMode { get; set; }
        public decimal Discount { get; set; }
        public decimal Amount { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal VatAmount { get; set; }

    }
    public class POS_MaintodaySales
    {
        public List<POS_todaySalesByDate> POS_todaySalesByDate { get; set; }
        public List<POS_todaySalesDetailByDate> POS_todaySalesDetailByDate { get; set; }
    }
    public class POS_todaySalesByDate
    {

        public string VoucherNo { get; set; }
        public string CustomerName { get; set; }
        public string PaymentMode { get; set; }
        public decimal Discount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal VatAmount { get; set; }
        public DateTime? VoucherDate { get; set; }

    }

    public class POS_todaySalesDetailByDate
    {

        public string ItemMasterItemName { get; set; }
        public string Unit { get; set; }
        public decimal TotalSoldQty { get; set; }
        public decimal TotalAmount { get; set; }

    }

    public class TodaySalesByCounterWise
    {
        public string CounterName { get; set; }
        public decimal Cash { get; set; }
        public decimal Card { get; set; }
        public decimal Credit { get; set; }
        public decimal Return { get; set; }
        public decimal ReturnCredit { get; set; }
        public decimal Sales { get; set; }
        public decimal Discount { get; set; }
        public decimal Vat { get; set; }

    }

    public class PosReportFilterModel
    {
        public int wpId { get; set; }
        public int stationId { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
    }
}

