using AutoMapper;
using Inspire.Erp.Application.Print;
using Inspire.Erp.Web.Common;
using Inspire.Erp.Web.ViewModels.Printer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inspire.Erp.Domain.Entities;
using Microsoft.Data.SqlClient;

namespace Inspire.Erp.Web.Controllers
{
    [Route("api/broadcast")]
    [ApiController]
    public class BroadcastController : ControllerBase
    {
        private readonly INotificationHub _notificationHub;
        private readonly IPrint print;
        private readonly IMapper _mapper;
        public BroadcastController(INotificationHub notificationHub, IPrint _print, IMapper mapper)
        {
            _notificationHub = notificationHub;
            print = _print;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task Get(string user, string message)
        {
            //await _notificationHub.BroadcastToUser(message, user);
            await _notificationHub.SendMessage(user, message);

        }

        [HttpPost]
        [Route("ping")]
        public async Task post([FromQuery]string user, [FromBody]ApiMethod message)
        {
            //await _notificationHub.BroadcastToUser(message, user);
            await _notificationHub.Send(user, message);

        }

        [HttpPost]
        [Route("register")]
        public void RegisterUser(PrinterConnectionViewModel printerConnectionViewModel)
        {
            var result = _mapper.Map<PrinterConnection>(printerConnectionViewModel);
            print.InsertPrintConncetionByService(result);
            //await _hub.Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        [HttpGet]
        [Route("GetAllPrinterConnections")]
        public async Task<List<PrinterConnection>> GetAllPrinterConnections()
        {
            
            return await print.GetAllAvailablePrinterUsers("SP_GetAllAvailablePrinterConnections");
        }

        [HttpGet]
        [Route("GetAllActivePrinterConnections/{status}")]
        public async Task<List<PrinterConnection>> GetAllActivePrinterConnections(bool status)
        {
            var param = new SqlParameter[]{
                        new SqlParameter() {
                            ParameterName = "@IsActive",
                            SqlDbType =  System.Data.SqlDbType.Bit,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = status
                        }};
            return await print.GetAllActiveAvailablePrinterUsers("SP_GetAllActiveAvailablePrinterConnections @IsActive", param);
        }
    }
}
