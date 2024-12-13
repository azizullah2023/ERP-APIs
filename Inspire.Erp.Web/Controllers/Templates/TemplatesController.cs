using Inspire.Erp.Application.Templates.Interfaces;
using Inspire.Erp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.Controllers.Templates
{
    [Route("api/Templates")]
    [Produces("application/json")]
    [ApiController]
    public class TemplatesController : ControllerBase
    {
        private ITemplates template;
        public TemplatesController(ITemplates template)
        {
            this.template = template;
        }

        [HttpPost]
        [Route("DeleteTemplate/{Id}")]
        public async Task<IActionResult> DeleteTemplate(int Id)
        {
            var result = await template.DeleteTemplate(Id);
            if(result.Result) return Ok(result.Message);
            else return BadRequest(result.Message);
        }
        [HttpGet]
        [Route("GetAllActiveTemplates")]
        public async Task<IActionResult> GetAllActiveTemplates()
        {
            return Ok(await template.GetAllActiveTemplates());
        }
        [HttpGet]
        [Route("GetTemplateById/{Id}")]
        public async Task<IActionResult> GetTemplateById(int Id)
        {
            return Ok(await template.GetTemplateById(Id));
        }
        [HttpGet]
        [Route("GetTemplateBytype/{type}")]
        public async Task<IActionResult> GetTemplateBytype(string type)
        {
            return Ok(await template.GetTemplateBytype(type));
        }

        [HttpPost]
        [Route("InsterTemplate")]
        public async Task<IActionResult> InsterTemplate(Qtemplates qtemplate)
        {
            var result = await template.InsterTemplate(qtemplate);
            if (result.Valid) return Ok(result.Message);
            else return BadRequest(result.Message);
        }
        [HttpPost]
        [Route("UpdateTemplate")]
        public async Task<IActionResult> UpdateTemplate(Qtemplates qtemplate)
        {
            var result = await template.UpdateTemplate(qtemplate);
            if (result.Valid) return Ok(result.Message);
            else return BadRequest(result.Message);

          //  return Ok(await template.UpdateTemplate(qtemplate));
        }

        [HttpGet]
        [Route("GetAllDeletedTemplates")]
        public async Task<IActionResult> GetAllDeletedTemplates()
        {
            return Ok(await template.GetAllDeletedTemplates());
        }
        [HttpGet]
        [Route("GetAllTemplates")]
        public async Task<IActionResult> GetAllTemplates()
        {
            return Ok(await template.GetAllTemplates());
        }

    }
}
