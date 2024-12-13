using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class CustomerContacts
    {
        public int CustomerContactsId { get; set; }
        public string CustomerContactsName { get; set; }
        public string CustomerContactsMobile { get; set; }
        public string CustomerContactsOffice { get; set; }
        public int? CustomerContactsDepartmentId { get; set; }
        public int? CustomerContactsCustomerId { get; set; }
        public bool? CustomerContactsActive { get; set; }
        public string CustomerContactsEmail { get; set; }
        public bool? CustomerContactsDelStatus { get; set; }
    }
}
