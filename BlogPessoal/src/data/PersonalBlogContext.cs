using BlogPessoal.src.models;
using Microsoft.EntityFrameworkCore;

namespace BlogPessoal.src.data
{

    /// <summary>
    /// <para>Resume: Class responsible for data base Blog context</para>
    /// <para>Created by: Joceline Gutierrez</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 12/05/2022</para>
    /// </summary>
    public class PersonalBlogContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<ThemeModel> Themes { get; set; }
        public DbSet<PostModel> Posts { get; set; }

        public PersonalBlogContext(DbContextOptions<PersonalBlogContext> opt) : base(opt)
        {

        }
    }
}
