using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Inspire.Erp.Application.Master;
using Inspire.Erp.Application.Account.Interfaces;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inspire.Erp.Application.Account;
using Inspire.Erp.Web.Common;
using Inspire.Erp.Application.Common;

namespace Inspire.Erp.Web.Controllers
////{
////    [Route("api/account")]
////    [Produces("application/json")]
////    [ApiController]
////    public class ChartofAccountsController : ControllerBase
////    {
////        private readonly IMapper _mapper;
////        private   IChartofAccountsService  chartofAccountsService;
////        public ChartofAccountsController(IChartofAccountsService _chartofAccountsService, IMapper mapper)
////        {

////            chartofAccountsService = _chartofAccountsService;
////            _mapper = mapper;
////        }
////        [HttpGet]
////        [Route("GetAllAccounts")]
////        public ApiResponse<List<ChartofAccountsViewModel>> GetAllAccounts()
////        {
////            IEnumerable<MasterAccountsTable> masterAccountsTables = chartofAccountsService.GetAllAccounts();

////            ApiResponse<List<ChartofAccountsViewModel>> apiResponse = new ApiResponse<List<ChartofAccountsViewModel>>
////            {
////                Valid = true,
////                Result = _mapper.Map<List<ChartofAccountsViewModel>>(masterAccountsTables),
////                Message = ""
////            };
////            return apiResponse;
////        }

////        [HttpGet]
////        [Route("GetDefaultCreditDebitAccounts")]
////        public ApiResponse<DrCrAccounts> GetDefaultCreditDebitAccounts()
////        {
////            DrCrAccounts masterAccountsTables = chartofAccountsService.GetDefaultCreditDebitAccounts();

////            ApiResponse<DrCrAccounts> apiResponse = new ApiResponse<DrCrAccounts>
////            {
////                Valid = true,
////                Result = masterAccountsTables,
////                Message = ""
////            };
////            return apiResponse;
////        }


////        [HttpGet("{id}", Name = "GetAllAccountsById")]
////        [Route("GetAllAccountsById/{id}")]
////        public List<ChartofAccountsViewModel> GetAllAccountsById(int id)
////        {
////            return chartofAccountsService.GetAllAccountsById(id).Select(k => new ChartofAccountsViewModel
////            {

////                MaSno = k.MaSno,
////                MaRelativeNo = k.MaRelativeNo,
////                MaAccNo = k.MaAccNo,
////                MaAccName = k.MaAccName,
////                MaAccType = k.MaAccType,
////                MaMainHead = k.MaMainHead,
////                MaSubHead = k.MaSubHead,
////                MaImageKey = k.MaImageKey,
////                MaSystemAcc = k.MaSystemAcc,
////                MaFsno = k.MaFsno,
////                MaStatus = k.MaStatus,
////                MaUserId = k.MaUserId,
////                MaDateCreated = k.MaDateCreated,
////                MaCurrencyId = k.MaCurrencyId,
////                MaGpAcc = k.MaGpAcc,
////                MaAcAcc = k.MaAcAcc,
////                MaEdAcc = k.MaEdAcc,
////                MaOpenBalance = k.MaOpenBalance,
////                MaTotalDebit = k.MaTotalDebit,
////                MaTotalCredit = k.MaTotalCredit,
////                MaManualCode = k.MaManualCode,
////                MaIsAirAcc = k.MaIsAirAcc,
////                MaIsSeaAcc = k.MaIsSeaAcc,
////                MaCostCenterId = k.MaCostCenterId,
////                //MaCostCenterSub = k.MaCostCenterSub,
////                MaSortNo = k.MaSortNo,
////                MaShowSuminTb = k.MaShowSuminTb,
////                MaAssetValue = k.MaAssetValue,
////                MaAssetDepValue = k.MaAssetDepValue,
////                MaAssetQty = k.MaAssetQty,
////                MaLifeInYrs = k.MaLifeInYrs,
////                MaAssetDepMode = k.MaAssetDepMode,
////                MaAssetDate = k.MaAssetDate,
////                MaIsAsset = k.MaIsAsset,
////                //MaDelStatus = k.MaDelStatus

////            }).ToList();
////        }

