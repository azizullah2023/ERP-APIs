using Inspire.Erp.Application.OrderApprovals.Interface;
using Inspire.Erp.Domain.Modals;
using Inspire.Erp.Infrastructure.Database.Repositoy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inspire.Erp.Application.OrderApprovals.Implementation
{
    public class OrderApprovals : IOrderApprovals
    {
        private IRepository<Domain.Entities.OrderApprovals> orderApprovals;

        public OrderApprovals(IRepository<Domain.Entities.OrderApprovals> orderApprovals)
        {
            this.orderApprovals = orderApprovals;
        }
        //Irfan 09 Dec 2023
        /// <summary>
        /// Name : DeleteOrderApproval
        /// Desc : used to delete order Approval
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Response<List<Domain.Entities.OrderApprovals>>> DeleteOrderApproval(Domain.Entities.OrderApprovals model)
        {
            List<Domain.Entities.OrderApprovals> data = new List<Domain.Entities.OrderApprovals>();
            try
            {
                this.orderApprovals.Delete(model);
                await Task.Delay(1000);
                var orderApp = this.orderApprovals.GetAll().ToList();
                foreach (var item in orderApp)
                {
                    data.Add(item);
                }
                return Response<List<Domain.Entities.OrderApprovals>>.Fail(data,"Data Found");
            }
            catch(Exception ex)
            {
                return Response<List<Domain.Entities.OrderApprovals>>.Fail(data, ex.Message);
            }
        }
        //Irfan 09 Dec 2023
        /// <summary>
        /// Name : GetOrderApprovals
        /// Desc : used to get all order Approvals
        /// </summary>
        /// <returns></returns>
        public async Task<Response<List<Domain.Entities.OrderApprovals>>> GetOrderApprovals()
        {
            List<Domain.Entities.OrderApprovals> data = new List<Domain.Entities.OrderApprovals>();
            try
            {
                await Task.Delay(1000);
                var orderApp = this.orderApprovals.GetAll().ToList();
                foreach (var item in orderApp)
                {
                    data.Add(item);
                }
                return Response<List<Domain.Entities.OrderApprovals>>.Fail(data, "Data Found");
            }
            catch (Exception ex)
            {
                return Response<List<Domain.Entities.OrderApprovals>>.Fail(data, ex.Message);
            }
        }
        //Irfan 09 Dec 2023
        /// <summary>
        /// Name : InsertOrderApproval
        /// Desc : used to get all order Approvals by voucher no
        /// </summary>
        /// <param name="voucherNO"></param>
        /// <returns></returns>
        public async Task<Response<List<Domain.Entities.OrderApprovals>>> GetOrderApprovalsByVoucherNo(string voucherNO)
        {
            List<Domain.Entities.OrderApprovals> data = new List<Domain.Entities.OrderApprovals>();
            try
            {
                await Task.Delay(1000);
                var orderApp = this.orderApprovals.GetAll().Where(x=>x.VoucherNo == voucherNO).ToList();
                foreach (var item in orderApp)
                {
                    data.Add(item);
                }
                return Response<List<Domain.Entities.OrderApprovals>>.Fail(data, "Data Found");
            }
            catch (Exception ex)
            {
                return Response<List<Domain.Entities.OrderApprovals>>.Fail(data, ex.Message);
            }
        }
        //Irfan 09 Dec 2023
        /// <summary>
        /// Name : GetOrderApprovalsByVoucherType
        /// Desc : used to get all order Approvals by voucher type
        /// </summary>
        /// <param name="voucherType"></param>
        /// <returns></returns>
        public async Task<Response<List<Domain.Entities.OrderApprovals>>> GetOrderApprovalsByVoucherType(string voucherType)
        {
            List<Domain.Entities.OrderApprovals> data = new List<Domain.Entities.OrderApprovals>();
            try
            {
                await Task.Delay(1000);
                var orderApp = this.orderApprovals.GetAsQueryable().Where(x=>x.VoucherType == voucherType).ToList();
                foreach (var item in orderApp)
                {
                    data.Add(item);
                }
                return Response<List<Domain.Entities.OrderApprovals>>.Fail(data, "Data Found");
            }
            catch (Exception ex)
            {
                return Response<List<Domain.Entities.OrderApprovals>>.Fail(data, ex.Message);
            }
        }

        //Irfan 09 Dec 2023
        /// <summary>
        /// Name : InsertOrderApproval
        /// Desc : used to insert new order Approval
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Response<List<Domain.Entities.OrderApprovals>>> InsertOrderApproval(Domain.Entities.OrderApprovals model)
        {
            List<Domain.Entities.OrderApprovals> data = new List<Domain.Entities.OrderApprovals>();
            try
            {
                this.orderApprovals.Insert(model);
                await Task.Delay(1000);
                var orderApp = this.orderApprovals.GetAsQueryable().ToList();
                foreach (var item in orderApp)
                {
                    data.Add(item);
                }
                return Response<List<Domain.Entities.OrderApprovals>>.Fail(data, "Data Found");
            }
            catch (Exception ex)
            {
                return Response<List<Domain.Entities.OrderApprovals>>.Fail(data, ex.Message);
            }
        }
        //Irfan 09 Dec 2023
        /// <summary>
        /// Name : UpdateOrderApproval
        /// Desc : used to update order Approval
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Response<List<Domain.Entities.OrderApprovals>>> UpdateOrderApproval(Domain.Entities.OrderApprovals model)
        {
            List<Domain.Entities.OrderApprovals> data = new List<Domain.Entities.OrderApprovals>();
            try
            {
                this.orderApprovals.Update(model);
                await Task.Delay(1000);
                var orderApp = this.orderApprovals.GetAll().ToList();
                foreach (var item in orderApp)
                {
                    data.Add(item);
                }
                return Response<List<Domain.Entities.OrderApprovals>>.Fail(data, "Data Found");
            }
            catch (Exception ex)
            {
                return Response<List<Domain.Entities.OrderApprovals>>.Fail(data, ex.Message);
            }
        }
    }
}
