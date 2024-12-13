using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class BatchBarCodes
    {
        public int BatchBarCodesId { get; set; }
        public int? BatchBarCodesItemId { get; set; }
        public string BatchBarCodesGenBatchBarcode { get; set; }
        public string BatchBarCodesBatchCode { get; set; }
        public DateTime? BatchBarCodesExpDate { get; set; }
        public string BatchBarCodesUnivBarcode { get; set; }
        public bool? BatchBarCodesDelStatus { get; set; }
    }
}
