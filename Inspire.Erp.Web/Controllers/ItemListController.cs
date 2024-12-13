using AutoMapper;
using Inspire.Erp.Application.Master;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Web.Common;
using Inspire.Erp.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NPOI.POIFS.Crypt.Dsig;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Inspire.Erp.Web.Controllers
{
    [Route("api/ItemList")]
    [Produces("application/json")]
    [ApiController]
    public class ItemListController : ControllerBase
    {
        private readonly IMapper _mapper;
        private IItemListService IItemListService;
        public ItemListController(IItemListService _IItemListService, IMapper mapper)
        {

            IItemListService = _IItemListService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("GetAllItemListSearch")]
        public ApiResponse<List<Application.Master.ItemListViewModel>> GetAllItemListSearch(Application.Master.ItemFilterModel data)
        {
            IEnumerable<Application.Master.ItemListViewModel> listItemnater = IItemListService.GetAllItemListSearch(data);

            var x = _mapper.Map<List<Application.Master.ItemListViewModel>>(listItemnater);
            ApiResponse<List<Application.Master.ItemListViewModel>> apiResponse = new ApiResponse<List<Application.Master.ItemListViewModel>>
            {
                Valid = true,
                Result = x,
                Message = ""
            };
            return apiResponse;
        }
        [HttpPost]
        [Route("UpdateRateItemList")]
       public ApiResponse<List<Application.Master.ItemListViewModel>> UpdateRateItemList(List<RateItemListrModel> dates)
        {
             var UpdateJobInvoiceViewModel = IItemListService.UpdateRateItemList(dates);
            var x = _mapper.Map<List<Application.Master.ItemListViewModel>>(UpdateJobInvoiceViewModel);


            ApiResponse<List<Application.Master.ItemListViewModel>> apiResponse = new ApiResponse<List<Application.Master.ItemListViewModel>>

            {
                Valid = true,
                Result =x,
                Message = "Rate has been updated."

            };
            return apiResponse;

        }
    }
    
}