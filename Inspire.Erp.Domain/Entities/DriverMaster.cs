using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class DriverMaster
    {
        public int? DriverMasterId { get; set; }
        public string DriverMasterName { get; set; }
        public string DriverMasterCode { get; set; }
        public string DriverMasterMobile1 { get; set; }
        public string DriverMasterMobile2 { get; set; }
        public string DriverMasterMobile3 { get; set; }
        public string DriverMasterMobile4 { get; set; }
        public string DriverMasterMobile5 { get; set; }
        public string DriverMasterMobile6 { get; set; }
        public bool? DriverMasterDelStatus { get; set; }
    }
}
