using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using System.Collections.Generic;

namespace BlogPessoal.src.repositories
{
    /// <summary>
    /// <para>Resume: Interface responsible for representing CRUD actions themes</para>
    /// <para>Created by: Joceline Gutierrez</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 29/04/2022</para>
    /// </summary>
    public interface ITheme
    {
        void AddTheme(AddThemeDTO theme);
        void UpdateTheme(UpdateThemeDTO theme);
        void DeleteTheme(int id);
        ThemeModel GetThemeById(int id);
        List<ThemeModel> GetThemeByDescription(string description);
    }
}
