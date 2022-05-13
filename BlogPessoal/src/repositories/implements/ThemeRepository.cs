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
    /// <para>Resume: Class responsible for implement ITheme.</para>
    /// <para>Created by: Joceline Gutierrez</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 12/05/2022</para>
    /// </summary>

    public class ThemeRepository : ITheme
    {
        #region Atributtes

        private readonly PersonalBlogContext _context;

        #endregion Atributtes

        #region Constructors

        /// <summary>
        /// <para>Resume: Constructor of class.</para>
        /// </summary>
        /// <param name="context">PersonalBlogContext</param>
        public ThemeRepository(PersonalBlogContext context)
        {
            _context = context;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// <para>Resume: Asynchronous method for add a new theme</para>
        /// </summary>
        /// <param name="theme">AddThemeDTO</param>
        public async Task AddThemeAsync(AddThemeDTO theme)
        {
            await _context.Themes.AddAsync(new ThemeModel
            {
                Description = theme.Description
            });
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resume: Asynchronous method for delete a theme</para>
        /// </summary>
        /// <param name="id">Id of theme</param>
        public async Task DeleteThemeAsync(int id)
        {
            _context.Themes.Remove(await GetThemeByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resume: Asynchronous method for get a theme by description</para>
        /// </summary>
        /// <param name="description">Description of theme</param>
        /// <returns>List of ThemeModel</returns>
        public async Task<List<ThemeModel>> GetThemeByDescriptionAsync(string description)
        {
            return await _context.Themes
                .Where(t => t.Description.Contains(description))
                .ToListAsync();
        }

        /// <summary>
        /// <para>Resume: Asynchronous method for get a theme by id</para>
        /// </summary>
        /// <param name="id">Id of user</param>
        /// <returns>ThemeModel</returns>
        public async Task<ThemeModel> GetThemeByIdAsync(int id)
        {
            return await _context.Themes.FirstOrDefaultAsync(t => t.Id == id);
        }

        /// <summary>
        /// <para>Resume: Asynchronous method for update an existent theme</para>
        /// </summary>
        /// <param name="theme">UpdateThemeDTO</param>
        public async Task UpdateThemeAsync(UpdateThemeDTO theme)
        {
            var themeUpdate = await GetThemeByIdAsync(theme.Id);
            themeUpdate.Description = theme.Description;
            _context.Themes.Update(themeUpdate);
            await _context.SaveChangesAsync();
        }
        #endregion Methods
    }
}
