using BlogProject.Application.DTOs.Comment;

namespace BlogProject.Application.Features.Queries.Post.GetByIdPost
{
    public class GetByIdPostResponse
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string? ImagePath { get; set; }
        public Guid UserId { get; set; }
        public string UserFullName { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<ListCommentDto> Comments { get; set; }
    }
}
