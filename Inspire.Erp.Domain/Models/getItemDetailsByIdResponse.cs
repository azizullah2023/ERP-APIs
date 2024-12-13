using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals
{
    public class getItemDetailsByIdResponse
    {
        public long Item_Master_Item_ID { get; set; }
        public string Item_Master_Item_Name { get; set; }
        public string Item_Master_Item_Type { get; set; }
        public long Item_Master_Location_ID { get; set; }
        public long Item_Master_Unit_ID { get; set; }
        public decimal Item_Master_Unit_Price { get; set; }
        public string Item_Master_Part_No { get; set; }
    }
}
