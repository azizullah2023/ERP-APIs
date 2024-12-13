using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Common
{
    public class CustomerQuotationModel
    {
        public CustomerQuotation custQuotation { get; set; }
        public List<CustomerQuotationDetails> customerQuotationDetails { get; set; }

    }
}
