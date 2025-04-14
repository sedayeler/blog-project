namespace BlogProject.WebUI.Models.Comment
{
    public class ListCommentViewModel
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
