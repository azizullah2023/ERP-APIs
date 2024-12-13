using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class TermsAndCondition
    {
        public int TermsAndConditionTermsId { get; set; }
        public string TermsAndConditionTermsConditions { get; set; }
        public bool? TermsAndConditionTermsDeleted { get; set; }
        public bool? TermsAndConditionsTermsDelStatus { get; set; }
    }
}
