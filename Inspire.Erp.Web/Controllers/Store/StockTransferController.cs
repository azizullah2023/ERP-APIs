using AutoMapper;
using Inspire.Erp.Application.Common;
using Inspire.Erp.Application.Store.Interface;
using Inspire.Erp.Application.StoreWareHouse.Interface;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Inspire.Erp.Web.Common;
using Inspire.Erp.Web.MODULE;
using Inspire.Erp.Web.ViewModels;
using Inspire.Erp.Web.ViewModels.Store;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.Controllers.Store
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockTransferController : ControllerBase
    {
        private IStockTransferService _st;
        private readonly IMapper _mapper;
        private IRepository<JobMaster> jobrepository; private IRepository<CustomerMaster> _customerMasterRepository; private IRepository<DepartmentMaster> departmentrepository;
        private IRepository<LocationMaster> locationrepository; private IRepository<ItemMaster> itemrepository; private IRepository<UnitMaster> unitrepository;
        public StockTransferController(IStockTransferService st, IMapper mapper, IRepository<JobMaster> _jobrepository, IRepository<LocationMaster> _locationrepository
            , IRepository<CustomerMaster> customerMasterRepository, IRepository<DepartmentMaster> _departmentrepository
            , IRepository<ItemMaster> _itemrepository, IRepository<UnitMaster> _unitrepository)
        {
            _st = st;
            _mapper = mapper;
            jobrepository = _jobrepository; locationrepository = _locationrepository; _customerMasterRepository = customerMasterRepository;
            itemrepository = _itemrepository; unitrepository = _unitrepository; departmentrepository = _departmentrepository;
        }
        /// <summary>
        /// Need to replace SP getStockItemsList by LINQ see from the vb.code and load it according to the reposnse 
        /// </summary>
        /// <returns></returns>
        [HttpGet("getStockItemsList")]
        public async Task<ApiResponse<List<StockItemResponse>>> getStockItemsList()
        {
            try
            {
                List<StockItemResponse> list = await _st.getStockItemResponse();
                ApiResponse<List<StockItemResponse>> response = new ApiResponse<List<StockItemResponse>>()
                {
                    Result = list,
                    Message = "Ok"
                };
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet]
        [Route("GetAllStockTransfer")]
        public ApiResponse<List<StockTransfer>> GetAllStockTransfer()
        {
            ApiResponse<List<StockTransfer>> apiResponse = new ApiResponse<List<StockTransfer>>
            {
                Valid = true,
                Result = _mapper.Map<List<StockTransfer>>(_st.GetStockTransfer()),
                Message = ""
            };
            return apiResponse;
        }
        /// <summary>
        ///  Need to replace SP getStockTransferReport by LINQ see from the vb.code and load it according to the reposnse  
        /// </summary>
        /// <returns></returns>
        //[HttpGet("getStockTransferReport")]
        //public async Task<ApiResponse<List<StockTransferResponseModel>>> getStockTransferReport()
        //{
        //    try
        //    {
        //        ApiResponse<List<StockTransferResponseModel>> response = new ApiResponse<List<StockTransferResponseModel>>()
        //        {
        //            Result = _st.getStockTransferReport().ToList(),
        //            Message = "Ok"
        //        };
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        [HttpPost]
        [Route("InsertStockTransfer")]
        public async Task<ApiResponse<StockTransferViewModel>> InsertStockTransfer([FromBody] StockTransferViewModel stockTranfermodel)
        {
            ApiResponse<StockTransferViewModel> apiResponse = new ApiResponse<StockTransferViewModel>();
            try
            {

                var param1 = _mapper.Map<StockTransfer>(stockTranfermodel);
                var param2 = _mapper.Map<List<StockTransferDetails>>(stockTranfermodel.StockTransferDetails);
                var param3 = new List<AccountsTransactions>();
                List<StockRegister> param4 = new List<StockRegister>();
                // clsAccountAndStock.StockTransfer_Accounts_STOCK_Transactions("", "", param1, param2, ref param4);
                var xs = await _st.InsertStockTransfer(param1, param2, param4);
                StockTransferViewModel openingStockVoucherViewModel = new StockTransferViewModel
                {

                    StockTransferId = xs.stockTransfer.StockTransferId,
                    StockTransferVoucherNo = xs.stockTransfer.StockTransferVoucherNo,
                    StockTransferStDate = xs.stockTransfer.StockTransferStDate,
                    StockTransferLocationIdFrom = xs.stockTransfer.StockTransferLocationIdFrom,
                    StockTransferLocationIdTo = xs.stockTransfer.StockTransferLocationIdTo,
                    StockTransferFSNo = xs.stockTransfer.StockTransferFSNo,
                    StockTransferStatus = xs.stockTransfer.StockTransferStatus,
                    StockTransferUserId = xs.stockTransfer.StockTransferUserId,
                    StockTransferNarration = xs.stockTransfer.StockTransferNarration,
                    StockTransferJobId = xs.stockTransfer.StockTransferJobId,
                    StockTransferApproved = xs.stockTransfer.StockTransferApproved,
                    StockTransferApprovedBy = xs.stockTransfer.StockTransferApprovedBy,
                    StockTransferApprovedDate = xs.stockTransfer.StockTransferApprovedDate,
                    StockTransferDelStatus = xs.stockTransfer.StockTransferDelStatus,

                };

                openingStockVoucherViewModel.StockTransferDetails = _mapper.Map<List<StockTransferDetailsViewModel>>(xs.stockTransferDetails);
                //openingStockVoucherViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionViewModel>>(xs.accountsTransactions);
                apiResponse = new ApiResponse<StockTransferViewModel>
                {
                    Valid = true,
                    Result = _mapper.Map<StockTransferViewModel>(openingStockVoucherViewModel),
                    Message = StockTransferMessage.SaveStockTransfer
                };
            }
            catch (Exception ex)
            {
                apiResponse = new ApiResponse<StockTransferViewModel>
                {
                    Valid = false,
                    Result = null,
                    Message = StockTransferMessage.SaveStockTransfer
                };
            }

            return apiResponse;
        }
        [HttpDelete]
        [Route("DeleteStockTransfers")]
        public IActionResult DeleteStockTransfers(string Id)
        {
            return Ok(_st.DeleteStockTransfers(Id));
        }

        //[HttpPost]
        //[Route("DeleteStockTransfer")]
        //public async Task<bool> DeleteStockTransfer([FromBody] StockTransferViewModel stockTranfermodel)
        //{
        //    try
        //    {
        //        var param1 = _mapper.Map<StockTransfer>(stockTranfermodel);
        //    var param2 = _mapper.Map<List<StockTransferDetails>>(stockTranfermodel.StockTransferDetails);

        //        //==============
        //       var  param3 = new List<AccountsTransactions>();
        //        List<StockRegister> param4 = new List<StockRegister>();

        //        var result= await _st.DeleteStockTransfer(param1, param2
        //       , param4
        //       );
        //    return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        //[HttpPost("deleteStockTransfer")]
        //public async Task<bool> deleteStockTransfer(StockTransferRequestModel obj)
        //{
        //    try
        //    {
        //        await _st.deleteStockTransfer(obj);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //[HttpPost("UpdateStockTransfer")]
        //public async Task<bool> UpdateStockTransfer(StockTransferRequestModel obj)
        //{
        //    try
        //    {
        //        await _st.UpdateStockTransfer(obj);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}




        [HttpPost]
        [Route("UpdateStockTransfer")]
        public async Task<ApiResponse<StockTransferViewModel>> UpdateStockTransfer([FromBody] StockTransferViewModel stockTranfermodel)
        {
            ApiResponse<StockTransferViewModel> apiResponse = new ApiResponse<StockTransferViewModel>();
            try
            {
                var param1 = _mapper.Map<StockTransfer>(stockTranfermodel);
                var param2 = _mapper.Map<List<StockTransferDetails>>(stockTranfermodel.StockTransferDetails);
                // var param3 = _mapper.Map<List<AccountsTransactions>>(stockTranfermodel.AccountsTransactions);
                //List<StockRegister> param4 = new List<StockRegister>();
                //var xs = _issueVoucherService.UpdateIssueVoucher(param1, param3, param2
                //    , param4
                //    );

                //==============
                //param3 = new List<AccountsTransactions>();
                List<StockRegister> param4 = new List<StockRegister>();
                clsAccountAndStock.StockTransfer_Accounts_STOCK_Transactions("", "", param1, param2, ref param4);

                var xs = await _st.UpdateStockTransfer(param1, param2, param4);
                StockTransferViewModel openingStockVoucherViewModel = new StockTransferViewModel
                {

                    StockTransferId = xs.stockTransfer.StockTransferId,
                    StockTransferVoucherNo = xs.stockTransfer.StockTransferVoucherNo,
                    StockTransferStDate = xs.stockTransfer.StockTransferStDate,
                    StockTransferLocationIdFrom = xs.stockTransfer.StockTransferLocationIdFrom,
                    StockTransferLocationIdTo = xs.stockTransfer.StockTransferLocationIdTo,
                    StockTransferFSNo = xs.stockTransfer.StockTransferFSNo,
                    StockTransferStatus = xs.stockTransfer.StockTransferStatus,
                    StockTransferUserId = xs.stockTransfer.StockTransferUserId,
                    StockTransferNarration = xs.stockTransfer.StockTransferNarration,
                    StockTransferJobId = xs.stockTransfer.StockTransferJobId,
                    StockTransferApproved = xs.stockTransfer.StockTransferApproved,
                    StockTransferApprovedBy = xs.stockTransfer.StockTransferApprovedBy,
                    StockTransferApprovedDate = xs.stockTransfer.StockTransferApprovedDate,
                    StockTransferDelStatus = xs.stockTransfer.StockTransferDelStatus,

                };

                openingStockVoucherViewModel.StockTransferDetails = _mapper.Map<List<StockTransferDetailsViewModel>>(xs.stockTransferDetails);
                //openingStockVoucherViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionViewModel>>(xs.accountsTransactions);
                apiResponse = new ApiResponse<StockTransferViewModel>
                {
                    Valid = true,
                    Result = _mapper.Map<StockTransferViewModel>(openingStockVoucherViewModel),
                    Message = StockTransferMessage.SaveStockTransfer
                };
            }
            catch (Exception ex)
            {
                apiResponse = new ApiResponse<StockTransferViewModel>
                {
                    Valid = false,
                    Result = null,
                    Message = StockTransferMessage.UpdateStockTransfer
                };
            }

            return apiResponse;
        }



        [HttpGet]
        [Route("GetSavedStockTransferDetails/{id}")]
        public ApiResponse<StockTransferViewModel> GetSavedStockTransferDetails(string id)
        {
            StockTransferModel stockTransfer = _st.GetSavedStockTransferDetails(id);

            if (stockTransfer.stockTransfer != null)
            {
                StockTransferViewModel stockTransferViewModel = new StockTransferViewModel
                {

                    StockTransferId = stockTransfer.stockTransfer.StockTransferId,
                    StockTransferVoucherNo = stockTransfer.stockTransfer.StockTransferVoucherNo,
                    StockTransferStDate = stockTransfer.stockTransfer.StockTransferStDate,
                    StockTransferLocationIdFrom = stockTransfer.stockTransfer.StockTransferLocationIdFrom,
                    StockTransferLocationIdTo = stockTransfer.stockTransfer.StockTransferLocationIdTo,
                    StockTransferFSNo = stockTransfer.stockTransfer.StockTransferFSNo,
                    StockTransferStatus = stockTransfer.stockTransfer.StockTransferStatus,
                    StockTransferUserId = stockTransfer.stockTransfer.StockTransferUserId,
                    StockTransferNarration = stockTransfer.stockTransfer.StockTransferNarration,
                    StockTransferJobId = stockTransfer.stockTransfer.StockTransferJobId,
                    StockTransferApproved = stockTransfer.stockTransfer.StockTransferApproved,
                    StockTransferApprovedBy = stockTransfer.stockTransfer.StockTransferApprovedBy,
                    StockTransferApprovedDate = stockTransfer.stockTransfer.StockTransferApprovedDate,
                    StockTransferDelStatus = stockTransfer.stockTransfer.StockTransferDelStatus,

                };
                stockTransferViewModel.StockTransferDetails = _mapper.Map<List<StockTransferDetailsViewModel>>(stockTransfer.stockTransferDetails);
                //stockTransferViewModel.AccountsTransactions = _mapper.Map<List<AccountTransactionViewModel>>(openingstockVoucher.accountsTransactions);
                ApiResponse<StockTransferViewModel> apiResponse = new ApiResponse<StockTransferViewModel>
                {
                    Valid = true,
                    Result = stockTransferViewModel,
                    Message = ""
                };
                return apiResponse;
            }
            return null;
        }

        [HttpGet]
        [Route("TransferLoadDropdown")]
        public ResponseInfo TransferLoadDropdown()
        {
            var objresponse = new ResponseInfo();


            var jobMasters = jobrepository.GetAsQueryable().Where(a => a.JobMasterJobDelStatus != true).Select(c => new
            {
                c.JobMasterJobId,
                JobMasterJobName = c.JobMasterJobName.Trim(),
            }).ToList();
            var LocationMaster = locationrepository.GetAsQueryable().Where(a => a.LocationMasterLocationDelStatus != true).Select(c => new
            {
                c.LocationMasterLocationId,
                LocationMasterLocationName = c.LocationMasterLocationName.Trim(),
            }).ToList();

            var customerMasters = _customerMasterRepository.GetAsQueryable().Where(k => k.CustomerMasterCustomerDelStatus != true).Select(a => new
            {
                a.CustomerMasterCustomerNo,
                CustomerMasterCustomerName = a.CustomerMasterCustomerName.Trim(),
                a.CustomerMasterCustomerContactPerson,
                a.CustomerMasterCustomerAddress,
                a.CustomerMasterCustomerReffAcNo
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
            var departmentMasters = departmentrepository.GetAsQueryable().Where(a => a.DepartmentMasterDepartmentDelStatus != true).Select(c => new
            {
                c.DepartmentMasterDepartmentId,
                DepartmentMasterDepartmentName = c.DepartmentMasterDepartmentName.Trim()
            }).ToList();
            objresponse.ResultSet = new
            {
                jobMasters = jobMasters,
                LocationMaster = LocationMaster,
                customerMasters = customerMasters,
                itemMaster = itemMaster,
                unitMasters = unitMasters,
                departmentMasters= departmentMasters
            };
            return objresponse;
        }
    }
}