using System.Net;

namespace BarberShopApi.Application.Exceptions
{
    public abstract class BarberException(string message) : SystemException(message)
    {
        public abstract HttpStatusCode GetStatusCode();
        public abstract IList<string> GetErrorMessages();
    }
}
 