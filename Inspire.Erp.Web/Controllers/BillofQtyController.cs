using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Inspire.Erp.Application.Sales.Interfaces;
using Inspire.Erp.Application.Common;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Web.Common;
using Inspire.Erp.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Inspire.Erp.Web.ViewModels.Accounts;
using Inspire.Erp.Domain.Modals.Sales;
using Inspire.Erp.Web.ViewModels.Procurement;
using Inspire.Erp.Application.Store.Implementation;
using Inspire.Erp.Domain.Modals.Stock;
using Inspire.Erp.Application.Account;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Microsoft.EntityFrameworkCore;

namespace Inspire.Erp.Web.Controllers
{
    [Route("api")]
    [Produces("application/json")]
    [ApiController]
    public class BillofQtyController : ControllerBase
    {
        private IBillofQtyService _customerQuotationService;
        private readonly IMapper _mapper;
        private IRepository<BillofQtyMaster> _BillofQtyMasterRepository; private IRepository<BillOfQTyDetails> _BillOfQTyDetailsRepository;
        private IRepository<JobMaster> jobrepository; private IRepository<ItemMaster> itemrepository; private IRepository<UnitMaster> unitrepository;
        public BillofQtyController(IBillofQtyService customerQuotationService, IMapper mapper, IRepository<JobMaster> _jobrepository, IRepository<ItemMaster> _itemrepository, IRepository<UnitMaster> _unitrepository
            , IRepository<BillOfQTyDetails> BillOfQTyDetailsRepository, IRepository<BillofQtyMaster> BillofQtyMasterRepository)
        {
            _customerQuotationService = customerQuotationService;
            _mapper = mapper;
            jobrepository = _jobrepository; itemrepository = _itemrepository; unitrepository = _unitrepository;
            _BillofQtyMasterRepository = BillofQtyMasterRepository; _BillOfQTyDetailsRepository = BillOfQTyDetailsRepository;
        }
        [HttpPost]
        [Route("InsertBillofQty")]
        public ApiResponse<BillofQtyMasterModel> InsertBillofQty([FromBody] BillofQtyMasterModel customerQuotationCompositeView)
        {
            ApiResponse<BillofQtyMasterModel> apiResponse = new ApiResponse<BillofQtyMasterModel>();

            try
            {
                var param1 = _mapper.Map<BillofQtyMaster>(customerQuotationCompositeView);
                var param2 = _mapper.Map<List<BillOfQTyDetails>>(customerQuotationCompositeView.BillOfQTyDetails);
                var xs = _customerQuotationService.InsertBillofQty(param1, param2);

                BillofQtyMasterModel CustomerQuotationViewModel = new BillofQtyMasterModel();

                if (xs != null)
                {
                    CustomerQuotationViewModel = _mapper.Map<BillofQtyMasterModel>(xs);
                    CustomerQuotationViewModel.BillOfQTyDetails = _mapper.Map<List<BillOfQTyDetailsModel>>(xs.BillOfQTyDetails);

                    apiResponse = new ApiResponse<BillofQtyMasterModel>
                    {
                        Valid = true,
                        Result = CustomerQuotationViewModel,
                        Message = BillofQtyMessage.SaveBillofQty
                    };
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                apiResponse = new ApiResponse<BillofQtyMasterModel>
                {
                    Valid = false,
                    Error = true,
                    Exception = ex.Message,
                    Message = BillofQtyMessage.BillofQtyError
                };
            }
            return apiResponse;

        }
        [HttpPost]
        [Route("UpdateBillofQty")]
        public ApiResponse<BillofQtyMasterModel> UpdateBillofQty([FromBody] BillofQtyMasterModel customerQuotationCompositeView)
        {
            var param1 = _mapper.Map<BillofQtyMaster>(customerQuotationCompositeView);
            var param2 = _mapper.Map<List<BillOfQTyDetails>>(customerQuotationCompositeView.BillOfQTyDetails);
            var xs = _customerQuotationService.UpdateBillofQty(param1, param2);
            if (xs != null)
            {
                BillofQtyMasterModel CustomerQuotationViewModel = new BillofQtyMasterModel();
                CustomerQuotationViewModel = _mapper.Map<BillofQtyMasterModel>(xs);
                CustomerQuotationViewModel.BillOfQTyDetails = _mapper.Map<List<BillOfQTyDetailsModel>>(xs.BillOfQTyDetails);

                ApiResponse<BillofQtyMasterModel> apiResponse = new ApiResponse<BillofQtyMasterModel>
                {
                    Valid = true,
                    Result = CustomerQuotationViewModel,
                    Message = BillofQtyMessage.UpdateBillofQty
                };
                return apiResponse;
            }
            else
            {
                return null;
            }
        }
        [HttpDelete]
        [Route("DeleteBillofQty/{id}")]
        public ApiResponse<int> DeleteBillofQty(decimal id)
        {
            ApiResponse<int> apiResponse = new ApiResponse<int>
            {
                Valid = true,
                Result = _mapper.Map<int>(_customerQuotationService.DeleteBillofQty(id)),
                Message = ""
            };
            return apiResponse;

        }
        [HttpGet]
        [Route("GetBillofQty")]
        public ApiResponse<List<BillofQtyMaster>> GetBillofQty()
        {
            ApiResponse<List<BillofQtyMaster>> apiResponse = new ApiResponse<List<BillofQtyMaster>>
            {
                Valid = true,
                Result = _mapper.Map<List<BillofQtyMaster>>(_customerQuotationService.GetBillofQty()),
                Message = ""
            };
            return apiResponse;
        }
        [HttpGet]
        [Route("GetSavedBillofQty/{id}")]
        public ApiResponse<BillofQtyMasterModel> GetSavedBillofQty(decimal id)
        {

            ApiResponse<BillofQtyMasterModel> apiResponse = new ApiResponse<BillofQtyMasterModel>
            {
                Valid = true,
                Result = _mapper.Map<BillofQtyMasterModel>(_customerQuotationService.GetSavedBillofQty(id)),
                Message = ""
            };
            return apiResponse;
        }

        [HttpGet]
        [Route("BOQLoadDropdown")]
        public ResponseInfo BOQLoadDropdown()
        {
            var objresponse = new ResponseInfo();


            var jobMasters = jobrepository.GetAsQueryable().Where(a => a.JobMasterJobDelStatus != true).Select(c => new
            {
                c.JobMasterJobId,
                JobMasterJobName = c.JobMasterJobName.Trim(),
            }).ToList();
            var itemMaster = itemrepository.GetAsQueryable().Where(k => k.ItemMasterAccountNo != 0 && (k.ItemMasterDelStatus != true)
                    && k.ItemMasterItemType != ItemMasterStatus.Group).Select(k => new
                    {
                        k.ItemMasterItemId,
                        k.ItemMasterPartNo,
                        ItemMasterItemName = k.ItemMasterItemName.Trim()
                    }).ToList();
            var unitMasters = unitrepository.GetAsQueryable().Where(a => a.UnitMasterUnitDelStatus != true).Select(x => new
            {
                x.UnitMasterUnitId,
                UnitMasterUnitFullName = x.UnitMasterUnitFullName.Trim(),
                UnitMasterUnitShortName = x.UnitMasterUnitShortName.Trim()
            }).ToList();
            objresponse.ResultSet = new
            {
                jobMasters = jobMasters,
                itemMaster = itemMaster,
                unitMasters = unitMasters,
            };
            return objresponse;
        }

        [HttpGet]
        [Route("LoadBOQ")]
        public ResponseInfo LoadBOQ(int customerEnqId)
        {
            var objResponse = new ResponseInfo();
            var detailList = _BillOfQTyDetailsRepository.GetAsQueryable().AsNoTracking().ToList();
            var details = _BillofQtyMasterRepository.GetAsQueryable().AsNoTracking().ToList();

            // Filtering detailList based on PurchaseOrderNos and customerEnqId
            var data = (from d in detailList
                        join c in details on d.BillQtyId equals c.Id
                        where (customerEnqId == -1 || c.CustomerEnqId == customerEnqId)
                        && d.IsEdit == true
                        select new BoqDetailDto
                        {
                            Id = c.Id,
                            dId = d.Id,
                            BillofqtyNo = c.BillofqtyNo,
                            Date = c.Date,
                            CustomerEnqId = c.CustomerEnqId,
                            ItemCode = d.ItemCode,
                            Qty = d.Qty,
                            UnitCode = d.UnitCode
                        }).OrderBy(x => x.dId)
                .ToList();

            objResponse.ResultSet = new
            {
                result = data
            };
            return objResponse;
        }

    }
}
