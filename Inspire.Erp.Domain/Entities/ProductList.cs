using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class ProductList
    {
        public int? ProductListId { get; set; }
        public int? ProductListItemId { get; set; }
        public string ProductListItemName { get; set; }
        public double? ProductListWPrice { get; set; }
        public double? ProductListRPrice { get; set; }
        public DateTime? ProductListExpDate { get; set; }
        public string ProductListBonus { get; set; }
        public string ProductListMoviCole { get; set; }
        public string ProductListPackingSize { get; set; }
        public string ProductListGroupHead { get; set; }
        public bool? ProductListDelStatus { get; set; }
    }
}
