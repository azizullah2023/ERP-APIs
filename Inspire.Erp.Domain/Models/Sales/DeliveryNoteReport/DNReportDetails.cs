using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Models.Sales.DeliveryNoteReport
{
    public class DNReportDetails
    {
        public string  CustomerName { get; set; }
        public DateTime? DaliveryDate { get; set; }
        public string DeliverySatus { get; set; }
        public string Location { get; set; }
        public long DelId { get; set; }
        public List<DNItemList> ItemList { get; set; }

    }

    public class DNItemList
    {
        public long? MaterialId { get; set; }
        public string Description { get; set; }
        public decimal? Qty { get; set; }

    }
}
