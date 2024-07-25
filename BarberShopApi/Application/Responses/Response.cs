namespace BarberShopApi.Application.Responses
{
    public class Response<T>(T data, int code = 200, string message = null)
    {
        public T Data { get; set; } = data;
        public int Code { get; private set; } = code;
        public string Message { get; private set; } = message;
    }
}
