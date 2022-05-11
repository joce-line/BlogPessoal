using BlogPessoal.src.dtos;
using BlogPessoal.src.repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogPessoal.src.controllers
{
    [ApiController]
    [Route("api/Themes")]
    [Produces("application/json")]
    public class ThemeController : ControllerBase
    {
        #region Attributes
        private readonly ITheme _repository;
        #endregion


        #region Constructors
        public ThemeController(ITheme repository)
        {
            _repository = repository;
        }
        #endregion

        #region Métodos

        [HttpPost]
        [Authorize]
        public IActionResult AddTheme([FromBody] AddThemeDTO theme)
        {
            if (!ModelState.IsValid) return BadRequest();

            _repository.AddTheme(theme);
            return Created($"api/Themes", theme);
        }

        [HttpPut]
        [Authorize(Roles = "ADMINISTRATOR")]
        public IActionResult UpdateTheme([FromBody] UpdateThemeDTO theme)
        {
            if (!ModelState.IsValid) return BadRequest();

            _repository.UpdateTheme(theme);
            return Ok(theme);
        }

        [HttpDelete("deletar/{idTheme}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        public IActionResult DeleteTheme([FromRoute] int idTheme)
        {
            _repository.DeleteTheme(idTheme);
            return NoContent();
        }

        [HttpGet("id/{idTheme}")]
        [Authorize]
        public IActionResult GetThemeById([FromRoute] int idTheme)
        {
            var theme = _repository.GetThemeById(idTheme);

            if (theme == null) return NotFound();

            return Ok(theme);
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetThemeByDescription([FromQuery] string descriptionTheme)
        {
            var themes = _repository.GetThemeByDescription(descriptionTheme);

            if (themes.Count < 1) return NoContent();

            return Ok(themes);
        }

        #endregion
    }
}