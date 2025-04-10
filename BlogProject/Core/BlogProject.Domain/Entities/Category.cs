using BlogProject.Domain.Entities.Common;

namespace BlogProject.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
