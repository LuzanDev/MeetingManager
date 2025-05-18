using MeetingManager.Application.Dto.AuthDto;
using MeetingManager.Application.Interfaces.Services;
using MeetingManager.Application.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace MeetingManager.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Выполнение входа пользователя с получение JWT-токена
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     POST /api/auth/login
        ///     {
        ///         "email": "some@gmail.com",
        ///         "password": "Adsgf++434"
        ///     }
        /// </remarks> 
        /// <param name="dto">Информация о входящем пользователе</param>
        /// <returns>Login пользователя и JWT-токен</returns>
        /// <response code="200">Вход выполнен успешно</response>
        /// <response code="401">Ошибка авторизации с телом</response>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var response = await _authService.LoginAsync(dto.Email, dto.Password);

            if (!response.IsSuccess)
            {
                return Unauthorized(new
                {
                    errorCode = response.ErrorCode,
                    errorMessage = response.ErrorMessage
                });
            }
            return Ok(new { userName = response.Data.UserName, token = response.Data.Token });
        }

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     POST /api/auth/register
        ///     {
        ///         "email": "user@example.com",
        ///         "name": "Oleg",
        ///         "login": "some.oleg477",
        ///         "password": "Gdfg++543"
        ///     }
        /// </remarks> 
        /// <param name="dto">Информация о создаваемом пользователе</param>
        /// <returns>Ответ 200</returns>
        /// <response code="200">Пользователь успешно создан</response>
        /// <response code="400">Ошибка на стороне клиента с телом</response>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var response = await _authService.RegisterAsync(dto);
            if (!response.IsSuccess)
            {
                return BadRequest(new
                {
                    errorCode = response.ErrorCode,
                    errorMessage = response.ErrorMessage
                });
            }
            return Ok("Регистрация прошла успешно");
        }
    }
}
