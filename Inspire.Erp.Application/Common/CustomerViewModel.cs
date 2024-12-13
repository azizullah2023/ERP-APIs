using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Common
{
    public class CustomerViewModel
    {
        public CustomerMaster customerMaster { get; set; }
        public List<CustomerContacts> customercontacts { get; set; }
        public List<CustomerDepartments> customerdepartments { get; set; }
    }
}
