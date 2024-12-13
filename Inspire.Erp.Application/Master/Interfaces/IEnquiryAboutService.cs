using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Master
{
    public interface IEnquiryAboutService
    {
        public IEnumerable<EnquiryAbout> InsertEnquiryAbout(EnquiryAbout enquiryAbout);
        public IEnumerable<EnquiryAbout> UpdateEnquiryAbout(EnquiryAbout enquiryAbout);
        public IEnumerable<EnquiryAbout> DeleteEnquiryAbout(EnquiryAbout enquiryAbout);
        public IEnumerable<EnquiryAbout> GetAllEnquiryAbout();
        public IEnumerable<EnquiryAbout> GetAllEnquiryAboutById(int id);
    }
}
