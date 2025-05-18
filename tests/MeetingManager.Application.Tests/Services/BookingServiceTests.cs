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
        private readonly Mock<IBookingRepository> _bookingRepositoryMock;
        private readonly Mock<IRoomRepository> _roomRepositoryMock;
        private readonly BookingService _service;

        public BookingServiceTests()
        {
            _bookingRepositoryMock = new Mock<IBookingRepository>();
            _roomRepositoryMock = new Mock<IRoomRepository>();
            _service = new BookingService(_bookingRepositoryMock.Object, _roomRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnError_WhenRoomNotFound()
        {
            // Arrange
            var dto = new CreateBookingDto
            {
                RoomId = 1,
                StartTime = DateTime.UtcNow.AddHours(1),
                EndTime = DateTime.UtcNow.AddHours(2),
                BookedBy = "User"
            };

            _roomRepositoryMock
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Room, bool>>>()))
                .ReturnsAsync((Room)null); // Комната не найдена

            // Act
            var result = await _service.CreateAsync(dto);

            // Assert
            result.Data.Should().BeNull();
            result.ErrorMessage.Should().Be("Комната с таким идентификатором не существует.");
            Assert.Equal((int)ErrorCode.RoomNotFound, result.ErrorCode);
        }
    }
}
