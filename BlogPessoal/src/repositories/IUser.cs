using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogPessoal.src.repositories
{
    /// <summary>
    /// <para>Resume: Interface responsible for representing CRUD actions users</para>
    /// <para>Created by: Joceline Gutierrez</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 29/04/2022</para>
    /// </summary>
    public interface IUser
    {
        Task AddUserAsync(AddUserDTO user);
        Task UpdateUserAsync(UpdateUserDTO user);
        Task DeleteUserAsync(int id);
        Task<UserModel> GetUserByIdAsync(int id);
        Task<UserModel> GetUserByEmailAsync(string email);
        Task<List<UserModel>> GetUsersByNameAsync(string name);

    }
}
