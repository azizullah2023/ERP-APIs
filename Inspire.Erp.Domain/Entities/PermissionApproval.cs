using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Entities
{
    public class PermissionApproval
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int UserId { get; set; }
        public string VoucherType { get; set; }
        public bool IsApproval { get; set; }
        public int LevelOrder { get; set; }
        public int VoucherTypeId { get; set; }
    }
}
