using System.Net;


namespace BarberShopApi.Application.Exceptions
{
    public class NotFoundException(string message) : BarberException(message)
    {
        public override IList<string> GetErrorMessages()
        {
            return [Message];
        }

        public override HttpStatusCode GetStatusCode()
        {
            return HttpStatusCode.NotFound;
        }
    }
}
