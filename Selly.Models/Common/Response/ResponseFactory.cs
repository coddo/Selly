namespace Selly.Models.Common.Response
{
    public static class ResponseFactory
    {
        public static Response CreateResponse(bool success, int code)
        {
            return new Response(success, code);
        }

        public static Response CreateResponse(bool success, ResponseCode code)
        {
            return new Response(success, code);
        }
    }

    public static class ResponseFactory<T>
        where T : class
    {
        public static Response<T> CreateResponse(bool success, int code, T data = null)
        {
            return new Response<T>(success, code, data);
        }

        public static Response<T> CreateResponse(bool success, ResponseCode code, T data = null)
        {
            return new Response<T>(success, code, data);
        }
    }
}