////        [HttpPost]
////        [Route("InsertAccounts")]
////        public List<ChartofAccountsViewModel> InsertBrand([FromBody] ChartofAccountsViewModel masterAccountsTable)
////        {
////            var result = _mapper.Map<MasterAccountsTable>(masterAccountsTable);
////            return chartofAccountsService.InsertAccounts(result).Select(k => new ChartofAccountsViewModel
////            {
////                MaSno = k.MaSno,
////                MaRelativeNo = k.MaRelativeNo,
////                MaAccNo = k.MaAccNo,
////                MaAccName = k.MaAccName,
////                MaAccType = k.MaAccType,
////                MaMainHead = k.MaMainHead,
////                MaSubHead = k.MaSubHead,
////                MaImageKey = k.MaImageKey,
////                MaSystemAcc = k.MaSystemAcc,
////                MaFsno = k.MaFsno,
////                MaStatus = k.MaStatus,
////                MaUserId = k.MaUserId,
////                MaDateCreated = k.MaDateCreated,
////                MaCurrencyId = k.MaCurrencyId,
////                MaGpAcc = k.MaGpAcc,
////                MaAcAcc = k.MaAcAcc,
////                MaEdAcc = k.MaEdAcc,
////                MaOpenBalance = k.MaOpenBalance,
////                MaTotalDebit = k.MaTotalDebit,
////                MaTotalCredit = k.MaTotalCredit,
////                MaManualCode = k.MaManualCode,
////                MaIsAirAcc = k.MaIsAirAcc,
////                MaIsSeaAcc = k.MaIsSeaAcc,
////                MaCostCenterId = k.MaCostCenterId,
////                //MaCostCenterSub = k.MaCostCenterSub,
////                MaSortNo = k.MaSortNo,
////                MaShowSuminTb = k.MaShowSuminTb,
////                MaAssetValue = k.MaAssetValue,
////                MaAssetDepValue = k.MaAssetDepValue,
////                MaAssetQty = k.MaAssetQty,
////                MaLifeInYrs = k.MaLifeInYrs,
////                MaAssetDepMode = k.MaAssetDepMode,
////                MaAssetDate = k.MaAssetDate,
////                MaIsAsset = k.MaIsAsset,
////                //MaDelStatus = k.MaDelStatus
////            }).ToList();
////        }

////        [HttpPost]
////        [Route("UpdateAccounts")]
////        public List<ChartofAccountsViewModel> UpdateAccounts([FromBody] ChartofAccountsViewModel masterAccountsTable)
////        {
////            var result = _mapper.Map<MasterAccountsTable>(masterAccountsTable);
////            return chartofAccountsService.UpdateAccounts(result).Select(k => new ChartofAccountsViewModel
////            {
////                MaSno = k.MaSno,
////                MaRelativeNo = k.MaRelativeNo,
////                MaAccNo = k.MaAccNo,
////                MaAccName = k.MaAccName,
////                MaAccType = k.MaAccType,
////                MaMainHead = k.MaMainHead,
////                MaSubHead = k.MaSubHead,
////                MaImageKey = k.MaImageKey,
////                MaSystemAcc = k.MaSystemAcc,
////                MaFsno = k.MaFsno,
////                MaStatus = k.MaStatus,
////                MaUserId = k.MaUserId,
////                MaDateCreated = k.MaDateCreated,
////                MaCurrencyId = k.MaCurrencyId,
////                MaGpAcc = k.MaGpAcc,
////                MaAcAcc = k.MaAcAcc,
////                MaEdAcc = k.MaEdAcc,
////                MaOpenBalance = k.MaOpenBalance,
////                MaTotalDebit = k.MaTotalDebit,
////                MaTotalCredit = k.MaTotalCredit,
////                MaManualCode = k.MaManualCode,
////                MaIsAirAcc = k.MaIsAirAcc,
////                MaIsSeaAcc = k.MaIsSeaAcc,
////                MaCostCenterId = k.MaCostCenterId,
////                //MaCostCenterSub = k.MaCostCenterSub,
////                MaSortNo = k.MaSortNo,
////                MaShowSuminTb = k.MaShowSuminTb,
////                MaAssetValue = k.MaAssetValue,
////                MaAssetDepValue = k.MaAssetDepValue,
////                MaAssetQty = k.MaAssetQty,
////                MaLifeInYrs = k.MaLifeInYrs,
////                MaAssetDepMode = k.MaAssetDepMode,
////                MaAssetDate = k.MaAssetDate,
////                MaIsAsset = k.MaIsAsset,
////                //MaDelStatus = k.MaDelStatus
////            }).ToList();
////        }

