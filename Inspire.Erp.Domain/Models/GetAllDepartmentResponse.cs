using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals
{
    public class GetAllDepartmentResponse
    {
        public int Department_Master_Department_ID { get; set; }
        public string Department_Master_Department_Name { get; set; }
        public string Department_Master_Department_Code { get; set; }
        public bool? Department_Master_Department_Status { get; set; }
        public bool? Department_Master_Department_DelStatus { get; set; }
    }
}
