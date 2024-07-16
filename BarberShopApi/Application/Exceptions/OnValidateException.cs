using System.Net;

namespace BarberShopApi.Application.Exceptions
{
    public class OnValidateException(IList<string> errors) : BarberException(string.Empty)
    {
        private readonly IList<string> _errors = errors;


        public override IList<string> GetErrorMessages()
        {
            return _errors;
        }

        public override HttpStatusCode GetStatusCode()
        {
            return HttpStatusCode.BadRequest;
        }
    }
}
