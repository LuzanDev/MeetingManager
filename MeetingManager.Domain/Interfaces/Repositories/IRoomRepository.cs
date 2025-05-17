using MeetingManager.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManager.Domain.Interfaces.Repositories
{
    public interface IRoomRepository : IBaseRepository<Room>
    {
        Task<bool> ExistsByNameAsync(string name);
    }
}
