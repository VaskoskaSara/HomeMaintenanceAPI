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
            
            return Ok(result);
        }

        [HttpGet("positions")]
        public async Task<IActionResult> GetPositions()
        {
            PositionsQuery query = new();
            var result = await mediator.Send(query).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpPost("employees")]
        public async Task<IActionResult> GetAllEmployees([FromBody] EmployeeFilterRequest filter)
        {
            EmployeesQuery query = new(filter.Cities, filter.Price, filter.Experience, filter.ExcludeByContract, filter.CategoryIds);
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

        [HttpGet("employee/{id}")]
        public async Task<IActionResult> GetEmployeeById([FromRoute] Guid id)
        {
            EmployeeQuery query = new(id);
            var result = await mediator.Send(query).ConfigureAwait(false);
            return Ok(result);
        }


        [HttpGet("manage-bookings/{id}")]
        public async Task<IActionResult> GetAllEmployees([FromRoute] Guid id)
        {
            BookingsByEmployeeQuery query = new BookingsByEmployeeQuery(id);
            var result = await mediator.Send(query).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpPost("manage-avaliability")]
        public async Task<IActionResult> PostAvailability([FromBody] EmployeeDisabledDates command)
        {
            var result = await mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet("manage-bookings/disabled-dates/{id}")]
        public async Task<IActionResult> GetDisabledDatesByEmployee([FromRoute] Guid id)
        {
            DisabledDatesByEmployee query = new DisabledDatesByEmployee(id);
            var result = await mediator.Send(query).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet("manage-bookings/booked-dates/{id}")]
        public async Task<IActionResult> GetBookedDatesByEmployee([FromRoute] Guid id)
        {
            BookedDatesByEmployee query = new BookedDatesByEmployee(id);
            var result = await mediator.Send(query).ConfigureAwait(false);
            return Ok(result);
        }

    }
}
