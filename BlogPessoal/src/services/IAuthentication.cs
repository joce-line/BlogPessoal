using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using System.Threading.Tasks;

namespace BlogPessoal.src.services
{
    public interface IAuthentication
    {
        string EncodePassword(string password);
        Task CreateUserNotDuplicatedAsync(AddUserDTO dto);
        string GenerateToken(UserModel user);
        Task<AuthorizationDTO> GetAuthorizationAsync(AuthenticationDTO dto);
    }
}
