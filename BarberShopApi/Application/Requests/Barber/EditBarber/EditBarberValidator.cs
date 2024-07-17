using BarberShopApi.Application.Exceptions;
using FluentValidation;

namespace BarberShopApi.Application.Requests.Barber.EditBarber
{
    internal class EditBarberValidator : AbstractValidator<EditBarberRequest>
    {
        public EditBarberValidator()
        {
            RuleFor(request => request.Name).NotEmpty().WithMessage(ResourceErrorMessages.NAME_IS_EMPTY);
            RuleFor(request => request).Must(request => request.ClosingTime > request.OpeningTime).WithMessage(ResourceErrorMessages.INVALID_INTERVAL_HOURS);
        }
    }
}