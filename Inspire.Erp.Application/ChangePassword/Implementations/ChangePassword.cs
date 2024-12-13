using Inspire.Erp.Application.Authentication.Inerfaces;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Infrastructure;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Inspire.Erp.Application.Authentication.Implementations
{
    public class ChangePassword : IChangePassword
    {
        private readonly IRepository<ChangePasswordResponse> repo;
        private IConfiguration Configuration;
        private IRepository<UserFile> userFileRepository;

        public ChangePassword(IRepository<ChangePasswordResponse> _repo, IConfiguration configuration, IRepository<UserFile> userFileRepository)
        {
            repo = _repo;
            Configuration = configuration;
            this.userFileRepository = userFileRepository;
        }
        public UserFile GetUserById(long id)
        {
            var user = userFileRepository.GetAsQueryable().Where(x => x.UserId == id).SingleOrDefault();
            return user;
        }
       
        public Task<bool> UpdatePassword(ChangePasswordResponse Obj)
        {
            try
            {

                var user = userFileRepository.GetAsQueryable().Where(x => x.UserId == Obj.Userid).SingleOrDefault();
                if (user.UserId != null)
                {
                    user.Password = Obj.NewPassword;
                    userFileRepository.BeginTransaction();

                    user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

                    userFileRepository.Update(user);

                    userFileRepository.TransactionCommit();
                   
                }
               
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                return Task.FromResult(false);
            }
        }

    }
}