using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Inspire.Erp.Application.Account;
using Inspire.Erp.Application.Account.Implementation;
using Inspire.Erp.Application.Master;
using Inspire.Erp.Application.NewFolder.Interfaces;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Domain.Modals.File;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Inspire.Erp.Web.Common;
using Inspire.Erp.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inspire.Erp.Web.Controllers
{
    [Route("api")]
    [Produces("application/json")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IMapper _mapper;
        private IJobMasterService jobMasterService;
        private IFileService fileService;
        private IRepository<CurrencyMaster> currencyrepository; private IRepository<CountryMaster> countryrepository;
        private IChartofAccountsService chartofAccountsService; private IRepository<SalesManMaster> salesmanrepository;
        private IRepository<CustomerMaster> _customerMasterRepository; private IRepository<ProjectDescriptionMaster> ProjectDescriptionrepository;
        private IRepository<BankGuaranteeMaster> bankGuaranteerepository;
        public JobController(IJobMasterService _jobMasterService, IMapper mapper,
             IRepository<CurrencyMaster> _currencyrepository, IRepository<ProjectDescriptionMaster> _ProjectDescriptionrepository
            , IChartofAccountsService _chartofAccountsService, IRepository<BankGuaranteeMaster> _bankGuaranteerepository, IRepository<SalesManMaster> _salesmanrepository, IRepository<CustomerMaster> customerMasterRepository
            ,IFileService _fileService
            )
        {
            jobMasterService = _jobMasterService;
            _mapper = mapper;
            ProjectDescriptionrepository = _ProjectDescriptionrepository;
            chartofAccountsService = _chartofAccountsService; salesmanrepository = _salesmanrepository;
            currencyrepository = _currencyrepository; _customerMasterRepository = customerMasterRepository;
            bankGuaranteerepository = _bankGuaranteerepository;
            fileService = _fileService;
        }
        [HttpGet]
        [Route("GetJobMaster")]
        public ApiResponse<List<JobMaster>> GetAllJob()
        {
            ApiResponse<List<JobMaster>> apiResponse = new ApiResponse<List<JobMaster>>
            {
                Valid = true,
                Result = _mapper.Map<List<JobMaster>>(jobMasterService.GetAllJob()),
                Message = ""
            };
            return apiResponse;
        }


        [HttpGet]
        [Route("GetAllMasterById/{id}")]
        public ApiResponse<JobMaster> GetAllJobById(string id)
        {
            JobMaster jobMaster = jobMasterService.GetSavedJobMasterDetails(id);
            if (jobMaster != null)
            {
                ApiResponse<JobMaster> apiResponse = new ApiResponse<JobMaster>
                {
                    Valid = true,
                    Result = jobMaster,
                    Message = ""
                };
                return apiResponse;
            }
            return null;
        }

        [HttpPost]
        [Route("InsertJobMaster")]
        public ApiResponse<JobMaster> InsertJob([FromBody] JobMasterViewModel jobMaster)
        {
            ApiResponse<JobMaster> apiResponse = new ApiResponse<JobMaster>();
            var param1 = _mapper.Map<JobMaster>(jobMaster);
            var param2 = _mapper.Map<List<JobMasterBudgetDetails>>(jobMaster.JobMasterBudgetDetails);
            var param3 = _mapper.Map<List<JobMasterJobDetails>>(jobMaster.JobMasterJobDetails);
            var param4 = _mapper.Map<List<JobMasterJobWiseBankGuarantees>>(jobMaster.JobMasterJobWiseBankGuarantees);
            var param5 = _mapper.Map<List<JobStaff>>(jobMaster.JobStaff);
            var param6 = _mapper.Map<List<JobEquipment>>(jobMaster.JobEquipment);
            var param7 = _mapper.Map<List<JobExcutionDetails>>(jobMaster.JobExcutionDetails);
            var respone = jobMasterService.InsertJobMaster(param1, param2, param3, param4, param5, param6, param7);

            apiResponse = new ApiResponse<JobMaster>
            {
                Valid = true,
                Result = respone,
                Message = "Inserted"
            };
            return apiResponse;
        }
        [HttpPost]
        [Route("UpdateJobMaster")]
        public ApiResponse<JobMaster> UpdateJob([FromBody] JobMasterViewModel jobMaster)
        {

            ApiResponse<JobMaster> apiResponse = new ApiResponse<JobMaster>();
            var param1 = _mapper.Map<JobMaster>(jobMaster);
            var param2 = _mapper.Map<List<JobMasterBudgetDetails>>(jobMaster.JobMasterBudgetDetails);
            var param3 = _mapper.Map<List<JobMasterJobDetails>>(jobMaster.JobMasterJobDetails);
            var param4 = _mapper.Map<List<JobMasterJobWiseBankGuarantees>>(jobMaster.JobMasterJobWiseBankGuarantees);

            var xs = jobMasterService.UpdateJobMaster(param1, param2, param3, param4);

            apiResponse = new ApiResponse<JobMaster>
            {
                Valid = true,
                Result = xs,
                Message = "Updated"
            };
            return apiResponse;
        }
        [HttpPost]
        [Route("DeleteJob")]
        public List<JobMasterViewModel> DeleteJob([FromBody] JobMasterViewModel jobMaster)
        {
            var result = _mapper.Map<JobMaster>(jobMaster);
            return jobMasterService.DeleteJob(result).Select(k => new JobMasterViewModel
            {
                JobMasterJobId = k.JobMasterJobId,
                JobMasterJobName = k.JobMasterJobName,
                JobMasterJobDate = k.JobMasterJobDate,
                //JobMasterJobValue = k.JobMasterJobValue,
                //JobMasterJobRetention = k.JobMasterJobRetention,
                //JobMasterJobBalance = k.JobMasterJobBalance,
                //JobMasterJobStatus = k.JobMasterJobStatus,
                //JobMasterJobConsultant = k.JobMasterJobConsultant,
                //JobMasterJobRemarks = k.JobMasterJobRemarks,
                //JobMasterJobCustomerId = k.JobMasterJobCustomerId,
                //JobMasterJobCurrencyId = k.JobMasterJobCurrencyId,
                //JobMasterJobCurVal = k.JobMasterJobCurVal,
                //JobMasterJobOpenInv = k.JobMasterJobOpenInv,
                //JobMasterJobPerInv = k.JobMasterJobPerInv,
                //JobMasterJobConsultantReff = k.JobMasterJobConsultantReff,
                //JobMasterJobStatusVal = k.JobMasterJobStatusVal,
                //JobMasterJobType = k.JobMasterJobType,
                //JobMasterJobRelativeNo = k.JobMasterJobRelativeNo,
                //JobMasterJobCode = k.JobMasterJobCode,
                //JobMasterJobSupplierId = k.JobMasterJobSupplierId,
                //JobMasterJobSalesManId = k.JobMasterJobSalesManId,
                //JobMasterJobNumber = k.JobMasterJobNumber,
                //JobMasterJobIsSubJob = k.JobMasterJobIsSubJob,
                //JobMasterJobRetentionPercentage = k.JobMasterJobRetentionPercentage,
                //JobMasterJobSalesMan = k.JobMasterJobSalesMan,
                //JobMasterJobBudgetMaterialCost = k.JobMasterJobBudgetMaterialCost,
                //JobMasterJobMaterialCost = k.JobMasterJobMaterialCost,
                //JobMasterJobMaterialCostVar = k.JobMasterJobMaterialCostVar,
                //JobMasterJobBugetLabourCost = k.JobMasterJobBugetLabourCost,
                //JobMasterJobLabourCost = k.JobMasterJobLabourCost,
                //JobMasterJobLabourCostVar = k.JobMasterJobLabourCostVar,
                //JobMasterJobBudgetOverHeadCost = k.JobMasterJobBudgetOverHeadCost,
                //JobMasterJobOverHeadCost = k.JobMasterJobOverHeadCost,
                //JobMasterJobOverHeadCostVar = k.JobMasterJobOverHeadCostVar,
                //JobMasterJobMiscCost = k.JobMasterJobMiscCost,
                //JobMasterJobTotalCost = k.JobMasterJobTotalCost,
                //JobMasterJobProfit = k.JobMasterJobProfit,
                //JobMasterJobTotalInvValue = k.JobMasterJobTotalInvValue,
                //JobMasterJobTotalPayValue = k.JobMasterJobTotalPayValue,
                //JobMasterJobTotalRecValue = k.JobMasterJobTotalRecValue,
                //JobMasterJobTotalExpValue = k.JobMasterJobTotalExpValue,
                //JobMasterJobContractorName = k.JobMasterJobContractorName,
                //JobMasterJobContractType = k.JobMasterJobContractType,
                //JobMasterJobCommenceDate = k.JobMasterJobCommenceDate,
                //JobMasterJobWorkingDays = k.JobMasterJobWorkingDays,
                //JobMasterJobOrgCompleteDate = k.JobMasterJobOrgCompleteDate,
                //JobMasterJobPayTermDays = k.JobMasterJobPayTermDays,
                //JobMasterJobAdvanceAmount = k.JobMasterJobAdvanceAmount,
                //JobMasterJobDelStatus = k.JobMasterJobDelStatus


            }).ToList();
        }


        [HttpGet]
        [Route("GetJobCost/{id}")]
        public async Task<IActionResult> GeneratePdf(int id)
        {

            var html = await jobMasterService.GetJobHTML(id);
            var pdfRe = new AddPDFResponse();
            pdfRe.Html = html;
            var pdfBytes = await fileService.GeneratePdf(pdfRe);

            return File(pdfBytes.Result, "application/pdf", "generated.pdf");
        }
        [HttpPost]
        [Route("readpdffile")]
        public async Task<IActionResult> PostPdfFile()
        {
            List<string> uploadedFiles = new List<string>();
            var files = Request.Form.Files;

            if (files.Count == 0)
                return Content("File Not Selected");

            foreach (var file in files)
            {
                Random rn = new Random();
                int count = rn.Next(1111, 9999);
                var fileName = count + file.FileName;

                //string fileExtension = Path.GetExtension(file.FileName);
                //var pathBuilt = Path.Combine(Directory.GetCurrentDirectory() ,"Upload\\Pdfs");

                //if (!Directory.Exists(pathBuilt))
                //{
                //    Directory.CreateDirectory(pathBuilt);
                //}

                var path = Path.Combine(Directory.GetCurrentDirectory(), "Upload/Pdfs/",
                   fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                //JobDocuments jobDocuments = new JobDocuments();
                //jobDocuments.JobDocumentName = "/Upload/Pdfs/" + fileName;
                //jobMasterService.SaveJobDocument(jobDocuments);
            }


            return Ok(true);
        }

        [HttpGet]
        [Route("jobLoadDropdown")]
        public ResponseInfo jobLoadDropdown()
        {
            var objresponse = new ResponseInfo();

            var currencyMasters = currencyrepository.GetAsQueryable().Where(a => a.CurrencyMasterCurrencyDelStatus != true).Select(c => new
            {
                c.CurrencyMasterCurrencyId,
                c.CurrencyMasterCurrencyName,
                c.CurrencyMasterCurrencyRate
            }).ToList();
            var masterAccountsTables = chartofAccountsService.GetAllAccounts().Where(a => a.MaDelStatus != true).Select(c => new
            {
                c.MaAccNo,
                c.MaAccName,
                c.MaRelativeNo,
                c.MaSno
            }).ToList();
            var customerMasters = _customerMasterRepository.GetAsQueryable().Where(k => k.CustomerMasterCustomerDelStatus != true).Select(a => new
            {
                a.CustomerMasterCustomerNo,
                a.CustomerMasterCustomerName,
                a.CustomerMasterCustomerVatNo,
                a.CustomerMasterCustomerContactPerson,
                a.CustomerMasterCustomerAddress,
                a.CustomerMasterCustomerReffAcNo
            }).ToList();
            var SaleManList = salesmanrepository.GetAsQueryable().Where(a => a.SalesManMasterSalesManDelStatus != true).Select(c => new
            {
                c.SalesManMasterSalesManId,
                c.SalesManMasterSalesManName,
            }).ToList();
            var projectDescriptionMaster = ProjectDescriptionrepository.GetAsQueryable().Where(a => a.ProjectDescriptionMasterProjectDescriptionDelStatus != true).Select(c => new
            {
                c.ProjectDescriptionMasterProjectDescriptionId,
                c.ProjectDescriptionMasterProjectDescriptionName
            }).ToList();

            var bankGuaranteeMasters = bankGuaranteerepository.GetAsQueryable().Where(a => a.BankGuaranteeMasterDeleted != true).Select(c => new
            {
                c.BankGuaranteeMasterBgid,
                BankGuaranteeMasterBgname = c.BankGuaranteeMasterBgname.Trim()
            }).ToList();
            objresponse.ResultSet = new
            {
                currencyMasters = currencyMasters,
                masterAccountsTables = masterAccountsTables,
                SaleManList = SaleManList,
                customerMasters = customerMasters,
                bankGuaranteeMasters = bankGuaranteeMasters,
                projectDescriptionMaster = projectDescriptionMaster

            };
            return objresponse;
        }
    }
}
