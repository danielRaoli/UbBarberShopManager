using BarberShopApi.Application.Requests.Barber;
using BarberShopApi.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BarberShopApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BarberShopController : ControllerBase
    {
        private readonly IBarberShopRepository _repository;

        public BarberShopController(IBarberShopRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBarberShop(CreateBarberShopRequest request)
        {
            request.Validate();
            var response = await _repository.CreateBarberShop(request);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBarberShop([FromRoute] Guid id)
        {
            var request = new GetBarberShopRequest { Id = id };
            var response = await _repository.GetBarberShop(request);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBarberShops()
        {
            var response = await _repository.GetAllBarberShops();
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBarberShop(UpdateBarberShopRequest request, [FromRoute] Guid id)
        {
            request.Validate();
            request.Id = id;
            var response = await _repository.UpdateBarberShop(request);

            return Ok(response);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBarberShop([FromRoute] Guid id)
        {
            var request = new DeleteBarberShopRequest { BarberShopId = id };
            var response = await _repository.DeleteBarberShop(request);

            return Ok(response);
        }

        [HttpPost("{barbershopid}")]
        public async Task<IActionResult> RegisterBarber([FromRoute] Guid barbershopid, [FromBody]CreateBarberRequest request)
        {
            request.BarberShopId = barbershopid;
            var response = _repository.CreateBarber(request);

            return Ok(response);
        }

    }
}
