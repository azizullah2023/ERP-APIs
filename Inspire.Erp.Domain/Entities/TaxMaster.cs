using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class TaxMaster
    {
        public int TmId { get; set; }
        public string TmType { get; set; }
        public string TmName { get; set; }
        public decimal? TmPercentage { get; set; }
        public decimal? TmCgst { get; set; }
        public decimal? TmSgst { get; set; }
        public bool? TmDelStatus { get; set; }
    }
}
