using BarberShopApi.Application.Exceptions;
using BarberShopApi.Domain.Entities;

namespace BarberShopApi.Application.Requests.Client
{
    public class ScheduleServiceRequest
    {
        public Guid ServiceId { get; set; }
        public Guid UserId { get; set; }
        public string Hour { get; set; }
        public DateTime Date { get; set; }


        public void Validate()
        {
            var validator = new ScheduleServiceValidator();
            var result = validator.Validate(this);

            if (result.IsValid is false)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new OnValidateException(errors);
            }
        }
    }
}
