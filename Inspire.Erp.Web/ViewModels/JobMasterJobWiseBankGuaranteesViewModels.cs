using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public class JobMasterJobWiseBankGuaranteesViewModels
    {
        public int JobMasterJobWiseBankGuaranteesDtlId { get; set; }
        public int JobMasterJobWiseBankGuaranteesJobId { get; set; }
        public string JobMasterNo { get; set; }
        public long JobMasterJobWiseBankGuaranteesBgid { get; set; }
        public decimal? JobMasterJobWiseBankGuaranteesAmount { get; set; }
        public int? JobMasterJobWiseBankGuaranteesSlNo { get; set; }
        public bool? JobMasterJobWiseBankGuaranteesDelStatus { get; set; }

    }
}
