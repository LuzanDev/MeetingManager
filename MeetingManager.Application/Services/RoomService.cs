using MeetingManager.Application.Dto;
using MeetingManager.Domain.Entity;
using MeetingManager.Domain.Entity.Result;
using MeetingManager.Domain.Enums;
using MeetingManager.Domain.Interfaces.Repositories;
using MeetingManager.Application.Interfaces.Services;
using MeetingManager.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManager.Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<BaseResult<RoomDto>> AddAsync(CreateRoomDto dto)
        {
            if (await _roomRepository.ExistsByNameAsync(dto.Name))
            {
                return new BaseResult<RoomDto>()
                {
                    ErrorMessage = $"Комната \"{dto.Name}\" уже существует!",
                    ErrorCode = (int)ErrorCode.RoomAlreadyExists
                };
            }
            var room = new Room()
            {
                Name = dto.Name,
                Capacity = dto.Capacity,
            };
            room = await _roomRepository.AddAsync(room);
            return new BaseResult<RoomDto>
            {
                Data = new RoomDto { Id = room.Id, Name = room.Name, Capacity = room.Capacity }
            };
        }
        public async Task<CollectionResult<RoomDto>> GetAllAsync()
        {
            var rooms = await _roomRepository.GetAllAsync();
            if (rooms == null || !rooms.Any())
            {
                return new CollectionResult<RoomDto>()
                {
                    ErrorMessage = "Комнат не существует или еще не были созданы",
                    ErrorCode  = (int)ErrorCode.RoomCollectionNotFound
                };
            }
            return new CollectionResult<RoomDto>()
            {
                Count = rooms.Count,
                Data = rooms.Select(x => new RoomDto {Id = x.Id, Name = x.Name, Capacity = x.Capacity })
            };
        }
    }
}
