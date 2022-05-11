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
        public void CreateFourThemesOnDatabaseReturnFour()
        {

            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
               .UseInMemoryDatabase(databaseName: "db_blogpessoal5")
               .Options;

            _context = new PersonalBlogContext(opt);
            _repository = new ThemeRepository(_context);

            //GIVEN - Dado que registro 4 temas no banco de dados
            _repository.AddTheme(new AddThemeDTO("CSharp"));
            _repository.AddTheme(new AddThemeDTO("Phyton"));
            _repository.AddTheme(new AddThemeDTO("Java"));
            _repository.AddTheme(new AddThemeDTO("Backend"));

            //WHEN - Quando pesquiso
            //THEN - Então recebo 4 temas
            Assert.AreEqual(4, _context.Themes.Count());
        }

        [TestMethod]
        public void GetThemeByIdReturnTheme1()
        {
            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
               .UseInMemoryDatabase(databaseName: "db_blogpessoal6")
               .Options;

            _context = new PersonalBlogContext(opt);
            _repository = new ThemeRepository(_context);

            //GIVEN - Dado que registro 4 temas no banco de dados
            _repository.AddTheme(new AddThemeDTO("CSharp"));
            _repository.AddTheme(new AddThemeDTO("Phyton"));
            _repository.AddTheme(new AddThemeDTO("Java"));
            _repository.AddTheme(new AddThemeDTO("Backend"));

            //GIVEN - Dado que dou Id igual a 1
            var theme = _repository.GetThemeById(3);

            //WHEN - Quando procuro pelo id
            //THEN - Então o tema deve ser CSharp
            Assert.AreEqual("Java", theme.Description);
        }

        [TestMethod]
        public void GetThemeByDescriptionReturnOneTheme()
        {
            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
               .UseInMemoryDatabase(databaseName: "db_blogpessoal7")
               .Options;

            _context = new PersonalBlogContext(opt);
            _repository = new ThemeRepository(_context);

            //GIVEN - Dado que registro 4 temas no banco de dados
            _repository.AddTheme(new AddThemeDTO("CSharp"));
            _repository.AddTheme(new AddThemeDTO("Phyton"));
            _repository.AddTheme(new AddThemeDTO("Java"));
            _repository.AddTheme(new AddThemeDTO("Backend"));

            //GIVEN - Dado que pesquiso pela descrição Phyton
            var themes = _repository.GetThemeByDescription("Phyton");

            //WHEN - Quando pesquiso
            //THEN - Então deve retornar 1 tema
            Assert.AreEqual(1, themes.Count);
        }

        [TestMethod]
        public void UpdateThemeJavaReturnCSS()
        {
            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
               .UseInMemoryDatabase(databaseName: "db_blogpessoal8")
               .Options;

            _context = new PersonalBlogContext(opt);
            _repository = new ThemeRepository(_context);

            //GIVEN - Dado que registro 1 tema no banco de dados
            _repository.AddTheme(new AddThemeDTO("CSharp"));


            //GIVEN - Dado que passo o id 1 e o tema CSS
            var oldTheme = 
            _repository.GetThemeById(1);
            _repository.UpdateTheme(new UpdateThemeDTO(1, "CSS"));

            //THEN - Então, quando validamos pesquisa deve retornar descrição CSS
            Assert.AreEqual("CSS", _context.Themes.FirstOrDefault(t => t.Id == oldTheme.Id).Description);
        }

        [TestMethod]        
        public void DeleteThemesReturnNull()
        {
            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
               .UseInMemoryDatabase(databaseName: "db_blogpessoal9")
               .Options;

            _context = new PersonalBlogContext(opt);
            _repository = new ThemeRepository(_context);

            //GIVEN - Dado que registro 1 tema no banco de dados
            _repository.AddTheme(new AddThemeDTO("CSharp"));

            //GIVEN - Dado o numero do Id 1
            _repository.DeleteTheme(1);

            //THEN - Então depois de deletar o tema 1 retornar nulo
            Assert.IsNull(_repository.GetThemeById(1));

        }
    }
}
