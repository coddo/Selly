using System;
using System.Collections.Generic;
using System.Linq;
using Selly.BusinessLogic.Utility;
using Selly.Models;
using Selly.Models.Enums;

namespace Selly.BusinessLogic.Validation
{
    public static class SalesValidator
    {
        public static bool ValidateOrder(Order order, bool tolerateEmptyGuids = true)
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

        public static bool ValidateOrderItems(IEnumerable<OrderItem> orderItems, SaleType saleType)
        {
            return orderItems.All(orderItem => ValidateOrderItem(orderItem, saleType));
        }

        public static bool ValidateOrderItem(OrderItem orderItem, SaleType saleType)
        {
            switch (saleType)
            {
                case SaleType.Normal:
                    return orderItem.Quantity > 0;
                case SaleType.Return:
                    return orderItem.Quantity < 0;
                case SaleType.Exchange:
                    return !FloatingPointUtility.AreEqual(orderItem.Quantity, 0);
                default:
                    return false;
            }
        }
    }
}