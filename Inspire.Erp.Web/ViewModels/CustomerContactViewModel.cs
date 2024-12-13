using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public class CustomerContactViewModel
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
