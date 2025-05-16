using MeetingManager.Domain.Entity;
using MeetingManager.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManager.Domain.Repositories
{
    public interface IRoomRepository : IBaseRepository<Room>
    {
        Task<bool> ExistsByNameAsync(string name);
        Task<List<Room>> GetAllAsync();
    }
}
