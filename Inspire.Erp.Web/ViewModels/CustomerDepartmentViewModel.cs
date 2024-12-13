using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public class CustomerDepartmentViewModel
    {
        public long CustomerDepartmentsId { get; set; }
        public long? CustomerDepartmentsDepartmentId { get; set; }
        public long? CustomerDepartmentsCustomerId { get; set; }
        public bool? CustomerDepartmentsActive { get; set; }
        public bool? CustomerDepartmentsDelStatus { get; set; }

    }
}