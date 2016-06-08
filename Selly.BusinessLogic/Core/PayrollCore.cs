using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Selly.BusinessLogic.Core.Base;
using Selly.DataAdapter;
using Selly.DataLayer.Extensions;
using Selly.DataLayer.Extensions.Repositories;
using Selly.Models;
using Selly.Models.Common.Response;
using Selly.Models.Enums;
using Selly.Models.Enums.EnumExtensions;

namespace Selly.BusinessLogic.Core
{
    public class PayrollCore : BaseSinglePkCore<PayrollRepository, Payroll, DataLayer.Payroll>
    {
        private PayrollCore()
        {
        }

        public static async Task<Response<Payroll>> GetForOrder(Guid orderId, IList<string> navigationProperties = null)
        {
            using (var payrollRepository = DataLayerUnitOfWork.Repository<PayrollRepository>())
            {
                var payroll = await payrollRepository.GetForOrder(orderId, navigationProperties).ConfigureAwait(false);
                if (payroll == null)
                {
                    return ResponseFactory<Payroll>.CreateResponse(false, ResponseCode.ErrorNotFound);
                }

                return ResponseFactory<Payroll>.CreateResponse(true, ResponseCode.Success, payroll.CopyTo<Payroll>());
            }
        }

        public static async Task<Response<IList<Payroll>>> GetAllForClient(Guid clientId, IList<string> navigationProperties = null)
        {
            using (var payrollRepository = DataLayerUnitOfWork.Repository<PayrollRepository>())
            {
                var payrolls = await payrollRepository.GetAllForClient(clientId, navigationProperties).ConfigureAwait(false);
                if (payrolls == null)
                {
                    return ResponseFactory<IList<Payroll>>.CreateResponse(false, ResponseCode.ErrorNotFound);
                }

                return ResponseFactory<IList<Payroll>>.CreateResponse(true, ResponseCode.Success, payrolls.CopyTo<Payroll>());
            }
        }

        public static async Task<Response<Payroll>> MakePayment(Guid orderId, Guid clientId)
        {
            using (var unitOfWork = new DataLayerUnitOfWork())
            {
                var orderRepository = unitOfWork.TrackingRepository<OrderRepository>();
                var payrollRepository = unitOfWork.TrackingRepository<PayrollRepository>();

                var order = await orderRepository.GetAsync(orderId, new[]
                {
                    nameof(Order.OrderItems)
                }).ConfigureAwait(false);
                if (order == null)
                {
                    return ResponseFactory<Payroll>.CreateResponse(false, ResponseCode.ErrorInvalidInput);
                }

                var payroll = new DataLayer.Payroll
                {
                    ClientId = clientId,
                    OrderId = orderId,
                    Date = DateTime.Now,
                    Value = order.OrderItems.Sum(orderItem => orderItem.Price * orderItem.Quantity)
                };
                payroll = await payrollRepository.CreateAsync(payroll, true).ConfigureAwait(false);

                if (payroll == null)
                {
                    return ResponseFactory<Payroll>.CreateResponse(false, ResponseCode.Error);
                }

                order.Status = OrderStatus.Finalized.ToInt();
                var updatedOrder = await orderRepository.UpdateAsync(order, true).ConfigureAwait(false);

                if (updatedOrder == null)
                {
                    return ResponseFactory<Payroll>.CreateResponse(false, ResponseCode.Error);
                }

                return ResponseFactory<Payroll>.CreateResponse(true, ResponseCode.Success, payroll.CopyTo<Payroll>());
            }
        }
    }
}