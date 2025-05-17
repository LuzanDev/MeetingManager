using MeetingManager.Application.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManager.Application.Dto.Bookings
{
    [ValidBookingTime(ErrorMessage = "Неверный интервал бронирования.")]
    public class CreateBookingDto
    {
        [Required]
        public long RoomId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        [StringLength(100)]
        public string BookedBy { get; set; }
    }

}
