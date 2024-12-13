using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class CustomerDepartments
    {
        public long CustomerDepartmentsId { get; set; }
        public long? CustomerDepartmentsDepartmentId { get; set; }
        public long? CustomerDepartmentsCustomerId { get; set; }
        public bool? CustomerDepartmentsActive { get; set; }
        public bool? CustomerDepartmentsDelStatus { get; set; }
    }
}
