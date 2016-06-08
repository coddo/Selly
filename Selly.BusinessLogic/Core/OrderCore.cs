using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Selly.BusinessLogic.Core.Base;
using Selly.BusinessLogic.Validation;
using Selly.DataAdapter;
using Selly.DataLayer.Extensions;
using Selly.DataLayer.Extensions.Repositories;
using Selly.Models;
using Selly.Models.Common.Response;
using Selly.Models.Enums;
using Selly.Models.Enums.EnumExtensions;
using Payroll = Selly.DataLayer.Payroll;

namespace Selly.BusinessLogic.Core
{
    public class OrderCore : BaseSinglePkCore<OrderRepository, Order, DataLayer.Order>
    {
        private OrderCore()
        {
        }

        public new static async Task<Response<Order>> CreateAsync(Order order, bool refreshFromDb = false, IList<string> navigationProperties = null)
        {
            Parallel.ForEach(order.OrderItems.Where(orderItem => orderItem.Id == Guid.Empty), orderItem => { orderItem.Id = Guid.NewGuid(); });

            if (!SalesValidator.ValidateOrder(order))
            {
                return ResponseFactory<Order>.CreateResponse(false, ResponseCode.ErrorInvalidInput);
            }

            order.Date = DateTime.Now;
            order.Status = OrderStatus.Created.ToInt();

            using (var orderRepository = DataLayerUnitOfWork.Repository<OrderRepository>())
            {
                var createdOrder =
                    await orderRepository.CreateAsync(order.CopyTo<DataLayer.Order>(), refreshFromDb, navigationProperties).ConfigureAwait(false);

                if (createdOrder == null)
                {
                    return ResponseFactory<Order>.CreateResponse(false, ResponseCode.Error);
                }

                return ResponseFactory<Order>.CreateResponse(true, ResponseCode.Success, createdOrder.CopyTo<Order>());
            }
        }

        public new static async Task<Response<Order>> UpdateAsync(Order order, bool refreshFromDb = false, IList<string> navigationProperties = null)
        {
            if (!SalesValidator.ValidateOrder(order, false))
            {
                return ResponseFactory<Order>.CreateResponse(false, ResponseCode.ErrorInvalidInput);
            }

            using (var orderRepository = DataLayerUnitOfWork.Repository<OrderRepository>())
            {
                var updatedOrder =
                    await orderRepository.UpdateAsync(order.CopyTo<DataLayer.Order>(), refreshFromDb, navigationProperties).ConfigureAwait(false);

                if (updatedOrder == null)
                {
                    return ResponseFactory<Order>.CreateResponse(false, ResponseCode.Error);
                }

                return ResponseFactory<Order>.CreateResponse(true, ResponseCode.Success, updatedOrder.CopyTo<Order>());
            }
        }

        public static async Task<Response<IList<Order>>> GetAllAsync(string orderBy, bool orderAscending, IList<string> navigationProperties = null)
        {
            using (var orderRepository = DataLayerUnitOfWork.Repository<OrderRepository>())
            {
                var orders = await orderRepository.GetAllAsync(navigationProperties).ConfigureAwait(false);

                if (orders == null || orders.Count == 0)
                {
                    return ResponseFactory<IList<Order>>.CreateResponse(true, ResponseCode.SuccessNoContent);
                }

                var sortedOrders = SortOrders(orders.CopyTo<Order>(), orderBy, orderAscending);

                return ResponseFactory<IList<Order>>.CreateResponse(true, ResponseCode.Success, sortedOrders);
            }
        }

        public static async Task<Response<IList<Order>>> GetAllForUserAsync(Guid clientId, string orderBy, bool orderAscending,
            IList<string> navigationProperties = null)
        {
            using (var orderRepository = DataLayerUnitOfWork.Repository<OrderRepository>())
            {
                var orders = await orderRepository.GetByClientId(clientId, navigationProperties).ConfigureAwait(false);

                if (orders == null || orders.Count == 0)
                {
                    return ResponseFactory<IList<Order>>.CreateResponse(true, ResponseCode.SuccessNoContent);
                }

                var sortedOrders = SortOrders(orders.CopyTo<Order>(), orderBy, orderAscending);

                return ResponseFactory<IList<Order>>.CreateResponse(true, ResponseCode.Success, sortedOrders);
            }
        }

