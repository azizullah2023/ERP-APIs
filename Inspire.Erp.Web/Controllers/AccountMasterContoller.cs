using AutoMapper;
using Inspire.Erp.Application.Master;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Web.Common;
using Inspire.Erp.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Inspire.Erp.Web.Controllers
{
    [Route("api/master")]
    [Produces("application/json")]
    [ApiController]
    public class AccountMasterController : ControllerBase
    {
        private readonly IMapper _mapper;
        private IMasterAccountsTableService MasterAccountsTableService;
        public AccountMasterController(IMasterAccountsTableService _MasterAccountsTableService, IMapper mapper)
        {

            MasterAccountsTableService = _MasterAccountsTableService;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("account/GetAllAccount")]
        public IActionResult GetAllAccount()
        {
            try
            {


                IEnumerable<MasterAccountsTable> listItemnater = MasterAccountsTableService.GetAllAccount();
                var x = _mapper.Map<List<MasterAccountsTableViewModel>>(listItemnater);
                ApiResponse<List<MasterAccountsTableViewModel>> apiResponse = new ApiResponse<List<MasterAccountsTableViewModel>>
                {
                    Valid = true,
                    Result = x,
                    Message = ""
                };

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("account/GetAllGroup")]
        public ApiResponse<List<MasterAccountsTableViewModel>> GetAllGroup()
        {
            IEnumerable<MasterAccountsTable> listItemnater = MasterAccountsTableService.GetAllGroup();
            var x = _mapper.Map<List<MasterAccountsTableViewModel>>(listItemnater);
            ApiResponse<List<MasterAccountsTableViewModel>> apiResponse = new ApiResponse<List<MasterAccountsTableViewModel>>
            {
                Valid = true,
                Result = x,
                Message = ""
            };

            return apiResponse;
        }

        [HttpGet]
        [Route("account/GetAllAccountNotGroup")]
        public ApiResponse<List<MasterAccountsTableViewModel>> GetAllAccountNotGroup()
        {
            IEnumerable<MasterAccountsTable> listItemnater = MasterAccountsTableService.GetAllAccountNotGroup();
            var x = _mapper.Map<List<MasterAccountsTableViewModel>>(listItemnater);
            ApiResponse<List<MasterAccountsTableViewModel>> apiResponse = new ApiResponse<List<MasterAccountsTableViewModel>>
            {
                Valid = true,
                Result = x,
                Message = ""
            };

            return apiResponse;
        }

        //[HttpGet]
        //[Route("GetAllItemStockType")]
        //public ApiResponse<List<ItemStockTypeViewModel>> GetAllItemStockType()
        //{
        //    IEnumerable<ItemStockType> listItemnater = MasterAccountsTableService.GetAllStockType();
        //    var x = _mapper.Map<List<ItemStockTypeViewModel>>(listItemnater);
        //    ApiResponse<List<ItemStockTypeViewModel>> apiResponse = new ApiResponse<List<ItemStockTypeViewModel>>
        //    {
        //        Valid = true,
        //        Result = x,
        //        Message = ""
        //    };

        //    return apiResponse;
        //}

        [HttpGet]
        [Route("account/GetAllAccountById/{id}")]
        public ApiResponse<List<MasterAccountsTableViewModel>> GetAllAccountById(string id)
        {
            IEnumerable<MasterAccountsTable> listItemnater = MasterAccountsTableService.GetAllAccountById(id);

            var x = _mapper.Map<List<MasterAccountsTableViewModel>>(listItemnater);
            ApiResponse<List<MasterAccountsTableViewModel>> apiResponse = new ApiResponse<List<MasterAccountsTableViewModel>>
            {
                Valid = true,
                Result = x,
                Message = ""
            };

            return apiResponse;

        }

        [HttpGet]
        [Route("account/GetAllAccountMasterById/{id}")]
        public ApiResponse<MasterAccountsTableViewModel> GetAllAccountMasterById(string id)
        {
            MasterAccountsTable listItemnater = MasterAccountsTableService.GetAllAccountMasterById(id);

            var x = _mapper.Map<MasterAccountsTableViewModel>(listItemnater);
            ApiResponse<MasterAccountsTableViewModel> apiResponse = new ApiResponse<MasterAccountsTableViewModel>
            {
                Valid = true,
                Result = x,
                Message = ""
            };

            return apiResponse;

        }


        [HttpGet]
        [Route("account/GetAllAccountByName/{id}")]
        public ApiResponse<List<MasterAccountsTableViewModel>> GetAllAccountByName(string id)
        {
            IEnumerable<MasterAccountsTable> listItemnater = MasterAccountsTableService.GetAccountMastersByName(id);

            var x = _mapper.Map<List<MasterAccountsTableViewModel>>(listItemnater);
            ApiResponse<List<MasterAccountsTableViewModel>> apiResponse = new ApiResponse<List<MasterAccountsTableViewModel>>
            {
                Valid = true,
                Result = x,
                Message = ""
            };

            return apiResponse;

        }

        [HttpPost]
        [Route("account/InsertAccount")]
        public ApiResponse<List<MasterAccountsTableViewModel>> InsertAccount([FromBody] MasterAccountsTableViewModel MasterAccountsTable)
        {
            //if (MasterAccountsTable.AccountMasterImageBase64 != null && (MasterAccountsTable.AccountMasterItemId == null || MasterAccountsTable.AccountMasterItemId == 0))
            //{
            //    MasterAccountsTable.ItemImages = new List<ItemImagesViewModel> {
            //                                            new ItemImagesViewModel {
            //                                                 ItemImagesItemImage = Convert.FromBase64String(MasterAccountsTable.AccountMasterImageBase64.Split(',')[1])
            //                                            } };
            //}

            var model = _mapper.Map<MasterAccountsTable>(MasterAccountsTable);

            //var data = (MasterAccountsTable.MaAccNo == null || MasterAccountsTable.MaAccNo == "") ? MasterAccountsTableService.InsertAccount(model) : MasterAccountsTableService.UpdateAccount(model);
            var data = (MasterAccountsTable.MaAccNo == null || MasterAccountsTable.MaAccNo == "") ? MasterAccountsTableService.InsertAccountGroup(model) : MasterAccountsTableService.UpdateAccount(model);
            List<MasterAccountsTableViewModel> result = _mapper.Map<List<MasterAccountsTableViewModel>>(data);
            ApiResponse<List<MasterAccountsTableViewModel>> apiResponse = new ApiResponse<List<MasterAccountsTableViewModel>>
            {
                Valid = true,
                Result = result,
                Message = (MasterAccountsTable.MaAccNo == null || MasterAccountsTable.MaAccNo == "") ? AccountMasterMessage.SaveAccount : AccountMasterMessage.UpdateAccount
            };
            return apiResponse;
        }

        [HttpPost]
        [Route("account/NewAccount")]
        public ApiResponse<MasterAccountsTableViewModel> NewAccount([FromBody] MasterAccountsTableViewModel MasterAccountsTable)
        {
            //if (MasterAccountsTable.AccountMasterImageBase64 != null && (MasterAccountsTable.AccountMasterItemId == null || MasterAccountsTable.AccountMasterItemId == 0))
            //{
            //    MasterAccountsTable.ItemImages = new List<ItemImagesViewModel> {
            //                                            new ItemImagesViewModel {
            //                                                 ItemImagesItemImage = Convert.FromBase64String(MasterAccountsTable.AccountMasterImageBase64.Split(',')[1])
            //                                            } };
            //}

            var model = _mapper.Map<MasterAccountsTable>(MasterAccountsTable);

            //var data = (MasterAccountsTable.MaSno == null || MasterAccountsTable.MaSno == 0) ? MasterAccountsTableService.NewAccount(model) : MasterAccountsTableService.UpdateNewAccount(model);
            var data = (MasterAccountsTable.MaSno == null || MasterAccountsTable.MaSno == 0) ? MasterAccountsTableService.AddNewAccount(model) : MasterAccountsTableService.UpdateNewAccount(model);
            MasterAccountsTableViewModel result = _mapper.Map<MasterAccountsTableViewModel>(data);
            ApiResponse<MasterAccountsTableViewModel> apiResponse = new ApiResponse<MasterAccountsTableViewModel>
            {
                Valid = true,
                Result = result,
                Message = (MasterAccountsTable.MaSno == null || MasterAccountsTable.MaSno == 0) ? AccountMasterMessage.SaveAccount : AccountMasterMessage.UpdateAccount
            };
            return apiResponse;
        }

        [HttpPost]
        [Route("account/DeleteNewAccount")]
        public ApiResponse<MasterAccountsTableViewModel> DeleteNewAccount([FromBody] MasterAccountsTableViewModel MasterAccountsTable)
        {
            ApiResponse<MasterAccountsTableViewModel> apiResponse = null;
            if ((MasterAccountsTable.MaAccNo != null && MasterAccountsTable.MaAccNo != ""))
            {
                MasterAccountsTable.MaDelStatus = true;
                var model = _mapper.Map<MasterAccountsTable>(MasterAccountsTable);

                var data = MasterAccountsTableService.UpdateNewAccount(model);
                MasterAccountsTableViewModel result = _mapper.Map<MasterAccountsTableViewModel>(data);
                apiResponse = new ApiResponse<MasterAccountsTableViewModel>
                {
                    Valid = true,
                    Result = result,
                    Message = (MasterAccountsTable.MaAccNo == null || MasterAccountsTable.MaAccNo == "") ? AccountMasterMessage.SaveAccount : AccountMasterMessage.UpdateAccount
                };
            }
            else
            {
                apiResponse = new ApiResponse<MasterAccountsTableViewModel>
                {
                    Valid = true,
                    Result = null,
                    Message = AccountMasterMessage.DeleteFailed
                };
            }


            return apiResponse;
        }

        [HttpPost]
        [Route("UpdateAccount")]
        public ApiResponse<List<MasterAccountsTableViewModel>> UpdateAccount([FromBody] MasterAccountsTableViewModel MasterAccountsTable)
        {
            //if (MasterAccountsTable.AccountMasterImageBase64 != null)
            //{
            //    MasterAccountsTable.ItemImages = new List<ItemImagesViewModel> {
            //                                            new ItemImagesViewModel {
            //                                                 ItemImagesItemImage = Convert.FromBase64String(MasterAccountsTable.AccountMasterImageBase64.Split(',')[1])
            //                                            } };
            //}

            var model = _mapper.Map<MasterAccountsTable>(MasterAccountsTable);

            var data = MasterAccountsTableService.UpdateAccount(model);
            List<MasterAccountsTableViewModel> result = MasterAccountsTable.MaAccNo == null ? null : _mapper.Map<List<MasterAccountsTableViewModel>>(data);
            ApiResponse<List<MasterAccountsTableViewModel>> apiResponse = new ApiResponse<List<MasterAccountsTableViewModel>>
            {
                Valid = true,
                Result = result,
                Message = ""
            };
            return apiResponse;
        }

        [HttpPost]
        [Route("account/DeleteAccount")]
        public ApiResponse<List<MasterAccountsTableViewModel>> DeleteAccount([FromBody] MasterAccountsTableViewModel MasterAccountsTable)
        {
            ApiResponse<List<MasterAccountsTableViewModel>> apiResponse = null;
            if ((MasterAccountsTable.MaAccNo != null && MasterAccountsTable.MaAccNo != ""))
            {
                MasterAccountsTable.MaDelStatus = true;
                var model = _mapper.Map<MasterAccountsTable>(MasterAccountsTable);

                var data = MasterAccountsTableService.UpdateAccount(model);
                List<MasterAccountsTableViewModel> result = _mapper.Map<List<MasterAccountsTableViewModel>>(data);
                apiResponse = new ApiResponse<List<MasterAccountsTableViewModel>>
                {
                    Valid = true,
                    Result = result,
                    Message = (MasterAccountsTable.MaAccNo == null || MasterAccountsTable.MaAccNo == "") ? AccountMasterMessage.SaveAccount : AccountMasterMessage.UpdateAccount
                };
            }
            else
            {
                apiResponse = new ApiResponse<List<MasterAccountsTableViewModel>>
                {
                    Valid = true,
                    Result = null,
                    Message = AccountMasterMessage.DeleteFailed
                };
            }


            return apiResponse;
        }
    }
}
////{
////    [Route("api/master")]
////    [Produces("application/json")]
////    [ApiController]
////    public class AccountMasterController : ControllerBase
////    {
////        private readonly IMapper _mapper;
////        private IMasterAccountsTableService MasterAccountsTableService;
////        public AccountMasterController(IMasterAccountsTableService _MasterAccountsTableService, IMapper mapper)
////        {

////            MasterAccountsTableService = _MasterAccountsTableService;
////            _mapper = mapper;
////        }
////        [HttpGet]
////        [Route("account/GetAllAccount")]
////        public ApiResponse<List<MasterAccountsTableViewModel>> GetAllAccount()
////        {
////            IEnumerable<MasterAccountsTable> listItemnater = MasterAccountsTableService.GetAllAccount();
////            var x = _mapper.Map<List<MasterAccountsTableViewModel>>(listItemnater);
////            ApiResponse<List<MasterAccountsTableViewModel>> apiResponse = new ApiResponse<List<MasterAccountsTableViewModel>>
////            {
////                Valid = true,
////                Result = x,
////                Message = ""
////            };

////            return apiResponse;
////        }

////        [Route("account/GetAllGroup")]
////        public ApiResponse<List<MasterAccountsTableViewModel>> GetAllGroup()
////        {
////            IEnumerable<MasterAccountsTable> listItemnater = MasterAccountsTableService.GetAllGroup();
////            var x = _mapper.Map<List<MasterAccountsTableViewModel>>(listItemnater);
////            ApiResponse<List<MasterAccountsTableViewModel>> apiResponse = new ApiResponse<List<MasterAccountsTableViewModel>>
////            {
////                Valid = true,
////                Result = x,
////                Message = ""
////            };

////            return apiResponse;
////        }

////        [HttpGet]
////        [Route("account/GetAllAccountNotGroup")]
////        public ApiResponse<List<MasterAccountsTableViewModel>> GetAllAccountNotGroup()
////        {
////            IEnumerable<MasterAccountsTable> listItemnater = MasterAccountsTableService.GetAllAccountNotGroup();
////            var x = _mapper.Map<List<MasterAccountsTableViewModel>>(listItemnater);
////            ApiResponse<List<MasterAccountsTableViewModel>> apiResponse = new ApiResponse<List<MasterAccountsTableViewModel>>
////            {
////                Valid = true,
////                Result = x,
////                Message = ""
////            };

////            return apiResponse;
////        }

////        //[HttpGet]
////        //[Route("GetAllItemStockType")]
////        //public ApiResponse<List<ItemStockTypeViewModel>> GetAllItemStockType()
////        //{
////        //    IEnumerable<ItemStockType> listItemnater = MasterAccountsTableService.GetAllStockType();
////        //    var x = _mapper.Map<List<ItemStockTypeViewModel>>(listItemnater);
////        //    ApiResponse<List<ItemStockTypeViewModel>> apiResponse = new ApiResponse<List<ItemStockTypeViewModel>>
////        //    {
////        //        Valid = true,
////        //        Result = x,
////        //        Message = ""
////        //    };

////        //    return apiResponse;
////        //}

////        [HttpGet("{id}", Name = "GetAllAccountById")]
////        [Route("account/GetAllAccountById/{id}")]
////        public ApiResponse<List<MasterAccountsTableViewModel>> GetAllAccountById(string id)
////        {
////            IEnumerable<MasterAccountsTable> listItemnater = MasterAccountsTableService.GetAllAccountById(id);

////            var x = _mapper.Map<List<MasterAccountsTableViewModel>>(listItemnater);
////            ApiResponse<List<MasterAccountsTableViewModel>> apiResponse = new ApiResponse<List<MasterAccountsTableViewModel>>
////            {
////                Valid = true,
////                Result = x,
////                Message = ""
////            };

////            return apiResponse;

////        }

////        [HttpGet("{id}", Name = "GetAllAccountMasterById")]
////        [Route("account/GetAllAccountMasterById/{id}")]
////        public ApiResponse<MasterAccountsTableViewModel> GetAllAccountMasterById(string id)
////        {
////            MasterAccountsTable listItemnater = MasterAccountsTableService.GetAllAccountMasterById(id);

////            var x = _mapper.Map<MasterAccountsTableViewModel>(listItemnater);
////            ApiResponse<MasterAccountsTableViewModel> apiResponse = new ApiResponse<MasterAccountsTableViewModel>
////            {
////                Valid = true,
////                Result = x,
////                Message = ""
////            };

////            return apiResponse;

////        }


////        [HttpGet("{id}", Name = "GetAllAccountByName")]
////        [Route("account/GetAllAccountByName/{id}")]
////        public ApiResponse<List<MasterAccountsTableViewModel>> GetAllAccountByName(string id)
////        {
////            IEnumerable<MasterAccountsTable> listItemnater = MasterAccountsTableService.GetAccountMastersByName(id);

////            var x = _mapper.Map<List<MasterAccountsTableViewModel>>(listItemnater);
////            ApiResponse<List<MasterAccountsTableViewModel>> apiResponse = new ApiResponse<List<MasterAccountsTableViewModel>>
////            {
////                Valid = true,
////                Result = x,
////                Message = ""
////            };

////            return apiResponse;

////        }

////        [HttpPost]
////        [Route("account/InsertAccount")]
////        public ApiResponse<List<MasterAccountsTableViewModel>> InsertAccount([FromBody] MasterAccountsTableViewModel MasterAccountsTable)
////        {
////            //if (MasterAccountsTable.AccountMasterImageBase64 != null && (MasterAccountsTable.AccountMasterItemId == null || MasterAccountsTable.AccountMasterItemId == 0))
////            //{
////            //    MasterAccountsTable.ItemImages = new List<ItemImagesViewModel> {
////            //                                            new ItemImagesViewModel {
////            //                                                 ItemImagesItemImage = Convert.FromBase64String(MasterAccountsTable.AccountMasterImageBase64.Split(',')[1])
////            //                                            } };
////            //}

////            var model = _mapper.Map<MasterAccountsTable>(MasterAccountsTable);

////            var data = (MasterAccountsTable.MaAccNo == null || MasterAccountsTable.MaAccNo == "") ? MasterAccountsTableService.InsertAccount(model) : MasterAccountsTableService.UpdateAccount(model);
////            List<MasterAccountsTableViewModel> result = _mapper.Map<List<MasterAccountsTableViewModel>>(data);
////            ApiResponse<List<MasterAccountsTableViewModel>> apiResponse = new ApiResponse<List<MasterAccountsTableViewModel>>
////            {
////                Valid = true,
////                Result = result,
////                Message = (MasterAccountsTable.MaAccNo == null || MasterAccountsTable.MaAccNo == "") ? AccountMasterMessage.SaveAccount : AccountMasterMessage.UpdateAccount
////            };
////            return apiResponse;
////        }

////        [HttpPost]
////        [Route("account/NewAccount")]
////        public ApiResponse<MasterAccountsTableViewModel> NewAccount([FromBody] MasterAccountsTableViewModel MasterAccountsTable)
////        {
////            //if (MasterAccountsTable.AccountMasterImageBase64 != null && (MasterAccountsTable.AccountMasterItemId == null || MasterAccountsTable.AccountMasterItemId == 0))
////            //{
////            //    MasterAccountsTable.ItemImages = new List<ItemImagesViewModel> {
////            //                                            new ItemImagesViewModel {
////            //                                                 ItemImagesItemImage = Convert.FromBase64String(MasterAccountsTable.AccountMasterImageBase64.Split(',')[1])
////            //                                            } };
////            //}

////            var model = _mapper.Map<MasterAccountsTable>(MasterAccountsTable);

////            var data = (MasterAccountsTable.MaSno == null || MasterAccountsTable.MaSno == 0) ? MasterAccountsTableService.NewAccount(model) : MasterAccountsTableService.UpdateNewAccount(model);
////            MasterAccountsTableViewModel result = _mapper.Map<MasterAccountsTableViewModel>(data);
////            ApiResponse<MasterAccountsTableViewModel> apiResponse = new ApiResponse<MasterAccountsTableViewModel>
////            {
////                Valid = true,
////                Result = result,
////                Message = (MasterAccountsTable.MaSno == null || MasterAccountsTable.MaSno == 0) ? AccountMasterMessage.SaveAccount : AccountMasterMessage.UpdateAccount
////            };
////            return apiResponse;
////        }

////        [HttpPost]
////        [Route("account/DeleteNewAccount")]
////        public ApiResponse<MasterAccountsTableViewModel> DeleteNewAccount([FromBody] MasterAccountsTableViewModel MasterAccountsTable)
////        {
////            ApiResponse<MasterAccountsTableViewModel> apiResponse = null;
////            if ((MasterAccountsTable.MaAccNo != null && MasterAccountsTable.MaAccNo != ""))
////            {
////                MasterAccountsTable.MaDelStatus = true;
////                var model = _mapper.Map<MasterAccountsTable>(MasterAccountsTable);

////                var data = MasterAccountsTableService.UpdateNewAccount(model);
////                MasterAccountsTableViewModel result = _mapper.Map<MasterAccountsTableViewModel>(data);
////                apiResponse = new ApiResponse<MasterAccountsTableViewModel>
////                {
////                    Valid = true,
////                    Result = result,
////                    Message = (MasterAccountsTable.MaAccNo == null || MasterAccountsTable.MaAccNo == "") ? AccountMasterMessage.SaveAccount : AccountMasterMessage.UpdateAccount
////                };
////            }
////            else
////            {
////                apiResponse = new ApiResponse<MasterAccountsTableViewModel>
////                {
////                    Valid = true,
////                    Result = null,
////                    Message = AccountMasterMessage.DeleteFailed
////                };
////            }


////            return apiResponse;
////        }

////        [HttpPost]
////        [Route("UpdateAccount")]
////        public ApiResponse<List<MasterAccountsTableViewModel>> UpdateAccount([FromBody] MasterAccountsTableViewModel MasterAccountsTable)
////        {
////            //if (MasterAccountsTable.AccountMasterImageBase64 != null)
////            //{
////            //    MasterAccountsTable.ItemImages = new List<ItemImagesViewModel> {
////            //                                            new ItemImagesViewModel {
////            //                                                 ItemImagesItemImage = Convert.FromBase64String(MasterAccountsTable.AccountMasterImageBase64.Split(',')[1])
////            //                                            } };
////            //}

////            var model = _mapper.Map<MasterAccountsTable>(MasterAccountsTable);

////            var data = MasterAccountsTableService.UpdateAccount(model);
////            List<MasterAccountsTableViewModel> result = MasterAccountsTable.MaAccNo == null ? null : _mapper.Map<List<MasterAccountsTableViewModel>>(data);
////            ApiResponse<List<MasterAccountsTableViewModel>> apiResponse = new ApiResponse<List<MasterAccountsTableViewModel>>
////            {
////                Valid = true,
////                Result = result,
////                Message = ""
////            };
////            return apiResponse;
////        }

////        [HttpPost]
////        [Route("account/DeleteAccount")]
////        public ApiResponse<List<MasterAccountsTableViewModel>> DeleteAccount([FromBody] MasterAccountsTableViewModel MasterAccountsTable)
////        {
////            ApiResponse<List<MasterAccountsTableViewModel>> apiResponse = null;
////            if ((MasterAccountsTable.MaAccNo != null && MasterAccountsTable.MaAccNo != ""))
////            {
////                MasterAccountsTable.MaDelStatus = true;
////                var model = _mapper.Map<MasterAccountsTable>(MasterAccountsTable);

////                var data = MasterAccountsTableService.UpdateAccount(model);
////                List<MasterAccountsTableViewModel> result = _mapper.Map<List<MasterAccountsTableViewModel>>(data);
////                apiResponse = new ApiResponse<List<MasterAccountsTableViewModel>>
////                {
////                    Valid = true,
////                    Result = result,
////                    Message = (MasterAccountsTable.MaAccNo == null || MasterAccountsTable.MaAccNo == "") ? AccountMasterMessage.SaveAccount : AccountMasterMessage.UpdateAccount
////                };
////            }
////            else
////            {
////                apiResponse = new ApiResponse<List<MasterAccountsTableViewModel>>
////                {
////                    Valid = true,
////                    Result = null,
////                    Message = AccountMasterMessage.DeleteFailed
////                };
////            }


////            return apiResponse;
////        }
////    }
////}
///

