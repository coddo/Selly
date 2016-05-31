using System.Net;
using Selly.Models.Enums.EnumExtensions;

namespace Selly.Models.Common.ClientServerInteraction
{
    public class Response
    {
        private Response(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        internal Response(bool isSuccess, int statusCode) : this(isSuccess)
        {
            StatusCode = statusCode;
        }

        internal Response(bool isSuccess, HttpStatusCode statusCode) : this(isSuccess)
        {
            StatusCode = statusCode.ToInt();
        }

        public bool IsSuccess { get; set; }

        public int StatusCode { get; set; }
    }

    public class Response<T> : Response
        where T : class
    {
        internal Response(bool isSuccess, int statusCode, T data) : base(isSuccess, statusCode)
        {
            StatusCode = statusCode;
            Data = data;
        }

        internal Response(bool isSuccess, HttpStatusCode statusCode, T data) : base(isSuccess, statusCode)
        {
            StatusCode = statusCode.ToInt();
            Data = data;
        }

        public T Data { get; set; }
    }
}