using MeetingManager.Domain.Entity;
using MeetingManager.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManager.Infrastructure.Repositories
{
    public class RoomRepository : BaseRepository<Room>, IRoomRepository
    {
        public RoomRepository(ApplicationDbContext context, ILogger<BaseRepository<Room>> logger)
        : base(context, logger) { }
        
        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _dbSet.AnyAsync(room => room.Name == name);
        }
    }

}
