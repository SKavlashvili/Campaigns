using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Campaigns.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            return Ok();
        }
    }
}
