using MeetingManager.Application.Dto.Bookings;
using MeetingManager.Application.Dto.Room;
using MeetingManager.Application.Interfaces.Services;
using MeetingManager.Application.Services;
using Microsoft.AspNetCore.Authorization;
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
        /// <summary>
        /// Получение списка всех бронирований
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     GET /api/bookings
        ///     
        /// </remarks>
        /// <returns>Список бронирований</returns>
        /// <response code="200">Успешный ответ с телом</response>
        /// <response code="404">Ошибка на стороне клиента с телом</response>
        /// <response code="500">Внутренняя ошибка сервера</response>
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

        /// <summary>
        /// Создание бронирования комнаты
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     POST /api/bookings
        ///     {
        ///         "roomId": 3,
        ///         "startTime": "2025-05-22T10:00",
        ///         "endTime": "2025-05-22T11:30",
        ///         "bookedBy": "Sokolenko Roman"
        ///     }
        /// </remarks>    
        /// <param name="dto">Информация о создаваемом бронировании</param>
        /// <response code="201">Бронирование успешно создано</response>
        /// <response code="400">Ошибка на стороне клиента с телом</response>
        /// <response code="401">Ошибка аутентификации</response>
        /// <returns>Объект BookingDto с деталями бронирования</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(BookingDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

        /// <summary>
        /// Удаление бронирования
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     DELETE /api/bookings/{id}
        ///     
        /// </remarks> 
        /// <param name="id">Идентификатор бронирования</param>
        /// <returns>Нечего не возвращаетсяв случае успеха</returns>
        /// <response code="204">Бронирование успешно удалено</response>
        /// <response code="404">Ошибка на стороне клиента</response>
        /// <response code="401">Ошибка аутентификации</response>
        [Authorize]
        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
