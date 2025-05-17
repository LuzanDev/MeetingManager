using MeetingManager.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManager.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T>
    {
        IQueryable<T> GetDbSet();
        Task<List<T>> GetAllAsNoTrackingToListAsync();
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
