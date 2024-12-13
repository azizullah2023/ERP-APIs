using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Inspire.Erp.Application.Master;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inspire.Erp.Web.Controllers
{
    [Route("api/master")]
    [Produces("application/json")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IMapper _mapper;
        private IBrandMasterService BrandMasterService;
        public BrandController(IBrandMasterService _BrandMasterService, IMapper mapper)
        {

            BrandMasterService = _BrandMasterService;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("GetAllBrand")]
        public List<BrandMasterViewModel> GetAllBrand()
        {

            return BrandMasterService.GetAllBrand().Select(k => new BrandMasterViewModel
            {
                VendorMasterVendorId = k.VendorMasterVendorId,
                VendorMasterVendorName = k.VendorMasterVendorName,
                VendorMasterVendorStatus = k.VendorMasterVendorStatus,
                VendorMasterVendorAddress = k.VendorMasterVendorAddress,
                VendorMasterVendorPhone = k.VendorMasterVendorPhone,
                VendorMasterVendorFax = k.VendorMasterVendorFax,
                VendorMasterVendorEmail = k.VendorMasterVendorEmail,
                VendorMasterVendorDelStatus =k.VendorMasterVendorDelStatus
            }).ToList();
        }

        [HttpGet]
        [Route("GetAllBrandById/{id}")]
        public List<BrandMasterViewModel> GetAllBrandById(int id)
        {
            return BrandMasterService.GetAllBrandById(id).Select(k => new BrandMasterViewModel
            {

                VendorMasterVendorId = k.VendorMasterVendorId,
                VendorMasterVendorName = k.VendorMasterVendorName,
                VendorMasterVendorStatus = k.VendorMasterVendorStatus,
                VendorMasterVendorAddress = k.VendorMasterVendorAddress,
                VendorMasterVendorPhone = k.VendorMasterVendorPhone,
                VendorMasterVendorFax = k.VendorMasterVendorFax,
                VendorMasterVendorEmail = k.VendorMasterVendorEmail,
                VendorMasterVendorDelStatus = k.VendorMasterVendorDelStatus

            }).ToList();
        }


        [HttpPost]
        [Route("InsertBrand")]
        public List<BrandMasterViewModel> InsertBrand([FromBody] BrandMasterViewModel VendorMaster)
        {
            var result = _mapper.Map<VendorMaster>(VendorMaster);
            return BrandMasterService.InsertBrand(result).Select(k => new BrandMasterViewModel
            {
                VendorMasterVendorId = k.VendorMasterVendorId,
                VendorMasterVendorName = k.VendorMasterVendorName,
                VendorMasterVendorStatus = k.VendorMasterVendorStatus,
                VendorMasterVendorAddress = k.VendorMasterVendorAddress,
                VendorMasterVendorPhone = k.VendorMasterVendorPhone,
                VendorMasterVendorFax = k.VendorMasterVendorFax,
                VendorMasterVendorEmail = k.VendorMasterVendorEmail,
                VendorMasterVendorDelStatus = k.VendorMasterVendorDelStatus
            }).ToList();
        }

        [HttpPost]
        [Route("UpdateBrand")]
        public List<BrandMasterViewModel> UpdateBrand([FromBody] BrandMasterViewModel VendorMaster)
        {
            var result = _mapper.Map<VendorMaster>(VendorMaster);
            return BrandMasterService.UpdateBrand(result).Select(k => new BrandMasterViewModel
            {
                VendorMasterVendorId = k.VendorMasterVendorId,
                VendorMasterVendorName = k.VendorMasterVendorName,
                VendorMasterVendorStatus = k.VendorMasterVendorStatus,
                VendorMasterVendorAddress = k.VendorMasterVendorAddress,
                VendorMasterVendorPhone = k.VendorMasterVendorPhone,
                VendorMasterVendorFax = k.VendorMasterVendorFax,
                VendorMasterVendorEmail = k.VendorMasterVendorEmail,
                VendorMasterVendorDelStatus = k.VendorMasterVendorDelStatus
            }).ToList();
        }

        [HttpPost]
        [Route("DeleteBrand")]
        public List<BrandMasterViewModel> DeleteBrand([FromBody] BrandMasterViewModel VendorMaster)
        {
            var result = _mapper.Map<VendorMaster>(VendorMaster);
            return BrandMasterService.DeleteBrand(result).Select(k => new BrandMasterViewModel
            {
                VendorMasterVendorId = k.VendorMasterVendorId,
                VendorMasterVendorName = k.VendorMasterVendorName,
                VendorMasterVendorStatus = k.VendorMasterVendorStatus,
                VendorMasterVendorAddress = k.VendorMasterVendorAddress,
                VendorMasterVendorPhone = k.VendorMasterVendorPhone,
                VendorMasterVendorFax = k.VendorMasterVendorFax,
                VendorMasterVendorEmail = k.VendorMasterVendorEmail,
                VendorMasterVendorDelStatus = k.VendorMasterVendorDelStatus
            }).ToList();
        }
    }
}