using AutoMapper;
using Inspire.Erp.Application.Sales.Interface;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Domain.Models;
using Inspire.Erp.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.Controllers
{
    // [Produces("application/json")]
    [ApiController]
    [Route("api/ProformaInvoice")]
    public class ProformaInvoiceController : ControllerBase
    {
        private IProformaInvoice proformaInvoice;
        private IMapper mapper;

        public ProformaInvoiceController(IProformaInvoice proformaInvoice, IMapper mapper)
        {
            this.proformaInvoice = proformaInvoice;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllProformaInvoices")]
        public IActionResult GetAllProformaInvoices()
        {
            try
            {
                return Ok(proformaInvoice.GetProformaInvoices().ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }

        [HttpGet]
        [Route("GetProformaInvoiceById")]
        public IActionResult GetProformaInvoiceById(int id)
        {
            try
            {
                return Ok(proformaInvoice.GetProformaInvoiceById(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }

        [HttpPost]
        [Route("InsertProformaInvoice")]
        public IActionResult InsertProformaInvoices([FromBody] ProformaInvoice proformaInvoiceViewModel)
        {
            try
            {
                return Ok(proformaInvoice.InsertProformaInvoice(proformaInvoiceViewModel));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }
        [HttpPut]
        [Route("UpdateProformaInvoice")]
        public IActionResult UpdateProformaInvoices([FromBody] ProformaInvoice proformaInvoiceViewModel)
        {
            try
            {
                return Ok(proformaInvoice.UpdateProformaInvoice(proformaInvoiceViewModel));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }
         

        [HttpDelete]
        [Route("DeleteProformaInvoice")]
        public IActionResult DeleteProformaInvoices(int Id)
        {
            try
            {
                return Ok(proformaInvoice.DeleteProformaInvoice(Id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }
    }
}
