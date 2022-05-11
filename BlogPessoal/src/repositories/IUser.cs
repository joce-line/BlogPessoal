using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using System.Collections.Generic;

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
        void AddUser(AddUserDTO user);
        void UpdateUser(UpdateUserDTO user);
        void DeleteUser(int id);
        UserModel GetUserById(int id);
        UserModel GetUserByEmail(string email);
        List<UserModel> GetUsersByName(string name);

    }
}
