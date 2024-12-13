using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals.Sales
{
  public  class CustQuotationFilterModel
    {
        

        public int? CustomerId { get; set; }
        public int? ItemId { get; set; }
        public int? QuotationId { get; set; }
        public int? LocacationId { get; set; }
        public bool  IsbyDate { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

    }
}
