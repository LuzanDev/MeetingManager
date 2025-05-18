using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManager.Application.Dto.AuthDto
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public string UserName { get; set; }
    }
}
