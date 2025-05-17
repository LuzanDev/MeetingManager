using MeetingManager.Application.Dto.Bookings;
using MeetingManager.Application.Dto.Room;
using MeetingManager.Application.Interfaces.Services;
using MeetingManager.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace MeetingManager.API.Controllers
{
    [ApiController]
    [Route("api/bookings")]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBookings()
        {
            var response = await _bookingService.GetAllAsync();
            if (!response.IsSuccess)
            {
                return NotFound(new
                {
                    errorCode = response.ErrorCode,
                    errorMessage = response.ErrorMessage
                });
            }
            return Ok(new { bookings = response.Data, count = response.Count });
        }

        /// <summary>Создает новое бронирование комнаты</summary>
        /// <param name="dto">Данные для создания брони</param>
        /// <returns>Созданная бронь</returns>
        [HttpPost]
        [ProducesResponseType(typeof(BookingDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BookingDto>> CreateBooking([FromBody] CreateBookingDto dto)
        {

            var response = await _bookingService.CreateAsync(dto);
            if (!response.IsSuccess)
            {
                return BadRequest(new
                {
                    errorCode = response.ErrorCode,
                    errorMessage = response.ErrorMessage
                });
            }
            return CreatedAtAction(nameof(GetBookings), new
            {
                id = response.Data!.Id,
                roomId = response.Data.RoomId,
                roomName = response.Data.RoomName,
                startTime = response.Data.StartTime,
                endTime = response.Data.EndTime,
                bookedBy = response.Data.BookedBy,
            }, response.Data);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            var response = await _bookingService.DeleteAsync(id);

            if (!response.IsSuccess)
            {
                return NotFound(new
                {
                    errorCode = response.ErrorCode,
                    errorMessage = response.ErrorMessage
                });
            }
            return NoContent();
        }
    }
}
