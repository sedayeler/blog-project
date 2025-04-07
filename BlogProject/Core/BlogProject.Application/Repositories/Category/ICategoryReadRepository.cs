using BlogProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Application.Repositories
{
    public interface ICategoryReadRepository : IReadRepository<Category>
    {
    }
}
