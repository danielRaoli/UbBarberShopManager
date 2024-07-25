using BarberShopApi.Application.Requests.Barber;
using BarberShopApi.Application.Requests.Client;
using BarberShopApi.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BarberShopApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "client")]
    public class ClientController(IClientRepository repository) : ControllerBase
    {
        private readonly IClientRepository _repository = repository;



        /// <summary>
        /// retornar uma barbearia com todos os seus barbeiros
        /// </summary>
        /// <returns>Retorna uma barbearia</returns> 
        /// <response code="200">Retorna uma barbearia</response>
        /// <response code="404">Se a barbearia não for encontrada</response>
        /// <response code="401">Se o usuário não estiver autenticado</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("barbershop/{barbershopid}")]
        public async Task<IActionResult> GetBarberShop([FromRoute] Guid barbershopid)
        {
            var request = new GetBarberShopRequest { Id = barbershopid };
            var response = await _repository.GetBarberShop(request);

            return Ok(response);
        }


        /// <summary>
        /// retorna todas as barbearias existentes
        /// </summary>
        /// <returns>Retorna uma lista de barbearias</returns>
        /// <response code="200">Retorna uma lista de barbearias</response>
        /// <response code="401">Se o usuário não estiver autenticado</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("barbershop")]
        public async Task<IActionResult> GetAllBarberShops()
        {
            var response = await _repository.GetAllBarberShops();
            return Ok(response);
        }


        /// <summary>
        ///  agenda um serviço para o cliente
        /// </summary>
        /// <returns>Retorna o agendamento</returns>
        /// <response code="200">Retorna o agendamento</response>
        /// <response code="404">Se o serviço não for encontrado</response>
        /// <response code="401">Se o usuário não estiver autenticado</response>
        /// <response code="400">Problema na validação do agendamento</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("schedule/{serviceid}")]
        public async Task<IActionResult> ScheduleService([FromRoute] Guid serviceid, [FromBody] ScheduleServiceRequest request)
        {
            request.Validate();
            var claimId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            request.ServiceId = serviceid;
            request.UserId = Guid.Parse(claimId.Value);
            var response = await _repository.Schedule(request);
            return Ok(response);
        }

        /// <summary>
        ///  busca todos os agendamentos feitos por um cliente
        /// </summary>
        /// <returns>Retorna uma lista de agendamento</returns>
        /// <response code="200">Retorna lista de agendamento</response>
        /// <response code="401">Se o usuário não estiver autenticado</response>
        /// <response code="400">Problema na validação do agendamento</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("schedule")]
        public async Task<IActionResult> GetScheduleHistory()
        {
            var claimId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var request = new GetUserScheduleHistory { UserId = Guid.Parse(claimId.Value) };
            var response = await _repository.GetUserScheduleHistory(request);   

            return Ok(response);    
        }
    }
}
