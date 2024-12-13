using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Common
{
    public class CustomerEnquiryModel
    {
        public EnquiryMaster custEnquiry { get; set; }
        public List<EnquiryDetails> customerEnquiryDetails { get; set; }

    }
}
