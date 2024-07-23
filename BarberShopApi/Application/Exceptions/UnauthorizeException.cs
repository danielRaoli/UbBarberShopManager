using System.Net;

namespace BarberShopApi.Application.Exceptions
{
    public class UnauthorizeException(string message) : BarberException(message)
    {
        public override IList<string> GetErrorMessages()
        {
            return [Message];
        }

        public override HttpStatusCode GetStatusCode()
        {
            return HttpStatusCode.Unauthorized;
        }
    }
}
