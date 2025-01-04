using homeMaintenance.Application.Commands.EmployeeDisabledDates;
using homeMaintenance.Application.Queries.GetBookedDatesByEmployee;
using homeMaintenance.Application.Queries.GetBookingsByEmployee;
using homeMaintenance.Application.Queries.GetBookingsByUser;
using homeMaintenance.Application.Queries.GetDisabledDatesByEmployee;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HomeMaintenanceApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("employee/{id}/bookings")]
        public async Task<IActionResult> GetBookingsByEmployee([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new BookingsByEmployeeQuery(id)).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet("user/{id}/bookings")]
        public async Task<IActionResult> GetBookingsByUser([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new BookingsByUserQuery(id)).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpPost("employee/manage-avaliability")]
        public async Task<IActionResult> SetEmployeeAvailability([FromBody] EmployeeDisabledDates command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet("employee/{id}/disabled-dates")]
        public async Task<IActionResult> GetDisabledDatesByEmployee([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new DisabledDatesByEmployee(id)).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet("employee/{id}/booked-dates")]
        public async Task<IActionResult> GetBookedDatesByEmployee([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new BookedDatesByEmployee(id)).ConfigureAwait(false);
            return Ok(result);
        }
    }
}
