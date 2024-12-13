using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Application.Master
{
    public interface ICustomerTypeService
    {
        public IEnumerable<CustomerType> InsertCustType(CustomerType customerType);
        public IEnumerable<CustomerType> UpdateCustType(CustomerType customerType);
        public IEnumerable<CustomerType> DeleteCustType(CustomerType customerType);
        public IEnumerable<CustomerType> GetAllCustType();
        public IEnumerable<CustomerType> GetAllCustTypeById(int id);
    }
}