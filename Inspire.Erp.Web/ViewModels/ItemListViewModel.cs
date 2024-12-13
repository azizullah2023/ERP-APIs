using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Web.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public class ItemListViewModel
    {
        public string? BarcodePrint { get; set; }
        public long? ItemId { get; set; }
        public string? PartNo { get; set; }
        public string? Item_Name { get; set; }
        public long? Stock { get; set; }
        public string? Barcode { get; set; }
        public string? GroupName { get; set; }
        public string? VendorName { get; set; }
        public decimal? Price { get; set; }
        public long? Style { get; set; }
        public string? UD_UNIT { get; set; }
        public string? UD_Barcode { get; set; }
        public string? Unit_Details_ConversionType { get; set; }
        public decimal? Unit_Details_Rate { get; set; }
        public string? Packing { get; set; }
        public string? UD_Discrption { get; set; }
        public string? UD_UnitCost { get; set; }
        public string? UD_UnitPrice { get; set; }
        public string? SupplierName { get; set; }
       
    }

    public class ItemFilterModel
    {
        public string? Barcode { get; set; }
        public string? PartNo { get; set; }

        public string? ItemName { get; set; }
        public string? VendorName { get; set; }
        public string? Priceinsname { get; set; }
        public bool? IsContaining { get; set; }

    }
}

