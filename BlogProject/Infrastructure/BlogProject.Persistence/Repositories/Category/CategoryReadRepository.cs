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
    public class CategoryReadRepository : ReadRepository<Category>, ICategoryReadRepository
    {
        public CategoryReadRepository(BlogProjectDbContext context) : base(context)
        {
        }
    }
}
