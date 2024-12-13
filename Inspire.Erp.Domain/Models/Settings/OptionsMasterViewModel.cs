using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals.Settings
{
  public  class OptionsMasterViewModel
    {
        public int OptionsMasterId { get; set; }
        public string OptionsMasterDescription { get; set; }
        public string OptionsMasterType { get; set; }
        public bool OptionsMasterTypeBoolean { get; set; }
        public string OptionsMasterFullDescription { get; set; }
        public string OptionsMasterFormName { get; set; }
        public bool? OptionsMasterDelStatus { get; set; }

    }
}
