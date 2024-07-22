using BarberShopApi.Application.Requests.Barber;
using BarberShopApi.Application.Requests.Barber.AddService;
using BarberShopApi.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BarberShopApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BarberController : ControllerBase
    {
        private readonly IBarberRepository _repository;

        public BarberController(IBarberRepository repository)
        {
            _repository = repository;
        }

        [Authorize(Roles = "barbershop")]
        [HttpPost("{barberid}/service")]
        public async Task<IActionResult> AddBarberService([FromRoute] Guid barberid, [FromBody] AddBarberServiceRequest request)
        {
            var claimId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            request.UserId = Guid.Parse(claimId.Value);
            request.BarberId = barberid;
            var response = await _repository.AddBarberService(request);
            return Ok(response);
        }

        [Authorize(Roles = "barbershop")]
        [HttpDelete("{barberid}/service/{serviceid}")]
        public async Task<IActionResult> DeleteBarberService([FromRoute] Guid barberid, [FromRoute] Guid serviceid)
        {
            var claimId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var request = new DeleteBarberServiceRequest { BarberId = barberid, ServiceId = serviceid, UserId = Guid.Parse(claimId.Value)};
            var response =await  _repository.DeleteBarberService(request);

            return Ok(response);
        }
    }
}
