using BlogPessoal.src.dtos;
using BlogPessoal.src.repositories;
using BlogPessoal.src.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

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


        #region Métodos

        [HttpGet("id/{idUser}")]
        [Authorize(Roles ="NORMAL,ADMINISTRATOR")]
        public IActionResult GetUserById ([FromRoute] int idUser)
        {
            var user = _repository.GetUserById(idUser);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpGet]
        [Authorize(Roles = "NORMAL,ADMINISTRATOR")]
        public IActionResult GetUsersByName([FromQuery] string nameUser)
        {
            var users = _repository.GetUsersByName(nameUser);
            if (users.Count < 1) return NoContent();
            return Ok(users);
        }

        [HttpGet("email/{emailUser}")]
        [Authorize(Roles = "NORMAL,ADMINISTRATOR")]
        public IActionResult GetUserByEmail([FromRoute] string emailUser)
        {
            var user = _repository.GetUserByEmail(emailUser);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult AddUser([FromBody] AddUserDTO user)
        {
            if (!ModelState.IsValid) return BadRequest();

            try //para tratar exceções
            {
                _services.CreateUserNotDuplicated(user);
                return Created($"api/Usuarios/{user.Email}", user);
            } 
            catch(Exception ex) //catch significa que vai pegar a excessão que ta la na autenticação 
            {
                return Unauthorized(ex.Message);
            }
            
        }

        [HttpPut]
        [Authorize(Roles = "NORMAL,ADMINISTRATOR")] //vai permitir que seja acessado por um usuario normal e um admin
        public IActionResult UpdateUser([FromBody] UpdateUserDTO user)
        {
            if (!ModelState.IsValid) return BadRequest();

            user.Password = _services.EncodePassword(user.Password);

            _repository.UpdateUser(user);
            return Ok(user);
        }

        [HttpDelete("delete/{idUser}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        public IActionResult DeleteUser([FromRoute] int idUser)
        {
            _repository.DeleteUser(idUser);
            return NoContent();
        }

        #endregion
    }
}
