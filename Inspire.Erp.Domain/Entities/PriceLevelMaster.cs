using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class PriceLevelMaster
    {
        public PriceLevelMaster()
        {
            ItemPriceLevelDetails = new HashSet<ItemPriceLevelDetails>();
        }

        public long PriceLevelMasterPriceLevelId { get; set; }
        public string PriceLevelMasterPriceLevelName { get; set; }
        public bool? PriveLevelMasterPriceLevelDelStatus { get; set; }

        public virtual ICollection<ItemPriceLevelDetails> ItemPriceLevelDetails { get; set; }
    }
}
