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
    }
}