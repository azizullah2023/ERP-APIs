using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Models
{
    public class PurVoucherRateQuantityModel
    {
        public decimal? Rate { get; set; }
        public decimal? Quantity { get; set; }
        public DateTime? PurchaseDate { get; set; }
        
    }

    public class StockRegisterModel
    {
        public decimal? Rate { get; set; }
        public decimal? Quantity { get; set; }
        public decimal SIN { get; set; }
        public DateTime? AssingedDate { get; set; }
        public string PurchaseId { get; set; }
        public decimal? NetStockBalance { get; set; }
        public long StoreId { get; set; }
    }
}
