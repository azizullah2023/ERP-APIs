using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Entities
{
    public class Menu
    {
        public Menu()
        {
            WorkGroupPermissions = new HashSet<WorkGroupPermissions>();

        }
        public int Id { get; set; }
        public string Title { get; set; }
        public bool? IsDeleted { get; set; }
        public int? ParentId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string RouteUrl { get; set; }
        public string IconClass { get; set; }
        public int? Order { get; set; }
        public string Page { get; set; }
        // Navigation property for many-to-many relationship
        //public ICollection<WorkGroupMenuDetail> MenusWorkgroupDetails { get; set; }
        public ICollection<WorkGroupPermissions> WorkGroupPermissions { get; set; }
    }
}
