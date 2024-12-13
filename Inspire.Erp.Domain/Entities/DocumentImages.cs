using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class DocumentImages
    {
        public int? DocumentImageId { get; set; }
        public string DocumentImageName { get; set; }
        public int? DocumentImageDocumentId { get; set; }
        public byte[] DocumentImageImage { get; set; }
        public string DocumentImageExtension { get; set; }
        public string DocumentImageVoucherType { get; set; }
        public bool? DocumentImageDelStatus { get; set; }
    }
}
