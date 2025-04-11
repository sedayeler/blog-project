using BlogProject.Application.DTOs.User;

namespace BlogProject.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<RegisterResponse> RegisterAsync(RegisterRequest request);
        Task<bool> LoginAsync(string email, string password);
        Task LogoutAsync();
    }
}
