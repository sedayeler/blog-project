using BlogProject.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        DbSet<T> Table { get; }
    }
}
