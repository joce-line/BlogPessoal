using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


namespace BlogPessoal.src.repositories.implements
{
    public class PostRepository : IPost
    {
        #region Atributtes

        private readonly PersonalBlogContext _context;

        #endregion Atributtes

        #region Contructors

        public PostRepository(PersonalBlogContext context)
        {
            _context = context;
        }

        #endregion Constructors

        #region Methods
        public void DeletePost(int id)
        {
            _context.Posts.Remove(GetPostById(id));
            _context.SaveChanges();
        }

        public List<PostModel> GetAllPosts()
        {
            return _context.Posts.ToList();
        }

        public PostModel GetPostById(int id)
        {
            return _context.Posts.FirstOrDefault(p => p.Id == id);
        }

        public List<PostModel> GetPostBySearch(string title, string descriptionTheme, string nameCreator)
        {
            switch (title, descriptionTheme, nameCreator)
            {
                case (null, null, null):
                    return GetAllPosts();

                case (null, null, _):
                    return _context.Posts
                            .Include(p => p.Theme)
                            .Include(p => p.Creator)
                            .Where(p => p.Creator.Name.Contains(nameCreator))
                            .ToList();

                case (null, _, null):
                    return _context.Posts
                            .Include(p => p.Theme)
                            .Include(p => p.Creator)
                            .Where(p => p.Description.Contains(descriptionTheme))
                            .ToList();

                case (_, null, null):
                    return _context.Posts
                            .Include(p => p.Theme)
                            .Include(p => p.Creator)
                            .Where(p => p.Title.Contains(title))
                            .ToList();

                case (_, _, null):
                    return _context.Posts
                            .Include(p => p.Theme)
                            .Include(p => p.Creator)
                            .Where(p => p.Title.Contains(title) & p.Description.Contains(descriptionTheme))
                            .ToList();

                case (null, _, _):
                    return _context.Posts
                            .Include(p => p.Theme)
                            .Include(p => p.Creator)
                            .Where(p => p.Description.Contains(descriptionTheme) & p.Creator.Name.Contains(nameCreator))
                            .ToList();

                case (_, null, _):
                    return _context.Posts
                            .Include(p => p.Theme)
                            .Include(p => p.Creator)
                            .Where(p => p.Title.Contains(title) & p.Creator.Name.Contains(nameCreator))
                            .ToList();

                case (_, _, _):
                    return _context.Posts
                            .Include(p => p.Theme)
                            .Include(p => p.Creator)
                            .Where(p => p.Title.Contains(title) | p.Description.Contains(descriptionTheme) | p.Creator.Name.Contains(nameCreator))
                            .ToList();
            }
        }

        public void NewPost(NewPostDTO post)
        {
            _context.Posts.Add(new PostModel
            {
                Title = post.Title,
                Description = post.Description,
                Photo = post.Photo,
                Creator = _context.Users.FirstOrDefault(u => u.Email == post.EmailCreator),
                Theme = _context.Themes.FirstOrDefault(t => t.Description == post.DescriptionTheme)
            });
            _context.SaveChanges();
        }

        public void UpdatePost(UpdatePostDTO post)
        {
            var existingPost = GetPostById(post.Id);
            existingPost.Title = post.Title;
            existingPost.Description = post.Description;
            existingPost.Photo = post.Photo;
            existingPost.Theme = _context.Themes.FirstOrDefault(t => t.Description == post.DescriptionTheme);
            _context.Posts.Update(existingPost);
            _context.SaveChanges();

        }
        #endregion Methods

    }
}
