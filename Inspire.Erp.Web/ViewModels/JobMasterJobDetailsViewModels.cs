using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public class JobMasterJobDetailsViewModels
    {

        public int JobMasterJobDetailsJobDetId { get; set; }
        public int? JobMasterJobDetailsJobId { get; set; }
        public string JobMasterNo { get; set; }
        public string JobMasterJobDetailsDescription { get; set; }
        public string JobMasterJobDetailsStatus { get; set; }
        public string JobMasterJobDetailsRefNo { get; set; }
        public DateTime? JobMasterJobDetailsStatusDate { get; set; }
       // public bool? JobMasterJobDetailsIsEdit { get; set; }
        public int? JobMasterJobDetailsUnitIdN { get; set; }
        public double? JobMasterJobDetailsQtyN { get; set; }
        public int? JobMasterJobDetailsDescriptionId { get; set; }
        public decimal? JobMasterJobDetailsJdAmt { get; set; }
        public bool? JobMasterJobDetailsDelStatus { get; set; }




    }
}
