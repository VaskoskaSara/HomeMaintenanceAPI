using homeMaintenance.Application.Commands.TransactionInformation;
using homeMaintenance.Application.DTOs.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace HomeMaintenanceApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {

        protected IMediator mediator;
        public PaymentController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("create-intent")]
        public async Task<IActionResult> CreatePaymentIntent([FromBody] PaymentRequest request)
        {
            if (string.IsNullOrEmpty(request.PaymentMethodId) && request.Amount != 0)
            {
                return BadRequest(new { error = "Payment method is required." });
            }

            if (request.Amount == 0)
            {
                // by contract
                return Ok();
            }

            try
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = request.Amount * 100,
                    Currency = "usd",
                    PaymentMethod = request.PaymentMethodId,
                    Confirm = false,
                    AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                    {
                        Enabled = true,
                        AllowRedirects = "never"
                    }
                };

                var service = new PaymentIntentService();
                PaymentIntent intent = service.Create(options);

                return Ok(new { clientSecret = intent.ClientSecret });
            }
            catch (StripeException e)
            {
                return BadRequest(new { error = e.StripeError.Message });
            }
        }

        [HttpPost("save-transaction")]
        public async Task<IActionResult> SaveTransaction([FromBody] TransactionInfoCommand command)
        {
            var result = await mediator.Send(command).ConfigureAwait(false);

            return Ok(result);
        }
    }
}
