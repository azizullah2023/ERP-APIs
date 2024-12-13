using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class SupplierDetails
    {
        public int SupplierDetailsId { get; set; }
        public int? SupplierDetailsSupplierInsId { get; set; }
        public byte[] SupplierDetailsContract { get; set; }
        public byte[] SupplierDetailsContract1 { get; set; }
        public byte[] SupplierDetailsContract2 { get; set; }
        public bool? SupplierDetailsDelStatus { get; set; }
    }
}
