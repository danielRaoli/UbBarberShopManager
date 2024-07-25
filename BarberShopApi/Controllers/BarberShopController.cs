using BarberShopApi.Application.Requests.Barber;
using BarberShopApi.Application.Requests.Barber.CreatedBarber;
using BarberShopApi.Application.Requests.Barber.EditBarber;
using BarberShopApi.Application.Requests.BarberShop;
using BarberShopApi.Application.Responses;
using BarberShopApi.Domain.Entities;
using BarberShopApi.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Security.Claims;

namespace BarberShopApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "barbershop")]
    public class BarberShopController : ControllerBase
    {
        private readonly IBarberShopRepository _repository;

        public BarberShopController(IBarberShopRepository repository)
        {
            _repository = repository;
        }


        /// <summary>
        /// Cria uma barbearia caso o usuário não tenha nenhuma
        /// </summary>
        /// <returns>Retorna a barbearia que foi criada</returns>
        /// <response code="200">Retorna a barbearia</response>
        /// <response code="401">Se o usuário não estiver autenticado</response>
        /// <response code="400">caso exista erro na declaração do item</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateBarberShop(CreateBarberShopRequest request)
        {
            request.Validate();
            var claimId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            request.UserId = Guid.Parse(claimId.Value);

            var response = await _repository.CreateBarberShop(request);
            return Ok(response);
        }

        /// <summary>
        /// Recupera uma barbearia caso o usuário contenha
        /// </summary>
        /// <returns>Retorna uma barbearia</returns>
        /// <response code="200">Retorna a barbearia</response>
        /// <response code="404">Se o item não for encontrado</response>
        /// <response code="401">Se o usuário não estiver autenticado</response>
        [HttpGet("/mybarber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMyBarber()
        {
            var claimId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var request = new GetMyBarberRequest { UserId = Guid.Parse(claimId.Value) };
            var response = await _repository.GetMyBarber(request);

            return Ok(response);
        }


        /// <summary>
        /// Atualiza uma barbearia
        /// </summary>
        /// <param name="request">O nome da barbearia</param>
        /// <returns>Retorna a barbearia com as informações atualizadas.</returns>
        /// <response code="200">Retorna o item</response>
        /// <response code="404">Se o item não for encontrado</response>
        /// <response code="401">Se o usuário não estiver autenticado</response>
        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBarberShop(UpdateBarberShopRequest request)
        {
           
            var claimId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            request.Validate();
            request.UserId = Guid.Parse(claimId.Value);

            var response = await _repository.UpdateBarberShop(request);

            return Ok(response);

        }

        /// <summary>
        /// Deleta uma barbearia
        /// </summary>
        /// <returns>Retorna uma barbearia</returns>
        /// <response code="200">Retorna a barbearia removida</response>
        /// <response code="404">Se o item não for encontrado</response>
        /// <response code="401">Se o usuário não estiver autenticado</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBarberShop([FromRoute] Guid id)
        {
            var claimId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var request = new DeleteBarberShopRequest { BarberShopId = id, UserId = Guid.Parse(claimId.Value) };

            var response = await _repository.DeleteBarberShop(request);

            return Ok(response);
        }

        /// <summary>
        /// registra um barbeiro na barbearia do usuário
        /// </summary>
        /// <returns>Retorna o barbeiro criado</returns>
        /// <response code="201">Retorna a barbearia criado</response>
        /// <response code="404">Se o item não for encontrado</response>
        /// <response code="401">Se o usuário não estiver autenticado</response>
        ///<response code="400">Se tiver algum erro na validação barbeiro</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("/barber")]                                
        public async Task<IActionResult> RegisterBarber([FromBody] CreateBarberRequest request)
        {
            var claimId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            request.UserId = Guid.Parse(claimId.Value);

            var response = await _repository.CreateBarber(request);

            return Ok(response);                                              
        }

        /// <summary>
        /// Pega o historico de agendamentos de um barbeiro especifico 
        /// </summary>
        /// <returns>Retorna uma lista de agendamentos</returns>
        /// <response code="200">Retorna uma lista de agendamento</response>
        /// <response code="404">Se o barbeiro não for encontrado</response>
        /// <response code="401">Se o usuário não estiver autenticado</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{barberid}/barber/history")]
        public async Task<IActionResult> GetBarberHistorySchedule([FromRoute] Guid barberid)
        {
            var claimId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var request = new GetBarberHistoryScheduleRequest { BarberId = barberid, UserId = Guid.Parse(claimId.Value)};

            var response = await _repository.GetBarberHistory(request);

            return Ok(response);
        }

        /// <summary>
        ///     Atualiza um barbeiro
        /// </summary>
        /// <returns>Retorna o barbeiro atualizado</returns>
        /// <response code="201">Retorna a barbearia criado</response>
        /// <response code="404">Se o barbeiro não for encontrado</response>
        /// <response code="401">Se o usuário não estiver autenticado</response>
        ///<response code="400">Se tiver algum erro na validação barbeiro</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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


        /// <summary>
        /// Deleta um barbeiro
        /// </summary>
        /// <returns>Retorna o barbeiro removido</returns>
        /// <response code="200">Retorna o barbeiro removido</response>
        /// <response code="404">Se o barbeiro ou barbearia não for encontrado</response>
        /// <response code="401">Se o usuário não estiver autenticado</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