////        [HttpPost]
////        [Route("DeleteAccounts")]
////        public List<ChartofAccountsViewModel> DeleteBrand([FromBody] ChartofAccountsViewModel masterAccountsTable)
////        {
////            var result = _mapper.Map<MasterAccountsTable>(masterAccountsTable);
////            return chartofAccountsService.DeleteAccounts(result).Select(k => new ChartofAccountsViewModel
////            {
////                MaSno = k.MaSno,
////                MaRelativeNo = k.MaRelativeNo,
////                MaAccNo = k.MaAccNo,
////                MaAccName = k.MaAccName,
////                MaAccType = k.MaAccType,
////                MaMainHead = k.MaMainHead,
////                MaSubHead = k.MaSubHead,
////                MaImageKey = k.MaImageKey,
////                MaSystemAcc = k.MaSystemAcc,
////                MaFsno = k.MaFsno,
////                MaStatus = k.MaStatus,
////                MaUserId = k.MaUserId,
////                MaDateCreated = k.MaDateCreated,
////                MaCurrencyId = k.MaCurrencyId,
////                MaGpAcc = k.MaGpAcc,
////                MaAcAcc = k.MaAcAcc,
////                MaEdAcc = k.MaEdAcc,
////                MaOpenBalance = k.MaOpenBalance,
////                MaTotalDebit = k.MaTotalDebit,
////                MaTotalCredit = k.MaTotalCredit,
////                MaManualCode = k.MaManualCode,
////                MaCostCenterId = k.MaCostCenterId,
////                MaShowSuminTb = k.MaShowSuminTb,
////                MaAssetValue = k.MaAssetValue,
////                MaAssetDepValue = k.MaAssetDepValue,
////                MaAssetQty = k.MaAssetQty,
////                MaLifeInYrs = k.MaLifeInYrs,
////                MaAssetDepMode = k.MaAssetDepMode,
////                MaAssetDate = k.MaAssetDate,
////                MaIsAsset = k.MaIsAsset,
////                MaIsSeaAcc = k.MaIsSeaAcc,
////                MaIsAirAcc = k.MaIsAirAcc,
////                //MaDelStatus = k.MaDelStatus
////            }).ToList();
////        }
////    }
////}
///

