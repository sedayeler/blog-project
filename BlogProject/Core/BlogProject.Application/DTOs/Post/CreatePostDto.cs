namespace BlogProject.Application.DTOs
{
    public class CreatePostDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string? ImagePath { get; set; }
        public Guid CategoryId { get; set; }
    }
}
