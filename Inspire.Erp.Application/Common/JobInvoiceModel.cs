using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Common
{
    public class JobInvoiceModel
    {
        public DateTime? Job_Invoice_Voucher_Date { get; set; }
        public string? Job_Invoice_Voucher_No { get; set; }
        public string? Job_Invoice_Customer_ID { get; set; }
        public string? Job_Invoice_Customer_Name { get; set; }
        public string? Job_Invoice_Location_ID { get; set; }
        public string? location { get; set; }
        public double? Job_Invoice_Discount { get; set; }
        public double? Job_Invoice_Gross_Amount { get; set; }
        public double? Job_Invoice_Net_Amount { get; set; }
        public int? Job_Invoice_Details_Unit_ID { get; set; }
        public string Job_Invoice_Details_Unit { get; set; }
        public string Item_Name { get; set; }
        public double? Job_Invoice_Details_Unit_Price { get; set; }
        public double? Job_Invoice_Details_Sold_Qty { get; set; }
        public int? Job_Invoice_Details_Item_ID { get; set; }
    }
}
