using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.repositories;
using BlogPessoal.src.repositories.implements;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPessoalTeste.Tests.repositories
{

    [TestClass]
    public class ThemeRepositoryTest
    {
        private PersonalBlogContext _context;

        private ITheme _repository;

        
        [TestMethod]
        public async Task CreateFourThemesOnDatabaseReturnFour()
        {

            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
               .UseInMemoryDatabase(databaseName: "db_blogpessoal5")
               .Options;

            _context = new PersonalBlogContext(opt);
            _repository = new ThemeRepository(_context);

            //GIVEN - Dado que registro 4 temas no banco de dados
            await _repository.AddThemeAsync(new AddThemeDTO("CSharp"));
            await _repository.AddThemeAsync(new AddThemeDTO("Phyton"));
            await _repository.AddThemeAsync(new AddThemeDTO("Java"));
            await _repository.AddThemeAsync(new AddThemeDTO("Backend"));

            //WHEN - Quando pesquiso
            //THEN - Então recebo 4 temas
            Assert.AreEqual(4, _context.Themes.Count());
        }

        [TestMethod]
        public async Task GetThemeByIdReturnTheme1()
        {
            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
               .UseInMemoryDatabase(databaseName: "db_blogpessoal6")
               .Options;

            _context = new PersonalBlogContext(opt);
            _repository = new ThemeRepository(_context);

            //GIVEN - Dado que registro 4 temas no banco de dados
            await _repository.AddThemeAsync(new AddThemeDTO("CSharp"));
            await _repository.AddThemeAsync(new AddThemeDTO("Phyton"));
            await _repository.AddThemeAsync(new AddThemeDTO("Java"));
            await _repository.AddThemeAsync(new AddThemeDTO("Backend"));

            //GIVEN - Dado que dou Id igual a 1
            var theme = await _repository.GetThemeByIdAsync(3);

            //WHEN - Quando procuro pelo id
            //THEN - Então o tema deve ser CSharp
            Assert.AreEqual("Java", theme.Description);
        }

        [TestMethod]
        public async Task GetThemeByDescriptionReturnTwoThemes()
        {
            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
               .UseInMemoryDatabase(databaseName: "db_blogpessoal7")
               .Options;

            _context = new PersonalBlogContext(opt);
            _repository = new ThemeRepository(_context);

            //GIVEN - Dado que registro 2 temas no banco de dados
            await _repository.AddThemeAsync(new AddThemeDTO("Java"));
            await _repository.AddThemeAsync(new AddThemeDTO("Javascript"));
            
            //WHEN - Quando pesquiso pela descrição Java
            var themes = await _repository.GetThemeByDescriptionAsync("Java");

            //THEN - Então deve retornar 2 tema
            Assert.AreEqual(2, themes.Count);
        }

        [TestMethod]
        public async Task UpdateThemeJavaReturnCSS()
        {
            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
               .UseInMemoryDatabase(databaseName: "db_blogpessoal8")
               .Options;

            _context = new PersonalBlogContext(opt);
            _repository = new ThemeRepository(_context);

            //GIVEN - Dado que registro 1 tema no banco de dados
            await _repository.AddThemeAsync(new AddThemeDTO("CSharp"));


            //WHEN - Quando passo o id 1 e a descrição CSS            
            await _repository.UpdateThemeAsync(new UpdateThemeDTO(1, "CSS"));

            var theme = await _repository.GetThemeByIdAsync(1);

            //THEN - Então, quando validamos pesquisa deve retornar descrição CSS
            Assert.AreEqual("CSS", theme.Description);
        }

        [TestMethod]        
        public async Task DeleteThemesReturnNull()
        {
            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
               .UseInMemoryDatabase(databaseName: "db_blogpessoal9")
               .Options;

            _context = new PersonalBlogContext(opt);
            _repository = new ThemeRepository(_context);

            //GIVEN - Dado que registro 1 tema no banco de dados
            await _repository.AddThemeAsync(new AddThemeDTO("CSharp"));

            //GIVEN - Dado o numero do Id 1
            await _repository.DeleteThemeAsync(1);

            //THEN - Então depois de deletar o tema 1 retornar nulo
            Assert.IsNull(await _repository.GetThemeByIdAsync(1));

        }
    }
}
