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
    /// <para>Resume: Class responsible for implement IUser.</para>
    /// <para>Created by: Joceline Gutierrez</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 12/05/2022</para>
    /// </summary>

    public class UserRepository : IUser
    {
        #region Attributes

        private readonly PersonalBlogContext _context;

        #endregion Attributes

        #region Constructors

        /// <summary>
        /// <para>Resume: Constructor of class.</para>
        /// </summary>
        /// <param name="context">PersonalBlogContext</param>
        public UserRepository(PersonalBlogContext context)
        {
            _context = context;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// <para>Resume: Asynchronous method for add a new user</para>
        /// </summary>
        /// <param name="user">AddUserDTO</param>

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

        /// <summary>
        /// <para>Resume: Asynchronous method for delete an existent user</para>
        /// </summary>
        /// <param name="id">Id of user</param>
        public async Task DeleteUserAsync(int id)
        {
            _context.Users.Remove(await GetUserByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resume: Asynchronous method for get a user by email</para>
        /// </summary>
        /// <param name="email">Email of user</param>
        /// <returns>UserModel</returns>
        public async Task<UserModel> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        /// <summary>
        /// <para>Resume: Asynchronous method for get a user by id</para>
        /// </summary>
        /// <param name="id">Id of user</param>
        /// <returns>UserModel</returns>
        public async Task<UserModel> GetUserByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        /// <summary>
        /// <para>Resume: Asynchronous method for get a user by name</para>
        /// </summary>
        /// <param name="name">Name of user</param>
        /// <returns>List UserModel</returns>
        public async Task<List<UserModel>> GetUsersByNameAsync(string name)
        {
            return await _context.Users
                .Where(u => u.Name.Contains(name))
                .ToListAsync();

        }

        /// <summary>
        /// <para>Resume: Asynchronous method for update an existent user</para>
        /// </summary>
        /// <param name="user">UpdateUserDTO</param>
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
