using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Entities
{
    public class StatementOfAccountSummaryRequest
    {
        public string AcNo { get; set; }
        public DateTime StartDate { get; set; }
    }
}
