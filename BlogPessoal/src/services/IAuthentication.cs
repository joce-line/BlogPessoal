using BlogPessoal.src.dtos;
using BlogPessoal.src.models;

namespace BlogPessoal.src.services
{
    public interface IAuthentication
    {
        string EncodePassword(string password);
        void CreateUserNotDuplicated(AddUserDTO dto);
        string GenerateToken(UserModel user);
        AuthorizationDTO GetAuthorization(AuthenticationDTO dto);
    }
}
