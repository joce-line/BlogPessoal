using BlogPessoal.src.dtos;
using BlogPessoal.src.repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        #region Mhetods

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddThemeAsync([FromBody] AddThemeDTO theme)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _repository.AddThemeAsync(theme);
            return Created($"api/Themes", theme);
        }

        [HttpPut]
        [Authorize(Roles = "ADMINISTRATOR")]
        public async Task<IActionResult> UpdateThemeAsync([FromBody] UpdateThemeDTO theme)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _repository.UpdateThemeAsync(theme);
            return Ok(theme);
        }

        [HttpDelete("deletar/{idTheme}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        public async Task<IActionResult> DeleteThemeAsync([FromRoute] int idTheme)
        {
            await _repository.DeleteThemeAsync(idTheme);
            return NoContent();
        }

        [HttpGet("id/{idTheme}")]
        [Authorize]
        public async Task<IActionResult> GetThemeByIdAsync([FromRoute] int idTheme)
        {
            var theme = await _repository.GetThemeByIdAsync(idTheme);

            if (theme == null) return NotFound();

            return Ok(theme);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetThemeByDescriptionAsync([FromQuery] string descriptionTheme)
        {
            var themes = await _repository.GetThemeByDescriptionAsync(descriptionTheme);

            if (themes.Count < 1) return NoContent();

            return Ok(themes);
        }

        #endregion
    }
}