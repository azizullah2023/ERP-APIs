using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inspire.Erp.Domain.Entities
{
    public partial class DamageMaster
    {
        public int? DamageMasterId { get; set; }
        public DateTime? DamageMasterVdate { get; set; }
        public string DamageMasterNarration { get; set; }
        public int? DamageMasterLocationId { get; set; }
        public bool? DamageMasterDelStatus { get; set; }
        public string DamageMasterVoucherNumber { get; set; }

        [NotMapped]
        public List<DamageDetails> DamageDetails { get; set; }
    }
}
//public class DamageMasterDetails
//{
//    public string? Damage_Details_Voucher_Number { get; set; }
//    public string? Location_Master_Location_Name { get; set; }
//    public long? Item_Master_Item_ID { get; set; }
//    public string? Item_Master_Item_Name { get; set; }
//    public int? Stock_Register_Material_ID { get; set; }
//    public int? Damage_Master_ID { get; set; }
//    public DateTime? Damage_Master_Vdate { get; set; }
//    public int? Damage_Master_Location_ID { get; set; }
//    public string? Damage_Master_Narration { get; set; }
//    public int? Damage_Details_Material_ID { get; set; }
//    public double? Damage_Details_QTY { get; set; }
//    public double? Damage_Details_Price { get; set; }
//    public double? Damage_Details_Amount { get; set; }
//    public string? Damage_Details_Remarks { get; set; }
//    public int? Damage_Details_Unit_ID { get; set; }
//}
//    public class DemageRequest
//    {

//        public List<VouchersNumbers> voucherNumberList { get; set; }
//        public List<DamageMaster> damageMasterList { get; set; }
//        public List<DamageDetails> damageDetailsList { get; set; }
//        public List<StockRegister> stockRegisterList { get; set; }
//    }
//    public class DamageEntryViewModal
//    {
//        public string? Damage_Details_Voucher_Number { get; set; }
//        public string? Location_Master_Location_Name { get; set; }
//        public long? Item_Master_Item_ID { get; set; }
//        public string? Item_Master_Item_Name { get; set; }
//        public int? Stock_Register_Material_ID { get; set; }
//        public int? Damage_Master_ID { get; set; }
//        public DateTime? Damage_Master_Vdate { get; set; }
//        public int? Damage_Master_Location_ID { get; set; }
//        public string? Damage_Master_Narration { get; set; }
//        public int? Damage_Details_Material_ID { get; set; }
//        public double? Damage_Details_QTY { get; set; }
//        public double? Damage_Details_Price { get; set; }
//        public double? Damage_Details_Amount { get; set; }
//        public string? Damage_Details_Remarks { get; set; }
//        public int? Damage_Details_Unit_ID { get; set; }
//    }
//}
