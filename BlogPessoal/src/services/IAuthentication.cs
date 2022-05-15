using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using System.Threading.Tasks;

namespace BlogPessoal.src.services
{
    /// <summary>
    /// <para>Resume: Interface responsible for representing authentication actions</para>
    /// <para>Created by: Joceline Gutierrez</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 12/05/2022</para>
    /// </summary>
    public interface IAuthentication
    {
        string EncodePassword(string password);
        Task CreateUserNotDuplicatedAsync(AddUserDTO dto);
        string GenerateToken(UserModel user);
        Task<AuthorizationDTO> GetAuthorizationAsync(AuthenticationDTO dto);
    }
}
