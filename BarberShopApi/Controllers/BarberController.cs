using BarberShopApi.Application.Requests.Barber;
using BarberShopApi.Application.Requests.Barber.AddService;
using BarberShopApi.Application.Responses;
using BarberShopApi.Domain.Entities;
using BarberShopApi.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BarberShopApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "barbershop")]
    public class BarberController : ControllerBase
    {
        private readonly IBarberRepository _repository;

        public BarberController(IBarberRepository repository)
        {
            _repository = repository;
        }


        /// <summary>
        /// registra o serviço de um barbeiro
        /// </summary>
        /// <returns>Retorna a barbearia que foi criada</returns>
        /// <response code="200">Retorna a barbearia</response>
        /// <response code="401">Se o usuário não estiver autenticado</response>
        /// <response code="400">caso exista erro na declaração do item</response>
        /// <response code="404">caso o barbeiro não seja encontrado</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("{barberid}/service")]
        public async Task<IActionResult> AddBarberService([FromRoute] Guid barberid, [FromBody] AddBarberServiceRequest request)
        {
            var claimId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            request.UserId = Guid.Parse(claimId.Value);
            request.BarberId = barberid;
            var response = await _repository.AddBarberService(request);
            return Ok(response);
        }

        /// <summary>
        /// Remove o serviço de um barbeiro
        /// </summary>
        /// <returns>Retorna o serviço removido</returns>
        /// <response code="200">Retorna o serviço removido</response>
        /// <response code="401">Se o usuário não estiver autenticado</response>
        /// <response code="400">caso exista erro na declaração do item</response>
        /// <response code="404">caso o barbeiro ou serviço não seja encontrado</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