        public static async Task<Response<Order>> Finalize(Order order)
        {
            if (!SalesValidator.ValidateOrder(order, false))
            {
                return ResponseFactory<Order>.CreateResponse(false, ResponseCode.ErrorInvalidInput);
            }

            order.Status = OrderStatus.Finalized.ToInt();

            using (var unitOfWork = new DataLayerUnitOfWork())
            {
                var orderRepository = unitOfWork.TrackingRepository<OrderRepository>();

                var dbModel = order.CopyTo<DataLayer.Order>();
                var updatedOrder = await orderRepository.UpdateAsync(dbModel).ConfigureAwait(false);

                if (updatedOrder == null)
                {
                    return ResponseFactory<Order>.CreateResponse(false, ResponseCode.Error);
                }

                return await CreatePayroll(order, unitOfWork, orderRepository);
            }
        }

        public static async Task<Response<Order>> Cancel(Order order)
        {
            if (!SalesValidator.ValidateOrder(order, false))
            {
                return ResponseFactory<Order>.CreateResponse(false, ResponseCode.ErrorInvalidInput);
            }

            order.Status = OrderStatus.Cancelled.ToInt();

            using (var orderRepository = DataLayerUnitOfWork.Repository<OrderRepository>())
            {
                var dbModel = order.CopyTo<DataLayer.Order>();
                var updatedOrder = await orderRepository.UpdateAsync(dbModel).ConfigureAwait(false);

                if (updatedOrder == null)
                {
                    return ResponseFactory<Order>.CreateResponse(false, ResponseCode.Error);
                }

                return ResponseFactory<Order>.CreateResponse(true, ResponseCode.Success, order);
            }
        }

        #region Private methods

        private static async Task<Response<Order>> CreatePayroll(Order order, DataLayerUnitOfWork unitOfWork, OrderRepository orderRepository)
        {
            var orderWithItems = await orderRepository.GetAsync(order.Id, new[]
            {
                nameof(Order.OrderItems)
            }).ConfigureAwait(false);

            var payroll = new Payroll
            {
                ClientId = order.ClientId,
                Date = DateTime.Now,
                OrderId = order.Id,
                Value = orderWithItems.OrderItems.Sum(orderItem => orderItem.Price * orderItem.Quantity)
            };

            payroll = await unitOfWork.TrackingRepository<PayrollRepository>().CreateAsync(payroll).ConfigureAwait(false);
            if (payroll == null)
            {
                return ResponseFactory<Order>.CreateResponse(false, ResponseCode.Error);
            }

            return ResponseFactory<Order>.CreateResponse(true, ResponseCode.Success, order);
        }

        private static IList<Order> SortOrders(IList<Order> orders, string orderBy, bool orderAscending)
        {
            if (string.IsNullOrEmpty(orderBy))
            {
                return orders;
            }

            Func<Order, object> orderClause;
            switch (orderBy)
            {
                case nameof(Order.ClientId):
                    orderClause = order => order.ClientId;
                    break;
                case nameof(Order.CurrencyId):
                    orderClause = order => order.CurrencyId;
                    break;
                case nameof(Order.Date):
                    orderClause = order => order.Date;
                    break;
                case nameof(Order.SaleType):
                    orderClause = order => order.SaleType;
                    break;
                case nameof(Order.Status):
                    orderClause = order => order.Status;
                    break;
                default:
                    return orders;
            }

            return orderAscending ? orders.OrderBy(orderClause).ToArray() : orders.OrderByDescending(orderClause).ToArray();
        }

        #endregion
    }
}