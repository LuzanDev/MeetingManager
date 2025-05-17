using MeetingManager.Domain.Entity;
using MeetingManager.Domain.Exceptions;
using MeetingManager.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManager.Infrastructure.Repositories
{
    public class BookingRepository : BaseRepository<Booking>, IBookingRepository
    {
        public BookingRepository(ApplicationDbContext context, ILogger<BaseRepository<Booking>> logger)
        : base(context, logger) { }
        public async Task<bool> ExistsAsync(Expression<Func<Booking, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public async Task<IEnumerable<Booking>> GetAllWithRoomsAsync()
        {
            return await _dbSet.Include(x => x.Room).AsNoTracking().ToListAsync();
        }
        
    }
}
