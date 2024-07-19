using BarberShopApi.Application.Exceptions;

namespace BarberShopApi.Application.Requests.Barber.EditBarber
{
    public class EditBarberRequest
    {
        public Guid BarberShopId { get; set; }
        public Guid BarberId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int OpeningTime { get; set; }
        public int ClosingTime { get; set; }


        public void Validate()
        {
            var validator = new EditBarberValidator();
            var result = validator.Validate(this);

            if (result.IsValid is false)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new OnValidateException(errors);
            }
        }
    }
}
