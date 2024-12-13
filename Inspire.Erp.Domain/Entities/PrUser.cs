using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class PrUser
    {
        public int PrUserId { get; set; }
        public int? PrUserUserId { get; set; }
        public string PrUserName { get; set; }
        public string PrUserVoudType { get; set; }
        public bool? PrUserUallow { get; set; }
        public bool? PrUserDelStatus { get; set; }
    }
}
