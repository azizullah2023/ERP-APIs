using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class CommonDocuments
    {
        public int? CommonDocumentsDocId { get; set; }
        public int? CommonDocumentsVouchIdN { get; set; }
        public string CommonDocumentsVoucherTypeV { get; set; }
        public string CommonDocumentsDocPathV { get; set; }
        public int? CommonDocumentsDocN { get; set; }
        public byte[] CommonDocumentsDocImage { get; set; }
        public string CommonDocumentsDocRemarksV { get; set; }
        public bool? CommonDocumentsDocDelStatusB { get; set; }
        public bool? CommonDocumentsDelStatus { get; set; }
    }
}
