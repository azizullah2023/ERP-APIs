using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals
{
    public class StockItemResponse
    {
        public long Item_Master_Item_Id { get; set; }
        public string? Item_Master_Item_Name { get; set; }
        public string? Item_Master_Batch_Code { get; set; }
        public string? Item_Master_Part_No { get; set; }
        public string? Item_Master_Barcode { get; set; }
        public decimal? Stock { get; set; }
    }
    public class ItemResponse
    {
        public long ItemId { get; set; }
        public string ItemName { get; set; }

        public string MaterialCode { get; set; }
        public string PartNo { get; set; }
        public string VendorName { get; set; }
        public double? Stock { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? LastPurPrice { get; set; }
        public decimal? LastSalePrice { get; set; }
        public double? LandingCost { get; set; }
    }
}
