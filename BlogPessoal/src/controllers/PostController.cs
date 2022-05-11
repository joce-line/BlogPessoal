using BlogPessoal.src.dtos;
using BlogPessoal.src.repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult NewPost([FromBody] NewPostDTO post)
        {            
            if (!ModelState.IsValid) return BadRequest();

            _repository.NewPost(post);
            return Created($"api/Posts", post);
        }

        [HttpPut]
        [Authorize]
        public IActionResult UpdatePost([FromBody] UpdatePostDTO post)
        {
            if (!ModelState.IsValid) return BadRequest();

            _repository.UpdatePost(post);
            return Ok(post);
        }

        [HttpDelete("delete/{idPost}")]
        [Authorize]
        public IActionResult DeletePost([FromRoute] int idPost)
        {
            _repository.DeletePost(idPost);
            return NoContent();
        }

        [HttpGet("id/{idPost}")]
        [Authorize]
        public IActionResult GetPostById([FromRoute] int idPost)
        {
            var post = _repository.GetPostById(idPost);

            if (post == null) return NotFound();

            return Ok(post);
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllPosts()
        {
            var list = _repository.GetAllPosts();

            if (list.Count < 1) return NoContent();

            return Ok(list);
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetPostBySearch(
            [FromQuery] string title,
            [FromQuery] string descriptionTheme,
            [FromQuery] string nameCreator)
        {
            var posts = _repository.GetPostBySearch(title, descriptionTheme, nameCreator);
            if (posts.Count < 1) return NoContent();
            return Ok(posts);
        }
        #endregion
    }
}
