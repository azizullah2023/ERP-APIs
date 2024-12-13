using Inspire.Erp.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public class JobMasterViewModel
    {

        public int JobMasterJobId { get; set; }
        public string JobMasterJobNo { get; set; }
        public string JobMasterJobName { get; set; }
        public DateTime? JobMasterJobDate { get; set; }
        public decimal? JobMasterJobValue { get; set; }
        public decimal? JobMasterJobRetention { get; set; }
        public decimal? JobMasterJobBalance { get; set; }
        public string JobMasterJobConsultant { get; set; }
        public string JobMasterJobRemarks { get; set; }
        public int? JobMasterJobCustomerId { get; set; }
        public int? JobMasterJobCurrencyId { get; set; }
        public int? JobMasterJobSalesManId { get; set; }
        public string JobMasterJobNumber { get; set; }
        public decimal? JobMasterJobCurVal { get; set; }

        public int? JobMasterJobWorkingDays { get; set; }
        public DateTime? JobMasterJobCommenceDate { get; set; }
        public DateTime? JobMasterJobOrgCompleteDate { get; set; }

        public int? JobMasterJobPayTermDays { get; set; }

        public decimal? JobMasterJobVariationAmount { get; set; }
        public decimal? JobMasterJobAdvPercOnVar { get; set; }
        public decimal? JobMasterJobAdvAmountOnVar { get; set; }
        public string JobMasterJobContractorName { get; set; }
        public string JobMasterJobContractType { get; set; }
        public string JobMasterJobConsultantReff { get; set; }
        public string JobMasterJobRetAccNo { get; set; }
        public string JobMasterJobAdvAccNo { get; set; }

        public int? JobMasterJobStatus { get; set; }
        public decimal? jobMasterJobOpenInv { get; set; }

        public double? JobMasterJobRetentionPercentage { get; set; }
        public decimal? JobMasterJobPerInv { get; set; }

        public decimal? JobMasterJobAdvPerc { get; set; }

        public decimal? JobMasterJobAdvAmount { get; set; }

        public List<JobMasterBudgetDetailsViewModels> JobMasterBudgetDetails { get; set; }
        public List<JobMasterJobDetailsViewModels> JobMasterJobDetails { get; set; }
        public List<JobMasterJobWiseBankGuaranteesViewModels> JobMasterJobWiseBankGuarantees { get; set; }

        public List<JobStaffViewModels> JobStaff { get; set; }
        public List<JobEquipmentViewModels> JobEquipment { get; set; }
        public List<JobExcutionDetailsViewModels> JobExcutionDetails { get; set; }
    }
    public class JobStaffViewModels
    {
        public int Id { get; set; }
        public int JobMasterJobId { get; set; }
        public int? JobMasterStaffId { get; set; }
        public string JobMasterStaffName { get; set; }
    }

    public class JobEquipmentViewModels
    {
        public int Id { get; set; }
        public int JobMasterJobId { get; set; }
        public int? JobMasterEquipId { get; set; }
        public string JobMasterEquipName { get; set; }
    }

    public class JobExcutionDetailsViewModels
    {
        public int Id { get; set; }
        public int JobMasterJobId { get; set; }
        public DateTime? JobMasterExecDate { get; set; }
        public string JobMasterExecDescription { get; set; }
    }
}
