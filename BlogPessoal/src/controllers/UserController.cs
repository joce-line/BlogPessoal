using BlogPessoal.src.dtos;
using BlogPessoal.src.repositories;
using BlogPessoal.src.services;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("id/{idUser}")]
        [Authorize(Roles ="NORMAL,ADMINISTRATOR")]
        public async Task<ActionResult> GetUserByIdAsync ([FromRoute] int idUser)
        {
            var user = await _repository.GetUserByIdAsync(idUser);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpGet]
        [Authorize(Roles = "NORMAL,ADMINISTRATOR")]
        public async Task<ActionResult> GetUsersByNameAsync([FromQuery] string nameUser)
        {
            var users = await _repository.GetUsersByNameAsync(nameUser);
            if (users.Count < 1) return NoContent();
            return Ok(users);
        }

        [HttpGet("email/{emailUser}")]
        [Authorize(Roles = "NORMAL,ADMINISTRATOR")]
        public async Task<ActionResult> GetUserByEmailAsync([FromRoute] string emailUser)
        {
            var user = await _repository.GetUserByEmailAsync(emailUser);
            if (user == null) return NotFound();
            return Ok(user);
        }

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

        [HttpPut]
        [Authorize(Roles = "NORMAL,ADMINISTRATOR")] 
        public async Task<ActionResult> UpdateUserAsync([FromBody] UpdateUserDTO user)
        {
            if (!ModelState.IsValid) return BadRequest();

            user.Password = _services.EncodePassword(user.Password);

            await _repository.UpdateUserAsync(user);
            return Ok(user);
        }

        [HttpDelete("delete/{idUser}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        public async Task<ActionResult> DeleteUserAsync([FromRoute] int idUser)
        {
            await _repository.DeleteUserAsync(idUser);
            return NoContent();
        }

        #endregion
    }
}
