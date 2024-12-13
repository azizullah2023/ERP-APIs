using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals
{
    public class ItemMasterResponse
    {
        public long ItemId { get; set; }
        public string ItemName { get; set; }

        public string VoucherNo { get; set; }
        public int? UnitDetailsId { get; set; }
        public bool ItemwiseInclusive { get; set; }
        public string BarCode { get; set; }
        public string UnitName { get; set; }
        public decimal? UnitPrice { get; set; }
        public double? UnitPriceTaxIncl { get; set; }
        public decimal? VATValue { get; set; }
        public double? OfferPrice { get; set; }
        public decimal SalesTax { get; set; }
        public decimal SalesTax1 { get; set; }

        public decimal? Discount { get; set; }
        public int? Quantity { get; set; }
    }
   
}
