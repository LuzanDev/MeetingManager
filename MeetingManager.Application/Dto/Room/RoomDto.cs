using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManager.Application.Dto.Room
{
    /// <summary>
    /// DTO комнаты
    /// </summary>
    public class RoomDto
    {
        /// <summary>
        /// Идентификатор комнаты
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Название комнаты
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Вместимость комнаты
        /// </summary>
        public int Capacity { get; set; }
    }
}
