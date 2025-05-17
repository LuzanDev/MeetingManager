using MeetingManager.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManager.Domain.Interfaces.Repositories
{
    public interface IBookingRepository : IBaseRepository<Booking>
    {
        Task<bool> ExistsAsync(Expression<Func<Booking, bool>> predicate);
        Task<IEnumerable<Booking>> GetAllWithRoomsAsync();
    }
}
