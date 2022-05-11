using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.repositories;
using BlogPessoal.src.repositories.implements;
using BlogPessoal.src.utilities;
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
    public class PostRepositoryTest
    {
        private PersonalBlogContext _context;
        private IUser _repositoryU;
        private ITheme _repositoryT;
        private IPost _repositoryP;

        [TestMethod]
        public void CreateThreePotsInTheSystemReturnThree()
        {
            //Definindo o contexto
            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal21")
                .Options;

            _context = new PersonalBlogContext(opt);
            _repositoryU = new UserRepository(_context);
            _repositoryT = new ThemeRepository(_context);
            _repositoryP = new PostRepository(_context);

            //GIVEN - Dado que registro 2 usuarios

            _repositoryU.AddUser(
                new AddUserDTO
                ("Joceline Gutierrez", "joceline@email.com", "3456543", "foto.img", UserType.NORMAL)
                );
            _repositoryU.AddUser(
                new AddUserDTO
                ("Dálmata Diamante", "dalmante@email.com", "745348", "fotodiamante.img", UserType.NORMAL)
                );

            //AND - e que registro 2 temas 
            _repositoryT.AddTheme(new AddThemeDTO("Leitura"));
            _repositoryT.AddTheme(new AddThemeDTO("Charadas"));

            //WHEN - Quando registro 3 postagens
            _repositoryP.NewPost(
                new NewPostDTO(
                "Indicação",
                "Indicação de leituras para viagens",
                "livrinhos.img",
                "joceline@email.com",
                "Leitura"
                ));
            _repositoryP.NewPost(
                new NewPostDTO(
                "Livros",
                "Livros que custam mais do que deviam",
                "liv.img",
                "joceline@email.com",
                "Leitura"
                ));
            _repositoryP.NewPost(
                new NewPostDTO(
                "Pergunta",
                "Quais as duas coisas que nunca se pode comer no café da manhã?",
                "imagemPensadora.img",
                "dalmante@email.com",
                "Charadas"
                ));

            //WHEN - Quando eu busco todas as postagens
            //THEN - Então eu tenho 3 postagens

            Assert.AreEqual(3, _repositoryP.GetAllPosts().Count());
        }

        [TestMethod]
        public void UpdatePostReturnPostUpdated()
        {
            //Definindo o contexto
            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal22")
                .Options;

            _context = new PersonalBlogContext(opt);
            _repositoryU = new UserRepository(_context);
            _repositoryT = new ThemeRepository(_context);
            _repositoryP = new PostRepository(_context);

            //GIVEN - Dado que registro 1 usuario
            _repositoryU.AddUser(
                new AddUserDTO
                ("Joceline Gutierrez", "joceline@email.com", "3456543", "foto.img", UserType.NORMAL)
                );

            //AND - E que registro 2 temas
            _repositoryT.AddTheme(new AddThemeDTO("Leitura"));
            _repositoryT.AddTheme(new AddThemeDTO("Charadas"));

            //AND - E que regisro uma postagem 
            _repositoryP.NewPost(
                new NewPostDTO(
                "Indicação",
                "Indicação de leituras para viagens",
                "livrinhos.img",
                "joceline@email.com",
                "Leitura"
                ));

            //WHEN - Quando atualizo postagem de id 1
            _repositoryP.UpdatePost(
                new UpdatePostDTO(
                    1,
                "Pergunta",
                "Quais as duas coisas que nunca se pode comer no café da manhã?",
                "imagemPensadora20.img",
                "Charadas"
                ));

            //THEN - Então eu tenho a postagem atualizada
            Assert.AreEqual(
                "Pergunta",
                _repositoryP.GetPostById(1).Title
                );
            Assert.AreEqual(
                "Quais as duas coisas que nunca se pode comer no café da manhã?",
                _repositoryP.GetPostById(1).Description
                );
            Assert.AreEqual(
                "imagemPensadora20.img",
                _repositoryP.GetPostById(1).Photo
                );
            Assert.AreEqual(
                "Charadas",
                _repositoryP.GetPostById(1).Theme.Description
                );

        }

        [TestMethod]
        public void GetPostsBySearchReturnCustom()
        {
            //Definindo o contexto
            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal23")
                .Options;

            _context = new PersonalBlogContext(opt);
            _repositoryU = new UserRepository(_context);
            _repositoryT = new ThemeRepository(_context);
            _repositoryP = new PostRepository(_context);

            //GIVEN - Dado que registro 2 usuarios

            _repositoryU.AddUser(
                new AddUserDTO
                ("Joceline Gutierrez", "joceline@email.com", "3456543", "foto.img", UserType.NORMAL)
                );
            _repositoryU.AddUser(
                new AddUserDTO
                ("Dálmata Diamante", "dalmante@email.com", "745348", "fotodiamante.img", UserType.NORMAL)
                );

            //AND - E que registro 2 temas
            _repositoryT.AddTheme(new AddThemeDTO("Leitura"));
            _repositoryT.AddTheme(new AddThemeDTO("Charadas"));

            //WHEN - Quando registro 3 postagens
            _repositoryP.NewPost(
                new NewPostDTO(
                "Indicação de livros",
                "Indicação de leituras para viagens",
                "livrinhos.img",
                "joceline@email.com",
                "Leitura"
                ));
            _repositoryP.NewPost(
                new NewPostDTO(
                "livros",
                "Livros que custam mais do que deviam",
                "liv.img",
                "joceline@email.com",
                "Leitura"
                ));
            _repositoryP.NewPost(
                new NewPostDTO(
                "Pergunta",
                "Quais as duas coisas que nunca se pode comer no café da manhã?",
                "imagemPensadora.img",
                "dalmante@email.com",
                "Charadas"
                ));

            //WHEN - Quando eu busco as postagens
            //RHEN - Então eu tenho as postagens que correspondem aos criterios
            Assert.AreEqual(
                2,
                _repositoryP.GetPostBySearch("livros", null, null).Count);
            Assert.AreEqual(
                2,
                _repositoryP.GetPostBySearch(null, null, "Joceline Gutierrez").Count);
            Assert.AreEqual(
                2,
                _repositoryP.GetPostBySearch(null, "Leitura", null).Count);

        }

    }
}
