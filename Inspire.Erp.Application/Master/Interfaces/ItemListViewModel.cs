using Inspire.Erp.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Inspire.Erp.Application.Master
{
    public class ItemListViewModel
    {
        public string? BarcodePrint { get; set; }
        public long? ItemId { get; set; }
        public string? PartNo { get; set; }
        public string? Item_Name { get; set; }
        public double? Stock { get; set; }
        public string? Barcode { get; set; }
        public string? GroupName { get; set; }
        public string? VendorName { get; set; }
        public decimal? Price { get; set; }
        public int Style { get; set; }
        public string? UD_UNIT { get; set; }
        public string? UD_Barcode { get; set; }
        public double? Unit_Details_ConversionType { get; set; }
        public double? Unit_Details_Rate { get; set; }
        public string? Packing { get; set; }
        public string? UD_Discrption { get; set; }
        public double? UD_UnitCost { get; set; }
        public double? UD_UnitPrice { get; set; }
        public string SupplierName { get; set; }

        public long? ItemMasterAccountNo { get; set; }

        public long? UnitDetailsID { get; set; }

        public string? ItemSize { get; set; }
        public bool? isUpdated { get; set; }
        public List<RateItemListrModel> RateItemListrModel { get; set; }
    }
    public class ItemFilterModel
    {
        public string? Barcode { get; set; }
        public string? PartNo { get; set; }

        public string? ItemName { get; set; }
        public string? VendorName { get; set; }

        public string? GruopName { get; set; }
        public string? Priceinsname { get; set; }
        public bool IsContaining { get; set; }   
    }
}