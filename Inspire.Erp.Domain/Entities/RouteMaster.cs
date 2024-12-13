using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Entities
{
    public partial class RouteMaster
    {
        public int RmId { get; set; }
        public string? RmName { get; set; }
        public int? RmVanId { get; set; }
        public int? RmSalesmanId { get; set; }
        public bool? RmStatus { get; set; }
        public bool? RmDelStatus { get; set; }
    }
}

