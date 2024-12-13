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
    public class EnquiryStatusController : ControllerBase
    {
        private readonly IMapper _mapper;
        private IEnquiryStatusService enquiryStatusService;
        public EnquiryStatusController(IEnquiryStatusService _enquiryStatusService, IMapper mapper)
        {

            enquiryStatusService = _enquiryStatusService;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("GetAllEnquiryStatus")]
        public List<EnquiryStatusViewModel> GetAllEnquiryStatus()
        {

            return enquiryStatusService.GetAllEnquiryStatus().Select(k => new EnquiryStatusViewModel
            {

                EnquiryStatusEnquiryStatusId = k.EnquiryStatusEnquiryStatusId,
                EnquiryStatusEnquiryStatus = k.EnquiryStatusEnquiryStatus,
                EnquiryStatusEnquiryStatusDelStatus = k.EnquiryStatusEnquiryStatusDelStatus

            }).ToList();
        }


        [HttpGet]
        [Route("GetAllEnquiryStatusById/{id}")]
        public List<EnquiryStatusViewModel> GetAllEnquiryStatusById(int id)
        {
            return enquiryStatusService.GetAllEnquiryStatusById(id).Select(k => new EnquiryStatusViewModel
            {

                EnquiryStatusEnquiryStatusId = k.EnquiryStatusEnquiryStatusId,
                EnquiryStatusEnquiryStatus = k.EnquiryStatusEnquiryStatus,
                EnquiryStatusEnquiryStatusDelStatus = k.EnquiryStatusEnquiryStatusDelStatus


            }).ToList();
        }

        [HttpPost]
        [Route("InsertEnquiryStatus")]
        public List<EnquiryStatusViewModel> InsertEnquiryStatus([FromBody] EnquiryStatusViewModel enquiryStatus)
        {

            var result = _mapper.Map<EnquiryStatus>(enquiryStatus);
            return enquiryStatusService.InsertEnquiryStatus(result).Select(k => new EnquiryStatusViewModel
            {
                EnquiryStatusEnquiryStatusId = k.EnquiryStatusEnquiryStatusId,
                EnquiryStatusEnquiryStatus = k.EnquiryStatusEnquiryStatus,
                EnquiryStatusEnquiryStatusDelStatus = k.EnquiryStatusEnquiryStatusDelStatus


            }).ToList();
        }

        [HttpPost]
        [Route("UpdateEnquiryStatus")]
        public List<EnquiryStatusViewModel> UpdateEnquiryStatus([FromBody] EnquiryStatusViewModel enquiryStatus)
        {
            var result = _mapper.Map<EnquiryStatus>(enquiryStatus);
            return enquiryStatusService.UpdateEnquiryStatus(result).Select(k => new EnquiryStatusViewModel
            {
                EnquiryStatusEnquiryStatusId = k.EnquiryStatusEnquiryStatusId,
                EnquiryStatusEnquiryStatus = k.EnquiryStatusEnquiryStatus,
                EnquiryStatusEnquiryStatusDelStatus = k.EnquiryStatusEnquiryStatusDelStatus

            }).ToList();
        }

        [HttpPost]
        [Route("DeleteEnquiryStatus")]
        public List<EnquiryStatusViewModel> DeleteEnquiryStatus([FromBody] EnquiryStatusViewModel enquiryStatus)
        {
            var result = _mapper.Map<EnquiryStatus>(enquiryStatus);
            return enquiryStatusService.DeleteEnquiryStatus(result).Select(k => new EnquiryStatusViewModel
            {
                EnquiryStatusEnquiryStatusId = k.EnquiryStatusEnquiryStatusId,
                EnquiryStatusEnquiryStatus = k.EnquiryStatusEnquiryStatus,
                EnquiryStatusEnquiryStatusDelStatus = k.EnquiryStatusEnquiryStatusDelStatus


            }).ToList();
        }
    }
}