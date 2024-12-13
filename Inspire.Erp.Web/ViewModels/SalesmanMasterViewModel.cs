using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public class SalesmanMasterViewModel

    {
        public int? SalesManMasterSalesManId { get; set; }
        public string SalesManMasterSalesManName { get; set; }
        public bool? SalesManMasterSalesManDeleted { get; set; }
        public string SalesManMasterSalesManContactNo { get; set; }
        public int? SalesManMasterSalesManLocationId { get; set; }
        public bool? SalesManMasterSalesManDelStatus { get; set; }

    }
}
