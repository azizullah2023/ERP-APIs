using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals
{
    public class GetStockVoucherDetailsResponse
    {
        public string Stock_Register_Trans_Type { get; set; }
        public string Stock_Register_Ref_Voucher_No { get; set; }
        public DateTime Stock_Register_Voucher_Date { get; set; }
        public decimal StockIn { get; set; }
        public decimal StockInAmt { get; set; }
        public decimal StockOut { get; set; }
        public decimal StockOutAmt { get; set; }
        public string StockLocation { get; set; }
        public string Job { get; set; }
        public int Stock_Register_Unit_ID { get; set; }
        public decimal Stock_Register_Rate { get; set; }
        public int Stock_Register_Material_ID { get; set; }
    }
}