{
    [Route("api/account")]
    [Produces("application/json")]
    [ApiController]
    public class ChartofAccountsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private IChartofAccountsService chartofAccountsService;
        public ChartofAccountsController(IChartofAccountsService _chartofAccountsService, IMapper mapper)
        {

            chartofAccountsService = _chartofAccountsService;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("GetAllAccounts")]
        public ApiResponse<List<ChartofAccountsViewModel>> GetAllAccounts()
        {
            try
            {
                IEnumerable<MasterAccountsTable> masterAccountsTables = chartofAccountsService.GetAllAccounts();
                ApiResponse<List<ChartofAccountsViewModel>> apiResponse = new ApiResponse<List<ChartofAccountsViewModel>>
                {
                    Valid = true,
                    Result = _mapper.Map<List<ChartofAccountsViewModel>>(masterAccountsTables),
                    Message = ""
                };
                return apiResponse;
            }
            catch (Exception ex)
            {

                throw ex;
            }



        }

        [HttpGet]
        [Route("GetAllBankAccounts")]
        public ApiResponse<List<ChartofAccountsViewModel>> GetAllBankAccounts()
        {
            try
            {
                IEnumerable<MasterAccountsTable> masterAccountsTables = chartofAccountsService.GetAllBankAccounts();
                ApiResponse<List<ChartofAccountsViewModel>> apiResponse = new ApiResponse<List<ChartofAccountsViewModel>>
                {
                    Valid = true,
                    Result = _mapper.Map<List<ChartofAccountsViewModel>>(masterAccountsTables),
                    Message = ""
                };
                return apiResponse;
            }
            catch (Exception ex)
            {

                throw ex;
            }



        }


        [HttpGet]
        [Route("GetDefaultCreditDebitAccounts")]
        public ApiResponse<DrCrAccounts> GetDefaultCreditDebitAccounts()
        {
            DrCrAccounts masterAccountsTables = chartofAccountsService.GetDefaultCreditDebitAccounts();

            ApiResponse<DrCrAccounts> apiResponse = new ApiResponse<DrCrAccounts>
            {
                Valid = true,
                Result = masterAccountsTables,
                Message = ""
            };
            return apiResponse;
        }


        [HttpGet]
        [Route("GetAllAccountsById/{id}")]
        public List<ChartofAccountsViewModel> GetAllAccountsById(int id)
        {
            return chartofAccountsService.GetAllAccountsById(id).Select(k => new ChartofAccountsViewModel
            {

                MaSno = k.MaSno,
                MaRelativeNo = k.MaRelativeNo,
                MaAccNo = k.MaAccNo,
                MaAccName = k.MaAccName,
                MaAccType = k.MaAccType,
                MaMainHead = k.MaMainHead,
                MaSubHead = k.MaSubHead,
                MaImageKey = k.MaImageKey,
                MaSystemAcc = k.MaSystemAcc,
                MaFsno = k.MaFsno,
                MaStatus = k.MaStatus,
                MaUserId = k.MaUserId,
                MaDateCreated = k.MaDateCreated,
                MaCurrencyId = k.MaCurrencyId,
                MaGpAcc = k.MaGpAcc,
                MaAcAcc = k.MaAcAcc,
                MaEdAcc = k.MaEdAcc,
                MaOpenBalance = k.MaOpenBalance,
                MaTotalDebit = k.MaTotalDebit,
                MaTotalCredit = k.MaTotalCredit,
                MaManualCode = k.MaManualCode,
                MaIsAirAcc = k.MaIsAirAcc,
                MaIsSeaAcc = k.MaIsSeaAcc,
                MaCostCenterId = k.MaCostCenterId,
                //MaCostCenterSub = k.MaCostCenterSub,
                MaSortNo = k.MaSortNo,
                MaShowSuminTb = k.MaShowSuminTb,
                MaAssetValue = k.MaAssetValue,
                MaAssetDepValue = k.MaAssetDepValue,
                MaAssetQty = k.MaAssetQty,
                MaLifeInYrs = k.MaLifeInYrs,
                MaAssetDepMode = k.MaAssetDepMode,
                MaAssetDate = k.MaAssetDate,
                MaIsAsset = k.MaIsAsset,
                //MaDelStatus = k.MaDelStatus

            }).ToList();
        }

        [HttpPost]
        [Route("InsertAccounts")]
        public List<ChartofAccountsViewModel> InsertBrand([FromBody] ChartofAccountsViewModel masterAccountsTable)
        {
            var result = _mapper.Map<MasterAccountsTable>(masterAccountsTable);
            return chartofAccountsService.InsertAccounts(result).Select(k => new ChartofAccountsViewModel
            {
                MaSno = k.MaSno,
                MaRelativeNo = k.MaRelativeNo,
                MaAccNo = k.MaAccNo,
                MaAccName = k.MaAccName,
                MaAccType = k.MaAccType,
                MaMainHead = k.MaMainHead,
                MaSubHead = k.MaSubHead,
                MaImageKey = k.MaImageKey,
                MaSystemAcc = k.MaSystemAcc,
                MaFsno = k.MaFsno,
                MaStatus = k.MaStatus,
                MaUserId = k.MaUserId,
                MaDateCreated = k.MaDateCreated,
                MaCurrencyId = k.MaCurrencyId,
                MaGpAcc = k.MaGpAcc,
                MaAcAcc = k.MaAcAcc,
                MaEdAcc = k.MaEdAcc,
                MaOpenBalance = k.MaOpenBalance,
                MaTotalDebit = k.MaTotalDebit,
                MaTotalCredit = k.MaTotalCredit,
                MaManualCode = k.MaManualCode,
                MaIsAirAcc = k.MaIsAirAcc,
                MaIsSeaAcc = k.MaIsSeaAcc,
                MaCostCenterId = k.MaCostCenterId,
                //MaCostCenterSub = k.MaCostCenterSub,
                MaSortNo = k.MaSortNo,
                MaShowSuminTb = k.MaShowSuminTb,
                MaAssetValue = k.MaAssetValue,
                MaAssetDepValue = k.MaAssetDepValue,
                MaAssetQty = k.MaAssetQty,
                MaLifeInYrs = k.MaLifeInYrs,
                MaAssetDepMode = k.MaAssetDepMode,
                MaAssetDate = k.MaAssetDate,
                MaIsAsset = k.MaIsAsset,
                //MaDelStatus = k.MaDelStatus
            }).ToList();
        }

        [HttpPost]
        [Route("UpdateAccounts")]
        public List<ChartofAccountsViewModel> UpdateAccounts([FromBody] ChartofAccountsViewModel masterAccountsTable)
        {
            var result = _mapper.Map<MasterAccountsTable>(masterAccountsTable);
            return chartofAccountsService.UpdateAccounts(result).Select(k => new ChartofAccountsViewModel
            {
                MaSno = k.MaSno,
                MaRelativeNo = k.MaRelativeNo,
                MaAccNo = k.MaAccNo,
                MaAccName = k.MaAccName,
                MaAccType = k.MaAccType,
                MaMainHead = k.MaMainHead,
                MaSubHead = k.MaSubHead,
                MaImageKey = k.MaImageKey,
                MaSystemAcc = k.MaSystemAcc,
                MaFsno = k.MaFsno,
                MaStatus = k.MaStatus,
                MaUserId = k.MaUserId,
                MaDateCreated = k.MaDateCreated,
                MaCurrencyId = k.MaCurrencyId,
                MaGpAcc = k.MaGpAcc,
                MaAcAcc = k.MaAcAcc,
                MaEdAcc = k.MaEdAcc,
                MaOpenBalance = k.MaOpenBalance,
                MaTotalDebit = k.MaTotalDebit,
                MaTotalCredit = k.MaTotalCredit,
                MaManualCode = k.MaManualCode,
                MaIsAirAcc = k.MaIsAirAcc,
                MaIsSeaAcc = k.MaIsSeaAcc,
                MaCostCenterId = k.MaCostCenterId,
                //MaCostCenterSub = k.MaCostCenterSub,
                MaSortNo = k.MaSortNo,
                MaShowSuminTb = k.MaShowSuminTb,
                MaAssetValue = k.MaAssetValue,
                MaAssetDepValue = k.MaAssetDepValue,
                MaAssetQty = k.MaAssetQty,
                MaLifeInYrs = k.MaLifeInYrs,
                MaAssetDepMode = k.MaAssetDepMode,
                MaAssetDate = k.MaAssetDate,
                MaIsAsset = k.MaIsAsset,
                //MaDelStatus = k.MaDelStatus
            }).ToList();
        }

        [HttpPost]
        [Route("DeleteAccounts")]
        public List<ChartofAccountsViewModel> DeleteBrand([FromBody] ChartofAccountsViewModel masterAccountsTable)
        {
            var result = _mapper.Map<MasterAccountsTable>(masterAccountsTable);
            return chartofAccountsService.DeleteAccounts(result).Select(k => new ChartofAccountsViewModel
            {
                MaSno = k.MaSno,
                MaRelativeNo = k.MaRelativeNo,
                MaAccNo = k.MaAccNo,
                MaAccName = k.MaAccName,
                MaAccType = k.MaAccType,
                MaMainHead = k.MaMainHead,
                MaSubHead = k.MaSubHead,
                MaImageKey = k.MaImageKey,
                MaSystemAcc = k.MaSystemAcc,
                MaFsno = k.MaFsno,
                MaStatus = k.MaStatus,
                MaUserId = k.MaUserId,
                MaDateCreated = k.MaDateCreated,
                MaCurrencyId = k.MaCurrencyId,
                MaGpAcc = k.MaGpAcc,
                MaAcAcc = k.MaAcAcc,
                MaEdAcc = k.MaEdAcc,
                MaOpenBalance = k.MaOpenBalance,
                MaTotalDebit = k.MaTotalDebit,
                MaTotalCredit = k.MaTotalCredit,
                MaManualCode = k.MaManualCode,
                MaCostCenterId = k.MaCostCenterId,
                MaShowSuminTb = k.MaShowSuminTb,
                MaAssetValue = k.MaAssetValue,
                MaAssetDepValue = k.MaAssetDepValue,
                MaAssetQty = k.MaAssetQty,
                MaLifeInYrs = k.MaLifeInYrs,
                MaAssetDepMode = k.MaAssetDepMode,
                MaAssetDate = k.MaAssetDate,
                MaIsAsset = k.MaIsAsset,
                MaIsSeaAcc = k.MaIsSeaAcc,
                MaIsAirAcc = k.MaIsAirAcc,
                //MaDelStatus = k.MaDelStatus
            }).ToList();
        }
    }
}

