using Microsoft.AspNetCore.Identity;

namespace BlogProject.Domain.Entities.Identity
{
    public class User : IdentityUser<Guid>
    {
        public string FullName { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
