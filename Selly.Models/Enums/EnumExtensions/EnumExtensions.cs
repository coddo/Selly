using System;
using System.Net;

namespace Selly.Models.Enums.EnumExtensions
{
    public static class EnumExtensions
    {
        public static int ToInt(this HttpStatusCode statusCode)
        {
            return (int) statusCode;
        }

        public static int ToInt(this SaleType saleType)
        {
            return (int) saleType;
        }

        public static int ToInt(this OrderStatus orderStatus)
        {
            return (int) orderStatus;
        }
    }
}