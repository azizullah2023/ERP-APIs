using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Inspire.Erp.Application.Master;
using Inspire.Erp.Application.Account.Interfaces;
using Inspire.Erp.Domain.Entities;
using Inspire.Erp.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inspire.Erp.Application.Account;
using Inspire.Erp.Web.Common;
using Inspire.Erp.Application.Common;
using Inspire.Erp.Application.Authentication.Inerfaces;
using Inspire.Erp.Domain.Modals;

namespace Inspire.Erp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChangPasswordController : ControllerBase
    {
        private IChangePassword vp;
        private readonly IMapper _mapper;
        public ChangPasswordController(IChangePassword _vp)
        {
            vp = _vp;
        }
        [HttpPost("ChangePasswordResponse")]
        public Task<bool> ChangePasswordResponse(ChangePasswordResponse Obj)
        {
            try
            {

                return vp.UpdatePassword(Obj);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
