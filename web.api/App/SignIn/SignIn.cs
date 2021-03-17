using Microsoft.AspNetCore.Mvc;

namespace web.api.App.SignIn
{
    [ApiController]
    [Route("signin")]
    public class SignIn : ControllerBase
    {
        // GET
        [HttpGet("google")]
        public IActionResult Google()
        {
            return Ok();
        }
    }
}