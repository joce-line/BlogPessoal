using Microsoft.EntityFrameworkCore;
using BlogPessoal.src.data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using BlogPessoal.src.models;

namespace BlogPessoalTeste.Tests.data
{
    [TestClass]
    public class PersonalBlogContextTest
    {
        private PersonalBlogContext _context;

        [TestInitialize]
        public void setup()
        {
            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal")
                .Options;

            _context = new PersonalBlogContext(opt);
        }


        [TestMethod]
        public void InsertNewUserOnBaseReturnUser()
        {
            UserModel user = new UserModel();

            user.Name = "Nomezinho";
            user.Email = "emaillegal@email.com";
            user.Password = "art64r5t";
            user.Photo = "fotinho da hora";

            _context.Users.Add(user);

            _context.SaveChanges();


            Assert.IsNotNull(_context.Users.FirstOrDefault(u => u.Email == "emaillegal@email.com"));
        }
    }
}
