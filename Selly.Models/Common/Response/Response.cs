namespace Selly.Models.Common.Response
{
    public class Response
    {
        private Response(bool success)
        {
            Success = success;
        }

        internal Response(bool success, int statusCode) : this(success)
        {
            StatusCode = statusCode;
        }

        internal Response(bool success, ResponseCode statusCode) : this(success)
        {
            StatusCode = statusCode.ToInt();
        }

        public bool Success { get; set; }

        public int StatusCode { get; set; }
    }

    public class Response<T> : Response
        where T : class
    {
        internal Response(bool success, int statusCode, T data) : base(success, statusCode)
        {
            StatusCode = statusCode;
            Data = data;
        }

        internal Response(bool success, ResponseCode statusCode, T data) : base(success, statusCode)
        {
            StatusCode = statusCode.ToInt();
            Data = data;
        }

        public T Data { get; set; }
    }
}