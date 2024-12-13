using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals.Sales
{
    public class CustPurchOrderFilterModel
    {
        public int? LocacationId { get; set; }

        public int? ItemId { get; set; }

        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }

        public bool IsDeliveryAgainstSales { get; set; }

        public bool IsAll { get; set; }

        public string LPO { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public bool isDataChecked { get; set; }

    }
}
