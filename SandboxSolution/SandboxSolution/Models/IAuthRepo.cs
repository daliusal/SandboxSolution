using SandboxSolution.Dtos;

namespace SandboxSolution.Models
{
    public interface IAuthRepo
    {
        Task Register(UserRegistrationDto user);
        Task<string> Login(UserDto user, string token);
        void CreatePasswordHash(string password, out byte[] hash, out byte[] salt);
        bool VerifyPasswordHash(User user, UserDto userInfo);
        string CreateToken(User user, string token);
        Task<bool> SaveChanges();
    }
}
