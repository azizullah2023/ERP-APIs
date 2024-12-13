using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals.File;
using Inspire.Erp.Domain.Modals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Master
{
    public interface IJobMasterService
    {
        public JobMaster InsertJobMaster(JobMaster jobMaster, List<JobMasterBudgetDetails> jobMasterBudgetDetails, List<JobMasterJobDetails> jobMasterJobDetails , List<JobMasterJobWiseBankGuarantees> jobMasterJobWiseBankGuarantees,List<JobStaff> jobStaff, List<JobEquipment> jobEquipment, List<JobExcutionDetails> jobExcutionDetails);
        public JobMaster UpdateJobMaster(JobMaster jobMaster, List<JobMasterBudgetDetails> jobMasterBudgetDetails, List<JobMasterJobDetails> jobMasterJobDetails , List<JobMasterJobWiseBankGuarantees> jobMasterJobWiseBankGuarantees);
        public JobMaster GetSavedJobMasterDetails(string jbno);

        public IEnumerable<JobMaster> DeleteJob(JobMaster jobMaster);
        public IEnumerable<JobMaster> GetAllJob();
        public IEnumerable<JobMaster> GetAllJobById(int id);

        public Task<string>  GetJobHTML(int id);

        public void SaveJobDocument(JobDocuments jobDocumnet);
    }
}
