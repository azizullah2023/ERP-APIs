using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class SalesManMaster
    {
        public int? SalesManMasterSalesManId { get; set; }
        public string SalesManMasterSalesManName { get; set; }
        public bool? SalesManMasterSalesManDeleted { get; set; }
        public string SalesManMasterSalesManContactNo { get; set; }
        public int? SalesManMasterSalesManLocationId { get; set; }
        public bool? SalesManMasterSalesManDelStatus { get; set; }
    }
}
