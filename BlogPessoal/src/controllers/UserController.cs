using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using BlogPessoal.src.repositories;
using BlogPessoal.src.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlogPessoal.src.controllers
{

    [ApiController] 
    [Route("api/Users")]
    [Produces("application/json")]

    public class UserController : ControllerBase
    {
        #region Attributes
        private readonly IUser _repository;
        private readonly IAuthentication _services;
        #endregion Attributes

        #region Constructors
        public UserController(IUser repository, IAuthentication service) 
        {
            _repository = repository;
            _services = service;
        }
        #endregion


        #region Methods

        /// <summary>
        /// Get a user by id
        /// </summary>
        /// <param name="idUser">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Returns the user</response>
        /// <response code="404">User not found</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("id/{idUser}")]
        [Authorize(Roles ="NORMAL,ADMINISTRATOR")]
        public async Task<ActionResult> GetUserByIdAsync ([FromRoute] int idUser)
        {
            var user = await _repository.GetUserByIdAsync(idUser);
            if (user == null) return NotFound();
            return Ok(user);
        }

        /// <summary>
        /// Get a user by name
        /// </summary>
        /// <param name="nameUser">string</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Returns the user</response>
        /// <response code="204">Name not found</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        [Authorize(Roles = "NORMAL,ADMINISTRATOR")]
        public async Task<ActionResult> GetUsersByNameAsync([FromQuery] string nameUser)
        {
            var users = await _repository.GetUsersByNameAsync(nameUser);
            if (users.Count < 1) return NoContent();
            return Ok(users);
        }

        /// <summary>
        /// Get user by email
        /// </summary>
        /// <param name="emailUser">string</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Returns the user</response>
        /// <response code="404">Email not found</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("email/{emailUser}")]
        [Authorize(Roles = "NORMAL,ADMINISTRATOR")]
        public async Task<ActionResult> GetUserByEmailAsync([FromRoute] string emailUser)
        {
            var user = await _repository.GetUserByEmailAsync(emailUser);
            if (user == null) return NotFound();
            return Ok(user);
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="user">AddUserDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Request example:
        ///
        ///     POST /api/Users
        ///     {
        ///        "name": "Joceline Gutierrez",
        ///        "email": "joceline@domain.com",
        ///        "password": "12345",
        ///        "photo": "URLFOTO",
        ///        "type": "NORMAL"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Return created user</response>
        /// <response code="400">Error in request</response>
        /// <response code="401">Email address already registered</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        [AllowAnonymous]
        public  async Task<ActionResult> AddUserAsync([FromBody] AddUserDTO user)
        {
            if (!ModelState.IsValid) return BadRequest();

            try 
            {
                await _services.CreateUserNotDuplicatedAsync(user);
                return Created($"api/Usuarios/{user.Email}", user);
            } 
            catch(Exception ex)  
            {
                return Unauthorized(ex.Message);
            }
            
        }

        /// <summary>
        /// Update a user
        /// </summary>
        /// <param name="user">UpdateUserDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Request example:
        ///
        ///     PUT /api/Users
        ///     {
        ///        "id": 1,    
        ///        "name": "Joceline Gutierrez",
        ///        "password": "12345",
        ///        "photo": "URLFOTO"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">User updated</response>
        /// <response code="400">Error in request</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        [Authorize(Roles = "NORMAL,ADMINISTRATOR")] 
        public async Task<ActionResult> UpdateUserAsync([FromBody] UpdateUserDTO user)
        {
            if (!ModelState.IsValid) return BadRequest();

            user.Password = _services.EncodePassword(user.Password);

            await _repository.UpdateUserAsync(user);
            return Ok(user);
        }

        /// <summary>
        /// Delete a user by id
        /// </summary>
        /// <param name="idUser">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="204">User deleted</response>
        /// <response code="403">Returns forbidden access</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpDelete("delete/{idUser}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        public async Task<ActionResult> DeleteUserAsync([FromRoute] int idUser)
        {
            try
            {
                await _repository.DeleteUserAsync(idUser);
                return NoContent();
            }
            catch (Exception ex)
            {
                return Forbid(ex.Message);
            }
            
        }

        #endregion
    }
}
