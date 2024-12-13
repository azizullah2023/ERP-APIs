using Inspire.Erp.Application.OrderApprovals.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.Controllers
{
    [Route("api/PurchassOrderTracking")]
    [Produces("application/json")]
    [ApiController]
    public class OrderApprovalsController : Controller
    {
        private IOrderApprovals orderApprovals;

        public OrderApprovalsController(IOrderApprovals orderApprovals)
        {
            this.orderApprovals = orderApprovals;
        }

        [HttpPost]
        [Route("GetOrderApprovalsByVoucherNo/{voucherNO}")]
        //Irfan 09 Dec 2023
        /// <summary>
        /// Name : InsertOrderApprovel
        /// Desc : used to get all order Approvals by voucher no
        /// </summary>
        /// <param name="voucherNO"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetOrderApprovalsByVoucherNo(string voucherNO)
        {
            return Ok(await this.orderApprovals.GetOrderApprovalsByVoucherNo(voucherNO));
        }
        [HttpPost]
        [Route("GetOrderApprovalsByVoucherType/{voucherType}")]
        //Irfan 09 Dec 2023
        /// <summary>
        /// Name : GetOrderApprovalsByVoucherType
        /// Desc : used to get all order Approvals by voucher type
        /// </summary>
        /// <param name="voucherType"></param>
        /// <returns></returns>
        public async Task<IActionResult>  GetOrderApprovalsByVoucherType(string voucherType)
        {
            return Ok(await this.orderApprovals.GetOrderApprovalsByVoucherType(voucherType));
        }
        [HttpPost]
        [Route("DeleteOrderApprovel")]
        //Irfan 09 Dec 2023
        /// <summary>
        /// Name : DeleteOrderApprovel
        /// Desc : used to delete order approvel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IActionResult> DeleteOrderApprovel(Domain.Entities.OrderApprovals model)
        {
            return Ok(await this.orderApprovals.DeleteOrderApproval(model));
        }

        [HttpGet]
        [Route("GetOrderApprovals")]
        //Irfan 09 Dec 2023
        /// <summary>
        /// Name : GetOrderApprovals
        /// Desc : used to get all order Approvals
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetOrderApprovals()
        {
            return Ok(await this.orderApprovals.GetOrderApprovals());
        }

        [HttpPost]
        [Route("InsertOrderApprovel")]
        //Irfan 09 Dec 2023
        /// <summary>
        /// Name : InsertOrderApprovel
        /// Desc : used to insert new order approvel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IActionResult> InsertOrderApprovel(Domain.Entities.OrderApprovals model)
        {
            return Ok(await this.orderApprovals.InsertOrderApproval(model));
        }

        [HttpPost]
        [Route("UpdateOrderApprovel")]
        //Irfan 09 Dec 2023
        /// <summary>
        /// Name : UpdateOrderApprovel
        /// Desc : used to update order approvel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IActionResult> UpdateOrderApprovel(Domain.Entities.OrderApprovals model)
        {
            return Ok(await this.orderApprovals.UpdateOrderApproval(model));
        }
    }
}
