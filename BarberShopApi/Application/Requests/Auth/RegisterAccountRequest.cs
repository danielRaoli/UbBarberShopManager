using BarberShopApi.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace BarberShopApi.Application.Requests.Auth
{

    public class RegisterAccountRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        [Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public Role Role { get; set; }
    }
}
