using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Entities
{
    public class WorkGroupMenuDetail
    {
        public int Id { get; set; }

        public int MenuId { get; set; }
        public Menu Menus { get; set; }

        public int WorkgroupId { get; set; }
        public WorkGroupMaster Workgroup { get; set; }

    }
}
