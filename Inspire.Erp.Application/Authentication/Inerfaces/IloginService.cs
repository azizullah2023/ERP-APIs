using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Inspire.Erp.Application.Authentication.Inerfaces
{
    public interface IloginService
    {
        UserFile SignIn(UserFile user);
        UserFile Authenticate(UserFile userData, string ipAddress);
        UserFile RefreshToken(UserFile userData);
        UserFile RevokeToken(UserFile userData);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);



        #region POS
        UserFile PosAuthenticate(UserFile userData);
        #endregion
        UserFile InsertUser(UserFile userData);
        UserFile UpdateUser(UserFile userData);
        UserFile DeleteUser(UserFile userData);
        public IEnumerable<UserFile> GetAllUserFile();
        UserFile GetUserById(long a);

    }
}
