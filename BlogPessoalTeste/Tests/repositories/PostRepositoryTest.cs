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
        public async Task CreateThreePotsInTheSystemReturnThree()
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

            await _repositoryU.AddUserAsync(
                new AddUserDTO
                ("Joceline Gutierrez", "joceline@email.com", "3456543", "foto.img", UserType.NORMAL)
                );
            await _repositoryU.AddUserAsync(
                new AddUserDTO
                ("Dálmata Diamante", "dalmante@email.com", "745348", "fotodiamante.img", UserType.NORMAL)
                );

            //AND - e que registro 2 temas 
            await _repositoryT.AddThemeAsync(new AddThemeDTO("Leitura"));
            await _repositoryT.AddThemeAsync(new AddThemeDTO("Charadas"));

            //WHEN - Quando registro 3 postagens
            await _repositoryP.NewPostAsync(
                new NewPostDTO(
                "Indicação",
                "Indicação de leituras para viagens",
                "livrinhos.img",
                "joceline@email.com",
                "Leitura"
                ));
            await _repositoryP.NewPostAsync(
                new NewPostDTO(
                "Livros",
                "Livros que custam mais do que deviam",
                "liv.img",
                "joceline@email.com",
                "Leitura"
                ));
            await _repositoryP.NewPostAsync(
                new NewPostDTO(
                "Pergunta",
                "Quais as duas coisas que nunca se pode comer no café da manhã?",
                "imagemPensadora.img",
                "dalmante@email.com",
                "Charadas"
                ));

            //WHEN - Quando eu busco todas as postagens
            var posts = await _repositoryP.GetAllPostsAsync();

            //THEN - Então eu tenho 3 postagens

            Assert.AreEqual(3, posts.Count());
        }

        [TestMethod]
        public async Task UpdatePostReturnPostUpdated()
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
            await _repositoryU.AddUserAsync(
                new AddUserDTO
                ("Joceline Gutierrez", "joceline@email.com", "3456543", "foto.img", UserType.NORMAL)
                );

            //AND - E que registro 2 temas
            await _repositoryT.AddThemeAsync(new AddThemeDTO("Leitura"));
            await _repositoryT.AddThemeAsync(new AddThemeDTO("Charadas"));

            //AND - E que regisro uma postagem 
            await _repositoryP.NewPostAsync(
                new NewPostDTO(
                "Indicação",
                "Indicação de leituras para viagens",
                "livrinhos.img",
                "joceline@email.com",
                "Leitura"
                ));

            //WHEN - Quando atualizo postagem de id 1
            await _repositoryP.UpdatePostAsync(
                new UpdatePostDTO(
                    1,
                "Pergunta",
                "Quais as duas coisas que nunca se pode comer no café da manhã?",
                "imagemPensadora20.img",
                "Charadas"
                ));

            var post = await _repositoryP.GetPostByIdAsync(1);

            //THEN - Então eu tenho a postagem atualizada
            Assert.AreEqual(
                "Pergunta",
                post.Title
                );
            Assert.AreEqual(
                "Quais as duas coisas que nunca se pode comer no café da manhã?",
                post.Description
                );
            Assert.AreEqual(
                "imagemPensadora20.img",
                post.Photo
                );
            Assert.AreEqual(
                "Charadas",
                post.Description
                );

        }

        [TestMethod]
        public async Task GetPostsBySearchReturnCustom()
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

            await _repositoryU.AddUserAsync(
                new AddUserDTO
                ("Joceline Gutierrez", "joceline@email.com", "3456543", "foto.img", UserType.NORMAL)
                );
            await _repositoryU.AddUserAsync(
                new AddUserDTO
                ("Dálmata Diamante", "dalmante@email.com", "745348", "fotodiamante.img", UserType.NORMAL)
                );

            //AND - E que registro 2 temas
            await _repositoryT.AddThemeAsync(new AddThemeDTO("Leitura"));
            await _repositoryT.AddThemeAsync(new AddThemeDTO("Charadas"));

            //WHEN - Quando registro 3 postagens
            await _repositoryP.NewPostAsync(
                new NewPostDTO(
                "Indicação de livros",
                "Indicação de leituras para viagens",
                "livrinhos.img",
                "joceline@email.com",
                "Leitura"
                ));
            await _repositoryP.NewPostAsync(
                new NewPostDTO(
                "livros",
                "Livros que custam mais do que deviam",
                "liv.img",
                "joceline@email.com",
                "Leitura"
                ));
            await _repositoryP.NewPostAsync(
                new NewPostDTO(
                "Pergunta",
                "Quais as duas coisas que nunca se pode comer no café da manhã?",
                "imagemPensadora.img",
                "dalmante@email.com",
                "Charadas"
                ));

            //WHEN - Quando eu busco as postagens
            var postsTest1 = await _repositoryP.GetPostBySearchAsync("livros", null, null);
            var postsTest2 = await _repositoryP.GetPostBySearchAsync(null, null, "Joceline Gutierrez");
            var postsTest3 = await _repositoryP.GetPostBySearchAsync(null, "Leitura", null);

            //RHEN - Então eu tenho as postagens que correspondem aos criterios
            Assert.AreEqual(
                2, postsTest1.Count);
            Assert.AreEqual(
                2, postsTest2.Count);
            Assert.AreEqual(
                2, postsTest3.Count);

        }

    }
}
