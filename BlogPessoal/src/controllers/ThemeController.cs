using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using BlogPessoal.src.repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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

        /// <summary>
        /// Create a new theme
        /// </summary>
        /// <param name="theme">AddThemeDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Request example:
        ///
        ///     POST /api/Themes
        ///     {
        ///        "description": "Música"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Return created theme</response>
        /// <response code="400">Error in request</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ThemeModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddThemeAsync([FromBody] AddThemeDTO theme)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _repository.AddThemeAsync(theme);
            return Created($"api/Themes", theme);
        }

        /// <summary>
        /// Update a theme
        /// </summary>
        /// <param name="theme">UpdateUserDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Request example:
        ///
        ///     PUT /api/Themes
        ///     {
        ///        "id": 1,    
        ///        "description": "Livros"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Theme updated</response>
        /// <response code="400">Error in request</response>
        /// <response code="403">Returns forbidden access</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPut]
        [Authorize(Roles = "ADMINISTRATOR")]
        public async Task<ActionResult> UpdateThemeAsync([FromBody] UpdateThemeDTO theme)
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                await _repository.UpdateThemeAsync(theme);
                return Ok(theme);
            }
            catch (Exception ex)
            {
                return Forbid(ex.Message);
            }
            
        }

        /// <summary>
        /// Delete a theme by id
        /// </summary>
        /// <param name="idTheme">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="204">Theme deleted</response>
        /// <response code="403">Returns forbidden access</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpDelete("delete/{idTheme}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        public async Task<ActionResult> DeleteThemeAsync([FromRoute] int idTheme)
        {
            try
            {
                await _repository.DeleteThemeAsync(idTheme);
                return NoContent();
            }
            catch (Exception ex)
            {
                return Forbid(ex.Message);
            }
            
        }

        /// <summary>
        /// Get a theme by id
        /// </summary>
        /// <param name="idTheme">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Returns the theme</response>
        /// <response code="404">Theme not found</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ThemeModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("id/{idTheme}")]
        [Authorize]
        public async Task<ActionResult> GetThemeByIdAsync([FromRoute] int idTheme)
        {
            var theme = await _repository.GetThemeByIdAsync(idTheme);

            if (theme == null) return NotFound();

            return Ok(theme);
        }

        /// <summary>
        /// Get a theme by description
        /// </summary>
        /// <param name="descriptionTheme">string</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Returns the theme</response>
        /// <response code="204">Theme not found</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ThemeModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetThemeByDescriptionAsync([FromQuery] string descriptionTheme)
        {
            var themes = await _repository.GetThemeByDescriptionAsync(descriptionTheme);

            if (themes.Count < 1) return NoContent();

            return Ok(themes);
        }

        #endregion
    }
}