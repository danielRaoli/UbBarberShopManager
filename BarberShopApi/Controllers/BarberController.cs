using BarberShopApi.Application.Requests.Barber;
using BarberShopApi.Application.Requests.Barber.AddService;
using BarberShopApi.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("{barberid}/service")]
        public async Task<IActionResult> AddBarberService([FromRoute] Guid barberid, [FromBody] AddBarberServiceRequest request)
        {
            request.BarberId = barberid;
            var response = _repository.AddBarberService(request);
            return Ok(response);
        }

        [HttpDelete("{barberid}/service/{serviceid}")]
        public async Task<IActionResult> DeleteBarberService([FromRoute] Guid barberid, [FromRoute] Guid serviceid)
        {
            var request = new DeleteBarberServiceRequest { BarberId = barberid, ServiceId = serviceid};
            var response = _repository.DeleteBarberService(request);

            return Ok(response);
        }
    }
}
