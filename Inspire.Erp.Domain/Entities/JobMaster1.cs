using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Inspire.Erp.Domain.Entities
{
    public partial class JobMaster1
    {
        [Key]
        public int ID { get; set; }
        public int? JobMaster1JobCode { get; set; }
        public int? JobMaster1ConsigneeId { get; set; }
        public string JobMaster1Awbno { get; set; }
        public int? JobMaster1DesignationId { get; set; }
        public int? JobMaster1NoPackages { get; set; }
        public int? JobMaster1CarrierId { get; set; }
        public string JobMaster1Transit { get; set; }
        public double? JobMaster1ActualWeight { get; set; }
        public double? JobMaster1ChargebleWeight { get; set; }
        public double? JobMaster1FreightRate { get; set; }
        public string JobMaster1Remarks { get; set; }
        public int? JobMaster1Status { get; set; }
        public string JobMaster1Type { get; set; }
        public DateTime? JobMaster1Date { get; set; }
        public string JobMaster1ContainerNo { get; set; }
        public string JobMaster1SealNo { get; set; }
        public string JobMaster1ContainerType { get; set; }
        public bool? JobMaster1DelStatus { get; set; }
    }
}
