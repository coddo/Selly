using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Selly.BusinessLogic.Core.Base;
using Selly.BusinessLogic.Utility;
using Selly.DataAdapter;
using Selly.DataLayer;
using Selly.DataLayer.Repositories;
using Selly.Models.Common.ClientServerInteraction;
using Selly.Models.Enums;
using Order = Selly.Models.Order;
using OrderItem = Selly.Models.OrderItem;

namespace Selly.BusinessLogic.Core
{
    public class OrderCore : BaseCore<OrderRepository, Order, DataLayer.Order>
    {
        private OrderCore()
        {
        }

        public new static async Task<Response<Order>> CreateAsync(Order order)
        {
            if (!ValidateOrder(order))
            {
                return ResponseFactory<Order>.CreateResponse(false, HttpStatusCode.BadRequest);
            }

            foreach (var orderItem in order.OrderItems.Where(orderItem => orderItem.Id == Guid.Empty))
            {
                orderItem.Id = Guid.NewGuid();
            }

            var result = await BaseCore<OrderRepository, Order, DataLayer.Order>.CreateAsync(order).ConfigureAwait(false);
            if (result == null)
            {
                return ResponseFactory<Order>.CreateResponse(false, HttpStatusCode.BadRequest);
            }

            return ResponseFactory<Order>.CreateResponse(true, HttpStatusCode.OK, result);
        }

        public new static async Task<Response<Order>> UpdateAsync(Order order)
        {
            if (!ValidateOrder(order, false))
            {
                return ResponseFactory<Order>.CreateResponse(false, HttpStatusCode.BadRequest);
            }

            var result = await BaseCore<OrderRepository, Order, DataLayer.Order>.UpdateAsync(order).ConfigureAwait(false);
            if (result == null)
            {
                return ResponseFactory<Order>.CreateResponse(false, HttpStatusCode.BadRequest);
            }

            return ResponseFactory<Order>.CreateResponse(true, HttpStatusCode.OK, result);
        }

        public static async Task<Response<IList<Order>>> GetAllAsync(string orderBy, bool orderAscending, IList<string> navigationProperties = null)
        {
            var orders = await GetAllAsync(navigationProperties);

            if (orders == null || orders.Count == 0)
            {
                return ResponseFactory<IList<Order>>.CreateResponse(false, HttpStatusCode.NoContent);
            }

            orders = SortOrders(orders, orderBy, orderAscending);

            return ResponseFactory<IList<Order>>.CreateResponse(true, HttpStatusCode.OK, orders);
        }

        public static async Task<Response<IList<Order>>> GetAllForUserAsync(Guid clientId, string orderBy, bool orderAscending,
            IList<string> navigationProperties = null)
        {
            using (var orderRepository = DataLayerUnitOfWork.Repository<OrderRepository>())
            {
                var orders = await orderRepository.GetListAsync(order => order.ClientId == clientId, navigationProperties);

                if (orders == null || orders.Count == 0)
                {
                    return ResponseFactory<IList<Order>>.CreateResponse(true, HttpStatusCode.NoContent);
                }

                var sortedOrders = SortOrders(orders.CopyTo<Order>(), orderBy, orderAscending);

                return ResponseFactory<IList<Order>>.CreateResponse(true, HttpStatusCode.OK, sortedOrders);
            }
        }

        #region Private methods

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

        private static bool ValidateOrder(Order order, bool tolerateEmptyGuids = true)
        {
            if (order.ClientId == Guid.Empty || order.OrderItems == null || order.OrderItems.Count == 0)
            {
                return false;
            }

            if (!tolerateEmptyGuids)
            {
                return order.OrderItems.All(orderItem => orderItem.Id != Guid.Empty);
            }

            if (!order.OrderItems.All(orderItem => orderItem.Price > 0))
            {
                return false;
            }

            return ValidateOrderItems(order.OrderItems, (SaleType) order.SaleType);
        }

        private static bool ValidateOrderItems(IEnumerable<OrderItem> orderItems, SaleType orderType)
        {
            switch (orderType)
            {
                case SaleType.Normal:
                    return orderItems.All(orderItem => orderItem.Quantity > 0);
                case SaleType.Return:
                    return orderItems.All(orderItem => orderItem.Quantity < 0);
                case SaleType.Exchange:
                    return orderItems.All(orderItem => !FloatingPointUtility.AreEqual(orderItem.Quantity, 0));
                default:
                    return false;
            }
        }

        #endregion
    }
}