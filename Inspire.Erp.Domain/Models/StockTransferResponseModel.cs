using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals
{
    public class StockTransferResponseModel
    {
        public int Stock_Transfer_STID { get; set; }
        public string? Stock_Transfer_STVNo { get; set; }
        public DateTime? Stock_Transfer_ST_DATE { get; set; }
        public string? Stock_Transfer_Location_ID_From { get; set; }
        public string? Stock_Transfer_Location_ID_To { get; set; }
        public string? Stock_Transfer_Customer_Name { get; set; }
    }
}
