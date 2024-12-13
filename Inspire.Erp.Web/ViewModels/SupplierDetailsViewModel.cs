using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public class SupplierDetailsViewModel
    {
        public int SupplierDetailsId { get; set; }
        public int? SupplierDetailsSupplierInsId { get; set; }
        public byte[] SupplierDetailsContract { get; set; }
        public byte[] SupplierDetailsContract1 { get; set; }
        public byte[] SupplierDetailsContract2 { get; set; }
        public bool? SupplierDetailsDelStatus { get; set; }
    }
}
