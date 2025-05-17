using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManager.Application.Dto.Bookings
{
    public class BookingDto
    {
        public long Id { get; set; }
        public long RoomId { get; set; }
        public string RoomName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string BookedBy { get; set; }
    }

}
