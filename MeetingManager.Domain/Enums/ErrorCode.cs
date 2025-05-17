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

        // 140 - 160 -- Error codes for entity Booking
        BookingNotFound = 140,
        BookingCollectionNotFound = 141,
        BookingAlreadyExists = 142,
        RoomAlreadyBooked = 143
    }
}
