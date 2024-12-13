using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inspire.Erp.Domain.Entities
{
    public partial class Suppliers
    {
        public Suppliers()
        {
            //PurchaseVoucher = new HashSet<PurchaseVoucher>();
        }

        public int SuppliersInsId { get; set; }
        public string SuppliersInsName { get; set; }
        public string SuppliersContactPerson { get; set; }
        public int? SuppliersCtId { get; set; }
        public int? SuppliersCityId { get; set; }
        public string SuppliersPoBox { get; set; }
        public string SuppliersTel1 { get; set; }
        public string SuppliersTel2 { get; set; }
        public string SuppliersFax { get; set; }
        public string SuppliersMobile { get; set; }
        public string SuppliersEmail { get; set; }
        public string SuppliersWebSite { get; set; }
        public int? SuppliersLocationId { get; set; }
        public string SuppliersRemarks { get; set; }
        public string SuppliersInsReffAcNo { get; set; }
        public string SuppliersSupplierType { get; set; }
        public int? SuppliersUserId { get; set; }
        public int? SuppliersCurrencyId { get; set; }
        public int? SuppliersConsup { get; set; }
        public int? SuppliersCrp { get; set; }
        public int? SuppliersCrl { get; set; }
        public bool? SuppliersStatus { get; set; }
        public bool? SuppliersDeleteStatus { get; set; }
        public bool? SuppliersStatusVal { get; set; }
        public string SuppliersPaymentTerms { get; set; }
        public double? SuppliersCreditDays { get; set; }
        public double? SuppliersCreditLimit { get; set; }
        public string SuppliersGroupNo { get; set; }
        public string SuppliersTrno { get; set; }
        public string SuppliersVatNo { get; set; }
        public bool? SuppliersDelStatus { get; set; }
        [NotMapped]
        public List<PurchaseVoucher> PurchaseVoucher { get; set; }
    }
}
