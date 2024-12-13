using Inspire.Erp.Application.Authentication.Inerfaces;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Infrastructure;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Inspire.Erp.Application.Authentication.Implementations
{
    public class LoginService : IloginService
    {
        private IRepository<UserFile> userFileRepository;
        private IConfiguration Configuration;
        public LoginService(IRepository<UserFile> _userFileRepository, IConfiguration _Configuration)
        {
            userFileRepository = _userFileRepository;
            Configuration = _Configuration;
        }
        public UserFile SignIn(UserFile userData)
        {
            var user = userFileRepository.GetAsQueryable().Where(x => x.UserName == userData.UserName && x.Password == userData.Password).SingleOrDefault();
            return user;
        }
        public UserFile Authenticate(UserFile userData, string ipAddress)
        {
            var user = userFileRepository.GetAsQueryable().Where(x => x.UserName == userData.UserName && x.Password == userData.Password).SingleOrDefault();
            if (user == null) return null;

            // authentication successful so generate jwt and refresh tokens
            var jwtToken = generateJwtToken(user);
            var refreshToken = generateRefreshToken(user);

            user.Token = jwtToken;
            user.RefreshToken = refreshToken.Token;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            userFileRepository.Update(user);
            return user;
        }
        public UserFile RefreshToken(UserFile userData)
        {

            if (userData?.RefreshToken is null)
            {
                return new UserFile
                {
                    IsValid = false,
                    Message = "Invalid client request"
                };
            }
            var principal = GetPrincipalFromExpiredToken(userData.Token);
            var username = principal.Identity.Name;
            var user = userFileRepository.GetAsQueryable().Where(x => x.UserName == username).SingleOrDefault();
            if (user is null || user.RefreshToken != userData.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return new UserFile
                {
                    IsValid = false,
                    Message = "Invalid client request"
                };
            }
            var newAccessToken = generateJwtToken(userData);
            var newRefreshToken = generateRefreshToken(userData);
            user.RefreshToken = newRefreshToken.Token;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            userFileRepository.Update(user);
            return user;
        }


        public UserFile RevokeToken(UserFile userData)
        {

            var user = userFileRepository.GetAsQueryable().Where(x => x.UserName == userData.UserName).SingleOrDefault();
            if (user == null)
                return new UserFile
                {
                    IsValid = false,
                    Message = "Invalid client request"
                };
            user.RefreshToken = null;
            userFileRepository.Update(user);
            return user;
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Configuration["ApplicationSettings:Secret"])),
                ValidateLifetime = true, //here we are saying that we don't care about the token's expiration date
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }


        private string generateJwtToken(UserFile user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = this.Configuration["ApplicationSettings:Secret"];
            var expiration = Convert.ToInt16(this.Configuration["ApplicationSettings:ExpirationInMinutes"]);
            var key = Encoding.UTF8.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(expiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private RefreshToken generateRefreshToken(UserFile user)
        {
            using (var rngCryptoServiceProvider = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomBytes),
                    Expires = DateTime.UtcNow.AddDays(7),
                    Created = DateTime.UtcNow
                };
            }


        }

        #region POS
        public UserFile PosAuthenticate(UserFile userData)
        {
            var user = userFileRepository.GetAsQueryable().Where(x => x.UserName == userData.UserName && x.Password == userData.Password).SingleOrDefault();
            if (user == null) return null;

            // authentication successful so generate jwt and refresh tokens
            var jwtToken = generateJwtToken(user);
            var refreshToken = generateRefreshToken(user);

            user.Token = jwtToken;
            user.RefreshToken = refreshToken.Token;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            userFileRepository.Update(user);
            return user;
        }
        #endregion
        public UserFile UpdateUser(UserFile user)
        {

            try
            {
                userFileRepository.BeginTransaction();

                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

                userFileRepository.Update(user);

                userFileRepository.TransactionCommit();

                return this.GetUserById(user.UserId);

            }
            catch (Exception ex)
            {
                userFileRepository.TransactionRollback();
                throw ex;
            }

        }

        public UserFile DeleteUser(UserFile user)
        {

            try
            {
                userFileRepository.BeginTransaction();
                user.Deleted = true;

                userFileRepository.Update(user);

                userFileRepository.TransactionCommit();


            }
            catch (Exception ex)
            {
                userFileRepository.TransactionRollback();
                throw ex;
            }

            return user;
        }

        public UserFile GetUserById(long id)
        {
            var user = userFileRepository.GetAsQueryable().Where(x => x.UserId == id).SingleOrDefault();
            return user;
        }
        public IEnumerable<UserFile> GetAllUserFile()
        {
            IEnumerable<UserFile> UserFile;
            try
            {
                UserFile = userFileRepository.GetAll().Where(a => a.Deleted != true).Select(k=>k);

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                //userFileRepository.Dispose();
            }
            return UserFile;
        }
        public UserFile InsertUser(UserFile user)
        {

            try
            {
                userFileRepository.BeginTransaction();
                if (user.UserId == 0)
                {
                    long maxcount = 0;
                    maxcount =
                        userFileRepository.GetAsQueryable()
                        .DefaultIfEmpty().Max(o => o == null ? 0 : (long)o.UserId) + 1;

                    user.UserId = maxcount;
                    user.LogInId = maxcount.ToString();
                }
                var jwtToken = generateJwtToken(user);
                var refreshToken = generateRefreshToken(user);

                user.Token = jwtToken;
                user.RefreshToken = refreshToken.Token;
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

                userFileRepository.Insert(user);

                userFileRepository.TransactionCommit();

                return this.GetUserById(user.UserId);

            }
            catch (Exception ex)
            {
                userFileRepository.TransactionRollback();
                throw ex;
            }

        }


    }


}

