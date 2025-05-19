using FluentAssertions;
using MeetingManager.Application.Dto.Bookings;
using MeetingManager.Application.Services;
using MeetingManager.Domain.Entity;
using MeetingManager.Domain.Enums;
using MeetingManager.Domain.Interfaces.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace MeetingManager.Application.Tests.Services
{
    public class BookingServiceTests
    {
        private readonly Mock<IRoomRepository> _roomRepoMock = new();
        private readonly Mock<IBookingRepository> _bookingRepoMock = new();
        private readonly BookingService _service;

        public BookingServiceTests()
        {
            _service = new BookingService(_bookingRepoMock.Object, _roomRepoMock.Object);
        }

        [Fact]
        public async Task CreateAsync_WhenRoomNotFound_ReturnsRoomNotFoundError()
        {
            // Arrange
            var dto = new CreateBookingDto
            {
                RoomId = 1,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1),
                BookedBy = "Test"
            };

            _roomRepoMock
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Room, bool>>>()))
                .ReturnsAsync((Room)null!);

            // Act
            var result = await _service.CreateAsync(dto);

            // Assert
            Assert.Null(result.Data);
            Assert.Equal((int)ErrorCode.RoomNotFound, result.ErrorCode);
            Assert.Equal("Комната с таким идентификатором не существует.", result.ErrorMessage);
        }

        [Fact]
        public async Task CreateAsync_WhenBookingConflictExists_ReturnsConflictError()
        {
            // Arrange
            var dto = new CreateBookingDto
            {
                RoomId = 1,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1),
                BookedBy = "Test"
            };

            var room = new Room { Id = 1, Name = "Room A" };

            _roomRepoMock
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Room, bool>>>()))
                .ReturnsAsync(room);

            _bookingRepoMock
                .Setup(b => b.ExistsAsync(It.IsAny<Expression<Func<Booking, bool>>>()))
                .ReturnsAsync(true); 

            // Act
            var result = await _service.CreateAsync(dto);

            // Assert
            Assert.Null(result.Data);
            Assert.Equal((int)ErrorCode.RoomAlreadyBooked, result.ErrorCode);
            Assert.Equal($"Комната {room.Name} уже забронирована в это время", result.ErrorMessage);
        }

        [Fact]
        public async Task CreateAsync_WhenNoConflict_CreatesBookingSuccessfully()
        {
            // Arrange
            var dto = new CreateBookingDto
            {
                RoomId = 1,
                StartTime = DateTime.Now.AddHours(1),
                EndTime = DateTime.Now.AddHours(3),
                BookedBy = "Oleg"
            };

            var room = new Room { Id = 1, Name = "Room A" };
            var booking = new Booking
            {
                Id = 42,
                RoomId = 1,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                BookedBy = dto.BookedBy
            };

            _roomRepoMock
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Room, bool>>>()))
                .ReturnsAsync(room);

            _bookingRepoMock
                .Setup(b => b.ExistsAsync(It.IsAny<Expression<Func<Booking, bool>>>()))
                .ReturnsAsync(false); 

            _bookingRepoMock
                .Setup(b => b.AddAsync(It.IsAny<Booking>()))
                .ReturnsAsync(booking);

            // Act
            var result = await _service.CreateAsync(dto);

            // Assert
            Assert.NotNull(result.Data);
            Assert.Equal(booking.Id, result.Data.Id);
            Assert.Equal(booking.BookedBy, result.Data.BookedBy);
            Assert.Equal(booking.RoomId, result.Data.RoomId);
            Assert.Equal(booking.StartTime, result.Data.StartTime);
            Assert.Equal(booking.EndTime, result.Data.EndTime);
            Assert.Equal(room.Name, result.Data.RoomName);
            Assert.Null(result.ErrorCode);
            Assert.Null(result.ErrorMessage);
        }
    }
}
