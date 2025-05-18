using MeetingManager.Domain.Entity.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MeetingManager.Domain.Entity;
using MeetingManager.Application.Interfaces.Services;
using MeetingManager.Application.Dto.Room;
using Microsoft.AspNetCore.Authorization;

namespace MeetingManager.API.Controllers
{
    [ApiController]
    [Route("api/rooms")]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;
        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }
        /// <summary>
        /// Получение всех комнат
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     GET /api/rooms
        ///     
        /// </remarks>
        /// <returns>Список комнат</returns>
        /// <response code="200">Успешный ответ с телом</response>
        /// <response code="404">Ошибка на стороне клиента с телом</response>
        /// <response code="500">Внутренняя ошибка сервера</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRooms()
        {
            var response = await _roomService.GetAllAsync();
            if (!response.IsSuccess)
            {
                return NotFound( new 
                { errorCode = response.ErrorCode, 
                    errorMessage = response.ErrorMessage 
                });
            }
            return Ok(new { rooms = response.Data, count = response.Count });
        }
        /// <summary>
        /// Создание комнаты
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     POST /api/rooms
        ///     {
        ///         "name": "Meeting",
        ///         "capacity": 20
        ///     }
        /// </remarks>    
        /// <param name="dto">Информация о создаваемой комнате</param>
        /// <response code="201">Комната успешно создана</response>
        /// <response code="400">Ошибка на стороне клиента с телом</response>
        /// <returns>Объект RoomDto с деталями комнаты</returns>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<RoomDto>> CreateRoom([FromBody] CreateRoomDto dto)
        {
            var response = await _roomService.AddAsync(dto);
            if (!response.IsSuccess)
            {
                return BadRequest(new
                {
                    errorCode = response.ErrorCode,
                    errorMessage = response.ErrorMessage
                });
            }
            return CreatedAtAction(nameof(GetRooms), new
            {
                id = response.Data!.Id,
                name = response.Data.Name,
                capacity = response.Data.Capacity,
            }, response.Data);
        }
    }
}
