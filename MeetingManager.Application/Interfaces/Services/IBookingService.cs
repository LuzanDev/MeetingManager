using MeetingManager.Application.Dto.Bookings;
using MeetingManager.Application.Dto.Room;
using MeetingManager.Domain.Entity.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManager.Application.Interfaces.Services
{
    public interface IBookingService
    {
        Task<CollectionResult<BookingDto>> GetAllAsync();
        Task<BaseResult<BookingDto>> CreateAsync(CreateBookingDto dto);
        Task<BaseResult> DeleteAsync(long id);

    }
}
