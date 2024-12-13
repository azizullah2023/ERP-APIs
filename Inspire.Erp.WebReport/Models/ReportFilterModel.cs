using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inspire.Erp.WebReport.Models
{
    public class ReportFilterModel
    {
        public string ReportName { get; set; }
        public string SelectionFormula { get; set; }
        public string DsnFileName { get; set; } = string.Empty;
        public List<ReportParameters> Parameters { get; set; }
    }
    public class ReportParameters
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}