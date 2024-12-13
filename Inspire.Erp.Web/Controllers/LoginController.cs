using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Inspire.Erp.Application.Authentication.Inerfaces;
using Inspire.Erp.Application.Master;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Domain.Enums;
using Inspire.Erp.Domain.Modals.Common;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using Inspire.Erp.Web.Common;
using Inspire.Erp.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inspire.Erp.Web.Controllers
{
    [Route("api")]
    [Produces("application/json")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IloginService loginsvc;
        private readonly IMapper _mapper;
        private IRepository<UserFile> userFileRepository;
        private IRepository<LocationMaster> locationrepository;
        private IRepository<FinancialPeriods> financialrepository;

        public LoginController(IloginService _loginsvc, IMapper mapper,
            IRepository<LocationMaster> _locationrepository, IRepository<UserFile> _userFileRepository, IRepository<FinancialPeriods> _financialrepository)
        {
            loginsvc = _loginsvc;
            _mapper = mapper; userFileRepository = _userFileRepository;
            locationrepository = _locationrepository; financialrepository = _financialrepository;
        }
        [HttpPost]
        [Route("login")]
        public ApiResponse<UserFile> login([FromBody] User user)
        {
            ApiResponse<UserFile> apiResponse = new ApiResponse<UserFile>()
            {
                Valid = true,
                Result = null,
                Message = ""
            };
            try
            {
                var k = loginsvc.SignIn(new UserFile { UserName = user.Username, Password = user.Password });
                if (k != null)
                {
                    apiResponse.Result = k;
                    apiResponse.Error = false;
                    apiResponse.Message = LoginMessage.Successfull;
                }
                else
                {
                    apiResponse.Valid = false;
                    apiResponse.Error = true;
                    apiResponse.Message = LoginMessage.Failed;

                }
            }
            catch (Exception ex)
            {
                apiResponse = new ApiResponse<UserFile>()
                {
                    Valid = false,
                    Result = null,
                    Message = DbMessage.Failed,
                    Error = true
                };
            }

            return apiResponse;
        }


        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] User user)
        {
            var response = loginsvc.Authenticate(new UserFile
            {
                UserName = user.Username,
                Password = user.Password
            }, ipAddress());

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            setTokenCookie(response.RefreshToken);
            response.FinancialPeriodsFsno = user.Financialperiod.FinancialPeriodsFsno;
            response.LocId = (int)user.Location.LocationMasterLocationId;
            return Ok(ApiResponse<UserFile>.Success(response));
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public IActionResult RefreshToken()
        {
            var refreshToken = Convert.ToString(Request.Cookies["refreshToken"]);

            var response = loginsvc.RefreshToken(new UserFile { RefreshToken = refreshToken });

            if (response == null)
                return Unauthorized(new { message = "Invalid token" });

            setTokenCookie(response.RefreshToken);

            return Ok(response);
        }

        [HttpPost("revoke-token")]
        public IActionResult RevokeToken([FromBody] User user)
        {
            // accept token from request body or cookie
            var token = user.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });

            var response = loginsvc.RevokeToken(new UserFile { RefreshToken = user.RefreshToken });

            if (response == null)
                return NotFound(new { message = "Token not found" });

            return Ok(new { message = "Token revoked" });
        }
        private void setTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
        [HttpPost]
        [Route("InsertUser")]
        public ApiResponse<UserFileViewModel> InserUser([FromBody] UserFileViewModel user)
        {
            ApiResponse<UserFileViewModel> apiResponse = new ApiResponse<UserFileViewModel>();

            var param1 = _mapper.Map<UserFile>(user);

            var xs = loginsvc.InsertUser(param1);

            UserFileViewModel userdata = new UserFileViewModel();
            userdata = _mapper.Map<UserFileViewModel>(xs);

            apiResponse = new ApiResponse<UserFileViewModel>
            {
                Valid = true,
                Result = userdata,
                Message = UserFileMessage.SaveUser
            };
            return apiResponse;
        }


        [HttpPost]
        [Route("UpdateUser")]
        public ApiResponse<UserFileViewModel> UpdateUser([FromBody] UserFileViewModel user)
        {
            ApiResponse<UserFileViewModel> apiResponse = new ApiResponse<UserFileViewModel>();

            var param1 = _mapper.Map<UserFile>(user);

            var xs = loginsvc.UpdateUser(param1);

            UserFileViewModel userdata = new UserFileViewModel();
            userdata = _mapper.Map<UserFileViewModel>(xs);

            apiResponse = new ApiResponse<UserFileViewModel>
            {
                Valid = true,
                Result = userdata,
                Message = UserFileMessage.UpdateUser
            };
            return apiResponse;
        }

        [HttpPost]
        [Route("DeleteUser")]
        public ApiResponse<UserFileViewModel> DeleteUser([FromBody] UserFileViewModel user)
        {
            ApiResponse<UserFileViewModel> apiResponse = new ApiResponse<UserFileViewModel>();

            var param1 = _mapper.Map<UserFile>(user);

            var xs = loginsvc.DeleteUser(param1);

            UserFileViewModel userdata = new UserFileViewModel();
            userdata = _mapper.Map<UserFileViewModel>(xs);

            apiResponse = new ApiResponse<UserFileViewModel>
            {
                Valid = true,
                Result = userdata,
                Message = UserFileMessage.DeleteUser
            };
            return apiResponse;
        }


        [HttpGet]
        [Route("GetAllUserFile")]
        public ApiResponse<List<UserFile>> GetAllUserFile()
        {
            ApiResponse<List<UserFile>> apiResponse = new ApiResponse<List<UserFile>>
            {
                Valid = true,
                Result = _mapper.Map<List<UserFile>>(loginsvc.GetAllUserFile()),
                Message = ""
            };
            return apiResponse;
        }

        [HttpGet]
        [Route("GetUserById")]

        public UserFile GetUserById(long id)
        {
            var user = loginsvc.GetUserById(id);

            return user;
        }

        [HttpGet]
        [Route("LoadDropdown")]
        public ResponseInfo LoadDropdown()
        {
            var objectresponse = new ResponseInfo();

            var LocationMaster = locationrepository.GetAsQueryable().Where(a => a.LocationMasterLocationDelStatus != true).Select(c => new
            {
                c.LocationMasterLocationId,
                c.LocationMasterLocationName
            }).ToList();
            var FinancialPeriods = financialrepository.GetAsQueryable().Where(x => x.FinancialPeriodsFsno != null).Select(k => new
            {
                k.FinancialPeriodsFsno,
                k.FinancialPeriodsStartDate,
                k.FinancialPeriodsEndDate
            });

            objectresponse.ResultSet = new
            {
                LocationMaster = LocationMaster,
                FinancialPeriods = FinancialPeriods
            };

            objectresponse.IsSuccess = true;
            return objectresponse;
        }

        [HttpGet]
        [Route("GetAllUser")]

        public ResponseInfo GetAllUser()
        {
            var objectresponse = new ResponseInfo();

            var user = userFileRepository.GetAsQueryable().AsNoTracking().Select(a => new
            {
                a.UserId,
                a.UserName
            }).ToList();
            objectresponse.ResultSet = new
            {
                result = user,
            };
            objectresponse.IsSuccess = true;
            return objectresponse;
        }
    }
}
