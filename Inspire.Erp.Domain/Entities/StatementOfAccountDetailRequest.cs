using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Entities
{
    public class StatementOfAccountDetailRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string AcNo { get; set; }
        public string Description { get; set; }
        public string Particulars { get; set; }
        public string JobNo { get; set; }
    }
}
