using CoreApiWithSwagger.Controllers.Base;
using CoreApiWithSwagger.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreApiWithSwagger.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [ApiVersion("2.0")]
    public class AuthenticationController : MyApiController
    {
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("login")]
        public ActionResult<JwtTokenResponse> Login([FromBody] LoginRequest loginRequest)
        {
            var model = new JwtTokenResponse()
            {
                Token = "ExampleToken"
            };

            return Ok(model);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("login")]
        [MapToApiVersion("2.0")]
        public ActionResult<JwtTokenResponse> Loginv2_0([FromBody] LoginRequest loginRequest)
        {
            var model = new JwtTokenResponse()
            {
                Token = "ExampleToken"
            };

            return Ok(model);
        }
    }
}