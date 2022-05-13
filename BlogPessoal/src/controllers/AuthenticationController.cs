using BlogPessoal.src.dtos;
using BlogPessoal.src.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlogPessoal.src.controllers
{
    [ApiController]
    [Route("api/Authentication")]
    [Produces("application/json")]

    public class AuthenticationController : ControllerBase
    {
        #region Attributes
        private readonly IAuthentication _services;
        #endregion

        #region Constructors
        public AuthenticationController(IAuthentication services)
        {
            _services = services;
        }
        #endregion

        #region Mhetods
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> AuthenticationAsync([FromBody] AuthenticationDTO authentication)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                var authorization = await _services.GetAuthorizationAsync(authentication);
                return Ok(authorization);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
        #endregion
    }
}
