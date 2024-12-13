using System;
using System.Collections.Generic;
using System.Text;
using Inspire.Erp.Domain.Entities;

namespace Inspire.Erp.Application.Common
{
    public partial class DeliveryNoteModel
    {
        public CustomerDeliveryNote deliveryNote { get; set; }
    
        public List<CustomerDeliveryNoteDetails> deliveryNoteDetails { get; set; }
    }
}
