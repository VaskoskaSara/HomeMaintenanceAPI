﻿using Azure.Identity;
using homeMaintenance.Application.Commands.UserLogin;
using homeMaintenance.Application.Commands.UserRegistration;
using homeMaintenance.Application.Queries.GetPositions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HomeMaintenanceApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        protected IMediator mediator;

        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("userRegistrations")]
        public async Task<IActionResult> UserRegistration([FromForm] UserRegistrationCommand command)
        {
            var result = await mediator.Send(command).ConfigureAwait(false);

            return Ok(result);
        }

        [HttpPost("userLogin")]
        public async Task<IActionResult> UserLogin([FromBody] UserLoginCommand command)
        {
            var result = await mediator.Send(command).ConfigureAwait(false);
            
            if(result)
                return Ok();
            else
            {
                throw new AuthenticationFailedException("Credentials are not valid");
            }
        }

        [HttpGet("positions")]
        public async Task<IActionResult> GetPositions()
        {
            PositionsQuery query = new();
            var result = await mediator.Send(query).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet("employees")]
        public async Task<IActionResult> GetAllEmployees([FromQuery] string? city, [FromQuery] int? price, [FromQuery] int? experience, [FromQuery] bool? byContract)
        {
            EmployeesQuery query = new(city, price, experience, byContract);
            var result = await mediator.Send(query).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet("cities")]
        public async Task<IActionResult> GetCities()
        {
            CitiesQuery query = new();
            var result = await mediator.Send(query).ConfigureAwait(false);
            return Ok(result);
        }
    }
}
