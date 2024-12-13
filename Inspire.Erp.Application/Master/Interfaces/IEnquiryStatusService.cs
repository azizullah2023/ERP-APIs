using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Master
{
    public interface IEnquiryStatusService
    {
        public IEnumerable<EnquiryStatus> InsertEnquiryStatus(EnquiryStatus enquiryStatus);
        public IEnumerable<EnquiryStatus> UpdateEnquiryStatus(EnquiryStatus enquiryStatus);
        public IEnumerable<EnquiryStatus> DeleteEnquiryStatus(EnquiryStatus enquiryStatus);
        public IEnumerable<EnquiryStatus> GetAllEnquiryStatus();
        public IEnumerable<EnquiryStatus> GetAllEnquiryStatusById(int id);
    }
}
