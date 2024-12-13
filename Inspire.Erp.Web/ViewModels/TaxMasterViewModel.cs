using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Inspire.Erp.Web.ViewModels
{
    public class TaxMasterViewModel
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
