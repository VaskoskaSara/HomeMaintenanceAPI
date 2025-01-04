using homeMaintenance.Application.Queries.GetCities;
using homeMaintenance.Application.Queries.GetPositions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HomeMaintenanceApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PositionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetPositions()
        {
            var result = await _mediator.Send(new PositionsQuery()).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet("cities")]
        public async Task<IActionResult> GetCities()
        {
            var result = await _mediator.Send(new CitiesQuery()).ConfigureAwait(false);
            return Ok(result);
        }
    }
}
