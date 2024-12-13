using System;
using System.Collections.Generic;

namespace Inspire.Erp.Web.ViewModels
{
     public partial class ImportTimeSheetViewModel
    {
        public string Date { get; set; }
        public string Task { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Hours { get; set; }
        public string ExpectedHours { get; set; }
        public int SerialNo { get; set; }
    }
}
