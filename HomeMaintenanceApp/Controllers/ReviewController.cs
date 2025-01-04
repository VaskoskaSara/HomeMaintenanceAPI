using homeMaintenance.Application.Commands.EmployeeReview;
using homeMaintenance.Application.Queries.GetPositions;
using homeMaintenance.Application.Queries.GetReviewsByUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HomeMaintenanceApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReviewController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> SubmitReview([FromForm] EmployeeReviewCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetReviewsByUser([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new ReviewsByUser(id)).ConfigureAwait(false);
            return Ok(result);
        }
    }
}
