using Inspire.Erp.Domain.DTO.Job_Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Inspire.Erp.Domain.Entities
{
    public partial class JobMaster
    {

        public int JobMasterJobId { get; set; }
        public string JobMasterJobNo { get; set; }
        public string JobMasterJobName { get; set; }
        public DateTime? JobMasterJobDate { get; set; }
        public decimal? JobMasterJobValue { get; set; }
        public decimal? JobMasterJobRetention { get; set; }
        public decimal? JobMasterJobBalance { get; set; }
        public int? JobMasterJobStatus { get; set; }
        public string JobMasterJobConsultant { get; set; }
        public string JobMasterJobRemarks { get; set; }
        public int? JobMasterJobCustomerId { get; set; }
        public int? JobMasterJobCurrencyId { get; set; }
        public decimal? JobMasterJobCurVal { get; set; }
        public decimal? JobMasterJobOpenInv { get; set; }
        public decimal? JobMasterJobPerInv { get; set; }
        public string JobMasterJobConsultantReff { get; set; }
        public bool? JobMasterJobStatusVal { get; set; }
        public string JobMasterJobType { get; set; }
        public int? JobMasterJobRelativeNo { get; set; }
        public string JobMasterJobCode { get; set; }
        public int? JobMasterJobSupplierId { get; set; }
        public int? JobMasterJobSalesManId { get; set; }
        public string JobMasterJobNumber { get; set; }
        public bool? JobMasterJobIsSubJob { get; set; }
        public double? JobMasterJobRetentionPercentage { get; set; }
        public int? JobMasterJobSalesMan { get; set; }
        public decimal? JobMasterJobBudgetMaterialCost { get; set; }
        public decimal? JobMasterJobMaterialCost { get; set; }
        public decimal? JobMasterJobMaterialCostVar { get; set; }
        public decimal? JobMasterJobBugetLabourCost { get; set; }
        public decimal? JobMasterJobLabourCost { get; set; }
        public decimal? JobMasterJobLabourCostVar { get; set; }
        public decimal? JobMasterJobBudgetOverHeadCost { get; set; }
        public decimal? JobMasterJobOverHeadCost { get; set; }
        public decimal? JobMasterJobOverHeadCostVar { get; set; }
        public decimal? JobMasterJobMiscCost { get; set; }
        public decimal? JobMasterJobTotalCost { get; set; }
        public decimal? JobMasterJobProfit { get; set; }
        public decimal? JobMasterJobTotalInvValue { get; set; }
        public decimal? JobMasterJobTotalPayValue { get; set; }
        public decimal? JobMasterJobTotalRecValue { get; set; }
        public decimal? JobMasterJobTotalExpValue { get; set; }
        public string JobMasterJobContractorName { get; set; }
        public string JobMasterJobContractType { get; set; }
        public DateTime? JobMasterJobCommenceDate { get; set; }
        public int? JobMasterJobWorkingDays { get; set; }
        public DateTime? JobMasterJobOrgCompleteDate { get; set; }
        public int? JobMasterJobPayTermDays { get; set; }
        public decimal? JobMasterJobAdvanceAmount { get; set; }
        public decimal? JobMasterJobNoCostCenter { get; set; }
        public decimal? JobMasterJobPendingPo { get; set; }
        public int? JobMasterJobHrjobId { get; set; }
        public decimal? JobMasterJobAdvPerc { get; set; }
        public decimal? JobMasterJobVariationAmount { get; set; }
        public decimal? JobMasterJobAdvPercOnVar { get; set; }
        public decimal? JobMasterJobAdvAmountOnVar { get; set; }
        public decimal? JobMasterJobAdvAmountTotal { get; set; }
        public string JobMasterJobRetAccNo { get; set; }
        public string JobMasterJobRetAccName { get; set; }
        public string JobMasterJobAdvAccNo { get; set; }
        public string JobMasterJobAdvAccName { get; set; }
        public decimal? JobMasterJobCumValue { get; set; }
        public decimal? JobMasterJobDirectCost { get; set; }
        public decimal? JobMasterJobRetAmount { get; set; }
        public decimal? JobMasterJobAdvInvoice { get; set; }
        public decimal? JobMasterJobAdvRecovery { get; set; }
        public decimal? JobMasterJobDeduction { get; set; }
        public decimal? JobMasterJobReceivable { get; set; }
        public decimal? JobMasterJobVatamount { get; set; }
        public decimal? JobMasterJobReceived { get; set; }
        public int? JobMasterJobBuildId { get; set; }
        public int? JobMasterJobFlatId { get; set; }
        public int? JobMasterJobContractId { get; set; }
        public int? JobMasterJobComplientId { get; set; }
        public int? JobMasterJobCustQtnId { get; set; }
        public bool? JobMasterJobDelStatus { get; set; }

        [NotMapped]
        public List<JobMasterBudgetDetails> JobMasterBudgetDetails { get; set; }
        [NotMapped]
        public List<JobMasterJobDetails> JobMasterJobDetails { get; set; }

        [NotMapped]
        public List<JobMasterJobWiseBankGuarantees> JobMasterJobWiseBankGuarantees { get; set; }
        [NotMapped]
        public List<AccountsTransactions> AccountsTransactions { get; set; }
        [NotMapped]
        public List<JobMasterExpenseDto> JobMasterExpenses { get; set; }

        [NotMapped]
        public List<JobStaff> JobStaff { get; set; }
        [NotMapped]
        public List<JobEquipment> JobEquipment { get; set; }
        [NotMapped]
        public List<JobExcutionDetails> JobExcutionDetails { get; set; }
    }

    public class JobStaff
    {
        public int Id { get; set; }
        public int JobMasterJobId { get; set; }
        public int? JobMasterStaffId { get; set; }
        public string JobMasterStaffName { get; set; }

        [JsonIgnore]
        public virtual JobMaster JobMasterJobJob { get; set; }
    }

    public class JobEquipment
    {
        public int Id { get; set; }
        public int JobMasterJobId { get; set; }
        public int? JobMasterEquipId { get; set; }
        public string JobMasterEquipName { get; set; }
        [JsonIgnore]
        public virtual JobMaster JobMasterJobJob { get; set; }
    }

    public class JobExcutionDetails
    {
        public int Id { get; set; }
        public int JobMasterJobId { get; set; }
        public DateTime? JobMasterExecDate { get; set; }
        public string JobMasterExecDescription { get; set; }
        [JsonIgnore]
        public virtual JobMaster JobMasterJobJob { get; set; }
    }
}
