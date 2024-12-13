using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals.AccountStatement
{
    public class GetUnitDetailsMasterList
    {
        public int? UnitMasterUnitId { get; set; }
        public int? UnitDetailsId { get; set; }
        public string UnitMasterUnitShortName { get; set; }
        public string UnitMasterUnitFullName { get; set; }
        public string UnitMasterUnitDescription { get; set; }
        public bool? UnitMasterUnitStatus { get; set; }
        public bool? UnitMasterUnitDelStatus { get; set; }
        public double? UnitDetailsConversionType { get; set; }
        public double? UnitDetailsRate { get; set; }



    }
}
