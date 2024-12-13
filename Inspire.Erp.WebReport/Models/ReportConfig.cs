using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inspire.Erp.WebReport.Models
{
    public class ReportConfig
    {
        public string ReportPath { get; set; }

        public string ConnectionServerName { get; set; }
        public string ConnectionUserDB { get; set; }
        public string ConnectionPasswordDB { get; set; }
        public string ConnectionDBName { get; set; }
        public string ConnectionTablePrefix { get; set; }

        public string SelectionFormula { get; set; }

        public string ExportPath { get; set; }
        public string ExportNamefile { get; set; }
        public string ExportFormat { get; set; }

    }
}