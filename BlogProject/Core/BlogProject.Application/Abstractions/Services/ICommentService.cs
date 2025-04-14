using BlogProject.Application.DTOs.Comment;

namespace BlogProject.Application.Abstractions.Services
{
    public interface ICommentService
    {
        Task CreateCommentAsync(CreateCommentDto dto);
        Task DeleteCommentAsync(Guid id);
    }
}
