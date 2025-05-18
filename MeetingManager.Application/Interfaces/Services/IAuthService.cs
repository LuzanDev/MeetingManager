using MeetingManager.Application.Dto.AuthDto;
using MeetingManager.Domain.Entity.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManager.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<BaseResult> RegisterAsync(RegisterDto dto);
        Task<BaseResult<AuthResponse>> LoginAsync(string email, string password);
    }
}
