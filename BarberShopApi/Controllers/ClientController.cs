﻿using BarberShopApi.Application.Requests.Barber;
using BarberShopApi.Application.Requests.Client;
using BarberShopApi.Domain.Enums;
using BarberShopApi.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BarberShopApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientController(IClientRepository repository) : ControllerBase
    {
        private readonly IClientRepository _repository = repository;


        [Authorize(Roles = "client")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBarberShop([FromRoute] Guid id)
        {
            var request = new GetBarberShopRequest { Id = id };
            var response = await _repository.GetBarberShop(request);

            return Ok(response);
        }

      
        [HttpGet]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> GetAllBarberShops()
        {
            var response = await _repository.GetAllBarberShops();
            return Ok(response);
        }

        [Authorize(Roles = "client")]
        [HttpPost("/schedule/{serviceid}")]
        public async Task<IActionResult> ScheduleService([FromRoute] Guid serviceid, [FromBody] ScheduleServiceRequest request)
        {
            request.Validate();
            var claimId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            request.ServiceId = serviceid;
            request.UserId = Guid.Parse(claimId.Value);
            var response = await _repository.Schedule(request);
            return Ok(response);
        }

        [Authorize(Roles = "client")]
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
