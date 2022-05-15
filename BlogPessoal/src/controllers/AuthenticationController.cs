using BlogPessoal.src.dtos;
using BlogPessoal.src.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        #region Methods

        /// <summary>
        /// Get Authorization
        /// </summary>
        /// <param name="authentication">AuthenticationDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Request example:
        ///
        ///     POST /api/Authentication
        ///     {
        ///        "email": "joceline@domain.com",
        ///        "senha": "12345"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Return user created with token</response>
        /// <response code="400">Error in request</response>
        /// <response code="401">Invalid email or password</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthorizationDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]        
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
