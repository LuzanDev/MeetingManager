using MeetingManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManager.Domain.Entity
{
    public class Booking : IEntityId<long>
    {
        public long Id { get; set; }

        public long RoomId { get; set; }
        public Room Room { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public required string BookedBy { get; set; }
    }
}
