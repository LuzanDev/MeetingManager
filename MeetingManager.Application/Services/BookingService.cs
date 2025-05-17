using MeetingManager.Application.Dto.Bookings;
using MeetingManager.Application.Dto.Room;
using MeetingManager.Application.Interfaces.Services;
using MeetingManager.Domain.Entity;
using MeetingManager.Domain.Entity.Result;
using MeetingManager.Domain.Enums;
using MeetingManager.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManager.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomRepository _roomRepository;

        public BookingService(IBookingRepository bookingRepository, IRoomRepository roomRepository)
        {
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
        }

        public async Task<BaseResult<BookingDto>> CreateAsync(CreateBookingDto dto)
        {
            var room = await _roomRepository.FirstOrDefaultAsync(r => r.Id == dto.RoomId);
            if (room == null)
            {
                return new BaseResult<BookingDto>
                {
                    ErrorMessage = "Комната с таким идентификатором не существует.",
                    ErrorCode = (int)ErrorCode.RoomNotFound
                };
            }
            
            if (await HasBookingConflictAsync(dto.RoomId, dto.StartTime, dto.EndTime))
            {
                return new BaseResult<BookingDto>
                {
                    ErrorMessage = $"Комната {room.Name} уже забронирована в это время",
                    ErrorCode = (int)ErrorCode.RoomAlreadyBooked
                };
            }
            var booking = new Booking
            {
                RoomId = dto.RoomId,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                BookedBy = dto.BookedBy,
            };

            booking = await _bookingRepository.AddAsync(booking);
            
            return new BaseResult<BookingDto>()
            {
                Data = new BookingDto 
                {
                    Id = booking.Id,
                    StartTime = booking.StartTime,
                    EndTime = booking.EndTime,
                    BookedBy = booking.BookedBy,
                    RoomId = booking.RoomId,
                    RoomName = room.Name
                }
            };
        }

        public async Task<BaseResult> DeleteAsync(long id)
        {
            var booking = await _bookingRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (booking == null)
            {
                return new BaseResult
                {
                    ErrorMessage = "Бронирование не найдено",
                    ErrorCode = (int)ErrorCode.BookingNotFound
                };
            }

            await _bookingRepository.DeleteAsync(booking);
            return new BaseResult { };
        }


        public async Task<CollectionResult<BookingDto>> GetAllAsync()
        {
            var bookings =  await _bookingRepository.GetAllWithRoomsAsync();
            if (bookings == null || !bookings.Any())
            {
                return new CollectionResult<BookingDto>()
                {
                    ErrorMessage = "Бронирование не существует или еще не были созданы",
                    ErrorCode = (int)ErrorCode.BookingCollectionNotFound
                };
            }
            return new CollectionResult<BookingDto>()
            {
                Count = bookings.Count(),
                Data = bookings.Select(x => new BookingDto 
                { 
                    Id = x.Id,
                    BookedBy = x.BookedBy,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    RoomId = x.RoomId,
                    RoomName = x.Room.Name
                })
            };
        }
        private async Task<bool> HasBookingConflictAsync(long roomId, DateTime startTime, DateTime endTime)
        {
            return await _bookingRepository.ExistsAsync(x =>
                x.RoomId == roomId &&
                x.StartTime < endTime &&
                x.EndTime > startTime);
        }

    }
}
