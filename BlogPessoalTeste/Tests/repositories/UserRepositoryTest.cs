using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.repositories;
using BlogPessoal.src.repositories.implements;
using BlogPessoal.src.utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPessoalTeste.Tests.repositories
{
    [TestClass]
    public class UserRepositoryTest
    {
        private PersonalBlogContext _context;
        private IUser _repository;


        [TestMethod]
        public async Task CreateFourUsersOnDatabaseReturnFour()
        {
            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal1")
                .Options;

            _context = new PersonalBlogContext(opt);
            _repository = new UserRepository(_context);

            //GIVEN - Dado que registro 4 usuarios no banco 
            await _repository.AddUserAsync(
                new AddUserDTO(
                    "Joceline Gutierrez",
                    "joceline@email.com",
                    "125234",
                    "img da hora",
                    UserType.NORMAL));

            await _repository.AddUserAsync(
                new AddUserDTO(
                    "Lulu Ponte",
                    "lulu@email.com",
                    "76543",
                    "img da ponte invertida",
                    UserType.NORMAL));

            await _repository.AddUserAsync(
                new AddUserDTO(
                    "Catarina Boaz",
                    "catarina@email.com",
                    "134652",
                    "URLFOTO",
                    UserType.NORMAL));

            await _repository.AddUserAsync(
                new AddUserDTO(
                    "Pericles da Silva",
                    "pericles@email.com",
                    "134652",
                    "URLFOTO",
                    UserType.NORMAL));

            //WHEN - Quando pesquiso lista total
            //THEN - Então recebo 4 usuarios

            Assert.AreEqual(4, _context.Users.Count());
        }

        [TestMethod]
        public async Task GetUserByEmailReturnNotNull()
        {
            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal2")
                .Options;

            _context = new PersonalBlogContext(opt);
            _repository = new UserRepository(_context);

            //GIVEN - Dado que registro um usuario no banco
            await _repository.AddUserAsync(
                new AddUserDTO(
                    "Jurandir das Neves",
                    "jurandir@email.com",
                    "87653",
                    "img.Jurandir",
                    UserType.NORMAL));

            //WHEN - Quando pesquiso pelo email deste usuario
            var user = await _repository.GetUserByEmailAsync("jurandir@email.com");

            //THEN - Então obtenho um usuario
            Assert.IsNotNull(user);
        }

        [TestMethod]
        public async Task GetUserByIdReturnNotNullAndUser()
        {
            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal3")
                .Options;

            _context = new PersonalBlogContext(opt);
            _repository = new UserRepository(_context);

            //GIVEN - Dado que registro um usuario no banco
            await _repository.AddUserAsync(
                new AddUserDTO(
                    "Gerivaldo Gomes",
                    "geri@email.com",
                    "76543",
                    "img da lua",
                    UserType.NORMAL));

            //WHEN - Quando pesquiso pelo id 6
            var user = await _repository.GetUserByIdAsync(1);

            //THEN - Então, deve me retornar um elemento não nulo
            Assert.IsNotNull(user);
            //THEN - Então, o elemento deve ser Gerivaldo Gomes
            Assert.AreEqual("Gerivaldo Gomes", user.Name);
        }

        [TestMethod]
        public async Task UpdateUserReturnUpdatedUser()
        {
            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal4")
                .Options;

            _context = new PersonalBlogContext(opt);
            _repository = new UserRepository(_context);

            //GIVEN - Dado que registro um usuario no banco
            await _repository.AddUserAsync(
            new AddUserDTO(
            "Estefânia Boaz",
            "estefania@email.com",
            "134652",
            "URLFOTO",
            UserType.NORMAL));

            //WHEN - Quando atualizamos o usuario
            var old = 
            _repository.GetUserByEmailAsync("estefania@email.com");
            await _repository.UpdateUserAsync(
            new UpdateUserDTO(
            1,
            "Estefânia Moura",
            "123456",
            "URLFOTONOVA"));

            //THEN - Então, quando validamos pesquisa deve retornar nome Estefânia Moura
            Assert.AreEqual(
            "Estefânia Moura",
            _context.Users.FirstOrDefault(u => u.Id == old.Id).Name);

            //THEN - Então, quando validamos pesquisa deve retornar senha 123456
            Assert.AreEqual("123456",
            _context.Users.FirstOrDefault(u => u.Id == old.Id).Password);
        }
    }
}
