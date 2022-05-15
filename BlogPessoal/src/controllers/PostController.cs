using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using BlogPessoal.src.repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        /// <summary>
        /// Create a new post
        /// </summary>
        /// <param name="post">NewPostDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Request example:
        ///
        ///     POST /api/Posts
        ///     {
        ///        "title": "Músicas para viajar",
        ///        "description": "Indicações de músicas para ir para outro espaço",
        ///        "photo": "URLFotoUniverso",
        ///        "emailCreator": "joceline@domain.com",
        ///        "descriptionTheme": "Música"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Return created post</response>
        /// <response code="400">Error in request</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PostModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> NewPostAsync([FromBody] NewPostDTO post)
        {            
            if (!ModelState.IsValid) return BadRequest();

            await _repository.NewPostAsync(post);
            return Created($"api/Posts", post);
        }

        /// <summary>
        /// Update a post
        /// </summary>
        /// <param name="post">UpdatePostDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Request example:
        ///
        ///     PUT /api/Posts
        ///     {
        ///        "id": 1,    
        ///        "title": "Músicas para viajar",
        ///        "description": "Indicações de músicas para ir para outro espaço",
        ///        "photo": "URLFotoUniverso",
        ///        "descriptionTheme": "Música"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Post updated</response>
        /// <response code="400">Error in request</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        [Authorize]
        public async Task<ActionResult> UpdatePostAsync([FromBody] UpdatePostDTO post)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _repository.UpdatePostAsync(post);
            return Ok(post);
        }

        /// <summary>
        /// Delete a post by id
        /// </summary>
        /// <param name="idPost">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="204">Post deleted</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("delete/{idPost}")]
        [Authorize]
        public async Task<ActionResult> DeletePostAsync([FromRoute] int idPost)
        {
            await _repository.DeletePostAsync(idPost);
            return NoContent();
        }

        /// <summary>
        /// Get post by id
        /// </summary>
        /// <param name="idPost">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Returns the post</response>
        /// <response code="404">Post not found</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("id/{idPost}")]
        [Authorize]
        public async Task<ActionResult> GetPostByIdAsync([FromRoute] int idPost)
        {
            var post = await _repository.GetPostByIdAsync(idPost);

            if (post == null) return NotFound();

            return Ok(post);
        }

        /// <summary>
        /// Get all posts
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">List of posts</response>
        /// <response code="204">Empty list</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("list")]
        [Authorize]
        public async Task<ActionResult> GetAllPostsAsync()
        {
            var list = await _repository.GetAllPostsAsync();

            if (list.Count < 1) return NoContent();

            return Ok(list);
        }

        /// <summary>
        /// Get posts by search
        /// </summary>
        /// <param name="title">string</param>
        /// <param name="descriptionTheme">string</param>
        /// <param name="nameCreator">string</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Returns the posts</response>
        /// <response code="204">Post does not exist for this search</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("search")]
        [Authorize]
        public async Task<ActionResult> GetPostBySearchAsync(
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
