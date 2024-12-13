using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class EquipmentTools
    {
        public int? EquipmentToolsId { get; set; }
        public string EquipmentToolsCode { get; set; }
        public string EquipmentToolsName { get; set; }
        public string EquipmentToolsDescription { get; set; }
        public double? EquipmentToolsValue { get; set; }
        public bool? EquipmentToolsDelStatus { get; set; }
    }
}
