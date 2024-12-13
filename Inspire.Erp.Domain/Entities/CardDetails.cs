using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class CardDetails
    {
        public int? CardDetailsId { get; set; }
        public string CardDetailsBillNo { get; set; }
        public string CardDetailsCardType { get; set; }
        public string CardDetailsCardNo { get; set; }
        public bool? CardDetailsDelStatus { get; set; }
    }
}
