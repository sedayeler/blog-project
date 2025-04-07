using BlogProject.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Application.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
    {
        Task<bool> AddAsync(T entity);
        Task<bool> AddRangeAsync(List<T> entities);
        bool Update(T entity);
        bool Remove(T entity);
        Task<bool> RemoveAsync(Guid id);
        bool RemoveRange(List<T> entities);
        Task<int> SaveAsync();
    }
}
