using BlogProject.Domain.Entities;

namespace BlogProject.Application.DTOs
{
    public class ListPostDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string? ImagePath { get; set; }
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
        //public Guid CategoryId { get; set; }
        //public string CategoryName { get; set; }
        public Category Category { get; set; }
    }
}
