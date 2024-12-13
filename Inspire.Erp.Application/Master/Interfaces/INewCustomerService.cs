using Inspire.Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Inspire.Erp.Application.Common;


namespace Inspire.Erp.Application.Master.Interfaces
{
    public interface INewCustomerService
    {
        public CustomerViewModel InsertNewCustomer(CustomerMaster customerMaster, List<CustomerContacts> customerContacts, List<CustomerDepartments> customerDepartments);
        public CustomerViewModel UpdateNewCustomer(CustomerMaster customerMaster, List<CustomerContacts> customerContacts, List<CustomerDepartments> customerDepartments);
        public int DeleteNewCustomers(CustomerMaster customerMaster, List<CustomerContacts> customerContacts, List<CustomerDepartments> customerDepartments);
        public CustomerViewModel GetSavedCustomers(int pvno);
        ////public CustomerViewModel GetAllCustomers(CustomerMaster customerMaster, List<CustomerContacts> customerContacts, List<CustomerDepartments> customerDepartments);
        // object InsertNewCustomer(CustomerMaster param1, List<CustomerDepartments> param3, List<CustomerContacts> param2);

        //// public IEnumerable<CustomerViewModel> GetAllCustomers();
        public IEnumerable<CustomerMaster> GetAllCustomers();

    }
}


