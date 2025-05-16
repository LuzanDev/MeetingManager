using MeetingManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManager.Domain.Entity
{
    public class Room : IEntityId<long>
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public int Capacity { get; set; }
    }
}
