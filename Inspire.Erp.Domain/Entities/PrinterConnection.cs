using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class PrinterConnection
    {
        public int Id { get; set; }
        public string SystemUser { get; set; }
        public string SytemConnectionId { get; set; }
        public string AppUserId { get; set; }
        public string AppConnectionId { get; set; }
        public string PrinterName { get; set; }
        public bool? IsActive { get; set; }
        public bool? Status { get; set; }
    }
}
