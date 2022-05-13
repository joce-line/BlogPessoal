using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPessoal.src.repositories.implements
{
    public class UserRepository : IUser
    {
        #region Attributes

        private readonly PersonalBlogContext _context;

        #endregion Attributes

        #region Constructors

        public UserRepository(PersonalBlogContext context)
        {
            _context = context;
        }

        #endregion Constructors

        #region Methods

        public async Task AddUserAsync(AddUserDTO user)
        {
            await _context.Users.AddAsync(new UserModel
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Photo = user.Photo,
                Type = user.Type
            });
            await _context.SaveChangesAsync();

        }

        public async Task DeleteUserAsync(int id)
        {
            _context.Users.Remove(await GetUserByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        public async Task<UserModel> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<UserModel> GetUserByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<UserModel>> GetUsersByNameAsync(string name)
        {
            return await _context.Users
                .Where(u => u.Name.Contains(name))
                .ToListAsync();

        }

        public async Task UpdateUserAsync(UpdateUserDTO user)
        {
            var oldUser = await GetUserByIdAsync(user.Id);
            oldUser.Name = user.Name;
            oldUser.Password = user.Password;
            oldUser.Photo = user.Photo;
            _context.Users.Update(oldUser);
            await _context.SaveChangesAsync();

            #endregion Methods
        }
    }
}
