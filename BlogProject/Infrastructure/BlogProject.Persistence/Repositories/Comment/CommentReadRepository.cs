using BlogProject.Application.Repositories;
using BlogProject.Domain.Entities;
using BlogProject.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Persistence.Repositories
{
    public class CommentReadRepository : ReadRepository<Comment>, ICommentReadRepository
    {
        public CommentReadRepository(BlogProjectDbContext context) : base(context)
        {
        }
    }
}
