using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Entities
{
    public class VoucherTypeMaster
    {
        public int Id { get; set; }
        public string VoucherTypeName { get; set; }
        public DateTime CreatedAt { get; set; }=DateTime.Now;
        public long LastVoucherNo { get; set; }
    }
}
