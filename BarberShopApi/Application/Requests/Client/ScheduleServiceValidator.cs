using BarberShopApi.Application.Exceptions;
using BarberShopApi.Application.Requests.Barber;
using FluentValidation;

namespace BarberShopApi.Application.Requests.Client
{
    public class ScheduleServiceValidator : AbstractValidator<ScheduleServiceRequest>
    {
        public ScheduleServiceValidator()
        {
            RuleFor(request => request.Date).GreaterThanOrEqualTo(DateTime.UtcNow).WithMessage(ResourceErrorMessages.INVALID_DATE);
            RuleFor(request => request.Date).LessThanOrEqualTo(DateTime.Now.AddMonths(1)).WithMessage(ResourceErrorMessages.INVALID_DATE);  
        }
    }
}
