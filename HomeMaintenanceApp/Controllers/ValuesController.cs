using Microsoft.AspNetCore.Mvc;

namespace HomeMaintenanceApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
    