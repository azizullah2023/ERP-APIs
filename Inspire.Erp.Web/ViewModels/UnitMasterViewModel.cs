using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public class UnitMasterViewModel
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
