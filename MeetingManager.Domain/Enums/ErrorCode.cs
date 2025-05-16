using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManager.Domain.Enums
{
    public enum ErrorCode
    {
        // 100 - 120 -- Error codes for entity Room
        RoomNotFound = 100,
        RoomCollectionNotFound = 101,
        RoomAlreadyExists = 102,
    }
}
