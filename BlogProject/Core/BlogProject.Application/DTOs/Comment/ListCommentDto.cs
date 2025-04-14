namespace BlogProject.Application.DTOs.Comment
{
    public class ListCommentDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Content { get; set; }
        public Guid UserId { get; set; }
        public string Username { get; set; }
    }
}
