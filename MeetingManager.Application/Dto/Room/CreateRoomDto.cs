using MeetingManager.Application.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManager.Application.Dto.Room
{
    public class CreateRoomDto
    {
        [Required(ErrorMessage = "Имя комнаты обязательно")]
        [NotEmptyOrWhitespace(ErrorMessage = "Имя обязательно и не может быть пробелами")]
        [StringLength(50, ErrorMessage = "Максимум 50 символов")]
        public string Name { get; set; }

        [Range(1, 200, ErrorMessage = "Вместимость должна быть от 1 до 200")]
        public int Capacity { get; set; }
    }

}
