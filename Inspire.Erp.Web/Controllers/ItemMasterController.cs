using AutoMapper;
using Inspire.Erp.Application.Master;
using Inspire.Erp.Application.Master.Interfaces;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Inspire.Erp.Web.Common;
using Inspire.Erp.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.Controllers
{
    [Route("api/master/item")]
    [Produces("application/json")]
    //[Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class ItemMasterController : ControllerBase
    {
        private readonly IMapper _mapper;
        private IItemMasterService itemMasterService;
        private ILocationMasterService locationMasterService;


        private IRepository<ItemMaster> itemrepository; private IRepository<VendorMaster> Brandrepository; private IRepository<ItemStockType> itemStockRepository;
        private readonly IRepository<SuppliersMaster> supplierrepository; private IRepository<TaxMaster> taxrepository; private IRepository<PriceLevelMaster> priceLevelrepository;
        private IRepository<UnitMaster> unitrepository; private IRepository<UnitDetails> _unitDetailsRepo;

        public ItemMasterController(IRepository<ItemMaster> _itemrepository, IRepository<VendorMaster> _Brandrepository, IRepository<ItemStockType> _itemStockRepository,
            IRepository<SuppliersMaster> _supplierrepository, IRepository<TaxMaster> _Taxrepository, IRepository<PriceLevelMaster> _priceLevelrepository,
            IRepository<UnitMaster> _unitrepository, IRepository<UnitDetails> unitDetailsRepo,
            IItemMasterService _itemMasterService, ILocationMasterService _locationMasterService, IMapper mapper)
        {
            itemMasterService = _itemMasterService;
            locationMasterService = _locationMasterService;
            _mapper = mapper; _unitDetailsRepo = unitDetailsRepo;
            Brandrepository = _Brandrepository; itemStockRepository = _itemStockRepository; supplierrepository = _supplierrepository;
            itemrepository = _itemrepository; taxrepository = _Taxrepository; priceLevelrepository = _priceLevelrepository; unitrepository = _unitrepository;
        }
        [HttpGet]
        [Route("GetAllItem")]
        public ApiResponse<List<ItemMasterViewModel>> GetAllItem()
        {
            List<ItemMaster> listItemnater = itemMasterService.GetAllItem();
            var x = _mapper.Map<List<ItemMasterViewModel>>(listItemnater);
            ApiResponse<List<ItemMasterViewModel>> apiResponse = new ApiResponse<List<ItemMasterViewModel>>
            {
                Valid = true,
                Result = x,
                Message = ""
            };
            return apiResponse;
        }

        [HttpGet]
        [Route("GetAllItemMaster")]
        public ApiResponse<List<ItemMasterViewModel>> GetAllItemMaster()
        {
            IEnumerable<ItemMaster> listItemnater = itemMasterService.GetAllItemMaster();
            var x = _mapper.Map<List<ItemMasterViewModel>>(listItemnater);
            ApiResponse<List<ItemMasterViewModel>> apiResponse = new ApiResponse<List<ItemMasterViewModel>>
            {
                Valid = true,
                Result = x,
                Message = ""
            };

            return apiResponse;
        }
        [HttpGet]
        [Route("GetAllItemGroupAndSubGroup")]
        public ApiResponse<List<ItemMasterViewModel>> GetAllItemGroupAndSubGroup()
        {
            IEnumerable<ItemMaster> listItemnater = itemMasterService.GetAllItemGroupAndSubGroup();
            var x = _mapper.Map<List<ItemMasterViewModel>>(listItemnater);
            ApiResponse<List<ItemMasterViewModel>> apiResponse = new ApiResponse<List<ItemMasterViewModel>>
            {
                Valid = true,
                Result = x,
                Message = ""
            };
            return apiResponse;
        }
        [HttpGet]
        [Route("itemmax")]
        public ApiResponse<int> ItemMax()
        {
            int max = itemMasterService.ItemMasterIdMax();
            ApiResponse<int> apiResponse = new ApiResponse<int>
            {
                Valid = true,
                Result = max,
                Message = ""
            };

            return apiResponse;
        }
        [HttpGet]
        [Route("GetAllItemNotGroup")]
        public ApiResponse<List<ItemMasterViewModel>> GetAllItemNotGroup()
        {
            IEnumerable<ItemMaster> listItemnater = itemMasterService.GetAllItemNotGroup();
            var x = _mapper.Map<List<ItemMasterViewModel>>(listItemnater);
            ApiResponse<List<ItemMasterViewModel>> apiResponse = new ApiResponse<List<ItemMasterViewModel>>
            {
                Valid = true,
                Result = x,
                Message = ""
            };
            return apiResponse;
        }
        [HttpGet]
        [Route("GetAllItemStockType")]
        public ApiResponse<List<ItemStockTypeViewModel>> GetAllItemStockType()
        {
            IEnumerable<ItemStockType> listItemnater = itemMasterService.GetAllStockType();
            var x = _mapper.Map<List<ItemStockTypeViewModel>>(listItemnater);
            ApiResponse<List<ItemStockTypeViewModel>> apiResponse = new ApiResponse<List<ItemStockTypeViewModel>>
            {
                Valid = true,
                Result = x,
                Message = ""
            };
            return apiResponse;
        }
        [HttpGet]
        [Route("GetAllItemById/{id}")]
        public ApiResponse<List<ItemMasterViewModel>> GetAllItemById(int id)
        {
            IEnumerable<ItemMaster> listItemnater = itemMasterService.GetAllItemById(id);

            var x = _mapper.Map<List<ItemMasterViewModel>>(listItemnater);
            ApiResponse<List<ItemMasterViewModel>> apiResponse = new ApiResponse<List<ItemMasterViewModel>>
            {
                Valid = true,
                Result = x,
                Message = ""
            };

            return apiResponse;

        }
        [HttpGet]
        [Route("GetAllItemMasterById/{id}")]
        public ApiResponse<ItemMasterViewModel> GetAllItemMasterById(int id)
        {
            ItemMaster listItemnater = itemMasterService.GetAllItemMasterById(id);

            var x = _mapper.Map<ItemMasterViewModel>(listItemnater);
            ApiResponse<ItemMasterViewModel> apiResponse = new ApiResponse<ItemMasterViewModel>
            {
                Valid = true,
                Result = x,
                Message = ""
            };

            return apiResponse;

        }
        [HttpGet]
        [Route("GetAllItemMasterByBarCode/{barCode}")]
        public ApiResponse<ItemMasterViewModel> GetAllItemMasterByBarCode(string barCode)
        {
            ItemMaster listItemnater = itemMasterService.GetAllItemMasterByBarCode(barCode);
            var x = _mapper.Map<ItemMasterViewModel>(listItemnater);
            ApiResponse<ItemMasterViewModel> apiResponse = new ApiResponse<ItemMasterViewModel>
            {
                Valid = true,
                Result = x,
                Message = ""
            };
            return apiResponse;
        }
        [HttpGet]
        [Route("SearchItemByBarcodeLink/{barCode}")]
        public async Task<IActionResult> SearchItemByBarcodeLink(ItemBarCodeSearchFilter model)
        {
            return Ok(await itemMasterService.SearchItemByBarcode(model));

        }
        [HttpGet("GetItemsBySearch")]
        public async Task<IActionResult> GetItemsBySearch(string query)
        {
            return Ok(await itemMasterService.GetItemsBySearch(query));

        }
        [HttpGet]
        [Route("GetAllItemByName/{id}")]
        public ApiResponse<List<ItemMasterViewModel>> GetAllItemByName(string id)
        {
            IEnumerable<ItemMaster> listItemnater = itemMasterService.GetItemMastersByName(id);

            var x = _mapper.Map<List<ItemMasterViewModel>>(listItemnater);
            ApiResponse<List<ItemMasterViewModel>> apiResponse = new ApiResponse<List<ItemMasterViewModel>>
            {
                Valid = true,
                Result = x,
                Message = ""
            };

            return apiResponse;

        }
        [HttpPost]
        [Route("GetAllItemSearchFilter")]
        public ApiResponse<List<ItemMasterViewModel>> GetAllItemSearchFilter(ItemFilterViewModel data)
        {
            IEnumerable<ItemMaster> listItemnater = itemMasterService.GetAllItemSearchFilter(data.ItemGroup, data.ItemName);

            var x = _mapper.Map<List<ItemMasterViewModel>>(listItemnater);
            ApiResponse<List<ItemMasterViewModel>> apiResponse = new ApiResponse<List<ItemMasterViewModel>>
            {
                Valid = true,
                Result = x,
                Message = ""
            };

            return apiResponse;

        }
        [HttpPost]
        [Route("InsertItem")]
        public async Task<ApiResponse<List<ItemMasterViewModel>>> InsertItem([FromBody] ItemMasterViewModel itemMaster)
        {
            if (itemMaster.ItemMasterImageBase64 != null && (itemMaster.ItemMasterItemId == null || itemMaster.ItemMasterItemId == 0))
            {
                itemMaster.ItemImages = new List<ItemImagesViewModel> {
                                                        new ItemImagesViewModel {
                                                             ItemImagesItemImage = Convert.FromBase64String(itemMaster.ItemMasterImageBase64.Split(',')[1])
                                                        } };
            }

            var model = _mapper.Map<ItemMaster>(itemMaster);
            var data = (itemMaster.ItemMasterItemId == null || itemMaster.ItemMasterItemId == 0) ? await itemMasterService.InsertItem(model) : itemMasterService.UpdateItem(model);
            List<ItemMasterViewModel> result = _mapper.Map<List<ItemMasterViewModel>>(data);
            ApiResponse<List<ItemMasterViewModel>> apiResponse = new ApiResponse<List<ItemMasterViewModel>>
            {
                Valid = true,
                Result = result,
                Message = (itemMaster.ItemMasterItemId == null || itemMaster.ItemMasterItemId == 0) ? ItemMasterMessage.SaveItem : ItemMasterMessage.UpdateItem
            };
            return apiResponse;
        }

        [HttpPost]
        [Route("NewItem")]
        public async Task<ApiResponse<ItemMasterViewModel>> NewItem([FromBody] ItemMasterViewModel itemMaster)
        {
            if (itemMaster.ItemMasterImageBase64 != null && (itemMaster.ItemMasterItemId == null || itemMaster.ItemMasterItemId == 0))
            {
                itemMaster.ItemImages = new List<ItemImagesViewModel> {
                                                        new ItemImagesViewModel {
                                                             ItemImagesItemImage = Convert.FromBase64String(itemMaster.ItemMasterImageBase64.Split(',')[1])
                                                        } };
            }

            var model = _mapper.Map<ItemMaster>(itemMaster);

            var data = (itemMaster.ItemMasterItemId == null || itemMaster.ItemMasterItemId == 0) ? await itemMasterService.NewItem(model) : itemMasterService.UpdateNewItem(model);
            ItemMasterViewModel result = _mapper.Map<ItemMasterViewModel>(data);
            ApiResponse<ItemMasterViewModel> apiResponse = new ApiResponse<ItemMasterViewModel>
            {
                Valid = true,
                Result = result,
                Message = (itemMaster.ItemMasterItemId == null || itemMaster.ItemMasterItemId == 0) ? ItemMasterMessage.SaveItem : ItemMasterMessage.UpdateItem
            };
            return apiResponse;
        }
        [HttpPost]
        [Route("DeleteNewItem")]
        public ApiResponse<ItemMasterViewModel> DeleteNewItem([FromBody] ItemMasterViewModel itemMaster)
        {
            ApiResponse<ItemMasterViewModel> apiResponse = null;
            if ((itemMaster.ItemMasterItemId != null && itemMaster.ItemMasterItemId != 0))
            {
                itemMaster.ItemMasterDelStatus = true;
                var model = _mapper.Map<ItemMaster>(itemMaster);

                var data = itemMasterService.UpdateNewItem(model);
                ItemMasterViewModel result = _mapper.Map<ItemMasterViewModel>(data);
                apiResponse = new ApiResponse<ItemMasterViewModel>
                {
                    Valid = true,
                    Result = result,
                    Message = ItemMasterMessage.DeleteFaileds
                };
            }
            else
            {
                apiResponse = new ApiResponse<ItemMasterViewModel>
                {
                    Valid = true,
                    Result = null,
                    Message = ItemMasterMessage.DeleteFailed
                };
            }


            return apiResponse;
        }
        [HttpPost]
        [Route("UpdateItem")]
        public ApiResponse<List<ItemMasterViewModel>> UpdateBrand([FromBody] ItemMasterViewModel itemMaster)
        {
            if (itemMaster.ItemMasterImageBase64 != null)
            {
                itemMaster.ItemImages = new List<ItemImagesViewModel> {
                                                        new ItemImagesViewModel {
                                                             ItemImagesItemImage = Convert.FromBase64String(itemMaster.ItemMasterImageBase64.Split(',')[1])
                                                        } };
            }

            var model = _mapper.Map<ItemMaster>(itemMaster);

            var data = itemMasterService.UpdateItem(model);
            List<ItemMasterViewModel> result = itemMaster.ItemMasterItemType == null ? null : _mapper.Map<List<ItemMasterViewModel>>(data);
            ApiResponse<List<ItemMasterViewModel>> apiResponse = new ApiResponse<List<ItemMasterViewModel>>
            {
                Valid = true,
                Result = result,
                Message = ""
            };
            return apiResponse;
        }
        [HttpPost]
        [Route("DeleteItem")]
        public ApiResponse<List<ItemMasterViewModel>> DeleteItem([FromBody] ItemMasterViewModel itemMaster)
        {
            ApiResponse<List<ItemMasterViewModel>> apiResponse = null;
            if ((itemMaster.ItemMasterItemId != null && itemMaster.ItemMasterItemId != 0))
            {
                itemMaster.ItemMasterDelStatus = true;
                var model = _mapper.Map<ItemMaster>(itemMaster);

                var data = itemMasterService.UpdateItem(model);
                List<ItemMasterViewModel> result = _mapper.Map<List<ItemMasterViewModel>>(data);
                apiResponse = new ApiResponse<List<ItemMasterViewModel>>
                {
                    Valid = true,
                    Result = result,
                    Message = (itemMaster.ItemMasterItemId == null || itemMaster.ItemMasterItemId == 0) ? ItemMasterMessage.SaveItem : ItemMasterMessage.UpdateItem
                };
            }
            else
            {
                apiResponse = new ApiResponse<List<ItemMasterViewModel>>
                {
                    Valid = true,
                    Result = null,
                    Message = ItemMasterMessage.DeleteFailed
                };
            }


            return apiResponse;
        }

        [HttpGet]
        [Route("GetAveragePrice")]
        public async Task<IActionResult> GetAveragePrice(int id)
        {
            return Ok(await itemMasterService.GetAveragePrice(id));
        }


        [HttpGet]
        [Route("LoadDropdown")]
        public ResponseInfo LoadDropdown()
        {
            var objectresponse = new ResponseInfo();
            var itemMaster = itemrepository.GetAsQueryable().Where(k => k.ItemMasterAccountNo == 0 && (k.ItemMasterDelStatus != true)
                     && k.ItemMasterItemType == ItemMasterStatus.Group).Select(k => new
                     {
                         k.ItemMasterItemId,
                         k.ItemMasterItemName
                     }).ToList();

            var VendorMasters = Brandrepository.GetAsQueryable().Where(c => c.VendorMasterVendorDelStatus != true).Select(a => new
            {
                a.VendorMasterVendorId,
                a.VendorMasterVendorName
            }).ToList();
            var Stocktype = itemStockRepository.GetAsQueryable().Where(c => c.ItemStockTypeDelStatus != 1).Select(a => new
            {
                a.ItemStockTypeId,
                a.ItemStockTypeDescription
            }).ToList();
            var SuppliersMaster = supplierrepository.GetAsQueryable().Where(a => a.SuppliersMasterSupplierDelStatus != true).Select(c => new
            {
                c.SuppliersMasterSupplierId,
                c.SuppliersMasterSupplierName
            }).ToList();
            var taxMasters = taxrepository.GetAsQueryable().Where(a => a.TmDelStatus != true).Select(c => new
            {
                c.TmId,
                c.TmName,
                c.TmPercentage
            }).ToList();
            var priceLevelMasters = priceLevelrepository.GetAsQueryable().Where(a => a.PriveLevelMasterPriceLevelDelStatus != true).Select(c => new
            {
                c.PriceLevelMasterPriceLevelId,
                c.PriceLevelMasterPriceLevelName
            }).ToList();
            var unitMasters = unitrepository.GetAsQueryable().Where(a => a.UnitMasterUnitDelStatus != true).Select(x => new
            {
                x.UnitMasterUnitId,
                x.UnitMasterUnitFullName,
                x.UnitMasterUnitShortName
            }).ToList();
            objectresponse.ResultSet = new
            {
                itemMaster = itemMaster,
                VendorMasters = VendorMasters,
                StockTypes = Stocktype,
                SuppliersMaster = SuppliersMaster,
                taxMasters = taxMasters,
                priceLevelMasters = priceLevelMasters,
                unitMasters = unitMasters
            };

            objectresponse.IsSuccess = true;
            return objectresponse;
        }

        [HttpGet]
        [Route("searchItemByName")]
        public ResponseInfo searchItemByName(string name)
        {
            var objectresponse = new ResponseInfo();

            var itemMaster = itemrepository.GetAsQueryable().Where(k => k.ItemMasterAccountNo != 0 && (k.ItemMasterDelStatus != true)
                     && k.ItemMasterItemType != ItemMasterStatus.Group && k.ItemMasterItemName.ToLower().Contains(name.ToLower())).Select(k => new
                     {
                         k.ItemMasterItemId,
                         k.ItemMasterItemName
                     }).ToList();

            objectresponse.ResultSet = new
            {
                itemMaster = itemMaster,
            };
            return objectresponse;
        }

        [HttpGet]
        [Route("GetItemMasterById/{id}")]
        public ResponseInfo GetItemMasterById(int id)
        {
            var objectresponse = new ResponseInfo();
            var listItemnater = itemrepository.GetAsQueryable().Where(a => a.ItemMasterItemId == id && a.ItemMasterDelStatus != true).ToList();
            var unitlist = _unitDetailsRepo.GetAsQueryable().Where(a => a.UnitDetailsItemId == id && a.UnitDetailsDelStatus != true).ToList();

            var data = listItemnater.Select(a => new
            {
                a.ItemMasterItemId,
                a.ItemMasterItemName,
                a.ItemMasterPartNo,
                a.ItemMasterUnitPrice,
                a.ItemMasterLastPurchasePrice,
                UnitDetails = unitlist.Select(c => new
                {
                    c.UnitDetailsId,
                    c.UnitDetailsUnitId,
                    c.UnitDetailsConversionType,
                    c.UnitDetailsRate,
                    c.UnitDetailsUnitPrice,
                    c.UnitDetailsUnitCost,
                }).FirstOrDefault(),
            }).FirstOrDefault();

            objectresponse.ResultSet = new
            {
                itemMaster = data,
            };
            return objectresponse;

        }
    }
}