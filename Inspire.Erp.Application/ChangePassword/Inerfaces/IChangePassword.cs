using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Inspire.Erp.Application.Authentication.Inerfaces
{
    public interface IChangePassword
    {
        UserFile GetUserById(long a);
        public Task<bool> UpdatePassword(ChangePasswordResponse Obj);
  
    }
}
