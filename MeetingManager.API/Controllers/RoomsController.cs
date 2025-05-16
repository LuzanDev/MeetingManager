using MeetingManager.Domain.Entity.Result;
using MeetingManager.Application.Dto ;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MeetingManager.Domain.Entity;
using MeetingManager.Domain.Repositories;
using MeetingManager.Application.Interfaces.Services;

namespace MeetingManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;
        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
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
