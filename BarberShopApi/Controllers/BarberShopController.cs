using BarberShopApi.Application.Requests.Barber;
using BarberShopApi.Application.Requests.Barber.CreatedBarber;
using BarberShopApi.Application.Requests.Barber.EditBarber;
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

        // Somente contas com role BarberShop
        [HttpPost]
        public async Task<IActionResult> CreateBarberShop(CreateBarberShopRequest request)
        {
            request.Validate();
            var response = await _repository.CreateBarberShop(request);
            return Ok(response);
        }
        // Todos os  usuarios
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBarberShop([FromRoute] Guid id)
        {
            var request = new GetBarberShopRequest { Id = id };
            var response = await _repository.GetBarberShop(request);

            return Ok(response);
        }

        //Somente Usuario Comum
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

        [HttpPost("{barbershopid}/barber")]
        public async Task<IActionResult> RegisterBarber([FromRoute] Guid barbershopid, [FromBody] CreateBarberRequest request)
        {
            request.BarberShopId = barbershopid;
            var response = _repository.CreateBarber(request);

            return Ok(response);
        }


        [HttpPut("{barbershopid}/barber/{barberid}")]
        public async Task<IActionResult> UpdateBarber([FromRoute] Guid barbershopid, [FromRoute] Guid barberid, [FromBody] EditBarberRequest request)
        {
            request.Validate();
            request.BarberShopId = barbershopid;
            request.BarberId = barberid;

            var response = _repository.EditBarber(request);
            return Ok(response);
        }

        [HttpDelete("{barbershopid}/barber/{barberid}")]
        public async Task<IActionResult> DeleteBarber([FromRoute] Guid barbershopid, [FromRoute] Guid barberid)
        {
            var request = new DeleteBarberRequest
            {
                BarberShopId = barbershopid,
                BarberId = barberid
            };

            var response = _repository.DeleteBarber(request);
            return Ok(response);
        }


    }
}
