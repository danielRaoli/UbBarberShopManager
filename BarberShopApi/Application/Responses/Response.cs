namespace BarberShopApi.Application.Responses
{
    public class Response<T>
    {
        public T Data { get; set; }
        private readonly int _code = 200;
        private readonly string _message = string.Empty;

        public Response(T data, int code = 200, string message = null)
        {
            Data = data;
            _code = code;
            _message = message;
        }
    }
}
