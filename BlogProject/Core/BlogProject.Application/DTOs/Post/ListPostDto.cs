namespace BlogProject.Application.DTOs
{
    public class ListPostDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string? ImagePath { get; set; }
        public string AuthorName { get; set; }
        public string CategoryName { get; set; }
    }
}
