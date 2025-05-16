using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManager.Application.Dto
{
    public class RoomDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
    }
}
