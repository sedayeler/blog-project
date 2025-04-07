using BlogProject.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }

        public Guid BlogPostId { get; set; }
        public BlogPost BlogPost { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
