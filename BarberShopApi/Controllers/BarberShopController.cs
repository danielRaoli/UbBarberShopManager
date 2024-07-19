using BarberShopApi.Application.Requests.Barber;
using BarberShopApi.Application.Requests.Barber.CreatedBarber;
using BarberShopApi.Application.Requests.Barber.EditBarber;
using BarberShopApi.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [Authorize(Roles = "barbershop")]
        [HttpPost]
        public async Task<IActionResult> CreateBarberShop(CreateBarberShopRequest request)
        {
            request.Validate();
            var claimId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            request.UserId = Guid.Parse(claimId.Value);

            var response = await _repository.CreateBarberShop(request);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBarberShop([FromRoute] Guid id)
        {
            var request = new GetBarberShopRequest { Id = id };
            var response = await _repository.GetBarberShop(request);

            return Ok(response);
        }

        [Authorize(Roles ="client")]
        [HttpGet]
        public async Task<IActionResult> GetAllBarberShops()
        {
            var response = await _repository.GetAllBarberShops();
            return Ok(response);
        }

        [Authorize(Roles = "barbershop")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBarberShop(UpdateBarberShopRequest request, [FromRoute] Guid id)
        {
           
            var claimId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            request.Validate();

            request.Id = id;
            request.UserId = Guid.Parse(claimId.Value);
            var response = await _repository.UpdateBarberShop(request);

            return Ok(response);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBarberShop([FromRoute] Guid id)
        {
            var claimId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var request = new DeleteBarberShopRequest { BarberShopId = id, UserId = Guid.Parse(claimId.Value) };

            var response = await _repository.DeleteBarberShop(request);

            return Ok(response);
        }

        [Authorize(Roles ="barbershop")]
        [HttpPost("{barbershopid}/barber")]
        public async Task<IActionResult> RegisterBarber([FromRoute] Guid barbershopid, [FromBody] CreateBarberRequest request)
        {
            var claimId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            request.BarberShopId = barbershopid;
            request.UserId = Guid.Parse(claimId.Value);

            var response = await _repository.CreateBarber(request);

            return Ok(response);
        }

        [Authorize(Roles = "barbershop")]
        [HttpPut("{barbershopid}/barber/{barberid}")]
        public async Task<IActionResult> UpdateBarber([FromRoute] Guid barbershopid, [FromRoute] Guid barberid, [FromBody] EditBarberRequest request)
        {
            var claimId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            request.Validate();

            request.UserId = Guid.Parse(claimId.Value);
            request.BarberShopId = barbershopid;
            request.BarberId = barberid;

            var response = await _repository.EditBarber(request);
            return Ok(response);
        }

        [Authorize(Roles = "barbershop")]
        [HttpDelete("{barbershopid}/barber/{barberid}")]
        public async Task<IActionResult> DeleteBarber([FromRoute] Guid barbershopid, [FromRoute] Guid barberid)
        {
            var claimId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var request = new DeleteBarberRequest
            {
                UserId = Guid.Parse(claimId.Value),
                BarberShopId = barbershopid,
                BarberId = barberid
            };

            var response = await _repository.DeleteBarber(request);
            return Ok(response);
        }


    }
}
