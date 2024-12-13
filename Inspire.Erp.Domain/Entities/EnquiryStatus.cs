using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class EnquiryStatus
    {
        public long? EnquiryStatusEnquiryStatusId { get; set; }
        public string EnquiryStatusEnquiryStatus { get; set; }
        public bool? EnquiryStatusEnquiryStatusDelStatus { get; set; }
    }
}
