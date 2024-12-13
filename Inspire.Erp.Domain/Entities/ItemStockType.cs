using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class ItemStockType
    {
        public int ItemStockTypeId { get; set; }
        public string ItemStockTypeDescription { get; set; }
        public long? ItemStockTypeDelStatus { get; set; }
    }
}
