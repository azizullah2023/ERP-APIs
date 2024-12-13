using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class ItemSupplierDetails
    {
        public long ItemSupplierDetailsItemSupplierDetailsId { get; set; }
        public long? ItemSupplierDetailsItemId { get; set; }
        public long? ItemSupplierDetailsSupplierId { get; set; }
        public decimal? ItemSupplierDetailsRate { get; set; }
        public int? ItemSupplierDetailsBatchId { get; set; }
        public bool? ItemSupplierDetailsDelStatus { get; set; }

        public virtual ItemMaster ItemSupplierDetailsItem { get; set; }
    }
}
