using BlogProject.Application.DTOs;

namespace BlogProject.Application.Abstractions.Services
{
    public interface IPostService
    {
        Task CreatePostAsync(CreatePostDto dto);
        Task UpdatePostAsync(UpdatePostDto dto);
        Task DeletePostAsync(Guid id);
        Task<List<ListPostDto>> GetAllPostsAsync();
        Task<ListPostDto> GetByIdPostAsync(Guid id);
    }
}
