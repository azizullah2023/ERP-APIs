using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inspire.Erp.Domain.Entities
{
    public partial class ManufactureItemsMaster
    {
        public int MI_Id_N { get; set; }
        public string MI_VocherNo { get; set; }
        public string MI_RF_V { get; set; }
        public int? MI_Item_ID { get; set; }
        public string MI_Remarks_V { get; set; }
        public bool? ManufactureItemsMasterStatus { get; set; }
        public bool? ManufactureItemsMasterDeleted { get; set; }
        [NotMapped]
        public List<ManufactureItemsMasterDetails> ManufactureItemsMasterDetails { get; set; }
    }    
}
