namespace Selly.Models.Common.Response
{
    public static class ResponseExtensions
    {
        public static int ToInt(this ResponseCode statusCode)
        {
            return (int)statusCode;
        }
    }
}
