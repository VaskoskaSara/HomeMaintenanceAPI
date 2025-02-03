using homeMaintenance.Application.DTOs.Requests;
using homeMaintenance.Application.Queries.GetEmployee;
using homeMaintenance.Application.Queries.GetEmployees;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HomeMaintenanceApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new EmployeeQuery(id)).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetEmployees([FromBody] EmployeeFilterRequest filter)
        {
            var query = new EmployeesQuery(filter.Cities, filter.Price, filter.Experience, filter.ExcludeByContract, filter.CategoryIds);
            var result = await _mediator.Send(query).ConfigureAwait(false);
            return Ok(result);
        }
    }
}
