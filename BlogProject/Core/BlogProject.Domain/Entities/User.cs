using Microsoft.AspNetCore.Identity;

namespace BlogProject.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string FullName { get; set; }
        //public string? RefreshToken { get; set; }
        //public DateTime? RefreshTokenEndDate { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
