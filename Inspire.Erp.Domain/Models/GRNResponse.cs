using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals
{
    public class GRNResponse
    {

        public int? PurOrder_Register_ID { get; set; }
        public string? Purchase_Order_PO_NO { get; set; }
        public string? PurOrder_Register_Status { get; set; }
        public DateTime? Purchase_Order_PO_Date { get; set; }
        public string? Suppliers_INS_Name { get; set; }
        public double? Purchase_Order_Details_Quantity { get; set; }
        public string? Purchase_Order_Details_Material_Description { get; set; }
        public double? Purchase_Order_Details_Rate { get; set; }
        public double? PurOrder_Register_Vat_Amount { get; set; }
        public int? Purchase_Order_Details_POD_ID { get; set; }
        public int? Purchase_Order_Details_Material_ID { get; set; }
        //public decimal? PurOrder_Register_QTY_Issued { get; set; }
        public double? IssueQty { get; set; }
        public double? BalanceQty { get; set; }
    }
}
