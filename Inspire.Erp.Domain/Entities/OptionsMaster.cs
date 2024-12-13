using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class OptionsMaster
    {
        public int OptionsMasterId { get; set; }
        public string OptionsMasterDescription { get; set; }
        public string OptionsMasterType { get; set; }
        public string OptionsMasterFullDescription { get; set; }
        public string OptionsMasterFormName { get; set; }
        public bool? OptionsMasterDelStatus { get; set; }
        public string OptionsMasterValueType { get; set; }
    }
}
