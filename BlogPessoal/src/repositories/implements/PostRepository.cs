using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPessoal.src.repositories.implements
{
    /// <summary>
    /// <para>Resume: Class responsible for implement IPost</para>
    /// <para>Created by: Joceline Gutierrez</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 12/05/2022</para>
    /// </summary>

    public class PostRepository : IPost
    {
        #region Atributtes

        private readonly PersonalBlogContext _context;

        #endregion Atributtes

        #region Contructors

        /// <summary>
        /// <para>Resume: Constructor of class.</para>
        /// </summary>
        /// <param name="context">PersonalBlogContext</param>
        public PostRepository(PersonalBlogContext context)
        {
            _context = context;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// <para>Resume: Asynchronous method for delete a post</para>
        /// </summary>
        /// <param name="id">Id of post</param>
        public async Task DeletePostAsync(int id)
        {
            _context.Posts.Remove(await GetPostByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resume: Asynchronous method for get all posts</para>
        /// </summary>
        /// <returns>List of PostModel</returns>
        public async Task<List<PostModel>> GetAllPostsAsync()
        {            
            return await _context.Posts                
                .ToListAsync();
        }

        /// <summary>
        /// <para>Resume: Asynchronous method for get a post by id</para>
        /// </summary>
        /// <param name="id">Id of post</param>
        /// <returns>PostModel</returns>
        public async Task<PostModel> GetPostByIdAsync(int id)
        {
            return await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
        }

        /// <summary>
        /// <para>Resume: Asynchronous method for get a theme by title or description theme or name creator</para>
        /// </summary>
        /// <param name="title">Title of theme</param>
        /// <param name="descriptionTheme">Description of theme</param>
        /// <param name="nameCreator">Creator of theme</param>
        /// <returns>List of PostModel</returns>
        public async Task<List<PostModel>> GetPostBySearchAsync(string title, string descriptionTheme, string nameCreator)
        {
            switch (title, descriptionTheme, nameCreator)
            {
                case (null, null, null):
                    return await GetAllPostsAsync();

                case (null, null, _):
                    return await _context.Posts
                            .Include(p => p.Theme)
                            .Include(p => p.Creator)
                            .Where(p => p.Creator.Name.Contains(nameCreator))
                            .ToListAsync();

                case (null, _, null):
                    return await _context.Posts
                            .Include(p => p.Theme)
                            .Include(p => p.Creator)
                            .Where(p => p.Description.Contains(descriptionTheme))
                            .ToListAsync();

                case (_, null, null):
                    return await _context.Posts
                            .Include(p => p.Theme)
                            .Include(p => p.Creator)
                            .Where(p => p.Title.Contains(title))
                            .ToListAsync();

                case (_, _, null):
                    return await _context.Posts
                            .Include(p => p.Theme)
                            .Include(p => p.Creator)
                            .Where(p => p.Title.Contains(title) & p.Description.Contains(descriptionTheme))
                            .ToListAsync();

                case (null, _, _):
                    return await _context.Posts
                            .Include(p => p.Theme)
                            .Include(p => p.Creator)
                            .Where(p => p.Description.Contains(descriptionTheme) & p.Creator.Name.Contains(nameCreator))
                            .ToListAsync();

                case (_, null, _):
                    return await _context.Posts
                            .Include(p => p.Theme)
                            .Include(p => p.Creator)
                            .Where(p => p.Title.Contains(title) & p.Creator.Name.Contains(nameCreator))
                            .ToListAsync();

                case (_, _, _):
                    return await _context.Posts
                            .Include(p => p.Theme)
                            .Include(p => p.Creator)
                            .Where(p => p.Title.Contains(title) | p.Description.Contains(descriptionTheme) | p.Creator.Name.Contains(nameCreator))
                            .ToListAsync();
            }
        }

        /// <summary>
        /// <para>Resume: Asynchronous method for add a new post.</para>
        /// </summary>
        /// <param name="post">NewPostDTO</param>
        public async Task NewPostAsync(NewPostDTO post)
        {
            await _context.Posts.AddAsync(new PostModel
            {
                Title = post.Title,
                Description = post.Description,
                Photo = post.Photo,
                Creator = _context.Users.FirstOrDefault(u => u.Email == post.EmailCreator),
                Theme = _context.Themes.FirstOrDefault(t => t.Description == post.DescriptionTheme)
            });
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resume: Asynchronous method for update an existent post.</para>
        /// </summary>
        /// <param name="post">UpdatePostDTO</param>
        public async Task UpdatePostAsync(UpdatePostDTO post)
        {
            var existingPost = await GetPostByIdAsync(post.Id);
            existingPost.Title = post.Title;
            existingPost.Description = post.Description;
            existingPost.Photo = post.Photo;
            existingPost.Theme = _context.Themes.FirstOrDefault(t => t.Description == post.DescriptionTheme);
            _context.Posts.Update(existingPost);
            await _context.SaveChangesAsync();

        }
        #endregion Methods

    }
}
