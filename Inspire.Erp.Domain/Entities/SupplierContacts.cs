using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class SupplierContacts
    {
        public int? SupplierContactsId { get; set; }
        public string SupplierContactsContactName { get; set; }
        public string SupplierContactsContactMobile { get; set; }
        public string SupplierContactsContactOffice { get; set; }
        public int? SupplierContactsDepartmentId { get; set; }
        public int? SupplierContactsSupplierId { get; set; }
        public bool? SupplierContactsActive { get; set; }
        public string SupplierContactsEmail { get; set; }
        public bool? SupplierContactsDelStatus { get; set; }
    }
}
