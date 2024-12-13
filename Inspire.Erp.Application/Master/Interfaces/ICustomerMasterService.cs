using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.Master
{
    public interface ICustomerMasterService
    {
        public Task<CustomerMaster> InsertCustomer(CustomerMaster customerMaster);
        public CustomerMaster UpdateCustomer(CustomerMaster customerMaster);
        public CustomerMaster DeleteCustomer(CustomerMaster customerMaster);
        public IEnumerable<CustomerMaster> GetAllCustomer();
        public CustomerMaster GetAllCustomerById(int id);
    }
}
