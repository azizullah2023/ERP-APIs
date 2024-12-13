using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Templates.Interfaces
{
    public interface ITemplates 
    {
        public Task<Response<Qtemplates>> InsterTemplate(Qtemplates qtemplate);
        public Task<Response<Qtemplates>> UpdateTemplate(Qtemplates qtemplate);
        public Task<Response<bool>> DeleteTemplate(int qtemplateId);
        public Task<Response<List<Qtemplates>>> GetAllActiveTemplates();
        public Task<Response<List<Qtemplates>>> GetAllTemplates();
        public Task<Response<List<Qtemplates>>> GetAllDeletedTemplates();
        public Task<Response<List<Qtemplates>>> GetTemplateById(int qtemplateId);
        public Task<Response<List<Qtemplates>>> GetTemplateBytype(string qtemplateType);
    }
}
