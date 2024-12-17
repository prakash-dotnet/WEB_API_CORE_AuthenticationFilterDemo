using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WEB_API_CORE_AuthenticationFilterDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class TestController : ControllerBase
    {
        [HttpGet("secure-data")]
        [ServiceFilter(typeof(CustomAuthenticationFilter))] // Apply the filter only this method
        public IActionResult GetSecureData()
        {
            return Ok(new { message = "This is secure data." });
        }

        [AllowAnonymous]  // it is a public method. Any one can access
        [HttpGet("public-data")]
        public IActionResult GetPublicData()
        {
            return Ok(new { message = "This is public data." });
        }
    }
}
