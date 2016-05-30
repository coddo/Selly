using System.Net;

namespace Selly.Models.Common.ClientServerInteraction
{
    public static class ResponseFactory
    {
        public static Response CreateResponse(bool isSuccess, int code)
        {
            return new Response(isSuccess, code);
        }

        public static Response CreateResponse(bool isSuccess, HttpStatusCode code)
        {
            return new Response(isSuccess, code);
        }
    }

    public static class ResponseFactory<T>
        where T : class
    {
        public static Response<T> CreateResponse(bool isSuccess, int code, T data)
        {
            return new Response<T>(isSuccess, code, data);
        }

        public static Response<T> CreateResponse(bool isSuccess, HttpStatusCode code, T data)
        {
            return new Response<T>(isSuccess, code, data);
        }
    }
}