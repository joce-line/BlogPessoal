using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPessoal.src.repositories.implements
{
    public class ThemeRepository : ITheme
    {
        #region Atributtes

        private readonly PersonalBlogContext _context;

        #endregion Atributtes

        #region Constructors

        public ThemeRepository(PersonalBlogContext context)
        {
            _context = context;
        }

        #endregion Constructors

        #region Methods
        public async Task AddThemeAsync(AddThemeDTO theme)
        {
            await _context.Themes.AddAsync(new ThemeModel
            {
                Description = theme.Description
            });
            await _context.SaveChangesAsync();
        }

        public async Task DeleteThemeAsync(int id)
        {
            _context.Themes.Remove(await GetThemeByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        public async Task<List<ThemeModel>> GetThemeByDescriptionAsync(string description)
        {
            return await _context.Themes
                .Where(t => t.Description.Contains(description))
                .ToListAsync();
        }

        public async Task<ThemeModel> GetThemeByIdAsync(int id)
        {
            return await _context.Themes.FirstOrDefaultAsync(t => t.Id == id);
        }

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
