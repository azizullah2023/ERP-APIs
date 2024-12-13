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
    public class EnquiryAboutController : ControllerBase
    {
        private readonly IMapper _mapper;
        private IEnquiryAboutService enquiryAboutService;
        public EnquiryAboutController(IEnquiryAboutService _enquiryAboutService, IMapper mapper)
        {

            enquiryAboutService = _enquiryAboutService;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("GetAllEnquiryAbout")]
        public List<EnquiryAboutViewModel> GetAllEnquiryAbout()
        {

            return enquiryAboutService.GetAllEnquiryAbout().Select(k => new EnquiryAboutViewModel
            {

                EnquiryAboutEnquiryAboutId = k.EnquiryAboutEnquiryAboutId,
                EnquiryAboutEnquiryAbout = k.EnquiryAboutEnquiryAbout,
                EnquiryAboutEnquiryAboutStatus = k.EnquiryAboutEnquiryAboutStatus,
                EnquiryAboutEnquiryAbountDelStatus = k.EnquiryAboutEnquiryAbountDelStatus
            }).ToList();
        }


        [HttpGet]
        [Route("GetAllEnquiryAboutById/{id}")]
        public List<EnquiryAboutViewModel> GetAllEnquiryAboutById(int id)
        {
            return enquiryAboutService.GetAllEnquiryAboutById(id).Select(k => new EnquiryAboutViewModel
            {

                EnquiryAboutEnquiryAboutId = k.EnquiryAboutEnquiryAboutId,
                EnquiryAboutEnquiryAbout = k.EnquiryAboutEnquiryAbout,
                EnquiryAboutEnquiryAboutStatus = k.EnquiryAboutEnquiryAboutStatus,
                EnquiryAboutEnquiryAbountDelStatus = k.EnquiryAboutEnquiryAbountDelStatus

            }).ToList();
        }

        [HttpPost]
        [Route("InsertEnquiryAbout")]
        public List<EnquiryAboutViewModel> InsertEnquiryAbout([FromBody] EnquiryAboutViewModel enquiryAbout)
        {

            var result = _mapper.Map<EnquiryAbout>(enquiryAbout);
            return enquiryAboutService.InsertEnquiryAbout(result).Select(k => new EnquiryAboutViewModel
            {
                EnquiryAboutEnquiryAboutId = k.EnquiryAboutEnquiryAboutId,
                EnquiryAboutEnquiryAbout = k.EnquiryAboutEnquiryAbout,
                EnquiryAboutEnquiryAboutStatus = k.EnquiryAboutEnquiryAboutStatus,
                EnquiryAboutEnquiryAbountDelStatus = k.EnquiryAboutEnquiryAbountDelStatus


            }).ToList();
        }

        [HttpPost]
        [Route("UpdateEnquiryAbout")]
        public List<EnquiryAboutViewModel> UpdateEnquiryAbout([FromBody] EnquiryAboutViewModel enquiryAbout)
        {
            var result = _mapper.Map<EnquiryAbout>(enquiryAbout);
            return enquiryAboutService.UpdateEnquiryAbout(result).Select(k => new EnquiryAboutViewModel
            {
                EnquiryAboutEnquiryAboutId = k.EnquiryAboutEnquiryAboutId,
                EnquiryAboutEnquiryAbout = k.EnquiryAboutEnquiryAbout,
                EnquiryAboutEnquiryAboutStatus = k.EnquiryAboutEnquiryAboutStatus,
                EnquiryAboutEnquiryAbountDelStatus = k.EnquiryAboutEnquiryAbountDelStatus

            }).ToList();
        }

        [HttpPost]
        [Route("DeleteEnquiryAbout")]
        public List<EnquiryAboutViewModel> DeleteEnquiryAbout([FromBody] EnquiryAboutViewModel enquiryAbout)
        {
            var result = _mapper.Map<EnquiryAbout>(enquiryAbout);
            return enquiryAboutService.DeleteEnquiryAbout(result).Select(k => new EnquiryAboutViewModel
            {
                EnquiryAboutEnquiryAboutId = k.EnquiryAboutEnquiryAboutId,
                EnquiryAboutEnquiryAbout = k.EnquiryAboutEnquiryAbout,
                EnquiryAboutEnquiryAboutStatus = k.EnquiryAboutEnquiryAboutStatus,
                EnquiryAboutEnquiryAbountDelStatus = k.EnquiryAboutEnquiryAbountDelStatus

            }).ToList();
        }
    }
}