using MeetingManager.Application.Dto.Room;
using MeetingManager.Domain.Entity.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManager.Application.Interfaces.Services
{
    public interface IRoomService
    {
        Task<CollectionResult<RoomDto>> GetAllAsync();
        Task<BaseResult<RoomDto>> AddAsync(CreateRoomDto dto);
    }
}
