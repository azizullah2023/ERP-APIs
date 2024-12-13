using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class LinkAccount
    {
        public string LinkAccountAccDesc { get; set; }
        public string LinkAccountAccKey { get; set; }
        public string LinkAccountAccNo { get; set; }
        public bool? LinkAccountDelStatus { get; set; }
    }
}
