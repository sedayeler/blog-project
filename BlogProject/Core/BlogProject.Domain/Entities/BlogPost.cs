using BlogProject.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Domain.Entities
{
    public class BlogPost : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishedAt { get; set; }
        public string? ImagePath { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
