using BlogPessoal.src.dtos;
using BlogPessoal.src.repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlogPessoal.src.controllers
{
    [ApiController]
    [Route("api/Posts")]
    [Produces("application/json")]
    public class PostController : ControllerBase
    {
        #region Atributos
        private readonly IPost _repository;
        #endregion

        #region Construtores
        public PostController(IPost repository)
        {
            _repository = repository;
        }
        #endregion

        #region Métodos

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> NewPostAsync([FromBody] NewPostDTO post)
        {            
            if (!ModelState.IsValid) return BadRequest();

            await _repository.NewPostAsync(post);
            return Created($"api/Posts", post);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdatePostAsync([FromBody] UpdatePostDTO post)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _repository.UpdatePostAsync(post);
            return Ok(post);
        }

        [HttpDelete("delete/{idPost}")]
        [Authorize]
        public async Task<IActionResult> DeletePostAsync([FromRoute] int idPost)
        {
            await _repository.DeletePostAsync(idPost);
            return NoContent();
        }

        [HttpGet("id/{idPost}")]
        [Authorize]
        public async Task<IActionResult> GetPostByIdAsync([FromRoute] int idPost)
        {
            var post = await _repository.GetPostByIdAsync(idPost);

            if (post == null) return NotFound();

            return Ok(post);
        }

        [HttpGet("list")]
        [Authorize]
        public async Task<IActionResult> GetAllPostsAsync()
        {
            var list = await _repository.GetAllPostsAsync();

            if (list.Count < 1) return NoContent();

            return Ok(list);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetPostBySearchAsync(
            [FromQuery] string title,
            [FromQuery] string descriptionTheme,
            [FromQuery] string nameCreator)
        {
            var posts = await _repository.GetPostBySearchAsync(title, descriptionTheme, nameCreator);
            if (posts.Count < 1) return NoContent();
            return Ok(posts);
        }
        #endregion
    }
}
