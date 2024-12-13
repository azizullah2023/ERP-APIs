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
    [Route("api")]
    [Produces("application/json")]
    [ApiController]
    public class CustomerTypeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private ICustomerTypeService customerTypeService;
        public CustomerTypeController(ICustomerTypeService _customerTypeService, IMapper mapper)
        {

            customerTypeService = _customerTypeService;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("GetAllCustType")]
        public List<CustomerTypeViewModel> GetAllCustType()
        {

            return customerTypeService.GetAllCustType().Select(k => new CustomerTypeViewModel
            {
                CustomerTypeId = k.CustomerTypeId,
                CustomerTypeName = k.CustomerTypeName,
                CustomerTypeUserId = k.CustomerTypeUserId,
                CustomerTypeDeleted = k.CustomerTypeDeleted,
                CustomerTypeStatus = k.CustomerTypeStatus,
                CustomerTypeDelStatus=k.CustomerTypeDelStatus

    }).ToList();
        }


        [HttpGet]
        [Route("GetAllCustTypeById/{id}")]
        public List<CustomerTypeViewModel> GetAllCustTypeById(int id)
        {
            return customerTypeService.GetAllCustTypeById(id).Select(k => new CustomerTypeViewModel
            {

                CustomerTypeId = k.CustomerTypeId,
                CustomerTypeName = k.CustomerTypeName,
                CustomerTypeUserId = k.CustomerTypeUserId,
                CustomerTypeDeleted = k.CustomerTypeDeleted,
                CustomerTypeStatus = k.CustomerTypeStatus,
                CustomerTypeDelStatus = k.CustomerTypeDelStatus

            }).ToList();
        }

        [HttpPost]
        [Route("InsertCustType")]
        public List<CustomerTypeViewModel> InsertCustType([FromBody] CustomerTypeViewModel customertype)
        {
            var result = _mapper.Map<CustomerType>(customertype);
            return customerTypeService.InsertCustType(result).Select(k => new CustomerTypeViewModel
            {

                CustomerTypeId = k.CustomerTypeId,
                CustomerTypeName = k.CustomerTypeName,
                CustomerTypeUserId = k.CustomerTypeUserId,
                CustomerTypeDeleted = k.CustomerTypeDeleted,
                CustomerTypeStatus = k.CustomerTypeStatus,
                CustomerTypeDelStatus = k.CustomerTypeDelStatus


            }).ToList();
        }

        [HttpPost]
        [Route("UpdateCustType")]
        public List<CustomerTypeViewModel> UpdateCustType([FromBody] CustomerTypeViewModel customertype)
        {
            var result = _mapper.Map<CustomerType>(customertype);
            return customerTypeService.UpdateCustType(result).Select(k => new CustomerTypeViewModel
            {

                CustomerTypeId = k.CustomerTypeId,
                CustomerTypeName = k.CustomerTypeName,
                CustomerTypeUserId = k.CustomerTypeUserId,
                CustomerTypeDeleted = k.CustomerTypeDeleted,
                CustomerTypeStatus = k.CustomerTypeStatus,
                CustomerTypeDelStatus = k.CustomerTypeDelStatus


            }).ToList();
        }

        [HttpPost]
        [Route("DeleteCustType")]
        public List<CustomerTypeViewModel> DeleteCustType([FromBody] CustomerTypeViewModel customertype)
        {
            var result = _mapper.Map<CustomerType>(customertype);
            return customerTypeService.DeleteCustType(result).Select(k => new CustomerTypeViewModel
            {

                CustomerTypeId = k.CustomerTypeId,
                CustomerTypeName = k.CustomerTypeName,
                CustomerTypeUserId = k.CustomerTypeUserId,
                CustomerTypeDeleted = k.CustomerTypeDeleted,
                CustomerTypeStatus = k.CustomerTypeStatus,
                CustomerTypeDelStatus = k.CustomerTypeDelStatus

            }).ToList();
        }
    }
}