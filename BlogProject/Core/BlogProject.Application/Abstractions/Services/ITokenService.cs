using BlogProject.Application.DTOs;
using BlogProject.Domain.Entities;

namespace BlogProject.Application.Abstractions.Services
{
    public interface ITokenService
    {
        TokenDto CreateAccessToken(int minute, User user);
        string CreateRefreshToken();
    }
}
